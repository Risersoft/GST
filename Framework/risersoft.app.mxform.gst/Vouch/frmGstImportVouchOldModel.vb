Imports System.IO
Imports System.Runtime.Serialization
Imports risersoft.API.GSTN
Imports risersoft.app.mxent
Imports risersoft.shared
Imports risersoft.shared.dal

<DataContract>
Public Class frmGstImportVouchOldModel
    Inherits clsFormDataModel
    Dim dicSQL As New clsCollecString(Of String), dsDB As DataSet
    Dim oMaster As clsMasterDataFICO

    Protected Overrides Sub InitViews()
        myView = Me.GetViewModel("Cust")
    End Sub

    Public Sub New(context As IProviderContext)
        MyBase.New(context)
        Me.InitViews()
        Me.InitForm()
        oMaster = New clsMasterDataFICO(context)
    End Sub

    Private Sub InitForm()
    End Sub

    Public Overrides Function PrepForm(oView As clsViewModel, ByVal prepMode As EnumfrmMode, ByVal prepIDX As String, Optional ByVal strXML As String = "") As Boolean
        Dim sql As String
        Me.FormPrepared = False
        'If prepMode = EnumfrmMode.acAddM Then prepIDX = Guid.Empty.ToString
        'sql = "Select * from GSTNTransaction Where GSTNTransactionID = '" & prepIDX & "'"
        'Me.InitData(sql, "", oView, prepMode, prepIDX, strXML)


        Me.FormPrepared = True
        Return Me.FormPrepared
    End Function

    Public Overrides Function Validate() As Boolean
        Me.InitError()

        Dim dsImport As DataSet = Me.DatasetCollection("import")
        Dim oRet = Me.ImportData(dsImport)
        If Not oRet.Success Then Me.AddError("", oRet.Message)


        Return Me.CanSave
    End Function

    Public Overrides Function VSave() As Boolean
        VSave = False
        If Me.Validate Then
            Dim ImportVouchDescrip As String = "Import job"

            Try
                Dim InvoiceIDList As New List(Of Integer), PaymentIDList As New List(Of Integer)
                Dim InvoiceAction = Sub(s, e)
                                        If Not InvoiceIDList.Contains(e.Row("invoiceid")) Then InvoiceIDList.Add(e.Row("invoiceid"))
                                    End Sub
                Dim PaymentAction = Sub(s, e)
                                        If Not PaymentIDList.Contains(e.Row("paymentid")) Then PaymentIDList.Add(e.Row("paymentid"))
                                    End Sub
                myContext.Provider.dbConn.BeginTransaction(myContext, Me.Name, "Import")

                'save dsdb
                For Each str1 As String In New String() {"partymain", "party", "customer", "vendor", "invoice", "InvoiceItemGst", "TXPD", "payment", "AT"}
                    If myUtils.IsInList(str1, "invoice") Then
                        AddHandler dsDB.Tables("invoice").RowChanged, InvoiceAction
                    ElseIf myUtils.IsInList(str1, "payment") Then
                        AddHandler dsDB.Tables("payment").RowChanged, PaymentAction

                    End If
                    If dsDB.Tables.Contains(str1) Then myContext.Provider.objSQLHelper.SaveResults(dsDB.Tables(str1), dicSQL(str1))
                Next
                RemoveHandler dsDB.Tables("invoice").RowChanged, InvoiceAction
                RemoveHandler dsDB.Tables("payment").RowChanged, PaymentAction

                Dim InvoiceIDCSV As String = myUtils.MakeCSV(InvoiceIDList, ",")
                For Each r1 As DataRow In dsDB.Tables("invoice").Select("invoiceid in (" & InvoiceIDCSV & ")")
                    If dsDB.Tables.Contains("customer") Then
                        Dim oProc As New clsGSTInvoiceSale(myContext)
                        'oProc.PopulateCalc(r1("invoiceid"), 0, dsDB.Tables("invoicegstcalc"))
                    Else
                        Dim oProc As New clsGSTInvoicePurch(myContext)
                        'purchase
                        'oProc.PopulateCalc(r1("invoiceid"), 0, dsDB.Tables("invoicegstcalc"))
                    End If

                Next
                Dim PaymentIDCSV As String = myUtils.MakeCSV(PaymentIDList, ",")
                For Each r1 As DataRow In dsDB.Tables("payment").Select("paymentid in (" & PaymentIDCSV & ")")
                    If dsDB.Tables.Contains("customer") Then
                        Dim oProc As New clsGSTAdvanceBase(myContext, "PC", "GSTR1")
                        'oProc.PopulateCalc(r1("paymentid"), 0, dsDB.Tables("invoicegstcalc"))
                    Else
                        Dim oProc As New clsGSTAdvanceBase(myContext, "PV", "GSTR2")
                        'purchase
                        'oProc.PopulateCalc(r1("paymentid"), 0, dsDB.Tables("invoicegstcalc"))
                    End If
                Next
                myContext.Provider.objSQLHelper.SaveResults(dsDB.Tables("invoicegstcalc"), dicSQL("invoicegstcalc"))


                frmMode = EnumfrmMode.acEditM
                myContext.Provider.dbConn.CommitTransaction(ImportVouchDescrip, frmIDX)
                VSave = True
            Catch e As Exception
                myContext.Provider.dbConn.RollBackTransaction(ImportVouchDescrip, e.Message)
                Me.AddException("", e)
            End Try
        End If
    End Function
    Public Function DateFromString(str1 As String) As DateTime
        Dim dated = DateTime.ParseExact(str1, New String() {"d-M-yyyy", "dd-MM-yyyy"}, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None)
        Return dated
        'http://www.basicdatepicker.com/samples/cultureinfo.aspx
        'https://stackoverflow.com/questions/47052779/parse-date-string-with-single-digit-day-e-g-1-11-2017-as-well-as-12-11-2017
        'https://docs.microsoft.com/en-us/dotnet/standard/base-types/standard-date-and-time-format-strings
    End Function
    Public Function ImportData(dsImport As DataSet) As clsProcOutput
        Dim oRet As New clsProcOutput, PartySubTypeTable As String = "", IVDocType As String = "", PYDocType As String = ""
        'Validate data and add error


        Trace.WriteLine("Begin Data Ingestion")


        For Each tbl As DataTable In dsImport.Tables
            If Not tbl.Columns.Contains("Validation") Then tbl.Columns.Add("Validation", GetType(String))
            If Not tbl.Columns.Contains("Action") Then tbl.Columns.Add("Action", GetType(String))

        Next

        Dim lst2 As New List(Of Integer)
        For Each tbl As DataTable In dsImport.Tables
            If tbl.Columns.Contains("postingdate") Then
                For Each r1 As DataRow In tbl.Select
                    Try
                        Dim Dated = Me.DateFromString(r1("postingdate"))
                        Dim PPID = oMaster.GetPostPeriodID(Dated)
                        If Not lst2.Contains(PPID) Then lst2.Add(PPID)
                    Catch ex As Exception
                        r1("validation") = "Invalid posting Date: " & ex.Message
                        oRet.AddError(ex.Message)
                    End Try
                Next
            End If
        Next
        Dim PostPeriodIDCSV As String = myUtils.MakeCSV(lst2, ",")

        'Get Data from DB
        dicSQL.Clear()

        Dim str1 As String = "select {0} from party where mainpartyid in (select mainpartyid from partymain where partytype = '{1}')"
        If dsImport.Tables.Contains("customer") Then
            'Sale Import
            PartySubTypeTable = "customer"
            IVDocType = "Is"
            PYDocType = "PC"
            'dicSQL.Add("partymain", "select * from partymain where partytype = 'C'")
            'dicSQL.Add("party", String.Format(str1, "*", "C"))
            'dicSQL.Add("customer", String.Format("select * from customer where partyid in (" & str1 & ")", "partyid", "C"))
            dicSQL.Add("partymain", "select * from partymain")
            dicSQL.Add("party", "select * from party")
            dicSQL.Add("customer", "select * from customer")

        Else
            'Purchase import
            PartySubTypeTable = "vendor"
            IVDocType = "IP"
            PYDocType = "PV"
            'dicSQL.Add("partymain", "select * from partymain where partytype = 'V'")
            'dicSQL.Add("party", String.Format(str1, "*", "V"))
            'dicSQL.Add("vendor", String.Format("select * from vendor where partyid in (" & str1 & ")", "partyid", "V"))
            dicSQL.Add("partymain", "select * from partymain")
            dicSQL.Add("party", "select * from party")
            dicSQL.Add("vendor", "select * from vendor")
        End If

        dicSQL.Add("campus", "select * from Campus")
        dicSQL.Add("division", "select * from division")
        dicSQL.Add("gstreg", "select * from gstreg")
        dicSQL.Add("taxarea", "select * from taxarea")
        dicSQL.Add("co", "Select distinct countryname As country, countrycode + ' - ' + countryname, countrycode, countryname from unlocodesub order by countryname")
        dicSQL.Add("su", "select distinct subdivisionname as subdivision, subdivisioncode + ' - ' + subdivisionname, countrycode, SubDivisionCode, SubDivisionName from unlocodesub order by countrycode, subdivisionname")
        dicSQL.Add("invoice", String.Format("select * from invoice where postperiodid in (" & PostPeriodIDCSV & ") And DOCTYPE='{0}'", IVDocType))
        dicSQL.Add("InvoiceItemGst", String.Format("select * from InvoiceItemGst where invoiceid in (select invoiceid from invoice where postperiodid in (" & PostPeriodIDCSV & ") AND DOCTYPE='{0}')", IVDocType))
        dicSQL.Add("TXPD", String.Format("select * from AdvanceTaxGst where invoiceid in (select invoiceid from invoice where postperiodid in (" & PostPeriodIDCSV & ") AND DOCTYPE='{0}')", IVDocType))
        dicSQL.Add("payment", String.Format("select * from payment where postperiodid in (" & PostPeriodIDCSV & ") AND DOCTYPE='{0}'", PYDocType))
        dicSQL.Add("AT", String.Format("select * from AdvanceTaxGst where paymentid in (select paymentid from payment where postperiodid in (" & PostPeriodIDCSV & ") AND DOCTYPE='{0}')", PYDocType))
        dicSQL.Add("invoicegstcalc", String.Format("select * from invoicegstcalc where invoiceid in (select invoiceid from invoice where postperiodid in ({0}) And DOCTYPE='{1}') or paymentid in (select paymentid from payment where postperiodid in ({0}) And DOCTYPE='{2}')", PostPeriodIDCSV, IVDocType, PYDocType))

        dsDB = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, dicSQL)
        myContext.Tables.SetAuto(dsDB.Tables("partymain"), dsDB.Tables("party"), "mainpartyid")
        myContext.Tables.SetAuto(dsDB.Tables("invoice"), dsDB.Tables("InvoiceItemGst"), "invoiceid", "_item")
        myContext.Tables.SetAuto(dsDB.Tables("invoice"), dsDB.Tables("TXPD"), "invoiceid", "_txpd")
        myContext.Tables.SetAuto(dsDB.Tables("payment"), dsDB.Tables("AT"), "paymentid")

        If dsImport.Tables.Contains("customer") Then
            'Sale Import
            myContext.Tables.SetAuto(dsDB.Tables("party"), dsDB.Tables("customer"), "partyid")
            myContext.Tables.SetAuto(dsDB.Tables("customer"), dsDB.Tables("invoice"), "customerid")
            myContext.Tables.SetAuto(dsDB.Tables("customer"), dsDB.Tables("payment"), "customerid")


        Else
            'Purchase import
            myContext.Tables.SetAuto(dsDB.Tables("party"), dsDB.Tables("vendor"), "partyid")
            myContext.Tables.SetAuto(dsDB.Tables("vendor"), dsDB.Tables("invoice"), "vendorid")
            myContext.Tables.SetAuto(dsDB.Tables("vendor"), dsDB.Tables("payment"), "vendorid")

        End If

        Dim cnt As Integer = 0
        For Each r1 As DataRow In dsImport.Tables(PartySubTypeTable).Select()
            cnt = cnt + 1
            Trace.WriteLine("Begin Party " & cnt)
            Dim rPartySub As DataRow
            oRet = oRet + Me.TryImportPartySubType(r1, dsDB, PartySubTypeTable, rPartySub)
        Next

        oRet = oRet + Me.TryImportVoucherList(IVDocType, "INV", dsImport)
        oRet = oRet + Me.TryImportVoucherList(PYDocType, "AT", dsImport)

        Trace.WriteLine("End Data Ingestion")


        Return oRet

    End Function
    Public Function TryImportVoucherList(DocType As String, TableName As String, dsImport As DataSet) As clsProcOutput
        Dim oRet As New clsProcOutput
        Dim lst1 = Me.FieldLists(DocType, True)
        Dim dt1 As DataTable = myContext.Data.SelectDistinct(dsImport.Tables(TableName), myUtils.MakeCSV(lst1, ","))

        Dim lst2 = Me.FieldLists(DocType, False)
        Dim dt2 As DataTable = myContext.Data.SelectDistinct(dsImport.Tables(TableName), myUtils.MakeCSV(lst2, ","))

        Dim cnt As Integer = 0, totalcnt As Integer = dt1.Select.Length
        For Each r1 As DataRow In dt1.Select
            cnt = cnt + 1
            Trace.WriteLine("Begin " & TableName & " Record " & cnt & " of " & totalcnt)

            Dim strf1 As String = myContext.SQL.GenerateSQLWhere(r1, myUtils.MakeCSV(lst1, ","))
            Dim rr2 As DataRow() = dt2.Select(strf1)
            If rr2.Length > 1 Then
                myUtils.ChangeAll(dsImport.Tables(TableName).Select(strf1), "Validation=DIfference in Voucher Rows")
            ElseIf myUtils.IsInList(DocType, "IS", "IP") Then
                Dim rInvoice As DataRow
                oRet = oRet + Me.TryImportInvoice(rr2(0), DocType, dsImport.Tables(TableName), dsImport.Tables("TXPD"), dsDB, rInvoice)
            ElseIf myUtils.IsInList(DocType, "PC", "PV") Then
                Dim rPayment As DataRow
                oRet = oRet + Me.TryImportAdvance(rr2(0), DocType, dsImport.Tables(TableName), dsDB, rPayment)
            End If
            For Each r2 As DataRow In dsImport.Tables(TableName).Select(strf1)
                r2("Action") = If(myUtils.IsInList(myUtils.cStrTN(r2("validation")), "OK", ""), "Add", "Nothing")
                If String.IsNullOrEmpty(myUtils.cStrTN(r2("validation"))) Then r2("validation") = "OK"
            Next

        Next
        Return oRet
    End Function
    Public Overrides Function GenerateIDOutput(dataKey As String, frmIDX As Integer) As clsProcOutput
        Dim oRet As New clsProcOutput

        Return oRet
    End Function

    Public Function FindCDNInvoiceID(CampusFilter As String, InvoiceNum As String, InvoiceDate As DateTime, CounterIDField As String, CounterIDValue As Integer) As Integer
        Dim strf1 As String = myUtils.CombineWhere(False, CampusFilter, "invoicenum='" & InvoiceNum & "'",
                                "invoicedate='" & Format(InvoiceDate, "dd-MMM-yyyy") & "'", If(String.IsNullOrEmpty(CounterIDField), "", CounterIDField & "=" & CounterIDValue))

        Dim sql As String = "select * from invoice where " & strf1
        Dim dt1 As DataTable = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql).Tables(0)
        If dt1.Rows.Count > 0 Then Return myUtils.cValTN(dt1.Rows(0)("invoiceid")) Else Return 0
    End Function
    Public Function FindRefundVouchID(CampusFilter As String, VouchNum As String, VouchDate As DateTime, CounterIDField As String, CounterIDValue As Integer) As Integer
        Dim strf1 As String = myUtils.CombineWhere(False, CampusFilter, "vouchnum='" & VouchNum & "'",
                                "dated='" & Format(VouchDate, "dd-MMM-yyyy") & "'", If(String.IsNullOrEmpty(CounterIDField), "", CounterIDField & "=" & CounterIDValue))

        Dim sql As String = "select * from payment where " & strf1
        Dim dt1 As DataTable = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql).Tables(0)
        If dt1.Rows.Count > 0 Then Return myUtils.cValTN(dt1.Rows(0)("paymentid")) Else Return 0
    End Function

    Public Function TryImportInvoice(rInv As DataRow, DocType As String, dtXL As DataTable, dtTxpdXL As DataTable, dsDB As DataSet, ByRef rInvoice As DataRow) As clsProcOutput
        Dim oRet As New clsProcOutput, SubTypeTable As String = ""
        Dim lst1 = Me.FieldLists(DocType, True)
        Dim strf1 As String = myContext.SQL.GenerateSQLWhere(rInv, myUtils.MakeCSV(lst1, ","))
        Try
            If myUtils.IsInList(DocType, "IS") Then
                'sales
                SubTypeTable = "Customer"
            Else
                'purchase
                SubTypeTable = "Vendor"
            End If
            Dim rParty As DataRow
            Dim rPartySub As DataRow = Me.FindPartySubTypeRow(myUtils.cStrTN(rInv("ctin")), rParty, SubTypeTable)
            Dim lst2 = New List(Of String)(New String() {"gstin", "gstinvoicetype", "invoicenum", "invoicedate", "val", "sply_ty"})
            If Me.CheckValid(rInv, myUtils.MakeCSV(lst2, ","), oRet) Then
                Dim rrGstReg() As DataRow = dsDB.Tables("gstreg").Select("gstin='" & rInv("gstin") & "'")
                If rrGstReg.Length > 0 Then
                    Dim rrCampus() As DataRow = dsDB.Tables("campus").Select("gstregid=" & rrGstReg(0)("gstregid"))
                    If rrCampus.Length > 0 Then
                        lst1.Remove("ctin")     'remove and use CounterPartyFilter to get existing invoice
                        Dim CounterPartyFilter = If(rPartySub Is Nothing, "", myContext.SQL.GenerateSQLWhere(rPartySub, SubTypeTable & "ID"))

                        lst1.Remove("gstin")
                        Dim CampusFilter = "campusid in (" & myUtils.MakeCSV(rrCampus, "campusid") & ")"

                        lst1.Remove("invoicedate")
                        Dim DateFilter = "invoicedate = '" & Format(Me.DateFromString(rInv("invoicedate")), "dd-MMM-yyyy") & "'"

                        Dim strf2 As String = myUtils.CombineWhere(False, CampusFilter, CounterPartyFilter, DateFilter, myContext.SQL.GenerateSQLWhere(rInv, myUtils.MakeCSV(lst1, ",")))
                        If dsDB.Tables("invoice").Select("doctype='" & DocType & "' and " & strf2).Length > 0 Then
                            oRet.AddError("Invoice already exists")
                        Else
                            rInvoice = myUtils.CopyOneRow(rInv, dsDB.Tables("invoice"),, False)
                            rInvoice("doctype") = DocType
                            rInvoice("campusid") = rrCampus(0)("campusid")
                            rInvoice("invoicedate") = Me.DateFromString(rInv("invoicedate"))
                            rInvoice("postingdate") = Me.DateFromString(rInv("postingdate"))
                            rInvoice("postperiodid") = oMaster.GetPostPeriodID(rInvoice("postingdate"))

                            If rInvoice("postperiodid") > 0 Then
                                Dim rrDivision() As DataRow = dsDB.Tables("division").Select("companyid=" & rrGstReg(0)("companyid") & " AND divisioncode='" & rInv("divisioncode") & "'")
                                If rrDivision.Length > 0 Then rInvoice("divisionid") = rrDivision(0)("divisionid") Else oRet.AddError("Division Not found")
                                Dim rr3() As DataRow = dsDB.Tables("taxarea").Select("taxareacode='" & myUtils.cStrTN(rInv("pos")) & "'")
                                If rr3.Length > 0 Then rInvoice("postaxareaid") = rr3(0)("taxareaid")
                                Select Case myUtils.cStrTN(rInvoice("gstinvoicetype")).Trim.ToLower
                                    Case "b2b"
                                        If rPartySub Is Nothing Then oRet.AddError("Counter Party not found") Else rInvoice(SubTypeTable & "id") = rPartySub(SubTypeTable & "id")
                                        Me.CheckValid(rInv, "inv_typ", oRet)
                                    Case "b2c"
                                        'allowed to have null customer
                                    Case "b2bur"
                                        If rPartySub Is Nothing Then oRet.AddError("Counter Party not found") Else rInvoice(SubTypeTable & "id") = rPartySub(SubTypeTable & "id")
                                    Case "exp"
                                        If rPartySub Is Nothing Then oRet.AddError("Counter Party not found") Else rInvoice(SubTypeTable & "id") = rPartySub(SubTypeTable & "id")
                                        Me.CheckValid(rInv, "exp_typ,sbnum,sbdt", oRet)
                                    Case "impg"
                                        If rPartySub Is Nothing Then oRet.AddError("Counter Party not found") Else rInvoice(SubTypeTable & "id") = rPartySub(SubTypeTable & "id")
                                        Me.CheckValid(rInv, "is_sez,boe_num,boe_dt,boe_val", oRet)
                                    Case "imps"
                                        If rPartySub Is Nothing Then oRet.AddError("Counter Party not found") Else rInvoice(SubTypeTable & "id") = rPartySub(SubTypeTable & "id")
                                    Case "cdn", "cdnr", "cdnur"
                                        If rPartySub Is Nothing Then
                                            oRet.AddError("Customer not found")
                                        Else
                                            rInvoice(SubTypeTable & "id") = rPartySub(SubTypeTable & "id")
                                            If Not myUtils.IsInList(myUtils.cStrTN(rInv("p_gst")), "Y") Then
                                                Dim CDNInvoiceDate = Me.DateFromString(myUtils.cStrTN(rInv("cdninvoicedate")))
                                                Dim CDNInvoiceID = Me.FindCDNInvoiceID(CampusFilter, myUtils.cStrTN(rInv("cdninvoicenum")), CDNInvoiceDate, SubTypeTable & "ID", rPartySub(SubTypeTable & "id"))
                                                If CDNInvoiceID = 0 Then oRet.AddError("CDN Invoice not found") Else rInvoice("cdninvoiceid") = CDNInvoiceID
                                            End If
                                        End If
                                        Me.CheckValid(rInv, "ntty", oRet)
                                    Case Else
                                        oRet.AddError("Gst Invoice Type not found")
                                End Select
                                If oRet.Success Then
                                    For Each rItem As DataRow In dtXL.Select(strf1)
                                        Dim oRet2 As New clsProcOutput
                                        Me.CheckValid(rItem, "num,hsn_sc,uqc,qty,basicrate", oRet2)
                                        If oRet2.Success Then
                                            If myUtils.IsInList(rInvoice("sply_ty"), "INTER") AndAlso (myUtils.cValTN(rItem("camt")) + myUtils.cValTN(rItem("samt"))) > 0 Then
                                                oRet2.AddError("Supply type and tax amount don't match")
                                            ElseIf myUtils.IsInList(rInvoice("sply_ty"), "INTRA") AndAlso (myUtils.cValTN(rItem("iamt"))) > 0 Then
                                                oRet2.AddError("Supply type and tax amount don't match")
                                            End If
                                            If oRet2.Success Then
                                                If rItem.Table.Columns.Contains("txval") AndAlso myUtils.cValTN(rItem("txval")) > myUtils.cValTN(rItem("qty")) * myUtils.cValTN(rItem("basicrate")) Then
                                                    oRet2.AddError("Taxable Value > Qty * Basic Rate")
                                                Else
                                                    Dim nri As DataRow = myUtils.CopyOneRow(rItem, dsDB.Tables("InvoiceItemGst"),, False)
                                                    nri("invoiceid") = rInvoice("invoiceid")
                                                    If myUtils.cValTN(nri("txval")) = 0 Then nri("txval") = myUtils.cValTN(rItem("qty")) * myUtils.cValTN(rItem("basicrate"))
                                                End If
                                            End If
                                        End If
                                        If Not oRet2.Success Then
                                            oRet.AddError("")
                                            rItem("validation") = oRet2.Message
                                        End If
                                    Next
                                    If dtTxpdXL.Columns.Contains("gstin") Then
                                        'Will be blank datatable if not specified
                                        Dim strf3 As String = myContext.SQL.GenerateSQLWhere(rInv, "gstin,invoicenum,invoicedate")
                                        For Each rPD In dtTxpdXL.Select(strf3)
                                            Dim rTXPD = myUtils.CopyOneRow(rPD, dsDB.Tables("TXPD"),, False)
                                            rTXPD("invoiceid") = rInvoice("invoiceid")
                                        Next
                                    End If
                                    If myUtils.IsInList(DocType, "IS") Then
                                        'sales
                                        Dim oProc As New clsGSTInvoiceSale(myContext)
                                        rInvoice("uniquekey") = oProc.CalcUniqueKey(rrGstReg(0)("gstin"), rInvoice("postperiodid"), rInvoice("invoicenum"), 0)
                                    Else
                                        Dim oProc As New clsGSTInvoicePurch(myContext)
                                        'purchase
                                        rInvoice("uniquekey") = oProc.CalcUniqueKey(rParty("partycode"), rInvoice("postperiodid"), rInvoice("invoicenum"), 0)
                                    End If
                                End If
                            Else
                                oRet.AddError("Invalid posting date")
                            End If
                        End If
                    Else
                        oRet.AddError("No campus defined for specified GSTIN")
                    End If
                Else
                    oRet.AddError("GSTIN Does not exist in system")
                End If
            End If

        Catch ex As Exception
            oRet.AddException(ex)
        End Try

        If (Not oRet.Success) AndAlso (Not String.IsNullOrEmpty(oRet.Message)) Then
            Trace.WriteLine(oRet.Message)
            For Each r1 As DataRow In dtXL.Select(strf1)
                r1("validation") = oRet.Message
            Next
        End If

        Return oRet

    End Function
    Public Function TryImportAdvance(rPay As DataRow, DocType As String, dtXL As DataTable, dsDB As DataSet, ByRef rPayment As DataRow) As clsProcOutput
        Dim oRet As New clsProcOutput, SubTypeTable As String = ""
        Dim lst1 = Me.FieldLists(DocType, True)
        Dim strf1 As String = myContext.SQL.GenerateSQLWhere(rPay, myUtils.MakeCSV(lst1, ","))
        Try
            If myUtils.IsInList(DocType, "PC") Then
                'sales
                SubTypeTable = "Customer"
            Else
                'purchase
                SubTypeTable = "Vendor"
            End If
            Dim rParty As DataRow
            Dim rPartySub As DataRow = Me.FindPartySubTypeRow(myUtils.cStrTN(rPay("ctin")), rParty, SubTypeTable)
            If rPartySub Is Nothing Then
                oRet.AddError("Counter party not found")
            Else
                If Me.CheckValid(rPay, myUtils.MakeCSV(lst1, ","), oRet) Then
                    Dim rrGstReg() As DataRow = dsDB.Tables("gstreg").Select("gstin='" & rPay("gstin") & "'")
                    If rrGstReg.Length > 0 Then
                        Dim rrCampus() As DataRow = dsDB.Tables("campus").Select("gstregid=" & rrGstReg(0)("gstregid"))
                        If rrCampus.Length > 0 Then
                            lst1.Remove("ctin")     'remove and use CounterPartyFilter to get existing invoice
                            Dim CounterPartyFilter = myContext.SQL.GenerateSQLWhere(rPartySub, SubTypeTable & "ID")

                            lst1.Remove("gstin")
                            Dim CampusFilter = "campusid in (" & myUtils.MakeCSV(rrCampus, "campusid") & ")"

                            lst1.Remove("dated")
                            Dim DateFilter = "dated = '" & Format(Me.DateFromString(rPay("Dated")), "dd-MMM-yyyy") & "'"

                            Dim strf2 As String = myUtils.CombineWhere(False, CampusFilter, CounterPartyFilter, DateFilter, myContext.SQL.GenerateSQLWhere(rPay, myUtils.MakeCSV(lst1, ",")))
                            If dsDB.Tables("payment").Select("doctype='" & DocType & "' and " & strf2).Length > 0 Then
                                oRet.AddError("Payment document already exists")
                            Else
                                rPayment = myUtils.CopyOneRow(rPay, dsDB.Tables("payment"),, False)
                                rPayment("doctype") = DocType
                                rPayment("campusid") = rrCampus(0)("campusid")
                                rPayment("dated") = Me.DateFromString(rPay("Dated"))
                                rPayment("postingdate") = Me.DateFromString(rPay("postingdate"))
                                rPayment("postperiodid") = oMaster.GetPostPeriodID(rPayment("postingdate"))
                                If rPayment("postperiodid") > 0 Then
                                    Dim rrDivision() As DataRow = dsDB.Tables("division").Select("companyid=" & rrGstReg(0)("companyid") & " AND divisioncode='" & rPay("divisioncode") & "'")
                                    If rrDivision.Length > 0 Then rPayment("divisionid") = rrDivision(0)("divisionid") Else oRet.AddError("Division Not found")
                                    rPayment(SubTypeTable & "id") = rPartySub(SubTypeTable & "id")
                                    Dim rr3() As DataRow = dsDB.Tables("taxarea").Select("taxareacode='" & myUtils.cStrTN(rPay("pos")) & "'")
                                    If rr3.Length > 0 Then rPayment("postaxareaid") = rr3(0)("taxareaid")
                                    For Each rItem As DataRow In dtXL.Select(strf1)
                                        Dim oRet2 As New clsProcOutput
                                        Me.CheckValid(rItem, "ad_amt", oRet2)
                                        If oRet2.Success Then
                                            Dim nri As DataRow = myUtils.CopyOneRow(rItem, dsDB.Tables("AT"),, False)
                                            nri("paymentid") = rPayment("paymentid")
                                        End If
                                        If Not oRet2.Success Then
                                            oRet.AddError("")
                                            rItem("validation") = oRet2.Message
                                        End If
                                    Next

                                    If myUtils.cStrTN(rPay("refundvouchnum")).Trim.Length > 0 Then
                                        rPayment("gstpaymenttype") = "R"

                                        Dim CDNInvoiceDate = Me.DateFromString(myUtils.cStrTN(rPay("refunddated")))
                                        Dim RefundPaymentID = Me.FindRefundVouchID(CampusFilter, myUtils.cStrTN(rPay("refundvouchnum")), CDNInvoiceDate, SubTypeTable & "ID", rPartySub(SubTypeTable & "id"))
                                        If RefundPaymentID = 0 Then oRet.AddError("Refund Advance Voucher not found") Else rPayment("refundpaymentid") = RefundPaymentID
                                    Else
                                        rPayment("gstpaymenttype") = "A"
                                    End If



                                Else
                                    oRet.AddError("Invalid posting date")
                                End If
                            End If
                        Else
                            oRet.AddError("No campus defined for specified GSTIN")
                        End If
                    Else
                        oRet.AddError("GSTIN Does not exist in system")
                    End If
                End If

            End If

        Catch ex As Exception
            oRet.AddException(ex)
        End Try

        If (Not oRet.Success) AndAlso (Not String.IsNullOrEmpty(oRet.Message)) Then
            Trace.WriteLine(oRet.Message)
            For Each r1 As DataRow In dtXL.Select(strf1)
                r1("validation") = oRet.Message
            Next
        End If


        Return oRet

    End Function

    Public Function TryImportPartySubType(r1 As DataRow, dsDB As DataSet, PartySubTypeTable As String, ByRef rPartySubType As DataRow) As clsProcOutput
        Dim rParty As DataRow
        Dim oRet As clsProcOutput = Me.ImportParty(r1, dsDB, rParty)
        If oRet.Success Then
            Dim rr1() As DataRow = dsDB.Tables(PartySubTypeTable).Select("partyid=" & rParty("partyid"))
            If rr1.Length > 0 Then
                rPartySubType = rr1(0)
            Else
                rPartySubType = myUtils.CopyOneRow(r1, dsDB.Tables(PartySubTypeTable),, False)
                rPartySubType("partyid") = rParty("partyid")
            End If
            rPartySubType(PartySubTypeTable & "code") = rParty("partycode")
        End If
        Return oRet

    End Function
    Public Function ImportParty(r1 As DataRow, dsDB As DataSet, ByRef rParty As DataRow) As clsProcOutput
        Dim oRet As New clsProcOutput
        Dim GSTIN As String = myUtils.cStrTN(r1("gstin"))
        If (Not String.IsNullOrEmpty(GSTIN)) AndAlso (Not GSTUtils.ValidateGSTIN(GSTIN)) Then oRet.AddError("Invalid GSTIN")
        If String.IsNullOrEmpty(r1("partycode")) Then oRet.AddError("Invalid Code")

        If oRet.Success Then
            Dim rr1() As DataRow = dsDB.Tables("partymain").Select("mpname='" & r1("mpname") & "'")
            Dim rPartyMain As DataRow
            If rr1.Length > 0 Then
                rPartyMain = rr1(0)
            Else
                rPartyMain = myUtils.CopyOneRow(r1, dsDB.Tables("partymain"),, False)
                If String.IsNullOrEmpty(myUtils.cStrTN(rPartyMain("countrycode"))) Then rPartyMain("countrycode") = "IN"
                Dim rrSub() As DataRow = dsDB.Tables("Su").Select("subdivisioncode='" & r1("statecode") & "' and countrycode='" & rPartyMain("countrycode") & "'")
                If rrSub.Length > 0 Then rPartyMain("state") = rrSub(0)("subdivisionname")
                If dsDB.Tables.Contains("customer") Then rPartyMain("partytype") = "C" Else rPartyMain("partytype") = "V"
            End If
            Dim rr2() As DataRow = dsDB.Tables("party").Select("partycode='" & r1("partycode") & "'")
            If rr2.Length > 0 Then
                rParty = rr2(0)
                r1("Validation") = "Exists"
                r1("action") = "None"
            Else
                rParty = myUtils.CopyOneRow(r1, dsDB.Tables("party"),, False)
                rParty("gstin") = GSTIN
                myFuncsBase.CopyAdd(myContext, rPartyMain, rParty, "", "Sel")
                rParty("mainpartyid") = rPartyMain("mainpartyid")
                Dim rr3() As DataRow = dsDB.Tables("taxarea").Select("taxareacode='" & myUtils.cStrTN(r1("taxareacode")) & "'")
                If rr3.Length > 0 Then rParty("taxareaid") = rr3(0)("taxareaid")
                r1("Validation") = "OK"
                r1("action") = "Add"
            End If
        Else
            Trace.WriteLine(oRet.Message)
            r1("Validation") = oRet.Message
        End If

        Return oRet


    End Function
    Public Function FieldLists(TableName As String, Unique As Boolean) As List(Of String)
        Dim lst1 As New List(Of String)
        Select Case TableName.Trim.ToLower

            Case "ip"

                If Unique Then

                    lst1 = New List(Of String)(New String() {"gstin", "ctin", "invoicenum", "invoicedate"})

                Else

                    lst1 = New List(Of String)(New String() {"gstin", "gstinvoicetype", "inv_typ", "sply_ty", "invoicenum", "invoicedate", "ctin", "pos", "rchrg", "diff_percent", "val", "is_sez", "stin", "boe_num", "boe_dt", "boe_val", "port_code", "ntty", "cdninvoicenum", "cdninvoicedate", "p_gst"})

                End If

            Case "is"

                If Unique Then

                    lst1 = New List(Of String)(New String() {"gstin", "ctin", "invoicenum", "invoicedate"})

                Else

                    lst1 = New List(Of String)(New String() {"gstin", "gstinvoicetype", "inv_typ", "sply_ty", "invoicenum", "invoicedate", "ctin", "pos", "rchrg", "diff_percent", "val", "exp_typ", "sbnum", "sbdt", "sbpcode", "ntty", "cdninvoicenum", "cdninvoicedate", "p_gst"})

                End If

            Case "ipi"

                lst1 = New List(Of String)(New String() {"num", "hsn_sc", "ZeroTax_Type", "desc", "uqc", "qty", "basicrate", "txval", "rt", "iamt", "camt", "samt", "csamt", "tx_i", "tx_c", "tx_s", "tx_cs", "elg"})
            Case "isi"
                lst1 = New List(Of String)(New String() {"num", "hsn_sc", "ZeroTax_Type", "desc", "uqc", "qty", "basicrate", "txval", "rt", "iamt", "camt", "samt", "csamt"})

            Case "pv", "pc"

                If Unique Then

                    lst1 = New List(Of String)(New String() {"gstin", "vouchnum", "dated"})

                Else

                    lst1 = New List(Of String)(New String() {"gstin", "vouchnum", "dated", "ctin", "pos", "gstinvoicetype", "gstinvoicesubtype", "refundvouchnum", "refunddated", "diff_percent"})

                End If

            Case "pvi", "pci"

                lst1 = New List(Of String)(New String() {"ad_amt", "rt", "ZeroTax_Type", "iamt", "camt", "samt", "csamt"})

            Case "txpd"

                lst1 = New List(Of String)(New String() {"ad_amt", "rt", "iamt", "camt", "samt", "csamt"})

        End Select
        Return lst1
    End Function
    Public Function FindPartySubTypeRow(CTIN As String, ByRef rParty As DataRow, SubTypeTable As String) As DataRow
        If Not String.IsNullOrEmpty(CTIN) Then
            Dim rrParty() As DataRow = dsDB.Tables("party").Select("gstin='" & CTIN & "'")
            If rrParty.Length = 0 Then rrParty = dsDB.Tables("party").Select("partycode='" & CTIN & "'")
            If rrParty.Length > 0 Then
                Dim rrSub() As DataRow = dsDB.Tables(SubTypeTable).Select("partyid=" & rrParty(0)("partyid"))
                If rrSub.Length > 0 Then Return rrSub(0)
            End If
        End If

    End Function
    Public Function CheckValid(r1 As DataRow, strReq As String, oRet As clsProcOutput) As Boolean
        Dim arr() As String = Split(strReq, ",")
        For Each str1 In arr
            If r1.Table.Columns.Contains(str1) Then
                Dim obj = r1(str1)
                If (obj Is DBNull.Value) OrElse (obj Is Nothing) Then
                    oRet.AddError("Complete " & str1)
                End If
            End If
        Next
        Return oRet.Success
    End Function
    Protected Friend Sub ProcessImport(Params As Dictionary(Of String, String))
        Dim oRet As New clsProcOutput
        Dim s = System.IO.File.Open(Params("path"), FileMode.Open)
        Dim dsImport As New DataSet
        Me.ProcessImport(dsImport, s)


        Dim model As New frmGstImportVouchOldModel(myContext)
        Dim vw As New clsViewModel(myContext)
        If model.PrepForm(vw, EnumfrmMode.acAddM, "") Then
            model.DatasetCollection.AddUpd("import", dsImport)
            If model.Validate() Then
                If model.VSave() Then
                    oRet.AddMessage("Imported")
                Else
                    oRet.AddError("Error on Save")

                End If
            Else
                oRet.AddError("Error on Validate")
            End If
        End If

    End Sub
    Public Function ProcessImport(dsImport As DataSet, s As Stream) As String
        Dim lst As New List(Of String)
        Dim objImporter As New clsSSGImport(myContext)
        objImporter.OpenStream(s)



        Dim dt1 = myUtils.AddTable(dsImport, objImporter.GenerateTableFromRange("customer"), "customer")
        If Not dt1 Is Nothing Then
            lst.Add("Found " & dt1.Rows.Count & " Rows for Customer </br>")
        End If


        Dim dt2 = myUtils.AddTable(dsImport, objImporter.GenerateTableFromRange("vendor"), "vendor")
        If Not dt2 Is Nothing Then

            lst.Add("Found " & dt2.Rows.Count & " Rows for Vendor </br>")
        End If


        Dim dt3 = myUtils.AddTable(dsImport, objImporter.GenerateTableFromRange("invoice"), "inv")
        If Not dt3 Is Nothing Then
            lst.Add("Found " & dt3.Rows.Count & " Rows for Invoices </br>")
        End If


        Dim dt4 = myUtils.AddTable(dsImport, objImporter.GenerateTableFromRange("advance"), "at")
        If Not dt4 Is Nothing Then
            lst.Add("Found " & dt4.Rows.Count & " Rows for Advance Tax </br>")
        End If


        Dim dt5 = myUtils.AddTable(dsImport, objImporter.GenerateTableFromRange("taxpaid"), "txpd")
        If Not dt5 Is Nothing Then
            lst.Add("Found " & dt5.Rows.Count & " Rows for Advance Adjusted </br>")
        End If


        Dim msg As String = myUtils.MakeCSV(vbCrLf, lst.ToArray)
        Return msg
    End Function

End Class
