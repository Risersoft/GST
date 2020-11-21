
Imports System.Collections.Concurrent
Imports System.Text
Imports System.Threading
Imports risersoft.app.mxent
Imports risersoft.shared

Public Class clsGSTInvoiceBase
    Inherits clsGSTDocBase
    Dim TolTxVal, TolTxAmt As Decimal, POS, AdjustIV As Boolean
    Dim dicSQL As New clsCollecString(Of String)

    Public Sub New(context As IProviderContext, DocType As String, ReturnField As String)
        MyBase.New(context, DocType, ReturnField, "InvoiceID")

    End Sub

    Public Overrides Function DeleteVouchFilter(strf1 As String) As clsProcOutput
        Dim dic As New clsCollecString(Of String), oRet As New clsProcOutput
        Dim sql2 = "select doctype, GstRegID, CTIN, ReturnPeriodID, gstinvoicetype, invoicenum, invoicedate, boe_num, boe_dt from gstlistinvoice() where " & myUtils.CombineWhere(False, strf1, "lasttransactionid is not null")
        Dim dt1 As DataTable = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql2).Tables(0)
        For Each r1 As DataRow In dt1.Select()
            Dim rDel = Me.AddDeleteAction(r1, r1("gstregid"), myUtils.cStrTN(r1("CTIN")))
        Next

        Dim strf2 As String = "invoiceid In (select invoiceid From invoice where " & strf1 & ")"
        dic.Add("item", "delete top (10000) from invoiceitemgst where " & strf2)
        dic.Add("vouch", "delete top (10000) from invoice where " & strf1)
        Try
            myContext.Provider.dbConn.BeginTransaction(myContext)
            For Each kvp In dic
                Trace.WriteLine("Deleting " & kvp.Key)
                Dim RowCount As Integer = 1
                While RowCount > 0
                    RowCount = myContext.Provider.objSQLHelper.ExecuteNonQuery(CommandType.Text, kvp.Value & "; select @@rowcount")
                    Trace.WriteLine($"RowCount={RowCount}")
                End While
            Next
            myContext.Provider.dbConn.CommitTransaction()
            oRet.AddMessage("Your invoice data has been deleted.")
        Catch ex As Exception
            Trace.WriteLine(ex.Message & ex.StackTrace)
            myContext.Provider.dbConn.RollBackTransaction()
            oRet.AddException(ex)
        End Try
        Return oRet
    End Function

    Public Overridable Function DeleteSummaryTable(strf1 As String) As clsProcOutput
        Dim dic As New clsCollecString(Of String), oRet As New clsProcOutput

        dic.Add("b2cs", "delete from GstnB2CS where " & strf1)
        dic.Add("nil", "delete from GstnNIL where " & strf1)
        dic.Add("HSN", "delete from GstnHSN where " & strf1)
        dic.Add("AT", "delete from GstnAtTxpd where " & strf1)
        Try
            myContext.Provider.dbConn.BeginTransaction(myContext)
            For Each kvp In dic
                Trace.WriteLine("Deleting " & kvp.Key)
                Dim RowCount = myContext.Provider.objSQLHelper.ExecuteNonQuery(CommandType.Text, kvp.Value & "; select @@rowcount")
                Trace.WriteLine($"RowCount={RowCount}")
            Next
            myContext.Provider.dbConn.CommitTransaction()
            oRet.AddMessage("Your summary data has been deleted.")
        Catch ex As Exception
            Trace.WriteLine(ex.Message & ex.StackTrace)
            myContext.Provider.dbConn.RollBackTransaction()
            oRet.AddException(ex)
        End Try
        Return oRet
    End Function

    Public Overrides Function DeleteCPVouchFilter(strf1 As String) As clsProcOutput
        Dim dic As New clsCollecString(Of String), oRet As New clsProcOutput
        Dim strf2 As String = "cpinvoiceid In (select cpinvoiceid From cpinvoice where " & strf1 & ")"
        dic.Add("item", "delete  top (10000) from cpinvoiceitem where " & strf2)
        dic.Add("vouch", "delete  top (10000) from cpinvoice where " & strf1)

        Try
            myContext.Provider.dbConn.BeginTransaction(myContext)
            For Each kvp In dic
                Trace.WriteLine("Deleting " & kvp.Key)
                Dim RowCount As Integer = 1
                While RowCount > 0
                    RowCount = myContext.Provider.objSQLHelper.ExecuteNonQuery(CommandType.Text, kvp.Value & "; select @@rowcount")
                    Trace.WriteLine($"RowCount={RowCount}")
                End While
            Next

            myContext.Provider.dbConn.CommitTransaction()
            oRet.AddMessage("Your CP invoice data has been deleted.")
        Catch ex As Exception
            Trace.WriteLine(ex.Message & ex.StackTrace)
            myContext.Provider.dbConn.RollBackTransaction()
            oRet.AddException(ex)
        End Try
        Return oRet
    End Function
    Public Overrides Function DeletePeriod(CampusFilterCheck As String, CampusFilterDoc As String, PeriodFilter As String, strFilter As String, DeleteSummary As Boolean) As clsProcOutput
        Dim oRet = Me.CheckFinalFilter(CampusFilterCheck, PeriodFilter)
        If oRet.Success Then
            Dim strf1 As String = myUtils.CombineWhere(False, CampusFilterDoc, PeriodFilter, $"DocType='{DocType}'", strFilter)
            oRet = Me.DeleteVouchFilter(strf1)

            If DeleteSummary Then
                Dim strf2 As String = myUtils.CombineWhere(False, CampusFilterCheck, PeriodFilter, strFilter)
                oRet = Me.DeleteSummaryTable(strf2)
            End If
        End If
        Return oRet
    End Function
    Public Overrides Function DeletePeriodCP(CampusFilterCheck As String, CampusFilterDoc As String, PeriodFilter As String) As clsProcOutput
        Dim oRet = Me.CheckFinalFilter(CampusFilterCheck, PeriodFilter)
        If oRet.Success Then
            Dim strf1 As String = myUtils.CombineWhere(False, CampusFilterDoc, PeriodFilter, $"DocType='{DocType}'")
            oRet = Me.DeleteCPVouchFilter(strf1)
        End If
        Return oRet
    End Function
    Public Overrides Function DeleteVouchImport(ImportFileID As String) As clsProcOutput
        Dim CampusFilter = "gstregid in (select gstregid from campus where campusid in (select campusid from invoice where importfileid='" & ImportFileID & "'))"
        Dim PeriodFilter = "postperiodid in (select returnperiodid from invoice where importfileid='" & ImportFileID & "')"
        Dim oRet = Me.CheckFinalFilter(CampusFilter, PeriodFilter)
        If oRet.Success Then
            Dim strf1 As String = String.Format("importfileid='{0}'", ImportFileID)
            oRet = Me.DeleteVouchFilter(strf1)
            oRet = Me.DeleteSummaryTable(strf1)
        End If
        Return oRet
    End Function
    Public Overrides Function DeleteCPVouchImport(ImportFileID As String) As clsProcOutput
        Dim CampusFilter = "gstregid in (select gstregid from cpinvoice where importfileid='" & ImportFileID & "')"
        Dim PeriodFilter = "postperiodid in (select returnperiodid from cpinvoice where importfileid='" & ImportFileID & "')"
        Dim oRet = Me.CheckFinalFilter(CampusFilter, PeriodFilter)
        If oRet.Success Then
            Dim strf1 As String = String.Format("importfileid='{0}'", ImportFileID)
            oRet = Me.DeleteCPVouchFilter(strf1)
        End If
        Return oRet
    End Function
    Public Overrides Function DeleteVouch(ID As Integer, AcceptWarning As Boolean) As clsProcOutput
        Dim oRet = Me.DeleteVouch(ID, AcceptWarning, "gstlistInvoice()", "InvoiceID")
        Return oRet
    End Function


    Public Overrides Function MatchUniqueFields(r1 As DataRow) As Boolean
        Dim match As Boolean = myUtils.MatchRowVersions(r1, New String() {"campusid", "ReturnPeriodID", "gstinvoicetype", "ctin", "invoicenum", "invoicedate", "boe_num", "boe_dt"})
        Return match
    End Function

    Public Overrides Function AddDeleteAction(r1 As DataRow, GstRegID As Integer, CTIN As String) As DataRow

        Dim FilterList As New List(Of String)
        FilterList.Add("gstregid=" & GstRegID)
        For Each str1 As String In New String() {"doctype", "gstinvoicetype", "ReturnPeriodID", "invoicenum", "invoicedate"}
            If r1.Table.Columns.Contains(str1) Then
                FilterList.Add(myContext.SQL.GenerateSQLWhereValue(r1.Table, str1, r1(str1, DataRowVersion.Original)))
            End If
        Next
        If Not String.IsNullOrEmpty(CTIN) Then
            FilterList.Add("ctin='" & CTIN & "'")
        End If
        Dim strf1 As String = myUtils.CombineWhere(False, FilterList.ToArray)

        Dim sql As String = "select * from gstnaction where " & strf1
        Dim dt1 As DataTable = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql).Tables(0)
        Dim nr As DataRow
        If dt1.Rows.Count > 0 Then nr = dt1.Rows(0) Else nr = myContext.Tables.AddNewRow(dt1)

        nr("gstregid") = GstRegID
        nr("actionflag") = "D"
        For Each str1 As String In New String() {"doctype", "ReturnPeriodID", "gstinvoicetype", "invoicenum", "invoicedate", "boe_num", "boe_dt"}
            If nr.Table.Columns.Contains(str1) AndAlso r1.Table.Columns.Contains(str1) Then
                nr(str1) = r1(str1, DataRowVersion.Original)
            End If
        Next
        nr("CTIN") = CTIN
        myContext.Provider.objSQLHelper.SaveResults(dt1, sql)

        Return nr
    End Function
    Public Overrides Sub PopulateCalc(IDValue As Integer, rVouch As DataRow, rGstReg As DataRow, dtVouchItem As DataTable, dtCalc As DataTable, rCDN As DataRow, rOrig As DataRow, dsMaster As DataSet)
        Dim rCalc = Me.GetCalcRow(IDValue, rVouch, dtCalc, dtVouchItem, rOrig)
        If myUtils.IsInList(Me.DocType, "IP") Then
            For Each str1 As String In New String() {"i", "c", "s", "cs"}
                rCalc("tx" & str1 & "summ") = myUtils.cValTN(dtVouchItem.Compute($"sum(tx_{str1})", $"isnull({IDField},0)={IDValue}"))
            Next
        End If
        Dim oProc = Me.GetValidator(IDValue, rVouch, rGstReg, rCDN, rOrig, dsMaster)
        Dim IsTaxable As Boolean = (dtVouchItem.Select("isnull(GstTaxType,'') in ('','taxable')").Length > 0) OrElse myUtils.IsInList(myUtils.cStrTN(rVouch("GSTInvoiceType")), "EXP")
        oProc.SetValue("IsTaxable", IsTaxable)

        For Each str1 As String In New String() {"gstr1", "gstr2", "gstr6", "anx01", "anx02"}
            rCalc(str1 & "section") = Me.CalculateSection(oProc, str1, dsMaster)
        Next

        For Each rVouchItem As DataRow In dtVouchItem.Select($"isnull({IDField},0)={IDValue}")
            For Each str1 As String In New String() {"gstr3b31", "gstr3b32", "gstr3b4a", "gstr3b4d", "gstr3b5"}
                oProc.AddOrUpdateRow(rVouchItem, "item")
                rVouchItem(str1 & "section") = Me.CalculateSection(oProc, str1, dsMaster)
            Next
        Next
    End Sub
    Public Overrides Function LoadVouchSQL(strFilter As String) As String
        Dim Sql = "Select InvoiceID, CustomerID, VendorID, CampusID,TransactionType, b2c_typ, CDNInvoiceID, RootInvoiceID, Inv_Typ, Exp_Typ, InvoiceNum,InvoiceDate,PartyName,GSTInvoiceType,CTIN,boe_num,boe_dt, sply_ty,RCHRG,POSTaxAreaID,ReturnPeriodID,txval,iamt,camt,samt,csamt from gstlistinvoice() " &
        myUtils.CombineWhere(True, strFilter)
        Return Sql
    End Function
    Public Overrides Function LoadVouchCPSQL(strFilter As String) As String
        Dim sql = "select cpinvoiceid, invoiceid, postaxareaid, gstregid, companyid, finyearid, ctin, inum,idt,nt_num,nt_dt,oinum,oidt,
                       gstinvoicetype, val, updby, matchcode, mmdescrip, actionflag, Inv_Typ, ntty,
                     txval, iamt, camt, samt, csamt, adjtxval, adjiamt, adjcamt, adjsamt, adjcsamt, 
                    matchperiodid, clubcpinvoiceid, invoicenum, invoicedate, returnperiodid, ntty from acclistcpinvoice() 
                    " & myUtils.CombineWhere(True, strFilter)
        Return sql
    End Function

