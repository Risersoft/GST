Imports risersoft.shared
Imports risersoft.API.GSTN.GSTR1
Imports risersoft.API.GSTN
Imports Newtonsoft.Json
Imports System.Text
Imports System.Web
Imports GSTN.API.Library.Models.GstNirvana

Public Class clsGSTNReturnGSTR1
    Inherits clsGSTNTypedReturnBase(Of GSTR1Total, SummaryOutward, GSTR1ApiClient, GSTR1AApiClient)
    Public Overrides Property CDNRApiAction As String = "CDNR"
    Public Sub New(context As IProviderContext)
        MyBase.New(context, "IS", "GSTR1")
    End Sub

    Public Overrides Function PrepareGSTRPayloadSQLUp(GstRegID As Integer, ReturnPeriodID As Integer) As clsCollecString(Of String)
        Dim rPP As DataRow = oMaster.rPostPeriod(ReturnPeriodID)

        Dim strf1 As String = "actionflag in ('U','M')"
        Dim strf2 As String = "GstRegID=" & GstRegID
        Dim strf3 As String = "ReturnPeriodID=" & ReturnPeriodID
        Dim strf4 As String = fncInvoiceFilter.Invoke()

        'Action flag not required/admissible for upload/modify
        Dim strFilterInv As String = myUtils.CombineWhere(False, strf1, strf2, strf3, strf4)
        Dim strFilterSumm As String = myUtils.CombineWhere(False, strf2, strf3)
        Dim dic As New clsCollecString(Of String)


        dic.Add("b2b", "select invoiceid,ctin, chksum, inum, idt, val, POSTaxAreaTinCode as pos, rchrg, etin,inv_typ,diff_percent, sum(txval) as txval,rt, sum(iamt) as iamt, sum(camt) as camt, sum(samt) as samt, sum(csamt) as csamt from gstListinvoiceitem()
" & myUtils.CombineWhere(True, strFilterInv, "gstInvoiceType='B2B' and DocType='IS' and isnull(isamendment,0)=0 and isnull(istaxable,1)=1") & " group by invoiceid, ctin, inum, idt, val, POSTaxAreaTinCode, rchrg, etin, inv_typ, rt, chksum,diff_percent")


        dic.Add("b2ba", "select invoiceid, ctin, chksum, oinum, oidt, inum,idt,val,POSTaxAreaTinCode as pos, rchrg, etin, inv_typ, diff_percent,rt,sum(txval) as txval,sum(iamt) as iamt,sum(camt) as camt, sum(samt) as samt, sum(csamt) as csamt from gstlistinvoiceitem()
" & myUtils.CombineWhere(True, strFilterInv, "gstInvoiceType='B2B' and DocType='IS' and isnull(isamendment,0)=1 and isnull(istaxable,1)=1") & " group by invoiceid, ctin,
oinum,oidt,inum, idt, val, POSTaxAreaTinCode, rchrg, diff_percent,etin, inv_typ, rt, chksum,invoiceid")


        dic.Add("b2cl", "select invoiceid,POSTaxAreaTinCode as pos, inum, idt, val, etin, diff_percent, sum(txval) as txval, rt, sum(iamt) as iamt, 
sum(csamt) as csamt from gstListinvoiceitem()" & myUtils.CombineWhere(True, strFilterInv, "gstInvoiceType='B2C' and b2c_typ='L' and DocType='IS' and isnull(isamendment,0)=0 and isnull(istaxable,1)=1") & " group by POSTaxAreaTinCode, diff_percent,inum, idt, val, etin, rt,invoiceid")


        dic.Add("b2cla", "select invoiceid,POSTaxAreaTinCode as pos,oinum,oidt, inum, idt, val, etin,diff_percent,  sum(txval) as txval, rt, sum(iamt) as iamt, sum(csamt) as csamt from gstListinvoiceitem()" & myUtils.CombineWhere(True, strFilterInv, "gstInvoiceType='B2C' and b2c_typ='L' and DocType='IS' and isnull(isamendment,0)=1 and isnull(istaxable,1)=1") & " group by POSTaxAreaTinCode,oinum,oidt,diff_percent, inum, idt, val, etin, rt ,invoiceid")


        dic.Add("b2cs", " select sply_ty, ecom_typ as typ, etin, POSTaxAreaTinCode as pos,diff_percent, rt, sum(txval) as txval, sum(camt) as camt, 
sum(samt) as samt, sum(iamt) as iamt, sum(csamt) as csamt from gstListinvoiceitem()" & myUtils.CombineWhere(True, strFilterSumm, "gstInvoiceType='B2C' and b2c_typ='S' and DocType='IS' and isnull(isamendment,0)=0 and isnull(istaxable,1)=1") & " group by sply_ty, txval, ecom_typ, etin, POSTaxAreaTinCode, rt,diff_percent")


        dic.Add("nil", " select Zero_typ as sply_ty, sum(Nil_Amt) as Nil_amt,sum(Expt_amt) as Expt_amt,Sum(ngsup_amt) as ngsup_amt 
from gstListinvoiceitem()" & myUtils.CombineWhere(True, strFilterSumm, "isnull(isamendment,0)=0 and DocType='IS' and isnull(istaxable,1)=0") & " group by Zero_typ")


        dic.Add("exp", "select invoiceid,exp_typ, inum, idt, val, sbpcode,sbnum, sbdt, diff_percent,sum(txval) as txval, rt, sum(iamt) as iamt, 
sum(csamt) as csamt from gstListinvoiceitem() " & myUtils.CombineWhere(True, strFilterInv, "gstInvoiceType='EXP' and DocType='IS' and isnull(isamendment,0)=0 and isnull(istaxable,1)=1") & " group by exp_typ, inum, idt, val,sbpcode,sbnum, sbdt, rt,diff_percent,invoiceid ")


        dic.Add("expa", " select invoiceid,exp_typ, oinum,oidt,inum, idt, val, sbpcode,sbnum, sbdt, diff_percent,sum(txval) as txval, rt, 
sum(iamt) as iamt, sum(csamt) as csamt from gstListinvoiceitem() " & myUtils.CombineWhere(True, strFilterInv, "gstInvoiceType='EXP' and DocType='IS' and isnull(isamendment,0)=1 and isnull(istaxable,1)=1") & " group by exp_typ, oinum,oidt,sbpcode,diff_percent,inum, idt, val, sbnum, sbdt, rt,invoiceid ")



        dic.Add("cdnr", " select invoiceid,ctin, chksum, ntty, nt_num, nt_dt,p_gst, inum, idt, val,diff_percent, sum(txval) as txval, rt,
sum(iamt) as iamt, sum(camt) as camt, sum(samt) as samt, sum(csamt) as csamt from gstListinvoiceitem()" & myUtils.CombineWhere(True, strFilterInv, "gstInvoiceType='CDN' and DocType='IS' and isnull(isamendment,0)=0 and isnull(istaxable,1)=1") & " group By ctin, ntty, nt_num, nt_dt, inum, idt, val, rt, chksum, diff_percent,p_gst,invoiceid ")



        dic.Add("cdnra", " select invoiceid,ctin, chksum, ont_num,ont_dt,ntty, nt_num, nt_dt, inum, idt, p_gst,val,diff_percent,rt, 
sum(txval) as txval,sum(iamt) as iamt, sum(camt) as camt, sum(samt) as samt, sum(csamt) as csamt 
from gstListinvoiceitem() " & myUtils.CombineWhere(True, strFilterInv, “gstInvoiceType='CDN' and DocType='IS' and isnull(isamendment,0)=1 and isnull(istaxable,1)=1") & " group By ctin, ont_num,ont_dt, ntty,diff_percent, nt_num, nt_dt, inum, idt,p_gst, val, rt, chksum,invoiceid ")




        dic.Add("cdnur", " select invoiceid, cdnur_typ as typ, ntty, nt_num, nt_dt,p_gst, inum, idt, val,diff_percent, sum(txval) as txval, rt, 
sum(iamt) as iamt, sum(csamt) as csamt from gstlistinvoiceitem() " & myUtils.CombineWhere(True, strFilterInv, “gstInvoiceType='CDNUR' and DocType='IS'  and 
isnull(isamendment,0)=0 and isnull(istaxable,1)=1") & " group By cdnur_typ,p_gst, ntty, nt_num, nt_dt, inum, idt, val, rt, Diff_Percent,invoiceid")



        dic.Add("cdnura", " select invoiceid,cdnur_typ as typ, ont_num,ont_dt,ntty, nt_num, nt_dt,p_gst, inum, idt, val,diff_percent,
sum(txval) as txval, rt,sum(iamt) as iamt, sum(csamt) as csamt 
from gstlistinvoiceitem() " & myUtils.CombineWhere(True, strFilterInv, “gstInvoiceType='CDNUR' and DocType='IS'and isnull(isamendment,0)=1 and isnull(istaxable,1)=1") & " group By cdnur_typ, ont_num,ont_dt,diff_percent,p_gst,ntty, nt_num, nt_dt, inum, idt, val, rt,invoiceid")



        dic.Add("at", " select POSTaxAreaTinCode as pos, sply_ty,diff_percent, rt, sum(factor*ad_amt) as ad_amt, sum(factor*iamt) as iamt, sum(factor*camt) as camt, 
sum(factor*samt) as samt, sum(factor*csamt) as csamt from GstListAdvanceVouchItem() " & myUtils.CombineWhere(True, strFilterSumm, “DocType='PC' and  isnull(isamendment,0)=0 and AdvanceVouchType in ('A','R','C') ") & " group by POSTaxAreaTinCode, sply_ty, rt,diff_percent ")


        dic.Add("txpd", " select sply_ty, POSTaxAreaTinCode as pos, rt,diff_percent, sum(ad_amt) as ad_amt, sum(iamt) as iamt,sum(camt) as camt,
sum(samt) as samt,sum(csamt) as csamt from GstListAdvanceVouchItem() " & myUtils.CombineWhere(True, strFilterSumm, “DocType='PC' and  isnull(isamendment,0)=0 and AdvanceVouchType ='T'") & " group by sply_ty, diff_percent,POSTaxAreaTinCode, rt")




        dic.Add("b2csa", " select omon,POSTaxAreaTinCode as pos,sply_ty, ecom_typ as typ, etin,diff_percent,rt, sum(txval) as txval, 
sum(camt) as camt, sum(samt) as samt, sum(iamt) as iamt, sum(csamt) as csamt from gstListinvoiceitem()" & myUtils.CombineWhere(True, " gstInvoiceType='B2C' and b2c_typ='S' and DocType='IS' and isnull(istaxable,1)=1 ",
Me.AmendmentFilterInv(rPP, GstRegID)) & " group by omon,diff_percent,sply_ty, txval, ecom_typ, etin, POSTaxAreaTinCode, rt  ")



        dic.Add("ata", " select omon,POSTaxAreaTinCode as pos, sply_ty, diff_percent,rt, sum(factor*ad_amt) as ad_amt, sum(factor*iamt) as iamt, 
sum(factor*camt) as camt, sum(factor*samt) as samt, sum(factor*csamt) as csamt from GstListAdvanceVouchItem()   " & myUtils.CombineWhere(True, “DocType='PC'  and AdvanceVouchType in ('A','R','C')",
Me.AmendmentFilterPY(ReturnPeriodID, GstRegID)) & " group by omon,POSTaxAreaTinCode, sply_ty,diff_percent, rt")




        dic.Add("txpda", " select omon,sply_ty, POSTaxAreaTinCode as pos, rt,diff_percent, sum(Ad_amt) as ad_amt, sum(iamt) as iamt,sum(camt) as camt,
sum(samt) as samt,sum(csamt) as csamt from GstListAdvanceVouchItem()  " & myUtils.CombineWhere(True, “DocType='PC'", "AdvanceVouchType ='T'",
Me.AmendmentFilterPY(ReturnPeriodID, GstRegID)) & " group by sply_ty, diff_percent,POSTaxAreaTinCode, rt,omon")




        Dim strval As String = "sum(txval)+sum(case when rchrg='y' then 0 else isnull(iamt,0)+isnull(camt,0)+isnull(samt,0)+isnull(csamt,0) end) as val"


        dic.Add("hsn", " select  hsn_sc, hsndescription as [desc], uqc, sum(QTY) as QTY ," & strval & ", sum(txval) as txval, sum(iamt) as iamt,
sum(camt) as camt,sum(samt) as samt, sum(csamt) as csamt from gstListinvoiceitem() " & myUtils.CombineWhere(True, strFilterSumm, “DocType='IS' and CDNInvoiceID is  null and  isnull(isamendment,0)=0") & " group by hsn_sc, hsndescription, uqc ")

        dic.Add("doc_issue", "select doc_num,numfrom as [from],numto as [to],totcount as totnum,CancelledCount as cancel,IssuedCount as net_issue
             from gstlistdocnumseries() " & myUtils.CombineWhere(True, strFilterSumm))

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
        dic.Add("b2b", "select gstnactionid,ctin, 'D' as flag, invoicenum as inum, invoicedate as  idt from gstnaction " & myUtils.CombineWhere(True, strFilter, "gstInvoiceType='B2B'", "DocType='IS'") &
                " union all select invoiceid,ctin, 'D' as flag, invoicenum as inum, invoicedate as  idt from gstlistinvoice() " & myUtils.CombineWhere(True, strFilter2, "gstInvoiceType='B2B'", "DocType='IS'"))
        dic.Add("b2cl", "select gstnactionid,ctin, 'D' as flag,  invoicenum as inum, invoicedate as  idt from gstnaction " & myUtils.CombineWhere(True, strFilter, "gstInvoiceType='B2C'", "DocType='IS'") &
                " union all select invoiceid,ctin, 'D' as flag,  invoicenum as inum, invoicedate as  idt from gstlistinvoice() " & myUtils.CombineWhere(True, strFilter2, "gstInvoiceType='B2C'", "DocType='IS'"))
        dic.Add("exp", "select gstnactionid,ctin, 'D' as flag,  invoicenum as inum, invoicedate as  idt from gstnaction " & myUtils.CombineWhere(True, strFilter, "gstInvoiceType='EXP'", "DocType='IS'") &
                " union all select invoiceid,ctin, 'D' as flag,  invoicenum as inum, invoicedate as  idt from gstlistinvoice() " & myUtils.CombineWhere(True, strFilter2, "gstInvoiceType='EXP'", "DocType='IS'"))
        dic.Add("cdn", "select gstnactionid,ctin, 'D' as flag, invoicenum as nt_num, invoicedate as nt_dt from gstnaction " & myUtils.CombineWhere(True, strFilter, "gstInvoiceType='CDN'", "DocType='IS'") &
                " union all select invoiceid,ctin, 'D' as flag, invoicenum as nt_num, invoicedate as nt_dt from gstlistinvoice() " & myUtils.CombineWhere(True, strFilter2, "gstInvoiceType='CDN'", "DocType='IS'"))
        dic.Add("cdnur", "select gstnactionid,ctin, 'D' as flag, invoicenum as nt_num, invoicedate as nt_dt from gstnaction " & myUtils.CombineWhere(True, strFilter, "gstInvoiceType='CDNUR'", "DocType='IS'") &
                " union all select invoiceid,ctin, 'D' as flag, invoicenum as nt_num, invoicedate as nt_dt from gstlistinvoice() " & myUtils.CombineWhere(True, strFilter2, "gstInvoiceType='CDNUR'", "DocType='IS'"))

        Return dic

    End Function
    Public Overrides Function PrepareGSTRAPayloadSQL(GstRegID As Integer, ReturnPeriodID As Integer) As clsCollecString(Of String)
        Dim strf2 As String = "GstRegID=" & GstRegID
        Dim strf3 As String = "ReturnPeriodID=" & ReturnPeriodID

        Dim strFilter As String = myUtils.CombineWhere(False, strf2, strf3)
        Dim dic As New clsCollecString(Of String)
        dic.Add("b2b", "select CPInvoiceID, ctin, CFS, POS, UpdBy, Flag, CFlag, OPD, ChkSum, inum, idt, val, rchrg, inv_typ,  sum(txval) as txval, rt, sum(iamt) as iamt, sum(camt) as camt, sum(samt) as samt, sum(csamt) as csamt from 
                            accListCPInvoice() " & myUtils.CombineWhere(True, strFilter, "gstInvoiceType='B2B' and DocType='IS' and isnull(isamendment,0)=0") & " group By CPInvoiceID,ctin, chksum, CFS, POS, UpdBy, Flag, CFlag, OPD, inum, idt, val,  rchrg, inv_typ, rt")
        dic.Add("cdn", "select CPInvoiceID,ctin, CFS, UpdBy, Flag, CFlag, OPD, ChkSum, ntty, nt_num, nt_dt, inum, idt,  sum(txval) as txval, rt, sum(iamt) as iamt, sum(camt) as camt, sum(samt) as samt, sum(csamt) as csamt from 
                            accListCPInvoice() " & myUtils.CombineWhere(True, strFilter, "gstInvoiceType='CDN' and DocType='IS' and isnull(isamendment,0)=0") & " group By CPInvoiceID, ctin, chksum, CFS,  UpdBy, Flag, CFlag, OPD, ntty, nt_num, nt_dt, inum, idt, rt")

        Return dic

    End Function
    Public Overrides Async Function UpdateDownloadedDataCP(GstRegID As Integer, ReturnPeriodID As Integer, results As List(Of GSTR1Total)) As Task(Of clsProcOutput(Of GstImportInfoGSTIN))
        'CP FIlter = uploaded by seller or vendor
        'TPFilter = 'uploaded by reciever or us
        Dim ds = Me.PopulateDataCP(results)
        Dim importer As New Import_GSTR2ATaskProvider(myContext.Controller)
        importer.ImportFileID = myContext.Controller.TaskInfo.ApiTaskID
        Dim oRet = Await importer.UpdateDownloadedInvoiceCP(GstRegID, ReturnPeriodID, ds, "updby='R'", "updby='S'")
        Return oRet


    End Function
    Public Overrides Async Function UpdateDownloadedDataTP(GstRegID As Integer, ReturnPeriodID As Integer, results As List(Of GSTR1Total)) As Task(Of clsProcOutput(Of GstImportInfoGSTIN))
        Dim rGstReg As DataRow = oMaster.GstRegRow(GstRegID)
        Dim rPP As DataRow = oMaster.rPostPeriod(ReturnPeriodID)
        Dim result As Boolean = myFuncs2.IsFinalPP(myContext, GstRegID, rPP, "GSTR1")
        Dim oRet As New clsProcOutput(Of GstImportInfoGSTIN)
        If result Then
            oRet.AddError("Period finalized")
        Else
            Dim importer As New Import_GSTR1TaskProvider(myContext.Controller)
            importer.ImportFileID = myContext.Controller.TaskInfo.ApiTaskID
            Dim ds = Me.PopulateDataTP(results)
            oRet = Await importer.UpdateDownloadedInvoiceTP(rGstReg, rPP, ds, "")
        End If
        Return oRet
    End Function

    Public Overrides Async Function PrepareSummaryViews(CampusFilter As String, PeriodFilter As String, SummaryType As String) As Task(Of List(Of clsDummyView))
        Dim lst As New List(Of clsDummyView)
        Dim strFilter As String = myUtils.CombineWhere(False, CampusFilter, PeriodFilter)
        If myUtils.IsInList(SummaryType, "defer") Then
            lst.Add(Await PrepareView("listInvoiceDefer", strFilter, "Invoice Deferment"))
        Else
            lst.Add(Await PrepareView("listGSTR1ReturnSumm", strFilter, "GSTR - 1 Return Summary"))
            lst.Add(Await PrepareView("listGSTR1T4", strFilter, "T4 -> B2B Invoices"))
            lst.Add(Await PrepareView("listGSTR1T5", strFilter, "T5 -> B2CL Invoices"))
            lst.Add(Await PrepareView("listGSTR1T6", strFilter, "T6 -> Zero Rate Invoices"))
            lst.Add(Await PrepareView("listGSTR1B2CST7", strFilter, "T7 -> B2CS"))
            lst.Add(Await PrepareView("listGSTR1B2CSCDNURT7", strFilter, "T7 -> B2CS CDN"))
            lst.Add(Await PrepareView("listGSTR1T8", strFilter, "T8 -> Nil, Exempt, Non GST"))
            lst.Add(Await PrepareView("listGSTR1B2BAT9", strFilter, "T9 -> B2B Amendment"))
            lst.Add(Await PrepareView("listGSTR1B2BCDNT9", strFilter, "T9 -> B2B CDN"))
            lst.Add(Await PrepareView("listGSTR1B2BCDNAT9", strFilter, "T9 -> B2B CDN Amendment"))
            lst.Add(Await PrepareView("listGSTR1B2CLAT9", strFilter, "T9 -> B2CL Amendment"))
            lst.Add(Await PrepareView("listGSTR1B2CLCDNT9", strFilter, "T9 -> B2CL CDN"))
            lst.Add(Await PrepareView("listGSTR1B2CLCDNAT9", strFilter, "T9 -> B2CL CDNA"))
            lst.Add(Await PrepareView("listGSTR1EXPAT9", strFilter, "T9 -> Zero Rate Amendment"))
            lst.Add(Await PrepareView("listGSTR1EXPCDNT9", strFilter, "T9 -> Zero Rate CDN"))
            lst.Add(Await PrepareView("listGSTR1EXPCDNAT9", strFilter, "T9 -> Zero Rate CDNA"))
            lst.Add(Await PrepareView("listGSTR1B2CSAT10", strFilter, "T10 -> B2CS Amendment"))
            lst.Add(Await PrepareView("listGSTR1B2CSCDNAT10", strFilter, "T10 -> B2CS CDN"))
            lst.Add(Await PrepareView("listGSTR1T11A", strFilter, "T11A -> Adv Rcpt"))
            lst.Add(Await PrepareView("listGSTR1T11C", strFilter, "T11C -> Adv Rcpt Amend"))
            lst.Add(Await PrepareView("listGSTR1T11B", strFilter, "T11B -> Adv Adjst"))
            lst.Add(Await PrepareView("listGSTR1T11D", strFilter, "T11D -> Adv Adjst Amend"))
            lst.Add(Await PrepareView("listGSTR1T12", strFilter, "T12 -> HSN Summary"))
            lst.Add(Await PrepareView("ListGstDocNumSeries", strFilter, "T13 - Documents Issued During Tax Period"))
        End If


        Return lst

    End Function
    Public Overrides Async Function PrepareReconcileViews(strFilter As String) As Task(Of List(Of clsDummyView))
        Dim lst As New List(Of clsDummyView)

        lst.Add(Await PrepareView("ListReconciliationSumm", strFilter, "Summary"))
        lst.Add(Await PrepareView("ListReconciliationVendSum", strFilter, "Vendor Wise Summary"))
        lst.Add(Await PrepareView("listpurchinvoicematch", strFilter, "Invoice Level Details"))

        Return lst

    End Function

    Public Overrides Sub PrepareCleanTPModel(lst As List(Of GSTR1Total))

        For Each objModel In lst
            If objModel.b2b IsNot Nothing Then objModel.b2b.ForEach(Sub(x)
                                                                        x.cfs = Nothing
                                                                        x.inv.ForEach(Sub(y)
                                                                                          y.cflag = Nothing
                                                                                          y.flag = "D"
                                                                                          y.itms = Nothing
                                                                                          y.chksum = Nothing
                                                                                          y.updby = Nothing
                                                                                      End Sub)
                                                                    End Sub)
            If objModel.b2ba IsNot Nothing Then objModel.b2ba.ForEach(Sub(x)
                                                                          x.cfs = Nothing
                                                                          x.inv.ForEach(Sub(y)
                                                                                            y.flag = "D"
                                                                                            y.cflag = Nothing
                                                                                            y.itms = Nothing
                                                                                            y.chksum = Nothing
                                                                                            y.updby = Nothing
                                                                                        End Sub)
                                                                      End Sub)

            If objModel.b2cl IsNot Nothing Then objModel.b2cl.ForEach(Sub(x) x.inv.ForEach(Sub(y)
                                                                                               y.flag = "D"
                                                                                               y.itms = Nothing
                                                                                               y.chksum = Nothing
                                                                                           End Sub))
            If objModel.b2cla IsNot Nothing Then objModel.b2cla.ForEach(Sub(x) x.inv.ForEach(Sub(y)
                                                                                                 y.flag = "D"
                                                                                                 y.itms = Nothing
                                                                                                 y.chksum = Nothing
                                                                                             End Sub))

            If objModel.b2cs IsNot Nothing Then objModel.b2cs.ForEach(Sub(x)
                                                                          x.chksum = Nothing
                                                                          x.flag = "D"
                                                                      End Sub)
            If objModel.b2csa IsNot Nothing Then objModel.b2csa.ForEach(Sub(x)
                                                                            x.chksum = Nothing
                                                                            x.flag = "D"
                                                                        End Sub)
            If objModel.nil IsNot Nothing Then
                objModel.nil.flag = "D"
                objModel.nil.chksum = Nothing
            End If
            If objModel.hsn IsNot Nothing Then
                objModel.hsn.flag = "D"
                objModel.hsn.chksum = Nothing
            End If


            If objModel.exp IsNot Nothing Then objModel.exp.ForEach(Sub(x) x.inv.ForEach(Sub(y)
                                                                                             y.chksum = Nothing
                                                                                             y.flag = "D"
                                                                                             y.itms = Nothing
                                                                                         End Sub))
            If objModel.expa IsNot Nothing Then objModel.expa.ForEach(Sub(x) x.inv.ForEach(Sub(y)
                                                                                               y.chksum = Nothing
                                                                                               y.flag = "D"
                                                                                               y.itms = Nothing
                                                                                           End Sub))

            If objModel.cdnr IsNot Nothing Then objModel.cdnr.ForEach(Sub(x)
                                                                          x.cfs = Nothing
                                                                          x.nt.ForEach(Sub(y)
                                                                                           y.cflag = Nothing
                                                                                           y.flag = "D"
                                                                                           y.itms = Nothing
                                                                                           y.chksum = Nothing
                                                                                           y.updby = Nothing
                                                                                       End Sub)
                                                                      End Sub)
            If objModel.cdnra IsNot Nothing Then objModel.cdnra.ForEach(Sub(x)
                                                                            x.cfs = Nothing
                                                                            x.nt.ForEach(Sub(y)
                                                                                             y.cflag = Nothing
                                                                                             y.flag = "D"
                                                                                             y.chksum = Nothing
                                                                                             y.itms = Nothing
                                                                                             y.updby = Nothing
                                                                                         End Sub)
                                                                        End Sub)

            If objModel.cdnur IsNot Nothing Then objModel.cdnur.ForEach(Sub(x)
                                                                            x.flag = "D"
                                                                            x.itms = Nothing
                                                                            x.chksum = Nothing
                                                                        End Sub)
            If objModel.cdnura IsNot Nothing Then objModel.cdnura.ForEach(Sub(x)
                                                                              x.flag = "D"
                                                                              x.itms = Nothing
                                                                              x.chksum = Nothing
                                                                          End Sub)

            If objModel.at IsNot Nothing Then objModel.at.ForEach(Sub(x)
                                                                      x.flag = "D"
                                                                      x.chksum = Nothing
                                                                  End Sub)
            If objModel.ata IsNot Nothing Then objModel.ata.ForEach(Sub(x)
                                                                        x.flag = "D"
                                                                        x.chksum = Nothing
                                                                    End Sub)
            If objModel.txpd IsNot Nothing Then objModel.txpd.ForEach(Sub(x)
                                                                          x.flag = "D"
                                                                          x.chksum = Nothing
                                                                      End Sub)
            If objModel.txpda IsNot Nothing Then objModel.txpda.ForEach(Sub(x)
                                                                            x.flag = "D"
                                                                            x.chksum = Nothing
                                                                        End Sub)
            If objModel.doc_issue IsNot Nothing Then
                objModel.doc_issue.flag = "D"
            End If



        Next

    End Sub
    Public Overrides Function GetSummary(GstRegID As Integer, ReturnPeriodID As Integer) As clsProcOutput(Of SummaryOutward)
        Dim oRet = MyBase.GetSummary(GstRegID, ReturnPeriodID)
        Dim summ = oRet.Result
        If summ IsNot Nothing Then
            summ.sec_sum = summ.sec_sum.Where(Function(x) x.ttl_cess + x.ttl_cgst + x.ttl_igst + x.ttl_sgst > 0).ToList
        End If
        Return oRet
    End Function
End Class
