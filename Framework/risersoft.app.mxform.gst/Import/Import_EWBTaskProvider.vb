Imports Newtonsoft.Json
Imports risersoft.shared
Imports GSTN.API.Library.Models.GstNirvana
Imports risersoft.app.mxent

Public Class Import_EWBTaskProvider
    Inherits ImportTaskProviderGstBase
    Public Overrides Property DocName As String = "Ewaybill"
    Public Overrides Property DocType As String = "EWB"

    Public Overrides Property TemplateName As String = "Ewaybill"
    Public Overrides Property TemplateFunctionName As String = "GstListEWBTemplate()"
    Public Sub New(controller As clsAppController)
        MyBase.New(controller)

        Me.fncPartyType = Function(rXL)
                              Dim CounterPartyType = Me.GetPartyType(rXL)
                              Dim str1 = If(myUtils.IsInList(CounterPartyType, "Supplier"), "V", "C")
                              Return str1
                          End Function

        Me.fncPartySubTypeTable = Function(rXL)
                                      Dim CounterPartyType = Me.GetPartyType(rXL)
                                      Dim str1 = If(myUtils.IsInList(CounterPartyType, "Supplier"), "Vendor", "Customer")
                                      Return str1
                                  End Function

    End Sub

    Private Function GetPartyType(rXL As DataRow) As String
        Dim CounterPartyType = myUtils.cStrTN(rXL("CounterPartyType"))
        If String.IsNullOrEmpty(CounterPartyType) Then
            If rXL("supplytype") = "Outward" Then
                CounterPartyType = "Customer"
            Else
                CounterPartyType = "Supplier"
            End If
        End If
        Return CounterPartyType
    End Function



    Public Overrides Sub PopulateMaster()
        oMaster.GetDataset2(False)

        Dim dicSQL As New clsCollecString(Of String)
        dicSQL.Add("campus", "select * from Campus")
        dicSQL.Add("gstreg", "select * from gstreg")
        dicSQL.Add("taxarea", "select *, dbo.val(tincode) as tincodenum from taxarea")
        dicSQL.Add("co", "Select distinct countryname As country, countrycode + ' - ' + countryname, countrycode, countryname from unlocodesub where countrycode ='IN' order by countryname")
        dicSQL.Add("su", "select distinct subdivisionname as subdivision, subdivisioncode + ' - ' + subdivisionname, countrycode, SubDivisionCode, SubDivisionName from unlocodesub where countrycode='IN' order by countrycode, subdivisionname")
        dicSQL.Add("partygstRegType", myFuncsBase.CodeWordSQL("party", "gstRegType", ""))
        dicSQL.Add("rule", "select * from validationrule where doctype='" & Me.DocType & "' and isnull(isdisabled,0)=0")
        dicSQL.Add("options", "select * from systemoptions")
        dicSQL.Add("users", "select * from users")
        dicSQL.Add("SupplyType", myFuncsBase.CodeWordSQL("EWayBill", "SupplyType", ""))
        dicSQL.Add("SubSupplyType", myFuncsBase.CodeWordSQL("EWayBill", "SubSupplyType", ""))
        dicSQL.Add("transactiontype", myFuncsBase.CodeWordSQL("EWayBill", "TransactionType", ""))
        dicSQL.Add("DocType", myFuncsBase.CodeWordSQL("EWayBill", "DocumentType", ""))
        dicSQL.Add("TransMode", myFuncsBase.CodeWordSQL("EWayBill", "TransportationMode", ""))
        dicSQL.Add("VehicleType", myFuncsBase.CodeWordSQL("EWayBill", "VehicleType", ""))
        dicSQL.Add("CounterPartyType", myFuncsBase.CodeWordSQL("EWayBill", "CounterPartyType", ""))
        dsMaster = myContext.DataProvider.objSQLHelper.ExecuteDataset(CommandType.Text, dicSQL)

    End Sub

    Public Overrides Function ExecutePreValidation(provider As IAppDataProvider, rEWB As DataRow, dtItems As DataTable, rXL As DataRow, objGroup As clsRowGroup) As clsProcOutput
        Dim oRet As New clsProcOutput, LineSNum As Integer = 0
        Dim PartySubTypeTable = Me.fncPartySubTypeTable(rXL)
        Dim dic = objGroup.dic
        Try
            oRet = Me.ExecutePreValidationMaster(rEWB, dtItems, rXL, dic)
            Me.CalculateDate(rXL, rEWB, "docdate")
            For Each r1 As DataRow In dtItems.Select("", "linesnum")
                LineSNum = LineSNum + 1
                r1("linesnum") = LineSNum
            Next

            'actToStateCode,actFromStateCode
            Dim PartyStateCode As Integer = 0
            Dim CampusStateCode As Integer = myUtils.cValTN(Left(rXL("GSTIN"), 2))
            If myUtils.cStrTN(rXL("CTIN")).Trim.Length > 0 Then PartyStateCode = myUtils.cValTN(Left(rXL("CTIN"), 2))
            If rXL("supplytype") = "Outward" Then
                rEWB("actFromStateCode") = CampusStateCode
                Dim rPOS = myFuncs2.FindTaxAreaRow(dsMaster.Tables("taxarea"), myUtils.cStrTN(rXL("toStateCode")))
                rEWB("actToStateCode") = If(PartyStateCode > 0, PartyStateCode, If(rPOS IsNot Nothing, rPOS("tincode"), 0))
            Else
                Dim rPOS = myFuncs2.FindTaxAreaRow(dsMaster.Tables("taxarea"), myUtils.cStrTN(rXL("fromStateCode")))
                rEWB("actFromStateCode") = If(PartyStateCode > 0, PartyStateCode, If(rPOS IsNot Nothing, rPOS("tincode"), 0))
                rEWB("actToStateCode") = CampusStateCode
            End If


            'fromStateCode
            For Each str1 As String In New String() {"fromStateCode", "toStateCode"}
                If myUtils.NullNot(rXL(str1)) Then
                    rEWB(str1) = rEWB("act" & str1)
                ElseIf rXL.Table.Columns.Contains(str1) Then
                    Dim rPOS = myFuncs2.FindTaxAreaRow(dsMaster.Tables("taxarea"), myUtils.cStrTN(rXL(str1)))
                    If rPOS IsNot Nothing Then
                        rEWB(str1) = rPOS("tincode")
                    End If
                End If
            Next

            'VAL
            Dim rOptions = dsMaster.Tables("options").Rows(0)
            If (Not myUtils.cBoolTN(rOptions("DisableAutoCalcVal"))) AndAlso (myUtils.cValTN(rEWB("val")) = 0) Then
                For Each r1 As DataRow In dtItems.Select("", "linesnum")
                    rEWB("VAL") = Math.Round((myUtils.cValTN(rXL("TXVAL")) + myUtils.cValTN(rXL("IAMT")) +
                                             myUtils.cValTN(rXL("SAMT")) + myUtils.cValTN(rXL("CAMT")) + myUtils.cValTN(rXL("CSAMT"))), 2)
                Next
            End If

            'partygstregtype,TransactionType, SupplyType, SubSupplyType, doctype
            For Each str1 As String In New String() {"partygstRegType", "supplytype", "subsupplytype", "transactiontype", "doctype"}
                If rXL.Table.Columns.Contains(str1) Then
                    Dim rrPOS() As DataRow = dsMaster.Tables(str1).Select("Descrip='" & myUtils.cStrTN(rXL(str1)) & "'")
                    If rrPOS.Length > 0 Then rEWB(str1) = rrPOS(0)("codeword")
                End If
            Next


            'sply_ty
            If myUtils.cValTN(rXL("IAMT")) > 0 Then
                rEWB("Sply_Ty") = "INTER"
            Else
                rEWB("Sply_Ty") = "INTRA"
            End If

            'partinfo
            If myUtils.cStrTN(rXL("TransMode")).Trim.Length = 0 Then
                If myUtils.NullNot(rXL("TransporterID")) Then
                    rEWB("PartInfo") = "A1"
                Else
                    rEWB("PartInfo") = "A2"
                End If
            Else
                rEWB("PartInfo") = "B"
            End If

            'Dim oProc As New clsGSTInvoiceSale(provider)
            'rEWB("uniquekey") = oProc.CalcUniqueKey(rXL("gstin"), myUtils.cValTN(rEWB("returnperiodid")), rEWB("invoicenum"), 0)


        Catch ex As Exception
            oRet.AddException(ex)

        End Try
        Return oRet
    End Function

    Public Overrides Function ExecutePreValidationMaster(rVouch As DataRow, dtItems As DataTable, rXL As DataRow, dic As clsCollecString(Of DataRow)) As clsProcOutput
        Dim oRet As New clsProcOutput
        Dim PartySubTypeTable = Me.fncPartySubTypeTable(rXL)
        Try
            If dic.ContainsKey("campus") Then rVouch("campusid") = dic("Campus")("campusid")
            If dic.ContainsKey("party") Then rVouch(PartySubTypeTable & "id") = dic("party")(PartySubTypeTable & "id")
            If dic.ContainsKey("returnperiod") Then rVouch("returnperiodid") = dic("returnperiod")("postperiodid")
            If dic.ContainsKey("transporter") Then rVouch("TransVendorID") = dic("transporter")("VendorID")
            rVouch("StatusNum") = 2


        Catch ex As Exception
            oRet.AddException(ex)
        End Try
        Return oRet
    End Function

    Protected Overrides Function GenerateSQL(provider As IAppDataProvider, objGroup As clsRowGroup, objPortion As clsPortionInfo) As clsCollecString(Of String)
        Dim rXL = objGroup.Rows(0), dic = objGroup.dic
        Dim PartyType As String = Me.fncPartyType(rXL)
        Dim PartySubTypeTable As String = Me.fncPartySubTypeTable(rXL)

        Dim dicSQL As New clsCollecString(Of String)
        Dim CampusFilter As String = If(dic.ContainsKey("campus"), myContext.SQL.GenerateSQLWhere(dic("campus"), "campusid"), "0=1")
        Dim FYFilter As String = "0=1"
        If dic.ContainsKey("returnperiod") Then
            Dim PostPeriodIDCSV = myUtils.MakeCSV(oMaster.PostPeriodTable.Select(myContext.SQL.GenerateSQLWhere(dic("returnperiod"), "finyearid")), "postperiodid")
            FYFilter = "returnperiodid in (" & PostPeriodIDCSV & ")"
        End If

        Dim CounterFilter As String = If(dic.ContainsKey("party"), myContext.SQL.GenerateSQLWhere(dic("party"), PartySubTypeTable & "ID"), $"isnull({PartySubTypeTable}ID,0)=0")
        Dim DocNumFilter = String.Format("docnum='{0}'", Me.CleanFilterString(myUtils.cStrTN(rXL("docnum"))))
        Dim DocTypeFilter As String
        Dim rrPOS() As DataRow = dsMaster.Tables("doctype").Select("Descrip='" & myUtils.cStrTN(rXL("doctype")) & "'")
        If rrPOS.Length > 0 Then
            DocTypeFilter = String.Format("doctype='{0}'", Me.CleanFilterString(myUtils.cStrTN(rrPOS(0)("codeword"))))
        Else
            DocTypeFilter = "0=1"
        End If

        Dim strf1 As String = myUtils.CombineWhere(False, CampusFilter, CounterFilter, DocNumFilter, DocTypeFilter, FYFilter)
        dicSQL.Add("vouch", "select * from EwayBill where " & strf1)
        dicSQL.Add("item", "select * from EwayBillItem where EwayBillid in (select EwayBillid from EwayBill where " & strf1 & ")")
        dicSQL.Add("vehicle", "select * from EwayBillVehicle where EwayBillid in (select EwayBillid from EwayBill where " & strf1 & ")")

        Return dicSQL
    End Function

    Public Overrides Function FindVouch(provider As IAppDataProvider, CampusFilter As String, DocNum As String, strDocDate As String, CounterFilter As String) As DataRow
        If (Not String.IsNullOrEmpty(DocNum)) AndAlso (Not String.IsNullOrEmpty(strDocDate)) Then
            Try
                Dim DocDate As DateTime
                If IsNumeric(strDocDate) Then
                    DocDate = DateTime.FromOADate(myUtils.cValTN(strDocDate))
                Else
                    DocDate = Me.DateFromString(myUtils.cStrTN((strDocDate)))
                End If
                Dim strf1 As String = myUtils.CombineWhere(False, "campusid in (select campusid from campus where " & CampusFilter & ")", "DocNum='" & DocNum & "'",
                                                "DocDate='" & Format(DocDate, "dd-MMM-yyyy") & "'", CounterFilter)

                Dim sql As String = "select * from ewaybill where " & strf1
                Dim dt1 As DataTable = provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql).Tables(0)
                If dt1.Rows.Count > 0 Then Return dt1.Rows(0)
            Catch ex As Exception
                myContext.Logger.logInformation("Cannot find Ewaybill due to exception: " & ex.Message)
            End Try

        End If

    End Function

    Protected Overrides Async Function HandleGroupData(provider As IAppDataProvider, objGroup As clsRowGroup, objPortion As clsPortionInfo) As Task(Of clsProcOutput)
        Dim oRet As New clsProcOutput
        Dim rXL = objGroup.Rows(0)
        Await Me.GetMasterData(provider, objGroup, objPortion)
        objGroup.dicSQL.Clear()
        Dim dic2 = Me.GenerateSQL(provider, objGroup, objPortion)
        dic2.CopyTo(objGroup.dicSQL)
        Dim dsDB = provider.objSQLHelper.ExecuteDataset(CommandType.Text, objGroup.dicSQL)
        Me.CheckAddOpInfo(provider, objGroup.dicSQL)

        If dsDB.Tables("vouch").Rows.Count > 0 Then

            Dim rVouch = dsDB.Tables("vouch").Rows(0)
            If myUtils.cStrTN(rVouch("ewaybillnum")).Trim.Length > 0 Then
                oRet.AddError($"Eway bill no. {rVouch("ewaybillnum")} already generated")
            Else
                'For before return period- returnperiodbefore, usable when file is uploaded for editing.
                Dim rPP = oMaster.rPostPeriod(myUtils.cValTN(dsDB.Tables("vouch").Rows(0)("returnperiodid")))
                objGroup.dic.Add("returnperiodbefore", rPP)

                For Each dt1 As DataTable In dsDB.Tables
                    myUtils.DeleteRows(dt1.Select)
                Next
                oRet.AddWarning("Duplicate Record")

            End If
        End If

        If oRet.Success Then
            Dim nr = myUtils.CopyOneRow(rXL, dsDB.Tables("vouch"))
            nr("importfileid") = Me.ImportFileID
            For Each r1 As DataRow In objGroup.Rows
                Dim nr2 As DataRow = myUtils.CopyOneRow(r1, dsDB.Tables("item"))
                Me.UpdateItem(r1, nr2)

                Dim nr3 As DataRow = myUtils.CopyOneRow(r1, dsDB.Tables("vehicle"))
                Me.UpdateItem(r1, nr3)
            Next

        End If
        oRet.Data = dsDB
        Return oRet
    End Function

    Public Overrides Async Function GetMasterData(provider As IAppDataProvider, objGroup As clsRowGroup, objPortion As clsPortionInfo) As Task(Of Boolean)
        Dim rXL = objGroup.Rows(0), dic = objGroup.dic
        Dim PartyType As String = Me.fncPartyType(rXL)
        Dim PartySubTypeTable As String = Me.fncPartySubTypeTable(rXL)
        Dim dicParty As New clsCollecString(Of String), dicTrans As New clsCollecString(Of String)
        Dim rGstReg As DataRow = Me.FindGSTIN(myUtils.cStrTN(rXL("gstin")))
        If rGstReg IsNot Nothing Then
            dic.Add("gstreg", rGstReg)
            'Campusid
            Dim rCampus As DataRow = Me.FindGstRegCampus(myUtils.cValTN(rGstReg("gstregid")))
            If rCampus IsNot Nothing Then
                dic.Add("campus", rCampus)
            End If

        End If

        'Customerid / Vendorid
        dicParty.Add("Address", myUtils.cStrTN(rXL("CounterPartyAddress")))
        dicParty.Add("City", myUtils.cStrTN(rXL("CounterPartyCity")))
        dicParty.Add("PinCode", myUtils.cStrTN(rXL("CounterPartyPincode")))
        Dim oRet = Await Me.GetOrAddParty(provider, myUtils.cStrTN(rXL("ctin")), myUtils.cStrTN(rXL("PartyGstRegType")), myUtils.cStrTN(rXL("partyname")), myUtils.cStrTN(rXL("PartyTaxAreaCode")), "", PartySubTypeTable, PartyType, If(myUtils.IsInList(PartyType, "V"), "MS", ""), dicParty)
        If oRet.Success Then
            Dim dt1 As DataTable = myContext.Data.outerJoin(oRet.Data, "party")
            dic.Add("party", dt1.Rows(0))
        End If

        'TransVendorID
        'dicTrans.Add(,)
        If Not String.IsNullOrEmpty(myUtils.cStrTN(rXL("TransporterID"))) Then
            Dim TinCode As Integer = myUtils.cValTN(Left(rXL("TransporterID"), 2))
            Dim rPOS = myFuncs2.FindTaxAreaRow(dsMaster.Tables("taxarea"), TinCode)
            If rPOS IsNot Nothing Then
                Dim TaxAreaCode As String = myUtils.cStrTN(rPOS("TaxAreaCode"))

                Dim oRet2 = Await Me.GetOrAddParty(provider, myUtils.cStrTN(rXL("TransporterID")), "R", myUtils.cStrTN(rXL("TransporterName")), TaxAreaCode, "", "Vendor", "V", "TR", dicTrans)
                If oRet2.Success Then
                    Dim dt1 As DataTable = myContext.Data.outerJoin(oRet2.Data, "party")
                    dic.Add("transporter", dt1.Rows(0))
                End If
            End If
        End If

        Try
            oRet = Me.CalculateDate(myUtils.cStrTN(rXL("docdate")))
            If oRet.Success Then
                Dim rPostPeriod = oMaster.rPostPeriod(oRet.Dated)
                If (rGstReg IsNot Nothing) AndAlso (rPostPeriod IsNot Nothing) Then
                    dic.Add("returnperiod", rPostPeriod)
                    Dim rGstPP = oMaster.GstRegPPRow(rGstReg("gstregid"), myUtils.cValTN(rPostPeriod("PostPeriodid")))
                    dic.Add("GstRegPP", rGstPP)
                End If

            End If
        Catch ex As Exception
            myContext.Logger.logInformation("Could Not Get Posting Period for " & myUtils.cStrTN(rXL("returnperiod")) & ": " & ex.Message)
        End Try

        Return True
    End Function

    Protected Overrides Function GenerateFilter() As String
        Return "isnull(gstin,'')<>''"
    End Function

    Public Overrides Async Function ExecuteServer(rTask As DataRow, input As clsProcOutput) As Task(Of clsProcOutput)
        Dim Params = JsonConvert.DeserializeObject(Of Dictionary(Of String, String))(myUtils.cStrTN(rTask("infojson")))
        Dim path = Await myFuncs.DownloadIfReqd(myContext, rTask, Params("path"))
        Dim oRet = Await Me.ExecuteImport(path, myUtils.cStrTN(rTask("username")), myUtils.cStrTN(rTask("importfileid")))
        Return oRet
    End Function

    Public Overrides Async Function TryImportRowGroup(provider As IAppDataProvider, objGroup As clsRowGroup, objPortion As clsPortionInfo) As Task(Of clsProcOutput)
        Dim oRet = Await Me.HandleGroupData(provider, objGroup, objPortion)
        Dim info As New ImportErrorInfo()
        If oRet.Success Then
            If oRet.WarningCount > 0 Then info.Voucher.AddWarning(Me.DocumentExistsErrorCode, oRet.WarningMessage)
            'Have a new dataset ready for data to be saved from database and copy level 1 and level 2 rows into it
            Dim dsDB = oRet.Data
            Dim dic = objGroup.dic
            Dim rEWB = dsDB.Tables(0).Select()(0)
            Dim rEWBVeh = dsDB.Tables("vehicle").Select()(0)
            'Do the pre-operations, like getting IDs
            oRet = Me.ExecutePreValidation(provider, rEWB, dsDB.Tables("item"), objGroup.Rows(0), objGroup)
            If oRet.Success Then
                For Each str1 As String In New String() {"campus", "gstreg", "party", "transporter", "returnperiod", "returnperiodbefore"}
                    If Not dic.Exists(str1) Then dic.Add(str1, Nothing)
                Next

                Me.RunValidator(info, objGroup.Rows, rEWB, dsDB, "item", Sub(oProc, rItem)
                                                                             If rItem Is Nothing Then
                                                                                 oProc.AddOrUpdateRow(rEWBVeh, "vehicle")
                                                                                 oProc.SetValue("GSTUtils", New clsJINTFuncsGSTN(myContext))
                                                                                 For Each kvp In dic
                                                                                     oProc.AddOrUpdateRow(kvp.Value, kvp.Key)
                                                                                 Next
                                                                             End If
                                                                         End Sub)
                If info.Errorcount = 0 Then
                    'If all OK, go ahead and save. If not OK, add validation errors obtained to a new error datatable with same schema as ds, but with Validation and Warning columns.
                    Dim EWBDescrip = "No. " & myUtils.cStrTN(rEWB("docnum")) & " Dt. " & Format(rEWB("docdate"), "dd-MMM-yyyy")
                    Try

                        Dim oProc As New clsGSTEWB(provider)
                        oProc.PopulateCalc(myUtils.cValTN(rEWB("EwayBillid")), rEWB, dic("gstreg"), dsDB.Tables("item"), Nothing, Nothing, Nothing, Me.dsMaster)

                        'same as in model of ewaybill
                        provider.dbConn.BeginTransaction(provider, Me.GetType.Name, EnumfrmMode.acAddM, "EwayBillid")

                        For Each str1 As String In New String() {"vehicle", "item", "vouch"}
                            provider.objSQLHelper.SaveResults(dsDB.Tables(str1), objGroup.dicSQL(str1), dicOpInfo(str1), False, "0=1")
                        Next

                        provider.objSQLHelper.SaveResults(dsDB.Tables("vouch"), objGroup.dicSQL("vouch"), dicOpInfo("vouch"))

                        myUtils.ChangeAll(dsDB.Tables("item").Select, "EwayBillid=" & rEWB("EwayBillid"))
                        provider.objSQLHelper.SaveResults(dsDB.Tables("item"), objGroup.dicSQL("item"), dicOpInfo("item"))

                        myUtils.ChangeAll(dsDB.Tables("vehicle").Select, "EwayBillid=" & rEWB("EwayBillid"))
                        provider.objSQLHelper.SaveResults(dsDB.Tables("vehicle"), objGroup.dicSQL("vehicle"), dicOpInfo("vehicle"))

                        provider.dbConn.CommitTransaction(EWBDescrip, rEWB("EwayBillid"))
                    Catch ex As Exception
                        oRet.AddException(ex)
                        provider.dbConn.RollBackTransaction(EWBDescrip, ex.Message, False)
                        Dim obj = info.Voucher.AddException(Me.DatabaseTransactionErrorCode, ex)
                        Me.UpdateError(objGroup.Rows, info.Voucher)
                    End Try
                Else
                    If Not Me.ImportFileID = Guid.Empty Then
                        Dim nr = Me.CreateFileVouchRow(objPortion, rEWB, dsDB, objGroup, info, Sub(r1)
                                                                                                   r1("vouchnum") = rEWB("DocNum")
                                                                                                   r1("dated") = rEWB("DocDate")
                                                                                                   r1("ctin") = objGroup.Rows(0)("ctin")
                                                                                                   r1("totalamount") = rEWB("val")
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
        Else
            info.Voucher.AddError(Me.PreValidationErrorCode, oRet.Message)
            Me.UpdateError(objGroup.Rows, info.Voucher)
        End If
        Return oRet


    End Function

    Protected Overrides Sub UpdateItem(rSource As DataRow, rDest As DataRow)
        For Each str1 As String In New String() {"TransMode", "VehicleType"}
            If rDest.Table.Columns.Contains(str1) Then
                Dim rrPOS() As DataRow = dsMaster.Tables(str1).Select("Descrip='" & myUtils.cStrTN(rSource(str1)) & "'")
                If rrPOS.Length > 0 Then rDest(str1) = rrPOS(0)("codeword")
            End If
        Next

        If rDest.Table.Columns.Contains("TransDocDate") Then Me.CalculateDate(rSource, rDest, "TransDocDate")

        If rDest.Table.Columns.Contains("basicrate") Then
            rDest("basicrate") = If(myUtils.cValTN(rDest("qty")) > 0, myUtils.cValTN(rSource("TXVAL")) / myUtils.cValTN(rDest("qty")), 0)
        End If

    End Sub

    Public Overrides Function GenerateTemplate(CampusFilter As String, PeriodFilter As String, fileName As String, strFilter As String, SheetName As String) As clsProcOutput
        Dim Sql = String.Format("select * from {0}", TemplateFunctionName) & myUtils.CombineWhere(True,
                                 CampusFilter, PeriodFilter, strFilter)
        Return Me.GenerateTemplate(fileName, Sql, SheetName)
    End Function

End Class