End Class
Public Class clsGSTInvoicePurch
    Inherits clsGSTInvoiceBase
    Public Sub New(context As IProviderContext)
        MyBase.New(context, "IP", "GSTR2")
    End Sub


    Public Overrides Sub PopulateGstType(r1 As DataRow, dtItems As DataTable, TaxAreaCode As String)

        If myUtils.IsInList(myUtils.cStrTN(r1("TransactionType")), "Pur") Then
            If myUtils.IsInList(myUtils.cStrTN(r1("PartyGSTRegType")), "N") Then
                If myUtils.IsInList(TaxAreaCode, "OTH") Then
                    If dtItems.Select("ty='G'").Length > 0 Then
                        r1("GSTInvoiceType") = "IMPG"
                    Else
                        r1("GSTInvoiceType") = "IMPS"
                    End If
                Else
                    r1("GSTInvoiceType") = "B2BUR"
                End If
            Else
                r1("GSTInvoiceType") = "B2B"
            End If
        Else
            If myUtils.IsInList(myUtils.cStrTN(r1("PartyGSTRegType")), "N") Then
                r1("gstinvoicetype") = "CDNUR"
            Else
                r1("gstinvoicetype") = "CDN"
            End If
        End If

        If myUtils.IsInList(myUtils.cStrTN(r1("GSTInvoiceType")), "B2B") Then
            If myUtils.IsInList(myUtils.cStrTN(r1("PartyGSTRegType")), "SEZ", "SEZD") Then
                If dtItems.Select("I_RT>0").Length > 0 Then
                    r1("gstinvoicesubtype") = "SEWP"
                Else
                    r1("gstinvoicesubtype") = "SEWOP"
                End If
            ElseIf myUtils.IsInList(myUtils.cStrTN(r1("PartyGSTRegType")), "DE") Then
                r1("gstinvoicesubtype") = "DE"
            Else
                r1("gstinvoicesubtype") = "R"
            End If
        ElseIf myUtils.IsInList(myUtils.cStrTN(r1("GSTInvoiceType")), "CDN", "CDNUR") Then
            r1("gstinvoicesubtype") = Left(myUtils.cStrTN(r1("TransactionType")), 1)
        End If

    End Sub
    Public Overrides Sub UpdateInvoiceNumberDynamicPart(GstRegID As Integer, rInv As DataRow, dtSettings As DataTable)
        Dim oRet = myFuncs2.GetInvoiceNumberDynamicPart(dtSettings, oMaster, GstRegID, "INVI", myUtils.cStrTN(rInv("invoicenum")))
        If oRet.Success Then
            rInv("invoicenumbertemplateid") = oRet.ID
            rInv("invoicenumdynamicpart") = oRet.Result
        End If
    End Sub
    Public Overrides Sub UpdateInvoiceNumberDynamicPart(GstRegID As Integer, rInv As DataRow)
        Dim oRet As clsProcOutput(Of Long)
        If myUtils.IsInList(myUtils.cStrTN(rInv("rchrg")), "y") Then
            oRet = myFuncs2.GetInvoiceNumberDynamicPart(myContext.Provider, GstRegID, "INVI", myUtils.cStrTN(rInv("invoicenum")))
        End If
        If (Not oRet Is Nothing) AndAlso oRet.Success Then
            rInv("invoicenumbertemplateid") = oRet.ID
            rInv("invoicenumdynamicpart") = oRet.Result
        End If
    End Sub
    Protected Friend Overrides Function GetFactor(rVouch As DataRow) As Decimal
        Dim Factor = If(myUtils.IsInList(myUtils.cStrTN(rVouch("GSTInvoiceSubType")), "C"), -1, 1)
        Return Factor
    End Function
