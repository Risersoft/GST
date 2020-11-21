Imports risersoft.shared
Imports risersoft.app.mxent
Imports System.Runtime.Serialization
Imports System.Data.SqlClient

<DataContract>
Public Class frmGstInvoicePurchModel
    Inherits clsFormDataModel
    Dim PPFinal As Boolean = False
    Dim dicObjects As New clsCollecString(Of DataRow)

    Protected Overrides Sub InitViews()
        myView = Me.GetViewModel("ItemList")
    End Sub

    Public Sub New(context As IProviderContext)
        MyBase.New(context)
        Me.InitViews()
        Me.InitForm()
    End Sub

    Private Sub InitForm()
        Dim sql As String

        sql = "SELECT  VendorID, PartyName, TaxAreaCode, GSTRegType, GSTIN, VendorCode, TaxAreaID FROM  PurListVendor() Order By PartyName"
        Me.AddLookupField("VendorID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Vendor").TableName)

        sql = "SELECT  PostPeriodID, convert(varchar,Month) + ' / ' + convert(varchar,Year) as Month , SDate, EDate, ret_pd, IsFinal FROM postperiod Order By SDate desc"
        Me.AddLookupField("ReturnPeriodID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "ReturnPeriod").TableName)

        sql = myFuncsBase.CodeWordSQL("Party", "GSTRegType", "Tag2 = 'P'")
        Me.AddLookupField("PartyGSTRegType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "PartyGSTReg").TableName)

        sql = myFuncsBase.CodeWordSQL("Invoice", "Reason", "")
        Me.AddLookupField("rsn", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Reason").TableName)

        sql = "select TaxAreaID, Descrip, TaxAreaCode, TinCode from TaxArea Order by Descrip"
        Me.AddLookupField("POSTaxAreaID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "POS").TableName)
        Me.AddLookupField("ShipFromTaxAreaID", "POS")
        Me.AddLookupField("ShipToTaxAreaID", "POS")

        sql = "Select PartyID, PartyName, TaxAreaID from Party order by PartyName"
        Me.AddLookupField("ConsigneeID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Party").TableName)

        sql = "Select * from validationrule where doctype='IP' and isnull(isdisabled,0)=0"
        myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Rules")

        sql = "Select * from SystemOptions"
        myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "options")

        sql = myFuncsBase.CodeWordSQL("Invoice", "GstTax", "Tag2 = 'IP'")
        Me.AddLookupField("InvoiceItemGst", "GstTaxType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "GstTax").TableName)

        sql = "Select  Divisionid, (DivisionCode + '-' + DivisionName) as division,DivisionCode,CompanyID from Division order by DivisionCode"
        Me.AddLookupField("DivisionID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Division").TableName)

        Dim vlist As New clsValueList
        vlist.Add("Y", "Yes")
        vlist.Add("N", "No")
        Me.ValueLists.Add("RCHRG", vlist)
        Me.AddLookupField("RCHRG", "RCHRG")

        Me.ValueLists.Add("rfndelg", vlist)
        Me.AddLookupField("rfndelg", "rfndelg")

        Me.ValueLists.Add("sec7act", vlist)
        Me.AddLookupField("sec7act", "sec7act")

        Me.ValueLists.Add("IsSEZ", vlist)
        Me.AddLookupField("Is_SEZ", "IsSEZ")

        Me.ValueLists.Add("PreGST", vlist)
        Me.AddLookupField("PreGST", "PreGST")

        Dim vlist2 As New clsValueList
        vlist2.Add(0, "0")
        vlist2.Add(5, "5")
        vlist2.Add(12, "12")
        vlist2.Add(18, "18")
        vlist2.Add(28, "28")
        Me.ValueLists.Add("gstrate1", vlist2)
        Me.AddLookupField("I_RT", "gstrate1")

        Dim vlist21 As New clsValueList
        vlist21.Add(0, "0")
        vlist21.Add(2.5, "2.5")
        vlist21.Add(6, "6")
        vlist21.Add(9, "9")
        vlist21.Add(14, "14")
        Me.ValueLists.Add("gstrate2", vlist21)
        Me.AddLookupField("c_rt", "gstrate2")
        Me.AddLookupField("s_rt", "gstrate2")

        Dim vlist3 As New clsValueList
        vlist3.Add("False", "INR")
        vlist3.Add("True", "OTH")
        Me.ValueLists.Add("Currency", vlist3)
        Me.AddLookupField("IsForeignCurrency", "Currency")

        Dim vlst2 As New clsValueList
        vlst2.Add(True, "Yes")
        vlst2.Add(False, "No")
        Me.ValueLists.Add("ITCReversal", vlst2)
        Me.AddLookupField("ITCReversal_4243", "ITCReversal")

        Dim vlst22 As New clsValueList
        vlst22.Add(True, "Yes")
        vlst22.Add(False, "No")
        Me.ValueLists.Add("ISCreditISD", vlst22)
        Me.AddLookupField("ISCreditISD", "ISCreditISD")

        sql = "Select Code, Description from GstUqc order by Description"
        Me.AddLookupField("InvoiceItemGst", "Uqc", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "uqclist").TableName)

        Dim vlistapplicable As New clsValueList
        vlistapplicable.Add(65, "65")
        vlistapplicable.Add(100, "100")
        Me.ValueLists.Add("Diff_Percent", vlistapplicable)
        Me.AddLookupField("Diff_Percent", "Diff_Percent")

        sql = myFuncsBase.CodeWordSQL("Invoice", "TY", "")
        Me.AddLookupField("InvoiceItemGst", "TY", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "TY").TableName)

        sql = myFuncsBase.CodeWordSQL("Invoice", "ELG", "")
        Me.AddLookupField("InvoiceItemGst", "ELG", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "ELG").TableName)

        sql = "Select Code, Code + '-' + Description as Descrip,Description, TY from HsnSac "
        Me.AddLookupField("InvoiceItemGst", "HsnSac", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "HsnSac").TableName)

        sql = "Select * from gstrsection"
        myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "section")

    End Sub

    Public Overrides Function PrepForm(oView As clsViewModel, ByVal prepMode As EnumfrmMode, ByVal prepIDX As String, Optional ByVal strXML As String = "") As Boolean
        Dim sql, strf1, strf2 As String, dic As New clsCollecString(Of String), oDoc As New clsGSTInvoicePurch(myContext)

        Me.FormPrepared = False
        If prepMode = EnumfrmMode.acAddM Then prepIDX = 0
        sql = "Select * from Invoice Where InvoiceID = " & prepIDX
        Me.InitData(sql, "CDNInvoiceID,OrigInvoiceID", oView, prepMode, prepIDX, strXML)

        Dim str1 As String = myUtils.CombineWhere(True, myContext.AppModel.strFilterDBAppUser(myContext, Me.fRow, "CampusID"))
        sql = "Select CampusID, DispName, CompanyID, CampusType,TaxAreaCode, DivisionCodeList, GstRegID,GstRegType, PartialCredit, campuscode, GSTIN, RegDate from mmListCampus() " & str1 & " Order by DispName"
        Me.AddLookupField("CampusID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Campus").TableName)


        If myUtils.NullNot(myRow("diff_percent")) Then myRow("diff_percent") = 100
        If myUtils.NullNot(myRow("isamendment")) Then myRow("isamendment") = False
        If myUtils.NullNot(myRow("rchrg")) Then myRow("rchrg") = "N"

        dic.Add("my", "Select invoiceid from invoice where originvoiceid = " & frmIDX)
        dic.Add("orig", "Select invoiceid from invoice where originvoiceid = " & myUtils.cValTN(myRow("OrigInvoiceID")) & " and invoiceid<>" & frmIDX)
        Me.AddDataSet("AmendVouch", dic)

        If myUtils.cValTN(myRow("CDNInvoiceID")) > 0 Then
            sql = oDoc.LoadVouchSQL("InvoiceID = " & myUtils.cValTN(myRow("CDNInvoiceID")))
            Me.AddDataSet("CDNInvoice", sql)
            strf2 = "Codeword in ('DNP','CNP')"
            Dim dt As DataTable = Me.DatasetCollection("CDNInvoice").Tables(0)
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                Dim OrgInTypeCode As String = myUtils.cStrTN(dt.Rows(0)("GstInvoiceType"))
                If myUtils.IsInList(myUtils.cStrTN(OrgInTypeCode), "B2BUR", "IMPS") Then
                    strf1 = "Codeword in ('CDNUR') and Tag2 = 'IP'"
                Else
                    strf1 = "Codeword in ('CDN') and Tag2 = 'IP'"
                End If
                If frmMode = EnumfrmMode.acAddM Then
                    myRow("vendorid") = dt.Rows(0)("vendorid")
                    myRow("sply_ty") = dt.Rows(0)("sply_ty")
                    myRow("RCHRG") = dt.Rows(0)("RCHRG")
                    myRow("POSTaxAreaID") = dt.Rows(0)("POSTaxAreaID")
                End If
            End If
        Else
            strf2 = "Codeword in ('Pur')"
            strf1 = "Codeword not in ('CDN','CDNUR') and Tag2 = 'IP'"
        End If

        If myUtils.cValTN(myRow("OrigInvoiceID")) > 0 Then
            myRow("isamendment") = True
            sql = oDoc.LoadVouchSQL("InvoiceID = " & myUtils.cValTN(myRow("OrigInvoiceID")))
            Me.AddDataSet("OrgInvoice", sql)
            Dim dt As DataTable = Me.DatasetCollection("OrgInvoice").Tables(0)
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                If myUtils.cValTN(dt.Rows(0)("rootinvoiceid")) > 0 Then myRow("rootinvoiceid") = dt.Rows(0)("rootinvoiceid") Else myRow("rootinvoiceid") = myRow("OrigInvoiceID")
                strf1 = "Codeword in ('" & myUtils.cStrTN(dt.Rows(0)("GSTInvoiceType")) & "') and Tag2 = 'IP'"
                strf2 = "Codeword in ('" & myUtils.cStrTN(dt.Rows(0)("TransactionType")) & "')"
                If frmMode = EnumfrmMode.acAddM Then
                    myRow("vendorid") = dt.Rows(0)("vendorid")
                    myRow("cdninvoiceid") = dt.Rows(0)("cdninvoiceid")
                    myRow("sply_ty") = dt.Rows(0)("sply_ty")
                End If
            End If
        End If

        sql = myFuncsBase.CodeWordSQL("Invoice", "GstTypecode", strf1)
        Me.AddLookupField("GstInvoiceType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "GstInvoiceType").TableName)

        sql = myFuncsBase.CodeWordSQL("Invoice", "TransactionType", myUtils.CombineWhere(False, strf2, "Tag2 = 'IP'"))
        Me.AddLookupField("TransactionType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "TransactionType").TableName)

        Me.BindDataTable(myUtils.cValTN(prepIDX))

        If frmMode = EnumfrmMode.acAddM Then
            myRow("InvoiceDate") = Now.Date
            myFuncs2.CheckGetJsonData(myContext, Me)
            If Me.dsCombo.Tables("campus").Rows.Count = 1 Then myRow("campusid") = Me.dsCombo.Tables("campus").Rows(0)("campusid")
            If Me.dsCombo.Tables("gstinvoicetype").Rows.Count = 1 Then myRow("gstinvoicetype") = Me.dsCombo.Tables("gstinvoicetype").Rows(0)("codeword")
            If Me.dsCombo.Tables("TransactionType").Rows.Count = 1 Then myRow("transactiontype") = Me.dsCombo.Tables("TransactionType").Rows(0)("codeword")
        Else
            Dim oMaster As New clsMasterDataFICO(myContext)
            Dim rPostPeriod As DataRow = oMaster.rPostPeriod(myUtils.cValTN(myRow("ReturnPeriodID")))
            If Not IsNothing(rPostPeriod) Then
                PPFinal = myFuncs2.IsFinalPP(myContext, Me.SelectedRow("CampusID")("gstregid"), rPostPeriod, "GSTR2")
            End If
        End If

        myView.MainGrid.BindGridData(Me.dsForm, 1)
        myView.MainGrid.QuickConf("", True, "1-1-.8-1-2-.8-.8-1-1.2-1-1-1-1-1", True)
        strf1 = "<BAND INDEX = ""0"" TABLE = ""InvoiceItemGst"" IDFIELD=""InvoiceItemGstID""><COL KEY ="" InvoiceItemGstID, InvoiceID,GSTR3B31Section, GSTR3B32Section, GSTR3B4ASection, GSTR3B4DSection, GSTR3B5Section, RemarkItem,Hsn_Desc,ITCReversal_4243,ITCDate,GstTaxType,elg,tx_i,tx_c,tx_s,tx_cs,RT, I_RT, C_RT, S_RT, Cess_Rt,Description,TY,Qty,LineSNum""/><COL KEY=""Hsn_Sc"" CAPTION=""HSN""/><COL KEY=""I_RT"" FORMAT=""00.00"" CAPTION=""IGST Rate""/><COL KEY=""C_RT"" FORMAT=""00.00"" CAPTION=""CGST Rate""/><COL KEY=""S_RT"" FORMAT=""00.00"" CAPTION=""SGST Rate""/><COL KEY=""Cess_Rt"" FORMAT=""00.00"" CAPTION=""CESS Rate""/><COL KEY=""TXVAL"" CAPTION=""Taxable Value""/><COL KEY=""Uqc"" CAPTION=""Unit Name""/><COL KEY=""BasicRate"" CAPTION=""Basic Rate""/><COL KEY=""CSAMT"" CAPTION=""CESS""/><COL KEY=""IAMT"" CAPTION=""Integrated Tax""/><COL KEY=""CAMT"" CAPTION=""Central Tax""/><COL KEY=""SAMT"" CAPTION=""State Tax""/><COL KEY=""LineSNum"" CAPTION=""LineItemID""/></BAND>"
        myView.MainGrid.PrepEdit(strf1, EnumEditType.acCommandOnly)

        Me.FormPrepared = True
        Return Me.FormPrepared
    End Function

    Private Sub BindDataTable(ByVal InvoicePurchID As Integer)
        Dim ds As DataSet, Sql As String

        Sql = " Select InvoiceItemGstID, InvoiceID, GSTR3B31Section, GSTR3B32Section, GSTR3B4ASection, GSTR3B4DSection, GSTR3B5Section, RemarkItem, GstTaxType,elg,tx_i,tx_c,tx_s,tx_cs,TY,ITCReversal_4243, ITCDate, Hsn_Sc, Hsn_Desc, RT, LineSNum, Description, Uqc, Qty, BasicRate, TXVAL, I_RT, C_RT, S_RT, Cess_Rt, IAMT, CAMT, SAMT, CSAMT from InvoiceItemGst  Where InvoiceID = " & InvoicePurchID & " "
        ds = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql)

        myUtils.AddTable(Me.dsForm, ds, "InvoiceItemGst", 0)

    End Sub

    Public Function FindCampusID(CampusID As Integer) As DataRow
        Dim rrCampus() As DataRow = Me.dsCombo.Tables("Campus").Select("CampusID=" & CampusID)
        If rrCampus.Length > 0 Then Return rrCampus(0)
    End Function

    Public Overrides Function Validate() As Boolean
        Dim oMaster As New clsMasterDataFICO(myContext), LineSNum As Integer = 0
        Me.InitError()

        myFuncs2.SetDivision(myContext, Me.fRow, myRow.Row, myUtils.cValTN(myRow("campusid")))

        Dim oProc As New clsGSTInvoicePurch(myContext)
        If Me.SelectedRow("ShipFromTaxAreaID") Is Nothing Then
            oProc.PopulateGstType(myRow.Row, myView.MainGrid.myDV.Table, "")
        Else
            oProc.PopulateGstType(myRow.Row, myView.MainGrid.myDV.Table, myUtils.cStrTN(Me.SelectedRow("ShipFromTaxAreaID")("TAXAreaCode")))
        End If

        myFuncs2.CheckPopulateTaxAreaID(myRow.Row, Me.SelectedRow("VendorID"), Me.SelectedRow("ConsigneeID"))

        Dim CTIN As String = If(Me.SelectedRow("VendorID") Is Nothing, "", myUtils.cStrTN(Me.SelectedRow("vendorid")("gstin")))
        myRow("sply_ty") = myFuncs2.FindSupplyTypePurchase(CTIN, myUtils.cStrTN(Me.SelectedRow("campusid")("GstRegType")), Me.SelectedRow("POSTaxAreaID"), Me.SelectedRow("ShipFromTaxAreaID"))

        For Each r As DataRow In myView.MainGrid.myDV.Table.Select("", "linesnum")
            LineSNum = LineSNum + 1
            If myUtils.cValTN(r("I_RT")) > 0 Then
                r("RT") = myUtils.cValTN(r("I_RT"))
            Else
                r("RT") = myUtils.cValTN(r("C_RT")) + myUtils.cValTN(r("S_RT"))
            End If
            r("linesnum") = LineSNum
        Next

        'Validations
        If dicObjects Is Nothing Then dicObjects = New clsCollecString(Of DataRow) Else dicObjects.Clear()
        dicObjects.Add("gstreg", Me.SelectedRow("campusid"))
        dicObjects.Add("division", Me.SelectedRow("divisionid"))
        dicObjects.Add("party", Me.SelectedRow("VendorID"))
        dicObjects.Add("returnperiod", Me.SelectedRow("returnperiodid"))
        If myRow.Row.RowState = DataRowState.Modified Then
            Dim rPP = oMaster.rPostPeriod(myUtils.cValTN(myRow.Row("returnperiodid", DataRowVersion.Original)))
            dicObjects.Add("returnperiodbefore", rPP)
        End If


        For Each str1 As String In New String() {"pos", "shipfrom", "shipto"}
            dicObjects.Add(str1, Me.SelectedRow(str1 & "taxareaid"))
        Next
        If Me.DatasetCollection.ContainsKey("AmendVouch") Then
            If Me.DatasetCollection("AmendVouch").Tables("my").Rows.Count > 0 Then dicObjects.Add("AmendVouchMy", Me.DatasetCollection("AmendVouch").Tables("my").Rows(0))
            If Me.DatasetCollection("AmendVouch").Tables("orig").Rows.Count > 0 Then dicObjects.Add("AmendVouchOrig", Me.DatasetCollection("AmendVouch").Tables("orig").Rows(0))
        End If
        If Me.DatasetCollection.ContainsKey("OrgInvoice") Then
            Dim dt As DataTable = Me.DatasetCollection("OrgInvoice").Tables(0)
            If dt.Rows.Count > 0 Then
                dicObjects.Add("orig", dt.Rows(0))
                Dim r1 As DataRow = Me.FindCampusID(myUtils.cValTN(dt.Rows(0)("campusid")))
                dicObjects.Add("orig.GstReg", r1)
            End If
        End If
        If Me.DatasetCollection.ContainsKey("CDNInvoice") Then
            Dim dt As DataTable = Me.DatasetCollection("CDNInvoice").Tables(0)
            If dt.Rows.Count > 0 Then
                dicObjects.Add("cdn", dt.Rows(0))
                Dim r1 As DataRow = Me.FindCampusID(myUtils.cValTN(dt.Rows(0)("campusid")))
                dicObjects.Add("cdn.GstReg", r1)
            End If
        End If
        If (Not Me.SelectedRow("campusid") Is Nothing) Then
            Dim rGstPP = oMaster.GstRegPPRow(Me.SelectedRow("campusid")("gstregid"), myUtils.cValTN(myRow("returnperiodid")))
            dicObjects.Add("GstRegPP", rGstPP)
        End If

        For Each str1 As String In New String() {"campus", "gstreg", "party", "returnperiod", "returnperiodbefore", "pos", "shipfrom", "shipto", "orig", "cdn", "AmendVouchMy", "AmendVouchOrig", "GstRegPP"}
            If Not dicObjects.Exists(str1) Then dicObjects.Add(str1, Nothing)
        Next

        Dim oProc2 As New clsJintValidator(myContext)
        Dim dt1 = Me.dsCombo.Tables("rules")
        If dsCombo.Tables("options").Select.Length > 0 Then
            oProc2.AddOrUpdateRow(dsCombo.Tables("options").Rows(0), "")
        End If
        Dim lst = oProc2.RunValidator(myRow.Row, Me.dsForm, "InvoiceItemGst", "item", dt1, Sub(obj, rItem)
                                                                                               If rItem Is Nothing Then
                                                                                                   obj.SetValue("GSTUtils", New clsJINTFuncsGSTN(myContext))
                                                                                                   For Each kvp In dicObjects
                                                                                                       obj.AddOrUpdateRow(kvp.Value, kvp.Key)
                                                                                                   Next
                                                                                               Else
                                                                                                   'For hsnsac table = hsnsac.rt
                                                                                                   Dim rHSN = Me.SelectedRow(rItem, "Hsn_Sc")
                                                                                                   obj.AddOrUpdateRow(rHSN, "hsnsac")
                                                                                               End If
                                                                                           End Sub)

        Me.AddError(lst)

        Return Me.CanSave
    End Function

    Public Overrides Function VSave() As Boolean
        VSave = False

        If Me.Validate Then
            myRow("DocType") = "IP"
            myRow("BillOf") = "C"
            If frmMode = EnumfrmMode.acAddM Then myRow("StatusNum") = 2

            Dim oProc As New clsGSTInvoicePurch(myContext)
            myRow("uniquekey") = oProc.CalcUniqueKey(Me.SelectedRow("campusid")("campuscode"), myRow("ReturnPeriodID"), myRow("invoicenum"), myUtils.cValTN(myRow("isamendment")))
            oProc.CalcVouchActionRP(Me.SelectedRow("CampusID")("gstregid"), myRow("ReturnPeriodID"), myRow.Row)
            oProc.UpdateInvoiceNumberDynamicPart(Me.SelectedRow("CampusID")("gstregid"), myRow.Row)
            If Me.CanSave Then
                Dim InvoicePurchDescrip As String = " Purchase Invoice No: " & myRow("InvoiceNum").ToString & ", Dt. " & Format(myRow("InvoiceDate"), "dd-MMM-yyyy")
                Try

                    myContext.Provider.dbConn.BeginTransaction(myContext, Me.Name, Me.frmMode.ToString, "InvoiceID", frmIDX)
                    If (frmMode = EnumfrmMode.acEditM) AndAlso (Not oProc.MatchUniqueFields(myRow.Row)) Then
                        Dim rDel As DataRow = oProc.AddDeleteAction(myRow.Row, Me.SelectedRow("CampusID")("gstregid"), If(Me.SelectedRow("vendorid") Is Nothing, "", myUtils.cStrTN(Me.SelectedRow("vendorid")("gstin"))))
                    End If

                    oProc.PopulateCalc(myUtils.cValTN(myRow("InvoiceID")), myRow.Row, dicObjects("gstreg"), dsForm.Tables("InvoiceItemGst"), Nothing, dicObjects("cdn"), dicObjects("orig"), dsCombo)

                    myContext.Provider.objSQLHelper.SaveResults(myRow.Row.Table, "Select * from Invoice Where InvoiceID = " & frmIDX)
                    frmIDX = myRow("InvoiceID")
                    myUtils.ChangeAll(dsForm.Tables("InvoiceItemGst").Select, "InvoiceID=" & frmIDX)


                    myContext.Provider.objSQLHelper.SaveResults(dsForm.Tables("InvoiceItemGst"), "Select InvoiceItemGstID, InvoiceID, GSTR3B31Section, GSTR3B32Section, GSTR3B4ASection, GSTR3B4DSection, GSTR3B5Section, RemarkItem,elg,tx_i,tx_c,tx_s,tx_cs,ITCReversal_4243,ITCDate,Description,TY, RT,LineSNum, GstTaxType, Hsn_Desc, Hsn_Sc, TXVAL, I_RT, C_RT, S_RT, Cess_Rt, IAMT, CAMT,SAMT, CSAMT, Uqc,Qty,BasicRate from InvoiceItemGst")

                    If Me.vBag.ContainsKey("importfilevouchid") Then
                        Dim sql2 As String = "delete from importfilevouch where importfilevouchid=" & myUtils.cValTN(Me.vBag("importfilevouchid"))
                        myContext.Provider.objSQLHelper.ExecuteNonQuery(CommandType.Text, sql2)
                    End If

                    frmMode = EnumfrmMode.acEditM
                    myContext.Provider.dbConn.CommitTransaction(InvoicePurchDescrip, frmIDX)
                    VSave = True
                Catch ex As SqlException
                    myContext.Provider.dbConn.RollBackTransaction(InvoicePurchDescrip, ex.Message)
                    If ex.Number = 2601 OrElse ex.Number = 2627 Then
                        Me.AddError("", "Duplicate Invoice")
                    Else
                        Me.AddError("", ex.Message)
                    End If

                Catch ex2 As Exception
                    myContext.Provider.dbConn.RollBackTransaction(InvoicePurchDescrip, ex2.Message)
                    Me.AddError("", ex2.Message)
                End Try
            End If
        End If
    End Function

    Public Overrides Function DeleteEntity(EntityKey As String, ID As Integer, AcceptWarning As Boolean) As clsProcOutput
        Dim oRet As New clsProcOutput
        Try
            Select Case EntityKey.Trim.ToLower
                Case "invoice"
                    Dim oProc As New clsGSTInvoicePurch(myContext)
                    oRet = oProc.DeleteVouch(ID, AcceptWarning)
            End Select

        Catch ex As Exception
            oRet.AddException(ex)
        End Try


        Return oRet
    End Function
    Public Overrides Function GenerateIDOutput(dataKey As String, ID As Integer) As clsProcOutput
        Dim oRet As New clsProcOutput
        Select Case dataKey.Trim.ToLower
            Case "division"
                oRet = myFuncs2.FindDivisionList(myContext, Me.fRow, ID)
        End Select
        Return oRet
    End Function
End Class
