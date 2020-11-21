Imports risersoft.shared
Imports risersoft.API.GSTN.GSTR2
Imports risersoft.API.GSTN
Imports Newtonsoft.Json
Imports System.Text
Imports System.Web
Imports GSTN.API.Library.Models.GstNirvana

Public Class clsGSTNReturnGSTR2
    Inherits clsGSTNTypedReturnBase(Of GSTR2Total, SummaryInward, GSTR2ApiClient, GSTR2AApiClient)
    Public Overrides Property CDNRApiAction As String = "CDN"
    Public Sub New(context As IProviderContext)
        MyBase.New(context, "IP", "GSTR2")
    End Sub
    Public Overrides Function PrepareGSTRPayloadSQLUp(GstRegID As Integer, ReturnPeriodID As Integer) As clsCollecString(Of String)
        Dim strf1 As String = "actionflag in ('U','M')"
        Dim strf2 As String = "GstRegID=" & GstRegID
        Dim strf3 As String = " ReturnPeriodID =" & ReturnPeriodID
        Dim strf4 As String = fncInvoiceFilter.Invoke()

        Dim strFilterInv As String = myUtils.CombineWhere(False, strf1, strf2, strf3, strf4)
        Dim strFilterSumm As String = myUtils.CombineWhere(False, strf2, strf3)
        Dim dic As New clsCollecString(Of String)

        dic.Add("b2b", " select invoiceid, ctin, chksum, inum, idt, val, POSTaxAreaTinCode as pos, rchrg, inv_typ,  
sum(txval) as txval, rt, sum(iamt) as iamt, sum(camt) as camt, sum(samt) as samt, sum(csamt) as csamt, 
sum(tx_i) as tx_i, sum(tx_c) as tx_c, sum(tx_s) as tx_s, sum(tx_cs) as tx_cs, elg from gstListinvoiceitem() 
" & myUtils.CombineWhere(True, strFilterInv, "gstInvoiceType='B2B' and DocType='IP' and isnull(isamendment,0)=0 and isnull(istaxable,1)=1") & " group by inum, idt, val, 
POSTaxAreaTinCode, rchrg, inv_typ, rt, elg,invoiceid,ctin,chksum")

        dic.Add("b2ba", "select invoiceid, ctin, chksum, oinum, oidt, inum,idt,val,POSTaxAreaTinCode as pos, rchrg, inv_typ, rt,sum(txval) as txval,sum(iamt) as iamt,sum(camt) as camt, sum(samt) as samt, sum(csamt) as csamt,
sum(tx_i) as tx_i, sum(tx_c) as tx_c, sum(tx_s) as tx_s, sum(tx_cs) as tx_cs, elg from gstlistinvoiceitem()
" & myUtils.CombineWhere(True, strFilterInv, "gstInvoiceType='B2B' and DocType='IP' and isnull(isamendment,0)=1 and isnull(istaxable,1)=1") & " group by invoiceid, ctin,
oinum,oidt,inum, idt, val, POSTaxAreaTinCode, rchrg, inv_typ, rt, chksum,elg,invoiceid")

        dic.Add("b2bur", " select invoiceid, inum, idt, val, sply_ty,POSTaxAreaTinCode as pos, sum(txval) as txval, rt, sum(iamt) as iamt, 
sum(camt) as camt, sum(samt) as samt, sum(csamt) as csamt, sum(tx_c) as tx_c, sum(tx_s) as tx_s, 
sum(tx_cs) as tx_cs, elg from gstListinvoiceitem() " & myUtils.CombineWhere(True, strFilterInv, "gstInvoiceType='B2BUR' and DocType='IP' and isnull(isamendment,0)=0 and isnull(istaxable,1)=1") & " group by invoiceid,inum, idt, val, POSTaxAreaTinCode,  rt, elg,sply_ty ")

        dic.Add("imp_g", " select invoiceid, is_sez, stin, boe_num, boe_dt, boe_val, port_code,sum(txval) as txval, rt, sum(iamt) as iamt, 
sum(csamt) as csamt, elg, sum(tx_i) as tx_i, sum(tx_cs) as tx_cs from gstListinvoiceitem() 
" & myUtils.CombineWhere(True, strFilterInv, "gstInvoiceType='IMPG' and DocType='IP' and isnull(isamendment,0)=0 and isnull(istaxable,1)=1") & " group by is_sez, stin, boe_num, boe_dt, boe_val, rt, elg,invoiceid,port_code ")

        dic.Add("imp_s", " select invoiceid,inum, idt, val as ival, sum(txval) as txval, rt, POSTaxAreaTinCode as pos,sum(iamt) as iamt, sum(csamt) as csamt, elg, 
sum(tx_i) as tx_i, sum(tx_cs) as tx_cs from gstListinvoiceitem() " & myUtils.CombineWhere(True, strFilterInv, "gstInvoiceType='IMPS' and DocType='IP' and isnull(isamendment,0)=0 and isnull(istaxable,1)=1") & " group By inum, idt, val, rt, elg,POSTaxAreaTinCode,invoiceid")

        dic.Add("cdn", " select invoiceid,ctin, chksum, ntty, nt_num, nt_dt,  p_gst, inum, idt,  sum(txval) as txval, 
rt, sum(iamt) as iamt, sum(camt) as camt, sum(samt) as samt, sum(csamt) as csamt, sum(tx_i) as tx_i, 
sum(tx_c) as tx_c, sum(tx_s) as tx_s, sum(tx_cs) as tx_cs, elg from gstListinvoiceitem() 
" & myUtils.CombineWhere(True, strFilterInv, "gstInvoiceType='CDN' and DocType='IP' and isnull(isamendment,0)=0 and isnull(istaxable,1)=1") & " group by invoiceid, ctin, ntty, nt_num, nt_dt,  p_gst, inum, idt, rt, elg, chksum")

        dic.Add("cdna", " select invoiceid,ctin, chksum, ont_num,ont_dt,ntty, nt_num, nt_dt, p_gst, inum, idt, 
sum(txval) as txval,sum(iamt) as iamt, sum(camt) as camt, sum(samt) as samt, sum(csamt) as csamt,
 sum(tx_i) as tx_i, sum(tx_c) as tx_c, sum(tx_s) as tx_s, sum(tx_cs) as tx_cs, elg
from gstListinvoiceitem() " & myUtils.CombineWhere(True, strFilterInv, “gstInvoiceType='CDN' and DocType='IP' and isnull(isamendment,0)=1 and isnull(istaxable,1)=1") & " group By ctin, ont_num,ont_dt, ntty, nt_num, nt_dt, inum, idt,p_gst, elg, chksum,invoiceid ")

        dic.Add("cdnur", " select invoiceid,gstin as rtin,ntty, nt_num, nt_dt,  p_gst, inum, idt, sum(val) as val, inv_typ, 
sum(txval) as txval, rt, sum(iamt) as iamt, sum(camt) as camt, sum(samt) as samt, sum(csamt) as csamt, 
sum(tx_i) as tx_i, sum(tx_c) as tx_c, sum(tx_s) as tx_s, sum(tx_cs) as tx_cs, elg 
from gstListinvoiceitem() " & myUtils.CombineWhere(True, strFilterInv, "gstInvoiceType='CDNUR' and DocType='IP' and isnull(isamendment,0)=0 and isnull(istaxable,1)=1") & " group by invoiceid,ntty, nt_num, nt_dt, p_gst, inum, 
idt, rt, elg,gstin,inv_typ ")

        dic.Add("nilinter", " select sum(cpddr) as cpddr, sum(Nil_Amt) as NilSply,sum(Expt_amt) as Exptdsply,Sum(ngsup_amt) as ngsply 
from gstListinvoiceitem() " & myUtils.CombineWhere(True, strFilterSumm, "isnull(isamendment,0)=0 and isnull(istaxable,1)=0 and  sply_ty='inter' and DocType='IP' "))

        dic.Add("nilintra", " select sum(cpddr) as cpddr, sum(Nil_Amt) as NilSply,sum(Expt_amt) as Exptdsply,Sum(ngsup_amt) as ngsply 
from gstListinvoiceitem() " & myUtils.CombineWhere(True, strFilterSumm, "isnull(isamendment,0)=0 and isnull(istaxable,1)=0 and sply_ty='intra' and DocType='IP' "))

        dic.Add("txi", " select POSTaxAreaTinCode as pos, sply_ty, rt, sum(ad_amt) as adamt, sum(iamt) as iamt, sum(camt) as camt, 
sum(samt) as samt, sum(csamt) as csamt from GstListAdvanceVouchItem() " & myUtils.CombineWhere(True, strFilterSumm, " DocType='PV' and AdvanceVouchType in ('A','C','R') 
and isnull(isamendment,0)=0") & " group by POSTaxAreaTinCode, sply_ty, rt ")

        dic.Add("txpd", " select sply_ty, POSTaxAreaTinCode as pos, rt, sum(Ad_amt) as adamt, sum(iamt) as iamt,sum(camt) as camt,
sum(samt) as samt,sum(csamt) as csamt from  GstListAdvanceVouchItem()" & myUtils.CombineWhere(True, strFilterSumm, " DocType='IP' and isnull(isamendment,0)=0 and AdvanceVouchType ='T'") & " group by sply_ty, POSTaxAreaTinCode, rt")


        Dim strval As String = "sum(txval)+sum(case when rchrg='y' then 0 else isnull(iamt,0)+isnull(camt,0)+isnull(samt,0)+isnull(csamt,0) end) as val"

        dic.Add("hsn", " select hsn_sc, uqc, hsndescription as [desc],sum(QTY) as QTY , " & strval & ", sum(txval) As txval, sum(iamt) As iamt, 
sum(camt) As camt, sum(samt) As samt, sum(csamt) as csamt from  gstListinvoiceitem() " & myUtils.CombineWhere(True, strFilterSumm, " DocType='IP' and CDNInvoiceID is  null and isnull(isamendment,0)=0 ") & " group by hsn_sc,  uqc ,hsndescription")
        Return dic

    End Function
    Public Overrides Function PrepareGSTRPayloadSQLDel(GstRegID As Integer, ReturnPeriodID As Integer) As clsCollecString(Of String)
        Dim strf1 As String = "actionflag in ('D')"
        Dim strf2 As String = "GstRegID=" & GstRegID
        Dim strf3 As String = "ReturnPeriodID=" & ReturnPeriodID
        Dim strf4 As String = "lasttransactionid is null"
        Dim strf5 As String = "Canceldate is not null"

        Dim strFilter As String = myUtils.CombineWhere(False, strf1, strf2, strf3, strf4)
        Dim strFilter2 As String = myUtils.CombineWhere(False, strf2, strf3, strf5)
        Dim dic As New clsCollecString(Of String)
        dic.Add("b2b", "select ctin, 'D' as flag, invoicenum as inum, invoicedate as idt from gstnaction " & myUtils.CombineWhere(True, strFilter, "gstInvoiceType='B2B'", "DocType='IP'") &
                 " union all select ctin, 'D' as flag, invoicenum as inum, invoicedate as idt from gstlistinvoice()  " & myUtils.CombineWhere(True, strFilter2, "gstInvoiceType='B2B'", "DocType='IP'"))
        dic.Add("b2bur", "select ctin, 'D' as flag, invoicenum as inum,  invoicedate as idt from gstnaction " & myUtils.CombineWhere(True, strFilter, "gstInvoiceType='B2BUR'", "DocType='IP'") &
                " union all select ctin, 'D' as flag, invoicenum as inum,  invoicedate as idt from gstlistinvoice()  " & myUtils.CombineWhere(True, strFilter2, "gstInvoiceType='B2BUR'", "DocType='IP'"))
        dic.Add("impg", "select ctin, 'D' as flag, boe_num, boe_dt from gstnaction " & myUtils.CombineWhere(True, strFilter, "gstInvoiceType='IMPG'", "DocType='IP'") &
                " union all select ctin, 'D' as flag, boe_num, boe_dt from gstlistinvoice()  " & myUtils.CombineWhere(True, strFilter2, "gstInvoiceType='IMPG'", "DocType='IP'"))
        dic.Add("imps", "select ctin, 'D' as flag, invoicenum as inum, invoicedate as idt from gstnaction " & myUtils.CombineWhere(True, strFilter, "gstInvoiceType='IMPS'", "DocType='IP'") &
                " union all select ctin,'D' as flag, invoicenum as inum, invoicedate as idt from gstlistinvoice()  " & myUtils.CombineWhere(True, strFilter2, "gstInvoiceType='IMPS'", "DocType='IP'"))
        dic.Add("cdn", "select ctin, 'D' as flag, invoicenum as nt_num, invoicedate as nt_dt from gstnaction " & myUtils.CombineWhere(True, strFilter, "gstInvoiceType='CDN'", "DocType='IP'") &
                " union all select ctin,'D' as flag, invoicenum as nt_num, invoicedate as nt_dt from gstlistinvoice() " & myUtils.CombineWhere(True, strFilter2, "gstInvoiceType='CDN'", "DocType='IP'"))
        dic.Add("cdnur", "select ctin, 'D' as flag, invoicenum as nt_num, invoicedate as nt_dt from gstnaction " & myUtils.CombineWhere(True, strFilter, "gstInvoiceType='CDNUR'", "DocType='IP'") &
                " union all select ctin, 'D' as flag, invoicenum as nt_num, invoicedate as nt_dt from gstlistinvoice()  " & myUtils.CombineWhere(True, strFilter2, "gstInvoiceType='CDNUR'", "DocType='IP'"))
        Return dic

    End Function

    Public Overrides Function PrepareGSTRAPayloadSQL(GstRegID As Integer, ReturnPeriodID As Integer) As clsCollecString(Of String)
        Dim strf2 As String = "GstRegID=" & GstRegID
        Dim strf3 As String = "ReturnPeriodID=" & ReturnPeriodID

        Dim strFilter As String = myUtils.CombineWhere(False, strf2, strf3)
        Dim dic As New clsCollecString(Of String)
        dic.Add("b2b", "select CPInvoiceID, ctin, CFS, POS, UpdBy, Flag, CFlag, OPD, ChkSum, inum, idt, val, rchrg, inv_typ,  sum(txval) as txval, rt, sum(iamt) as iamt, sum(camt) as camt, sum(samt) as samt, sum(csamt) as csamt from 
                            accListCPInvoiceItem() " & myUtils.CombineWhere(True, strFilter, "gstInvoiceType='B2B' and DocType='IP' and isnull(isamendment,0)=0") & " group By CPInvoiceID,ctin, chksum, CFS, POS, UpdBy, Flag, CFlag, OPD, inum, idt, val,  rchrg, inv_typ, rt")
        dic.Add("b2ba", "select CPInvoiceID, ctin, CFS, POS, UpdBy, Flag, CFlag, OPD, ChkSum, diff_percent, oinum, oidt, inum, idt, val, rchrg, inv_typ,  sum(txval) as txval, rt, sum(iamt) as iamt, sum(camt) as camt, sum(samt) as samt, sum(csamt) as csamt from 
                            accListCPInvoiceItem() " & myUtils.CombineWhere(True, strFilter, "gstInvoiceType='B2BA' and DocType='IP' and isnull(isamendment,0)=0") & " group By CPInvoiceID,ctin, chksum, CFS, POS, UpdBy, Flag, CFlag, OPD, diff_percent, oinum, oidt, inum, idt, val,  rchrg, inv_typ, rt")
        dic.Add("cdn", "select CPInvoiceID,ctin, CFS, UpdBy, Flag, CFlag, OPD, ChkSum, ntty, nt_num, nt_dt, val, inum, idt,  sum(txval) as txval, rt, sum(iamt) as iamt, sum(camt) as camt, sum(samt) as samt, sum(csamt) as csamt from 
                            accListCPInvoiceItem() " & myUtils.CombineWhere(True, strFilter, "gstInvoiceType='CDN' and DocType='IP' and isnull(isamendment,0)=0") & " group By CPInvoiceID, ctin, chksum, CFS,  UpdBy, Flag, CFlag, OPD, ntty, nt_num, nt_dt, inum, idt, rt, val")
        dic.Add("cdna", "select CPInvoiceID,ctin, CFS, UpdBy, Flag, CFlag, OPD, ChkSum, ntty, ont_num, ont_dt, p_gst, diff_percent, nt_num, nt_dt, val, inum, idt,  sum(txval) as txval, rt, sum(iamt) as iamt, sum(camt) as camt, sum(samt) as samt, sum(csamt) as csamt from 
                            accListCPInvoiceItem() " & myUtils.CombineWhere(True, strFilter, "gstInvoiceType='CDNA' and DocType='IP' and isnull(isamendment,0)=0") & " group By CPInvoiceID, ctin, chksum, CFS,  UpdBy, Flag, CFlag, OPD, ntty, nt_num, nt_dt, ont_num, ont_dt, p_gst, diff_percent, inum, idt, rt, val")
        dic.Add("isd", "select CPInvoiceID, ctin, CFS, UpdBy, Flag, CFlag, ChkSum, 'ISD' as isd_docty, inum as docnum, idt as docdt, inum, idt, elg as itc_elg, sum(iamt) as iamt, sum(camt) as camt, sum(samt) as samt, sum(csamt) as cess from 
                        accListCPInvoiceItem() " & myUtils.CombineWhere(True, strFilter, "gstInvoiceType='ISD' and DocType='IP' and isnull(isamendment,0)=0") & " group By CPInvoiceID,ctin, chksum, CFS, UpdBy, Flag, CFlag, inum, idt, elg")
        Return dic

    End Function
    Public Overrides Async Function UpdateDownloadedDataCP(GstRegID As Integer, ReturnPeriodID As Integer, results As List(Of GSTR2Total)) As Task(Of clsProcOutput(Of GstImportInfoGSTIN))
        'CP FIlter = uploaded by seller or vendor
        'TPFilter = 'uploaded by reciever or us
        Dim importer As New Import_GSTR2ATaskProvider(myContext.Controller)
        importer.ImportFileID = myContext.Controller.TaskInfo.ApiTaskID
        Dim ds = Me.PopulateDataCP(results)
        Dim oRet = Await importer.UpdateDownloadedInvoiceCP(GstRegID, ReturnPeriodID, ds)
        Return oRet

    End Function


    Public Sub CheckTaxCredit(rGstReg As DataRow, rInv As DataRow, dtInvItem As DataTable)
        If (Not myUtils.cBoolTN(rGstReg("partialcredit"))) Then
            For Each rInvItem As DataRow In dtInvItem.Select("isnull(invoiceid,0) in (0," & myUtils.cValTN(rInv("invoiceid")) & ")")
                If myUtils.IsInList(myUtils.cStrTN(rInvItem("elg")), "NO") Then
                    rInvItem("tx_i") = 0
                    rInvItem("tx_s") = 0
                    rInvItem("tx_c") = 0
                    rInvItem("tx_cs") = 0
                Else
                    rInvItem("tx_i") = rInvItem("iamt")
                    rInvItem("tx_s") = rInvItem("samt")
                    rInvItem("tx_c") = rInvItem("camt")
                    rInvItem("tx_cs") = rInvItem("csamt")
                End If
            Next
        End If
    End Sub

    Public Overrides Async Function PrepareSummaryViews(CampusFilter As String, PeriodFilter As String, SummaryType As String) As Task(Of List(Of clsDummyView))
        Dim lst As New List(Of clsDummyView)
        Dim strFilter As String = myUtils.CombineWhere(False, CampusFilter, PeriodFilter)

        If myUtils.IsInList(SummaryType, "cp") Then
            lst.Add(Await PrepareView("listCPInvoiceSummIP", strFilter))
            lst.Add(Await PrepareView("listCPB2BIP", strFilter))
            lst.Add(Await PrepareView("listCPB2BAIP", strFilter))
            lst.Add(Await PrepareView("listCPCDNIP", strFilter))
            lst.Add(Await PrepareView("listCPCDNAIP", strFilter))
        Else
            lst.Add(Await PrepareView("listGSTR2ReturnSumm", strFilter))
            lst.Add(Await PrepareView("listGSTR2T34A", strFilter))
            lst.Add(Await PrepareView("listGSTR2T4C", strFilter))
            lst.Add(Await PrepareView("listGSTR2T4B", strFilter))
            lst.Add(Await PrepareView("listGSTR2T5", strFilter))
            lst.Add(Await PrepareView("listGSTR2B2BCDNT6", strFilter))
            lst.Add(Await PrepareView("listGSTR2IMPSCDNURT6", strFilter))
            lst.Add(Await PrepareView("listGSTR2B2BURCDNT6", strFilter))
            lst.Add(Await PrepareView("listGSTR2T7", strFilter))
            lst.Add(Await PrepareView("listGSTR2T10A", strFilter))
            lst.Add(Await PrepareView("listGSTR2T10B", strFilter))
            lst.Add(Await PrepareView("listGSTR2T13", strFilter))
        End If




        Return lst

    End Function
    Public Overrides Async Function PrepareReconcileViews(strFilter As String) As Task(Of List(Of clsDummyView))
        Dim lst As New List(Of clsDummyView)
        lst.Add(Await PrepareView("ListReconciliationSumm", strFilter, "Summary"))
        lst.Add(Await PrepareView("ListReconciliationVendSum", strFilter, "Vendor Wise Summary"))


        Dim vw = Await PrepareView("listpurchinvoicematch", strFilter, "Invoice Level Details")
        Dim dt1 = vw.mainGrid.myDS.Tables(0)


        If dt1.Rows.Count > 600000 Then
            Dim cnt As Integer = 0
            Dim PortionList As New List(Of clsPortionInfo)
            Dim rr1 = dt1.Select("", "CTIN")
            Dim oProc As New clsTableRowProc(Of clsPortionInfo, Boolean)(myContext)
            Dim Numthreads As Integer = If(Environment.ProcessorCount <= 2, 1, Environment.ProcessorCount)
            Dim lst2 = Await oProc.ExecutePortion(rr1, Function(CurrentRow, NextRow) As Boolean
                                                           Return myUtils.MatchRowCols(CurrentRow, NextRow, New String() {"ctin"})
                                                       End Function,
                    Function(provider As IAppDataProvider)
                        Dim info = New clsPortionInfo
                        info.ErrorTable = dt1.Clone
                        PortionList.Add(info)
                        Return info
                    End Function,
                   Function(objResult As clsProcOutput(Of Boolean), objPortion As clsPortionInfo, Rows As List(Of DataRow)) As Boolean
                       myUtils.CopyRows(Rows.ToArray, objPortion.ErrorTable)
                       Return True
                   End Function,
                    Numthreads, 500000)

            For Each objPortion In PortionList
                cnt = cnt + 1
                Dim vw2 = New clsDummyView(vw.myMode, vw.mvwContext)
                vw2.myCMain = vw.myCMain.Clone
                vw2.myCMain.strView = vw2.myCMain.strView & cnt
                vw2.mainGrid.BindView(myUtils.MakeDSfromTable(objPortion.ErrorTable, False))
                vw2.mainGrid.Genwidth(True)
                lst.Add(vw2)
            Next
        Else
            lst.Add(vw)
        End If


        Return lst

    End Function

End Class