End Class
Public Class clsGSTInvoiceSale
    Inherits clsGSTInvoiceBase
    Public Sub New(context As IProviderContext)
        MyBase.New(context, "IS", "GSTR1")
    End Sub
    Public Overrides Sub PopulateGstType(r1 As DataRow, dtItems As DataTable, TaxAreaCode As String)

        If myUtils.IsInList(myUtils.cStrTN(r1("TransactionType")), "Sales") Then
            If myUtils.IsInList(myUtils.cStrTN(r1("PartyGSTRegType")), "N") Then
                If myUtils.IsInList(TaxAreaCode, "OTH") Then
                    r1("GSTInvoiceType") = "EXP"
                Else
                    r1("GSTInvoiceType") = "B2C"
                End If
            Else
                r1("GSTInvoiceType") = "B2B"
            End If
        Else
            If myUtils.IsInList(myUtils.cStrTN(r1("PartyGSTRegType")), "N") Then
                r1("gstinvoicetype") = "CDNUR"
            Else
                r1("gstinvoicetype") = "CDN"
            End If
        End If

        If myUtils.IsInList(myUtils.cStrTN(r1("GSTInvoiceType")), "B2B") Then
            If myUtils.IsInList(myUtils.cStrTN(r1("PartyGSTRegType")), "SEZ", "SEZD") Then
                If myUtils.IsInList(myUtils.cStrTN(r1("ExportDutyType")), "WPAY") Then
                    r1("gstinvoicesubtype") = "SEWP"
                Else
                    r1("gstinvoicesubtype") = "SEWOP"
                End If
            ElseIf myUtils.IsInList(myUtils.cStrTN(r1("PartyGSTRegType")), "DE") Then
                r1("gstinvoicesubtype") = "DE"
            ElseIf myUtils.cValTN(r1("SaleBondedWH")) = 1 Then
                r1("gstinvoicesubtype") = "CBW"
            Else
                r1("gstinvoicesubtype") = "R"
            End If
        ElseIf myUtils.IsInList(myUtils.cStrTN(r1("GSTInvoiceType")), "B2C") Then
            If myUtils.cValTN(r1("Val")) > 250000 AndAlso myUtils.IsInList(myUtils.cStrTN(r1("sply_ty")), "INTER") Then
                r1("gstinvoicesubtype") = "L"
            Else
                r1("gstinvoicesubtype") = "S"
            End If
        ElseIf myUtils.IsInList(myUtils.cStrTN(r1("GSTInvoiceType")), "EXP") Then
            r1("gstinvoicesubtype") = r1("ExportDutyType")
        ElseIf myUtils.IsInList(myUtils.cStrTN(r1("GSTInvoiceType")), "CDN", "CDNUR") Then
            r1("gstinvoicesubtype") = Left(r1("TransactionType"), 1)
        End If
    End Sub
    Public Function GetNature(rInv As DataRow) As String
        Dim str1 As String = ""
        If myUtils.cValTN(rInv("isamendment")) > 0 Then
            str1 = "INVR"
        ElseIf myUtils.IsInList(rInv("transactiontype"), "CNS") Then
            str1 = "CN"
        ElseIf myUtils.IsInList(rInv("transactiontype"), "DNS") Then
            str1 = "DN"
        Else
            str1 = "INVO"
        End If
        Return str1
    End Function

    Public Overrides Sub UpdateInvoiceNumberDynamicPart(GstRegID As Integer, rInv As DataRow)
        Dim str1 As String = Me.GetNature(rInv)
        Dim dt As DataTable = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, "SELECT InvoiceNumberTemplateId,DocumentNature, CompanyId, GSTRegId, Prefix, Suffix FROM GstDocNumTemplate").Tables(0)
        Dim oRet = myFuncs2.GetInvoiceNumberDynamicPart(dt, oMaster, GstRegID, str1, myUtils.cStrTN(rInv("invoicenum")))
        If oRet.Success Then
            rInv("invoicenumbertemplateid") = oRet.ID
            rInv("invoicenumdynamicpart") = oRet.Result
        End If
    End Sub
    Public Overrides Sub UpdateInvoiceNumberDynamicPart(GstRegID As Integer, rInv As DataRow, dtSettings As DataTable)
        Dim str1 As String = Me.GetNature(rInv)
        Dim oRet = myFuncs2.GetInvoiceNumberDynamicPart(dtSettings, oMaster, GstRegID, str1, myUtils.cStrTN(rInv("invoicenum")))
        If oRet.Success Then
            rInv("invoicenumbertemplateid") = oRet.ID
            rInv("invoicenumdynamicpart") = oRet.Result
        End If
    End Sub
    Protected Friend Overrides Function GetFactor(rVouch As DataRow) As Decimal
        Dim Factor = If(myUtils.IsInList(myUtils.cStrTN(rVouch("GSTInvoiceSubType")), "C"), -1, 1)
        Return Factor
    End Function
    Protected Friend Overrides Function GetFactor2(rVouch As DataRow) As Decimal
        Dim Factor = If(myUtils.IsInList(myUtils.cStrTN(rVouch("RCHRG")), "Y", "YES"), 0, 1)
        Return Factor
    End Function

End Class
