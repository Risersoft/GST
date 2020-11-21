Imports GSTN.API.Library.Models.GstNirvana
Imports risersoft.shared

Public MustInherit Class ImportTaskProviderInvoiceBase(Of TDoc As clsGSTDocBase)
    Inherits ImportTaskProviderGstBase
    Public Overrides Property UpdateBatchSize As Integer = 0
    Public Overrides Property DocName As String = "Invoice"


    Public Sub New(controller As clsAppController)
        MyBase.New(controller)
    End Sub

    Public Overrides Function ExecutePreValidation(provider As IAppDataProvider, rInv As DataRow, dtItems As DataTable, rXL As DataRow, objGroup As clsRowGroup) As clsProcOutput
        Dim oRet As New clsProcOutput, LineSNum As Integer = 0
        Dim PartyType As String = Me.fncPartyType(rXL)
        Dim PartySubTypeTable As String = Me.fncPartySubTypeTable(rXL)
        Dim dic = objGroup.dic
        Try
            oRet = Me.ExecutePreValidationMaster(rInv, dtItems, rXL, dic)
            Me.CalculateDate(rXL, rInv, "invoicedate")
            Me.CalculateDate(rXL, rInv, "canceldate")
            For Each r1 As DataRow In dtItems.Select("", "linesnum")
                LineSNum = LineSNum + 1
                r1("linesnum") = LineSNum
            Next

            'isamendment
            rInv("isamendment") = myUtils.IsInList(myUtils.cStrTN(rXL("InvoiceType")), "amendment")
            rInv("rchrg") = If(rXL.Table.Columns.Contains("rchrg") AndAlso
                myUtils.IsInList(myUtils.cStrTN(rXL("rchrg")), "yes", "y"), "Y", "N")
            rInv("rfndelg") = If(rXL.Table.Columns.Contains("rfndelg") AndAlso
                myUtils.IsInList(myUtils.cStrTN(rXL("rfndelg")), "yes", "y"), "Y", "N")
            rInv("sec7act") = If(rXL.Table.Columns.Contains("sec7act") AndAlso
                myUtils.IsInList(myUtils.cStrTN(rXL("sec7act")), "yes", "y"), "Y", "N")
            rInv("clmrfnd") = If(rXL.Table.Columns.Contains("clmrfnd") AndAlso
                myUtils.IsInList(myUtils.cStrTN(rXL("clmrfnd")), "yes", "y"), "Y", "N")
            rInv("p_gst") = If(myUtils.IsInList(myUtils.cStrTN(rXL("p_gst")), "yes", "y"), "Y", "N")
            If myUtils.cValTN(rInv("val")) = 0 Then
                For Each r1 As DataRow In dtItems.Select
                    If myUtils.IsInList(rInv("rchrg"), "y") Then
                        rInv("val") = myUtils.cValTN(rInv("val")) + myUtils.cValTN(r1("txval"))
                    Else
                        rInv("val") = myUtils.cValTN(rInv("val")) + myUtils.cValTN(r1("txval")) + myUtils.cValTN(r1("csamt")) + myUtils.cValTN(r1("iamt")) + myUtils.cValTN(r1("camt")) + myUtils.cValTN(r1("samt"))
                    End If
                Next
            End If

            If myUtils.IsInList(DocType, "IP") Then rInv("Diff_Percent") = 100
            If myUtils.IsInList(DocType, "IP") Then
                Dim rrOption() As DataRow = dsMaster.Tables("options").Select()
                For Each rItem As DataRow In dtItems.Select("", "linesnum")
                    Dim rHSN As DataRow
                    Dim rrHSNSac() As DataRow = dsMaster.Tables("hsn").Select("code='" & myUtils.cStrTN(rItem("hsn_sc")) & "'")
                    If rrHSNSac.Length > 0 Then
                        rHSN = rrHSNSac(0)
                        Dim arr() As String = myUtils.cStrTN(rHSN("ITCInElgKeyword")).Split(New String() {","}, StringSplitOptions.RemoveEmptyEntries)
                        If myUtils.InStrList(myUtils.cStrTN(rItem("Hsn_Desc")), arr) Then rItem("ITCInElgSugg") = True
                    End If
                    If rrOption.Length > 0 Then
                        Dim rOption = rrOption(0)
                        Dim arr() As String = myUtils.cStrTN(rOption("ITCRemarkKeyword")).Split(New String() {","}, StringSplitOptions.RemoveEmptyEntries)
                        If myUtils.InStrList(myUtils.cStrTN(rItem("remarkitem")), arr) Then rItem("ITCInElgSugg") = True
                        If myUtils.InStrList(myUtils.cStrTN(rItem("remarkitem2")), arr) Then rItem("ITCInElgSugg") = True
                        If myUtils.InStrList(myUtils.cStrTN(rInv("remark")), arr) Then rItem("ITCInElgSugg") = True
                    End If

                Next


                If (rXL.Table.Columns.Contains("isforeigncurrency")) Then
                    rInv("isforeigncurrency") = myUtils.IsInList(myUtils.cStrTN(rXL("isforeigncurrency")), "Others")
                    Me.CalculateDate(rXL, rInv, "boe_dt")
                End If
            Else
                If myUtils.IsInList(myUtils.cStrTN(rXL("exportdutytype")), "WPAY", "WP", "WPGST", "With Payment of GST") Then
                    rInv("exportDutyType") = "WPAY"
                ElseIf myUtils.IsInList(myUtils.cStrTN(rXL("exportdutytype")), "WOPAY", "WOP", "WOPGST", "Without Payment of GST") Then
                    rInv("exportDutyType") = "WOPAY"
                Else
                    rInv("exportDutyType") = ""
                End If
                If myUtils.cValTN(rInv("sbnum")) = 0 Then rInv("sbnum") = DBNull.Value
                Me.CalculateDate(rXL, rInv, "SBDT")
                Dim str1 As String = "documentType", documentType As String = myUtils.cStrTN(rXL(str1))
                If documentType.Trim.Length = 0 Then
                    If myUtils.IsInList(myUtils.cStrTN(rXL("transactiontype")), "sales") Then
                        documentType = If(dtItems.Select("gsttaxtype='taxable'").Length > 0, "INV", "BOS")
                    ElseIf myUtils.IsInList(myUtils.cStrTN(rXL("transactiontype")), "dns") Then
                        documentType = "DN"
                    ElseIf myUtils.IsInList(myUtils.cStrTN(rXL("transactiontype")), "cns") Then
                        documentType = "CN"
                    End If
                    rInv(str1) = documentType
                Else
                    Dim rrPOS() As DataRow = dsMaster.Tables(str1).Select("Descrip='" & documentType & "'")
                    If rrPOS.Length > 0 Then rInv(str1) = rrPOS(0)("codeword")
                End If
            End If

            If dic.ContainsKey("GstReg") Then
                Dim rGstReg = dic("gstreg")
                Dim CampusFilter As String = myContext.SQL.GenerateSQLWhere(rGstReg, "gstregid")
                Dim PartyFilter As String = myContext.SQL.GenerateSQLWhere(rInv, PartySubTypeTable & "ID")
                'Originvoiceid
                If myUtils.cBoolTN(rInv("isamendment")) Then
                    Dim rOrig As DataRow
                    If myUtils.IsInList(rXL("TransactionType"), "CNP", "DNP", "CNS", "DNS") Then
                        rOrig = Me.FindVouch(provider, CampusFilter, myUtils.cStrTN(rXL("origNoteNum")), myUtils.cStrTN(rXL("origNoteDate")), PartyFilter)
                    Else
                        rOrig = Me.FindVouch(provider, CampusFilter, myUtils.cStrTN(rXL("origInvoiceNum")), myUtils.cStrTN(rXL("origInvoiceDate")), PartyFilter)
                    End If
                    If rOrig IsNot Nothing Then
                        rInv("origInvoiceid") = myUtils.cValTN(rOrig("invoiceid"))
                        dic.Add("orig", rOrig)
                        Dim dicSQL As New clsCollecString(Of String), frmIDX As Integer = 0
                        dicSQL.Add("my", "Select invoiceid from invoice where originvoiceid = " & frmIDX)
                        dicSQL.Add("orig", "Select invoiceid from invoice where originvoiceid = " & rInv("origInvoiceid") & " and invoiceid<>" & frmIDX)
                        Me.AddSQL(provider, dic, dicSQL, "AmendVouch")
                        Dim r1 As DataRow = Me.FindCampus(myUtils.cValTN(rOrig("campusid")))
                        dic.Add("orig.Campus", r1)
                        If r1 IsNot Nothing Then
                            Dim r2 As DataRow = Me.FindGstRegID(myUtils.cValTN(r1("gstregid")))
                            dic.Add("orig.GstReg", r2)
                        End If
                    End If
                End If
                ' Shipfromtaxareaid, Shiptotaxareaid
                For Each str1 As String In New String() {"shipfrom", "shipto"}
                    If rXL.Table.Columns.Contains(str1) Then
                        Dim rPOS = myFuncs2.FindTaxAreaRow(dsMaster.Tables("taxarea"), myUtils.cStrTN(rXL(str1)))
                        If rPOS IsNot Nothing Then
                            rInv(str1 & "taxareaid") = rPOS("taxareaid")
                            dic.Add(str1, rPOS)
                        End If
                    End If
                Next


                'GstRegType, TransactionType, Reason for issuing DN/CN
                For Each str1 As String In New String() {"transactionType", "partygstregtype", "rsn"}
                    Dim rrPOS() As DataRow = dsMaster.Tables(str1).Select("Descrip='" & myUtils.cStrTN(rXL(str1)) & "'")
                    If rrPOS.Length > 0 Then
                        rInv(str1) = rrPOS(0)("codeword")
                    Else
                        rInv(str1) = DBNull.Value
                    End If
                Next
                'sply_ty
                If myUtils.IsInList(DocType, "IP") Then
                    If Not rGstReg Is Nothing Then rInv("sply_ty") = myFuncs2.FindSupplyTypePurchase(myUtils.cStrTN(rXL("ctin")), myUtils.cStrTN(rGstReg("GstRegType")), If(dic.ContainsKey("pos"), dic("pos"), Nothing), If(dic.ContainsKey("shipfrom"), dic("shipfrom"), Nothing))
                Else
                    rInv("sply_ty") = myFuncs2.FindSupplyTypeSale(rGstReg, myUtils.cStrTN(rInv("PartyGstRegType")), If(dic.ContainsKey("pos"), dic("pos"), Nothing))
                End If




                If myUtils.IsInList(myUtils.cStrTN(rInv("TransactionType")), "CNP", "DNP", "CNS", "DNS") Then
                    'Cdninvoiceid
                    Dim rCDN = Me.FindVouch(provider, CampusFilter, myUtils.cStrTN(rXL("origInvoiceNum")), myUtils.cStrTN(rXL("origInvoiceDate")), PartyFilter)
                    If Not rCDN Is Nothing Then
                        rInv("cdnInvoiceid") = myUtils.cValTN(rCDN("invoiceid"))
                        dic.Add("cdn", rCDN)
                        Dim r1 As DataRow = Me.FindCampus(myUtils.cValTN(rCDN("campusid")))
                        dic.Add("cdn.Campus", r1)
                        If r1 IsNot Nothing Then
                            Dim r2 As DataRow = Me.FindGstRegID(myUtils.cValTN(r1("gstregid")))
                            dic.Add("cdn.GstReg", r2)
                        End If
                    End If
                End If
            End If



        Catch ex As Exception
            oRet.AddException(ex)

        End Try
        Return oRet
    End Function

    Protected Overrides Function GenerateSQL(dicWhere As clsCollecString(Of String)) As clsCollecString(Of String)
        Dim dic As New clsCollecString(Of String)

        dic.Add("vouch", "select * from invoice where " & dicWhere("vouch"))
        dic.Add("item", "select * from InvoiceItemGst where " & dicWhere("item"))
        Return dic
    End Function

    Protected Overrides Function GenerateSQL(provider As IAppDataProvider, objGroup As clsRowGroup, objPortion As clsPortionInfo) As clsCollecString(Of String)
        Dim rXL = objGroup.Rows(0), dic = objGroup.dic
        Dim PartyType As String = Me.fncPartyType(rXL)
        Dim PartySubTypeTable As String = Me.fncPartySubTypeTable(rXL)
        Dim CampusFilter As String = If(dic.ContainsKey("campus"), myContext.SQL.GenerateSQLWhere(dic("campus"), "campusid"), "0=1")
        Dim FYFilter As String = "0=1"
        If dic.ContainsKey("returnperiod") Then
            Dim PostPeriodIDCSV = myUtils.MakeCSV(oMaster.PostPeriodTable.Select(myContext.SQL.GenerateSQLWhere(dic("returnperiod"), "finyearid")), "postperiodid")
            FYFilter = "returnperiodid in (" & PostPeriodIDCSV & ")"
        End If
        Dim CounterFilter As String = If(dic.ContainsKey("party"), myContext.SQL.GenerateSQLWhere(dic("party"), PartySubTypeTable & "ID"), PartySubTypeTable & "ID" & "=0")
        'An amendment of a previous vouchnum can be present once per month.
        Dim PPFilter As String = If(myUtils.IsInList(myUtils.cStrTN(rXL("InvoiceType")), "amendment") AndAlso dic.ContainsKey("returnperiod"), myContext.SQL.GenerateSQLWhereField(dic("returnperiod"), "postperiodid", "returnperiodid"), "")
        Dim InvoiceNumFilter = String.Format("invoicenum='{0}'", Me.CleanFilterString(myUtils.cStrTN(rXL("invoicenum"))))
        Dim strf1 As String = myUtils.CombineWhere(False, If(myUtils.IsInList(DocType, "IS"), CampusFilter, CounterFilter), InvoiceNumFilter, FYFilter, PPFilter)

        Dim dicWhere As New clsCollecString(Of String)
        dicWhere.Add("vouch", strf1)
        dicWhere.Add("item", "invoiceid in (select invoiceid from invoice where " & strf1 & ")")



        Return dicWhere
    End Function
    Public Overrides Function FindVouch(provider As IAppDataProvider, CampusFilter As String, InvoiceNum As String, strInvoiceDate As String, CounterFilter As String) As DataRow
        If (Not String.IsNullOrEmpty(InvoiceNum)) AndAlso (Not String.IsNullOrEmpty(strInvoiceDate)) Then
            Try
                Dim InvoiceDate As DateTime
                If IsNumeric(strInvoiceDate) Then
                    InvoiceDate = DateTime.FromOADate(myUtils.cValTN(strInvoiceDate))
                Else
                    InvoiceDate = Me.DateFromString(myUtils.cStrTN((strInvoiceDate)))
                End If
                Dim strf1 As String = myUtils.CombineWhere(False, "campusid in (select campusid from campus where " & CampusFilter & ")", "invoicenum='" & InvoiceNum & "'",
                                                "invoicedate='" & Format(InvoiceDate, "dd-MMM-yyyy") & "'", CounterFilter)

                Dim oDoc As New clsGSTInvoiceBase(myContext, Me.DocType, "")
                Dim sql As String = oDoc.LoadVouchSQL(strf1)
                Dim dt1 As DataTable = provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql).Tables(0)
                If dt1.Rows.Count > 0 Then Return dt1.Rows(0)
            Catch ex As Exception
                myContext.Logger.logInformation("Cannot find Invoice due to exception: " & ex.Message)
            End Try

        End If

    End Function

    Protected Overrides Function GenerateFilter() As String
        Return "isnull(gstin,'')<>''"
    End Function
    Public Overrides Async Function TryImportRowGroup(provider As IAppDataProvider, objGroup As clsRowGroup, objPortion As clsPortionInfo) As Task(Of clsProcOutput)
        Dim dsDB = objGroup.dsDB
        Dim oRet = Await Me.HandleGroupData(provider, objGroup, objPortion)
        'Have a new dataset ready for data to be saved from database and copy level 1 and level 2 rows into it
        Dim rInvoice = dsDB.Tables(0).Select()(0)

        If rInvoice.RowState = DataRowState.Modified OrElse rInvoice.RowState = DataRowState.Unchanged Then
            myContext.Logger.logInformation("Row Already present in DB. Nothing to be done.")

        Else
            Dim info As New ImportErrorInfo()
            If oRet.WarningCount > 0 Then info.Voucher.AddWarning(Me.DocumentExistsErrorCode, oRet.WarningMessage)

            'Do the pre-operations, like getting IDs
            oRet = Me.ExecutePreValidation(provider, rInvoice, dsDB.Tables("item"), objGroup.Rows(0), objGroup)
            If oRet.Success Then
                Dim dic = objGroup.dic
                For Each str1 As String In New String() {"campus", "gstreg", "party", "returnperiod", "returnperiodbefore", "pos", "shipfrom", "shipto", "orig", "cdn", "AmendVouchMy", "AmendVouchOrig", "GstRegPP"}
                    If Not dic.Exists(str1) Then dic.Add(str1, Nothing)
                Next

                Me.RunValidator(info, objGroup.Rows, rInvoice, dsDB, "item", Sub(obj, rItem)
                                                                                 If rItem Is Nothing Then
                                                                                     obj.SetValue("GSTUtils", New clsJINTFuncsGSTN(myContext))
                                                                                     For Each kvp In dic
                                                                                         obj.AddOrUpdateRow(kvp.Value, kvp.Key)
                                                                                     Next
                                                                                 Else
                                                                                     'For hsnsac table = hsnsac.rt
                                                                                     Dim rHSN = Me.FindHSN(provider, rItem)
                                                                                     obj.AddOrUpdateRow(rHSN, "hsnsac")
                                                                                 End If
                                                                             End Sub)
                If info.Errorcount = 0 Then
                    'If all OK, go ahead and save. If not OK, add validation errors obtained to a new error datatable with same schema as ds, but with Validation and Warning columns.
                    Dim InvoiceDescrip = "No. " & myUtils.cStrTN(rInvoice("invoicenum")) & " Dt. " & Format(rInvoice("invoicedate"), "dd-MMM-yyyy")
                    Dim oProc As TDoc = Activator.CreateInstance(GetType(TDoc), New Object() {provider})
                    oProc.oMaster = Me.oMaster
                    oProc.CalcVouchActionRP(dic("gstreg")("gstregid"), rInvoice("ReturnPeriodID"), rInvoice)
                    oProc.UpdateInvoiceNumberDynamicPart(dic("gstreg")("gstregid"), rInvoice, Me.dsMaster.Tables("template"))
                    oProc.PopulateCalc(myUtils.cValTN(rInvoice("invoiceid")), rInvoice, dic("gstreg"), dsDB.Tables("item"), Nothing, dic("cdn"), dic("orig"), Me.dsMaster)

                Else
                    If Not Me.ImportFileID = Guid.Empty Then
                        Dim nr = Me.CreateFileVouchRow(objPortion, rInvoice, dsDB, objGroup, info, Sub(r1)
                                                                                                       r1("vouchnum") = rInvoice("invoicenum")
                                                                                                       r1("dated") = rInvoice("invoicedate")
                                                                                                       r1("ctin") = objGroup.Rows(0)("ctin")
                                                                                                       r1("totalamount") = rInvoice("val")
                                                                                                   End Sub)
                    End If
                    oRet.AddError(info.ErrorDescripTot)
                End If

            Else
                oRet.AddError("Unforeseen error in pre validation")
                myContext.Logger.logInformation(oRet.Message & oRet.StackTrace)
                info.Voucher.AddError(Me.PreValidationErrorCode, "Unforeseen error in pre validation")
                Me.UpdateError(objGroup.Rows, info.Voucher)
            End If
        End If

        Return oRet


    End Function

End Class
