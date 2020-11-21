Imports risersoft.shared
Imports risersoft.API.GSTN.ANX01
Imports risersoft.API.GSTN
Imports Newtonsoft.Json
Imports System.Text
Imports System.Web
Imports GSTN.API.Library.Models.GstNirvana

Public Class clsGSTNReturnANX01
    Inherits clsGSTNTypedReturnBase(Of SaveAnx01, Anx01Summary, ANX01ApiClient, ANX01ApiClient)
    Public Overrides Property CDNRApiAction As String = "CDNR"
    Public Sub New(context As IProviderContext)
        MyBase.New(context, "IS", "ANX01")
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


        dic.Add("b2b", "select invoiceid,ctin,doctyp,diffprcnt,pos,num,dt,val,hsn,sum(isnull(Factor*TXVAL,0)) as txval,rt,sum(isnull(Factor*igst,0)) as igst,
        sum(isnull(Factor*sgst,0)) as sgst,sum(isnull(Factor*cgst,0)) as cgst,sum(isnull(Factor*cess,0)) as cess from gstListinvoiceitemANX()
        " & myUtils.CombineWhere(True, strFilterInv, "gstInvoiceType='B2B' and DocType='IS' and isnull(isamendment,0)=0 and isnull(istaxable,1)=1") & "group by invoiceid,ctin,doctyp,diffprcnt,pos,num,dt,val,hsn, rt")

        dic.Add("b2c", "select invoiceid,POS, num,dt,val, hsn, diffprcnt, sum(isnull(Factor*TXVAL,0)) as txval,rt,sum(isnull(Factor*igst,0)) as igst,
        sum(isnull(Factor*sgst,0)) as sgst,sum(isnull(Factor*cgst,0)) as cgst,sum(isnull(Factor*cess,0)) as cess from gstListinvoiceitemANX()" & myUtils.CombineWhere(True, strFilterInv, "gstInvoiceType='B2C' and DocType='IS' and isnull(isamendment,0)=0 and isnull(istaxable,1)=1") & " group by POS, diffprcnt,num, dt, val, etin,hsn, rt,invoiceid")

        dic.Add("de", "select invoiceid,ctin,doctyp,diffprcnt,pos,num,dt,val,hsn,rt,sum(isnull(TXVAL,0)) as txval,sum(isnull(igst,0)) as igst,sum(isnull(sgst,0)) as sgst,sum(isnull(cgst,0)) as cgst,sum(isnull(cess,0)) as cess from gstListinvoiceitemANX() " & myUtils.CombineWhere(True, strFilterInv, "DocType= 'IS' and gstinvoicetype = 'B2B' and gstinvoicesubtype = 'DE' and isnull(istaxable,1)=1") & " group by invoiceid,ctin,doctyp,diffprcnt,pos,num,dt,val,hsn,rt")

        dic.Add("ecom", "select etin,sum(isnull(sup,0)) as sup,sum(isnull(retsup,0)) as retsup,sum(isnull(nsup,0)) as nsup,
		sum(isnull(igst,0)) as igst,sum(isnull(sgst,0)) as sgst,sum(isnull(cgst,0)) as cgst,sum(isnull(cess,0)) as cess from gstListTCS() " & myUtils.CombineWhere(True, strFilterInv, "Etin  is not null") & " group by etin")

        dic.Add("expwop", "select invoiceid,doctyp,num,dt,val,sb_num,sb_pcode,sb_dt,hsn,rt,SUM(isnull(txval,0)) as txval from gstListinvoiceitemANX() 
        " & myUtils.CombineWhere(True, strFilterInv, "DocType= 'IS' and gstinvoicetype = 'EXP' and ExportDutyType = 'WOPAY' and isnull(istaxable,1)=1") & " group by invoiceid,doctyp,num,dt,val,sb_num,sb_pcode,sb_dt,hsn,rt")

        dic.Add("expwp", "select invoiceid,doctyp,num,dt,val,sb_num,sb_pcode,sb_dt,hsn,rt,SUM(isnull(txval,0)) as txval,sum(isnull(igst,0)) as igst,sum(isnull(cess,0)) as cess from gstListinvoiceitemANX() " & myUtils.CombineWhere(True, strFilterInv, "
        DocType='IS' and gstInvoiceType='EXP' and ExportDutyType = 'WPAY' and  isnull(isamendment,0)=0 and isnull(istaxable,1)=1") & " group by invoiceid,doctyp,num,dt,val,sb_num,sb_pcode,sb_dt,hsn,rt")

        dic.Add("impg", "select invoiceid,doctyp,pos,boe_num,pcode,boe_dt,boe_val,hsn,rt,sum(isnull(TXVAL,0)) as txval,sum(isnull(igst,0)) as igst,sum(isnull(cess,0)) as cess from gstListinvoiceitemANX() 
        " & myUtils.CombineWhere(True, strFilterInv, "DocType= 'IP' and gstinvoicetype = 'IMPG' and IS_SEZ not in ('Y') and isnull(istaxable,1)=1") & " group by invoiceid,doctyp,pos,boe_num,pcode,boe_dt,boe_val,hsn,rt")

        dic.Add("impgsez", "select invoiceid,ctin,doctyp,pos,boe_num,pcode,boe_dt,boe_val,HSN,rt,sum(isnull(TXVAL,0)) as txval,sum(isnull(igst,0)) as igst,sum(isnull(cess,0)) as cess from gstListinvoiceitemANX() 
        " & myUtils.CombineWhere(True, strFilterInv, "DocType= 'IP' and gstinvoicetype = 'IMPG' and IS_SEZ in ('Y')") & " group by invoiceid,doctyp,ctin,doctyp,pos,boe_num,pcode,boe_dt,boe_val,HSN,rt")

        dic.Add("imps", "select invoiceid,pos,HSN,rt,sum(isnull(TXVAL,0)) as txval,sum(isnull(igst,0)) as igst,sum(isnull(cess,0)) as cess from gstListinvoiceitemANX() 
        " & myUtils.CombineWhere(True, strFilterInv, "DocType= 'IP' and gstinvoicetype = 'IMPS'") & " group by invoiceid,pos,HSN,rt")

        dic.Add("rev", "select invoiceid,ctin,diffprcnt,pos,num,dt,val,HSN,rt,sum(isnull(Factor*TXVAL,0)) as txval,sum(isnull(Factor*igst,0)) as igst,sum(isnull(Factor*sgst,0)) as sgst,sum(isnull(Factor*cgst,0)) as cgst,sum(isnull(Factor*cess,0)) as cess  from gstListinvoiceitemANX()
        " & myUtils.CombineWhere(True, strFilterInv, "DocType= 'IP' and RCHRG = 'Y' and  gstinvoicetype not in ('IMPG','IMPS')") & " group by invoiceid,ctin,diffprcnt,pos,num,dt,val,HSN,rt")

        dic.Add("sezwop", "select invoiceid,ctin,doctyp,pos,num,dt,val,hsn,rt,sum(isnull(TXVAL,0)) as txval from gstListinvoiceitemANX() 
        " & myUtils.CombineWhere(True, strFilterInv, "DocType= 'IS' and gstinvoicetype = 'B2B' and gstinvoicesubtype = 'SEWOP' and isnull(istaxable,1)=1") & " group by invoiceid,ctin,doctyp,pos,num,dt,val,hsn,rt")

        dic.Add("sezwp", "select invoiceid,ctin,doctyp,pos,num,dt,val,hsn,rt,sum(isnull(TXVAL,0)) as txval,sum(isnull(igst,0)) as igst,sum(isnull(cess,0)) as cess,diffprcnt from gstListinvoiceitemANX()
        " & myUtils.CombineWhere(True, strFilterInv, "DocType= 'IS' and gstinvoicetype = 'B2B' and gstinvoicesubtype = 'SEWP' and isnull(istaxable,1)=1") & " group by invoiceid,ctin,doctyp,pos,num,dt,val,hsn,rt,diffprcnt")

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
        dic.Add("b2b", "select gstnactionid,ctin, 'D' as flag, invoicenum as num, invoicedate as  dt from gstnaction " & myUtils.CombineWhere(True, strFilter, "gstInvoiceType='B2B'", "DocType='IS'") &
                " union all select invoiceid,ctin, 'D' as flag, invoicenum as num, invoicedate as  dt from gstlistinvoice() " & myUtils.CombineWhere(True, strFilter2, "gstInvoiceType='B2B'", "DocType='IS'"))
        dic.Add("b2c", "select gstnactionid,ctin, 'D' as flag,  invoicenum as num, invoicedate as  dt from gstnaction " & myUtils.CombineWhere(True, strFilter, "gstInvoiceType='B2C'", "DocType='IS'") &
                " union all select invoiceid,ctin, 'D' as flag,  invoicenum as num, invoicedate as  dt from gstlistinvoice() " & myUtils.CombineWhere(True, strFilter2, "gstInvoiceType='B2C'", "DocType='IS'"))
        dic.Add("de", "select gstnactionid,ctin, 'D' as flag,  invoicenum as num, invoicedate as  dt from gstnaction " & myUtils.CombineWhere(True, strFilter, "gstInvoiceType='B2B'", "gstinvoicesubtype = 'DE'", "DocType='IS'") &
                " union all select invoiceid,ctin, 'D' as flag,  invoicenum as num, invoicedate as  dt from gstlistinvoice() " & myUtils.CombineWhere(True, strFilter2, "gstInvoiceType='B2B'", "gstinvoicesubtype = 'DE'", "DocType='IS'"))
        dic.Add("expwop", "select gstnactionid,ctin, 'D' as flag,  invoicenum as num, invoicedate as  dt from gstnaction " & myUtils.CombineWhere(True, strFilter, "DocType = 'IS'", "gstinvoicetype = 'EXP'", "ExportDutyType = 'WOPAY'") &
                " union all select invoiceid,ctin, 'D' as flag,  invoicenum as num, invoicedate as  dt from gstlistinvoice() " & myUtils.CombineWhere(True, strFilter2, "DocType = 'IS'", "gstinvoicetype = 'EXP'", "ExportDutyType = 'WOPAY'"))
        dic.Add("expwp", "select gstnactionid,ctin, 'D' as flag,  invoicenum as num, invoicedate as dt from gstnaction " & myUtils.CombineWhere(True, strFilter, "DocType = 'IS'", "gstinvoicetype = 'EXP'", "ExportDutyType = 'WPAY'") &
                " union all select invoiceid,ctin, 'D' as flag,  invoicenum as num, invoicedate as  dt from gstlistinvoice() " & myUtils.CombineWhere(True, strFilter2, "DocType = 'IS'", "gstinvoicetype = 'EXP'", "ExportDutyType = 'WPAY'"))
        dic.Add("impg", "select gstnactionid,ctin, 'D' as flag,  invoicenum as boe_num, invoicedate as  boe_dt from gstnaction " & myUtils.CombineWhere(True, strFilter, "DocType= 'IP'", "gstinvoicetype = 'IMPG'", "IS_SEZ not in ('Y')") &
                " union all select invoiceid,ctin, 'D' as flag,  invoicenum as boe_num, invoicedate as  boe_dt from gstlistinvoice() " & myUtils.CombineWhere(True, strFilter2, "DocType= 'IP'", "gstinvoicetype = 'IMPG'", "IS_SEZ not in ('Y')"))
        dic.Add("impgsez", "select gstnactionid,ctin, 'D' as flag,  invoicenum as boe_num, invoicedate as  boe_dt from gstnaction " & myUtils.CombineWhere(True, strFilter, "DocType= 'IP'", "gstinvoicetype = 'IMPG'", "IS_SEZ in ('Y')") &
                " union all select invoiceid,ctin, 'D' as flag,  invoicenum as boe_num, invoicedate as  boe_dt from gstlistinvoice() " & myUtils.CombineWhere(True, strFilter2, "DocType= 'IP'", "gstinvoicetype = 'IMPG'", "IS_SEZ in ('Y')"))
        'dic.Add("imps", "select gstnactionid,ctin, 'D' as flag, invoicenum as nt_num, invoicedate as nt_dt from gstnaction " & myUtils.CombineWhere(True, strFilter,  "DocType= 'IP'", "gstinvoicetype = 'IMPS'") &
        '        " union all select invoiceid,ctin, 'D' as flag, invoicenum as nt_num, invoicedate as nt_dt from gstlistinvoice() " & myUtils.CombineWhere(True, strFilter2, "DocType= 'IP'", "gstinvoicetype = 'IMPS'"))
        dic.Add("rev", "select gstnactionid,ctin, 'D' as flag, invoicenum as num, invoicedate as dt from gstnaction " & myUtils.CombineWhere(True, strFilter, "DocType= 'IP'", "RCHRG = 'Y'", "gstinvoicetype not in ('IMPG','IMPS')") &
                " union all select invoiceid,ctin, 'D' as flag, invoicenum as num, invoicedate as dt from gstlistinvoice() " & myUtils.CombineWhere(True, strFilter2, "DocType= 'IP'", "RCHRG = 'Y'", "gstinvoicetype not in ('IMPG','IMPS')"))
        dic.Add("sezwop", "select gstnactionid,ctin, 'D' as flag, invoicenum as num, invoicedate as dt from gstnaction " & myUtils.CombineWhere(True, strFilter, "DocType= 'IS'", "gstinvoicetype = 'B2B'", "gstinvoicesubtype = 'SEWOP'") &
                " union all select invoiceid,ctin, 'D' as flag, invoicenum as num, invoicedate as dt from gstlistinvoice() " & myUtils.CombineWhere(True, strFilter2, "DocType= 'IS'", "gstinvoicetype = 'B2B'", "gstinvoicesubtype = 'SEWOP'"))
        dic.Add("sezwp", "select gstnactionid,ctin, 'D' as flag, invoicenum as num, invoicedate as dt from gstnaction " & myUtils.CombineWhere(True, strFilter, "DocType= 'IS'", "gstinvoicetype = 'B2B'", "gstinvoicesubtype = 'SEWP'") &
                " union all select invoiceid,ctin, 'D' as flag, invoicenum as num, invoicedate as dt from gstlistinvoice() " & myUtils.CombineWhere(True, strFilter2, "DocType= 'IS'", "gstinvoicetype = 'B2B'", "gstinvoicesubtype = 'SEWP'"))

        Return dic

    End Function

    Public Overrides Function PrepareGSTRAPayloadSQL(GstRegID As Integer, ReturnPeriodID As Integer) As clsCollecString(Of String)

    End Function

    'Public Overrides Async Function UpdateDownloadedDataCP(GstRegID As Integer, ReturnPeriodID As Integer, results As List(Of SaveAnx01)) As Task(Of clsProcOutput(Of GstImportInfoGSTIN))

    'End Function

    'Public Overrides Async Function UpdateDownloadedDataTP(GstRegID As Integer, ReturnPeriodID As Integer, restuls As List(Of SaveAnx01)) As Task(Of clsProcOutput(Of GstImportInfoGSTIN))

    'End Function

    Public Overrides Async Function PrepareSummaryViews(CampusFilter As String, PeriodFilter As String, SummaryType As String) As Task(Of List(Of clsDummyView))
        Dim lst As New List(Of clsDummyView)
        Dim strFilter As String = myUtils.CombineWhere(False, CampusFilter, PeriodFilter)
        If myUtils.IsInList(SummaryType, "defer") Then
            lst.Add(Await PrepareView("listInvoiceDefer", strFilter, "Invoice Deferment"))
        Else
            lst.Add(Await PrepareView("ListANX1B2B", strFilter, "B2B Supplies"))
            lst.Add(Await PrepareView("ListANX1B2BSumm", strFilter, "B2B Section Summary"))
            lst.Add(Await PrepareView("ListANX1B2C", strFilter, "B2C Supplies"))
            lst.Add(Await PrepareView("ListANX1B2CSumm", strFilter, "B2C Section Summary"))
            lst.Add(Await PrepareView("listANX1CPInvoiceSumm", strFilter, "Counter Party Summary"))
            lst.Add(Await PrepareView("ListANX1DE", strFilter, "Deemed Exports"))
            lst.Add(Await PrepareView("listANX1ECom", strFilter, "E-COM Supplies"))
            lst.Add(Await PrepareView("ListANX1ECOMSumm", strFilter, "E-COM Summary"))
            lst.Add(Await PrepareView("listANX1EXPWOP", strFilter, "EXP-WPAY"))
            lst.Add(Await PrepareView("listANX1EXPWP", strFilter, "EXP-WOPAY"))
            lst.Add(Await PrepareView("listANX1IMPG", strFilter, "IMPG"))
            lst.Add(Await PrepareView("listANX1IMPGSEZ", strFilter, "IMPGSEZ"))
            lst.Add(Await PrepareView("listANX1IMPS", strFilter, "IMPS"))
            lst.Add(Await PrepareView("ListANX1REV", strFilter, "Inward Supplies Attracting-RCHRG"))
            lst.Add(Await PrepareView("ListANX1SEZWOP", strFilter, "SEZWOP"))
            lst.Add(Await PrepareView("ListANX1SEZWP", strFilter, "SEZWP"))
            lst.Add(Await PrepareView("ListANX1StateWiseSumm", strFilter, "State Code Summary"))
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

    Public Overrides Sub PrepareCleanTPModel(lst As List(Of SaveAnx01))

        For Each objModel In lst
            If objModel.B2B IsNot Nothing Then objModel.B2B.ForEach(Sub(x)
                                                                        x.Docs.ForEach(Sub(y)
                                                                                           y.Flag = Flag.D
                                                                                       End Sub)
                                                                    End Sub)

            If objModel.B2C IsNot Nothing Then objModel.B2C.ForEach(Sub(x)
                                                                        x.Flag = Flag.D
                                                                    End Sub)

            If objModel.De IsNot Nothing Then objModel.De.ForEach(Sub(x)
                                                                      x.Docs.ForEach(Sub(y)
                                                                                         y.Flag = Flag.D
                                                                                     End Sub)
                                                                  End Sub)

            If objModel.Ecom IsNot Nothing Then objModel.Ecom.ForEach(Sub(x)
                                                                          x.Flag = Flag.D
                                                                      End Sub)

            If objModel.Expwop IsNot Nothing Then objModel.Expwop.ForEach(Sub(x)
                                                                              x.Flag = Flag.D
                                                                          End Sub)

            If objModel.Impg IsNot Nothing Then objModel.Impg.ForEach(Sub(x)
                                                                          x.Docs.ForEach(Sub(y)
                                                                                             y.Flag = Flag.D
                                                                                         End Sub)
                                                                      End Sub)

            If objModel.Impgsez IsNot Nothing Then objModel.Impgsez.ForEach(Sub(x)
                                                                                x.Docs.ForEach(Sub(y)
                                                                                                   y.Flag = Flag.D
                                                                                               End Sub)
                                                                            End Sub)

            If objModel.Imps IsNot Nothing Then objModel.Imps.ForEach(Sub(x)
                                                                          x.Flag = Flag.D
                                                                      End Sub)

            If objModel.Rev IsNot Nothing Then objModel.Rev.ForEach(Sub(x)
                                                                        x.Docs.ForEach(Sub(y)
                                                                                           y.Flag = Flag.D
                                                                                       End Sub)
                                                                    End Sub)

            If objModel.Mis IsNot Nothing Then objModel.Mis.ForEach(Sub(x)
                                                                        x.Docs.ForEach(Sub(y)
                                                                                           y.Flag = Flag.D
                                                                                       End Sub)
                                                                    End Sub)

            If objModel.Sezwop IsNot Nothing Then objModel.Sezwop.ForEach(Sub(x)
                                                                              x.Docs.ForEach(Sub(y)
                                                                                                 y.Flag = Flag.D
                                                                                             End Sub)
                                                                          End Sub)

            If objModel.Sezwp IsNot Nothing Then objModel.Sezwp.ForEach(Sub(x)
                                                                            x.Docs.ForEach(Sub(y)
                                                                                               y.Flag = Flag.D
                                                                                           End Sub)
                                                                        End Sub)


            'If objModel.Sezwp IsNot Nothing Then objModel.Sezwp.flag = "D"



        Next

    End Sub

End Class
