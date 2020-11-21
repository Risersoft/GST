Imports risersoft.shared
Imports risersoft.API.GSTN.ANX02
Imports risersoft.API.GSTN
Imports Newtonsoft.Json
Imports System.Text
Imports System.Web
Imports GSTN.API.Library.Models.GstNirvana

Public Class clsGSTNReturnANX02
    Inherits clsGSTNTypedReturnBase(Of SaveAnx02, Anx02Summary, ANX02ApiClient, ANX02ApiClient)
    Public Overrides Property CDNRApiAction As String = "CDN"
    Public Sub New(context As IProviderContext)
        MyBase.New(context, "IP", "ANX02")
    End Sub
    Public Overrides Function PrepareGSTRPayloadSQLUp(GstRegID As Integer, ReturnPeriodID As Integer) As clsCollecString(Of String)
        Dim strf1 As String = "actionflag in ('U','M')"
        Dim strf2 As String = "GstRegID=" & GstRegID
        Dim strf3 As String = " ReturnPeriodID =" & ReturnPeriodID
        Dim strf4 As String = fncInvoiceFilter.Invoke()

        Dim strFilterInv As String = myUtils.CombineWhere(False, strf1, strf2, strf3, strf4)
        Dim strFilterSumm As String = myUtils.CombineWhere(False, strf2, strf3)
        Dim dic As New clsCollecString(Of String)


        dic.Add("b2b", "select invoiceid,ctin,doctyp,chksum,num,dt from gstListinvoiceitemANX()
        " & myUtils.CombineWhere(True, strFilterInv, "gstInvoiceType='B2B' and DocType='IP' and GSTInvoiceSubType not in ('SEWP','SEWOP','DE') and PartyGstRegType in ('SEZ','SEZD') and RCHRG = 'N' and isnull(istaxable,1)=1") & " 
        group by invoiceid,ctin,doctyp,chksum,num,dt")

        dic.Add("sezwp", "select invoiceid,ctin,doctyp,pos,num,dt,val,hsn,rt,sum(isnull(TXVAL,0)) as txval,sum(isnull(igst,0)) as igst,sum(isnull(cess,0)) as cess,diffprcnt from gstListinvoiceitemANX()
        " & myUtils.CombineWhere(True, strFilterInv, "gstinvoicetype = 'B2B' and DocType= 'IP' and gstinvoicesubtype = 'SEWP' and isnull(istaxable,1)=1") & " group by invoiceid,ctin,doctyp,pos,num,dt,val,hsn,rt,diffprcnt")

        dic.Add("sezwop", "select invoiceid,ctin,doctyp,pos,num,dt,val,hsn,rt,sum(isnull(TXVAL,0)) as txval from gstListinvoiceitemANX() 
        " & myUtils.CombineWhere(True, strFilterInv, "gstinvoicetype = 'B2B' and DocType= 'IP' and gstinvoicesubtype = 'SEWOP' and isnull(istaxable,1)=1") & " group by invoiceid,ctin,doctyp,pos,num,dt,val,hsn,rt")

        dic.Add("de", "select invoiceid,ctin,doctyp,chksum,num,dt from gstListinvoiceitemANX()
        " & myUtils.CombineWhere(True, strFilterInv, "gstinvoicetype = 'B2B' and DocType= 'IP' and gstinvoicesubtype = 'DE' and isnull(istaxable,1)=1") & " group by invoiceid,ctin,doctyp,chksum,num,dt")

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

        dic.Add("b2b", "select gstnactionid,ctin, 'D' as flag, invoicenum as num, invoicedate as  dt from gstnaction " & myUtils.CombineWhere(True, strFilter, "gstInvoiceType='B2B'", "DocType='IP'", "GSTInvoiceSubType not in ('SEWP','SEWOP','DE')", "PartyGstRegType in ('SEZ','SEZD')", "RCHRG='N'") &
                " union all select invoiceid,ctin, 'D' as flag, invoicenum as num, invoicedate as  dt from gstlistinvoice() " & myUtils.CombineWhere(True, strFilter2, "gstInvoiceType='B2B'", "DocType='IP'", "GSTInvoiceSubType not in ('SEWP','SEWOP','DE')", "PartyGstRegType in ('SEZ','SEZD')", "RCHRG='N'"))
        dic.Add("sezwp", "select gstnactionid,ctin, 'D' as flag, invoicenum as num, invoicedate as dt from gstnaction " & myUtils.CombineWhere(True, strFilter, "gstinvoicetype='B2B'", "DocType='IP'", "gstinvoicesubtype='SEWP'") &
                " union all select invoiceid,ctin, 'D' as flag, invoicenum as num, invoicedate as dt from gstlistinvoice() " & myUtils.CombineWhere(True, strFilter2, "gstinvoicetype='B2B'", "DocType='IP'", "gstinvoicesubtype='SEWP'"))
        dic.Add("sezwop", "select gstnactionid,ctin, 'D' as flag,  invoicenum as num, invoicedate as  dt from gstnaction " & myUtils.CombineWhere(True, strFilter, "gstInvoiceType='B2B'", "DocType='IP'", "gstinvoicesubtype='SEWOP'") &
                " union all select invoiceid,ctin, 'D' as flag,  invoicenum as num, invoicedate as  dt from gstlistinvoice() " & myUtils.CombineWhere(True, strFilter2, "gstInvoiceType='B2B'", "DocType='IP'", "gstinvoicesubtype='SEWOP'"))
        dic.Add("de", "select gstnactionid,ctin, 'D' as flag,  invoicenum as num, invoicedate as  dt from gstnaction " & myUtils.CombineWhere(True, strFilter, "gstInvoiceType='B2B'", "DocType='IP'", "gstinvoicesubtype='DE'") &
                " union all select invoiceid,ctin, 'D' as flag,  invoicenum as num, invoicedate as  dt from gstlistinvoice() " & myUtils.CombineWhere(True, strFilter2, "gstInvoiceType='B2B'", "DocType='IP'", "gstinvoicesubtype='DE'"))

        Return dic

    End Function

    Public Overrides Function PrepareGSTRAPayloadSQL(GstRegID As Integer, ReturnPeriodID As Integer) As clsCollecString(Of String)
        Dim strf2 As String = "GstRegID=" & GstRegID
        Dim strf3 As String = "ReturnPeriodID=" & ReturnPeriodID

        Dim strFilter As String = myUtils.CombineWhere(False, strf2, strf3)
        Dim dic As New clsCollecString(Of String)
        dic.Add("b2b", "select CPInvoiceID, ctin, CFS, POS, UpdBy, Flag, CFlag, OPD, ChkSum, inum, idt, val, rchrg, inv_typ,  sum(txval) as txval, rt, sum(iamt) as iamt, sum(camt) as camt, sum(samt) as samt, sum(csamt) as csamt from 
                            accListCPInvoiceItem() " & myUtils.CombineWhere(True, strFilter, "gstInvoiceType='B2B' and DocType='IP' and isnull(isamendment,0)=0") & " group By CPInvoiceID,ctin, chksum, CFS, POS, UpdBy, Flag, CFlag, OPD, inum, idt, val,  rchrg, inv_typ, rt")
        'dic.Add("b2ba", "select CPInvoiceID, ctin, CFS, POS, UpdBy, Flag, CFlag, OPD, ChkSum, diff_percent, oinum, oidt, inum, idt, val, rchrg, inv_typ,  sum(txval) as txval, rt, sum(iamt) as iamt, sum(camt) as camt, sum(samt) as samt, sum(csamt) as csamt from 
        '                    accListCPInvoiceItem() " & myUtils.CombineWhere(True, strFilter, "gstInvoiceType='B2BA' and DocType='IP' and isnull(isamendment,0)=0") & " group By CPInvoiceID,ctin, chksum, CFS, POS, UpdBy, Flag, CFlag, OPD, diff_percent, oinum, oidt, inum, idt, val,  rchrg, inv_typ, rt")
        'dic.Add("cdn", "select CPInvoiceID,ctin, CFS, UpdBy, Flag, CFlag, OPD, ChkSum, ntty, nt_num, nt_dt, val, inum, idt,  sum(txval) as txval, rt, sum(iamt) as iamt, sum(camt) as camt, sum(samt) as samt, sum(csamt) as csamt from 
        '                    accListCPInvoiceItem() " & myUtils.CombineWhere(True, strFilter, "gstInvoiceType='CDN' and DocType='IP' and isnull(isamendment,0)=0") & " group By CPInvoiceID, ctin, chksum, CFS,  UpdBy, Flag, CFlag, OPD, ntty, nt_num, nt_dt, inum, idt, rt, val")
        'dic.Add("cdna", "select CPInvoiceID,ctin, CFS, UpdBy, Flag, CFlag, OPD, ChkSum, ntty, ont_num, ont_dt, p_gst, diff_percent, nt_num, nt_dt, val, inum, idt,  sum(txval) as txval, rt, sum(iamt) as iamt, sum(camt) as camt, sum(samt) as samt, sum(csamt) as csamt from 
        '                    accListCPInvoiceItem() " & myUtils.CombineWhere(True, strFilter, "gstInvoiceType='CDNA' and DocType='IP' and isnull(isamendment,0)=0") & " group By CPInvoiceID, ctin, chksum, CFS,  UpdBy, Flag, CFlag, OPD, ntty, nt_num, nt_dt, ont_num, ont_dt, p_gst, diff_percent, inum, idt, rt, val")

        Return dic

    End Function

    Public Overrides Async Function UpdateDownloadedDataCP(GstRegID As Integer, ReturnPeriodID As Integer, results As List(Of SaveAnx02)) As Task(Of clsProcOutput(Of GstImportInfoGSTIN))
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
            'lst.Add(Await PrepareView("listCPB2BAIP", strFilter))
            'lst.Add(Await PrepareView("listCPCDNIP", strFilter))
            'lst.Add(Await PrepareView("listCPCDNAIP", strFilter))
        Else
            lst.Add(Await PrepareView("ListANX2B2B", strFilter))
            'lst.Add(Await PrepareView("ListANX2SEZWP", strFilter))
            'lst.Add(Await PrepareView("ListANX2SEZWOP", strFilter))
            'lst.Add(Await PrepareView("ListANX2DE", strFilter))
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
