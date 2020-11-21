Imports Newtonsoft.Json
Imports risersoft.shared
Imports GSTN.API.Library.Models.GstNirvana
Imports risersoft.app.mxent

Public Class Import_TPTaskProvider
    Inherits ImportTaskProviderGstBase
    Public Overrides Property DocName As String = "TaxPayer"

    Public Sub New(controller As clsAppController)
        MyBase.New(controller)
    End Sub

    Public Overrides Property DocType As String = "TP"

    Public Overrides Property TemplateName As String = "CompanyAndCampus"
    Public Overrides Property TemplateFunctionName As String = "mmListCampus()"

    Public Overrides Async Function TryImportRowGroup(provider As IAppDataProvider, objGroup As clsRowGroup, objPortion As clsPortionInfo) As Task(Of clsProcOutput)

        Dim oRet = Await Me.HandleGroupData(provider, objGroup, objPortion)
        Dim info As New ImportErrorInfo()
        If oRet.WarningCount > 0 Then info.Voucher.AddWarning(Me.DocumentExistsErrorCode, oRet.WarningMessage)
        'Have a new dataset ready for data to be saved from database and copy level 1 and level 2 rows into it
        Dim dsDB = oRet.Data
        Dim rVouch = dsDB.Tables(0).Select()(0)
        Dim dic = objGroup.dic

        If objGroup.Rows.Count > 1 Then
            oRet.AddError("Multiple Records found from Same PanNum and GSTIN")
            info.Voucher.AddError(Me.PreValidationErrorCode, "Multiple Records found from Same PanNum and GSTIN")
            Me.UpdateError(objGroup.Rows, info.Voucher)
        Else
            'Do the pre-operations, like getting IDs
            oRet = Me.ExecutePreValidation(provider, rVouch, dsDB.Tables("party"), objGroup.Rows(0), objGroup)
            If oRet.Success Then
                Dim dt1 As DataTable = myContext.Data.outerJoin(rVouch.Table.DataSet, "tp")
                Me.RunValidator(info, objGroup.Rows, dt1.Rows(0), dsDB, "", Sub(obj, rItem)
                                                                                If rItem Is Nothing Then
                                                                                    obj.SetValue("GSTUtils", New clsJINTFuncsGSTN(myContext))
                                                                                    For Each kvp In dic
                                                                                        obj.AddOrUpdateRow(kvp.Value, kvp.Key)
                                                                                    Next
                                                                                End If
                                                                            End Sub)
                If info.Errorcount = 0 Then
                    'If all OK, go ahead and save. If not OK, add validation errors obtained to a new error datatable with same schema as ds, but with Validation and Warning columns.
                    Dim VouchDescrip = "Name. " & rVouch("MPName") & ""
                    Try
                        provider.dbConn.BeginTransaction(provider, Me.GetType.Name, EnumfrmMode.acAddM, "MainPartyID")

                        For Each str1 As String In New String() {"partymain", "company", "gstreg", "division", "party", "campus", "customer", "vendor"}
                            provider.objSQLHelper.SaveResults(dsDB.Tables(str1), objGroup.dicSQL(str1), dicOpInfo(str1), False)
                        Next

                        Dim rXL = objGroup.Rows(0)
                        If myUtils.IsInList(myUtils.cStrTN(rXL("HOCampus")), "YES", "Y", "yes") OrElse myUtils.NullNot(dsDB.Tables("company").Rows(0)("HOCampusID")) Then
                            dsDB.Tables("company").Rows(0)("HOCampusID") = dsDB.Tables("campus").Rows(0)("CampusID")
                            dsDB.Tables("gstreg").Rows(0)("IsHeadOffice") = True
                            provider.objSQLHelper.SaveResults(dsDB.Tables("company"), "Select * from Company")
                            provider.objSQLHelper.SaveResults(dsDB.Tables("gstreg"), "Select * from GSTReg")
                        End If

                        provider.dbConn.CommitTransaction(VouchDescrip, rVouch("MainPartyID"))
                    Catch ex As Exception
                        oRet.AddException(ex)
                        provider.dbConn.RollBackTransaction(VouchDescrip, ex.Message, False)
                        Dim obj = info.Voucher.AddException(Me.DatabaseTransactionErrorCode, ex)
                        Me.UpdateError(objGroup.Rows, info.Voucher)
                    End Try


                Else
                    If Not Me.ImportFileID = Guid.Empty Then
                        Dim nr = Me.CreateFileVouchRow(objPortion, rVouch, dsDB, objGroup, info, Sub(r1)
                                                                                                     r1("vouchnum") = objGroup.Rows(0)("GSTIN")
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

    Public Overrides Function ExecutePreValidation(provider As IAppDataProvider, rInv As DataRow, dtItems As DataTable, rXL As DataRow, objGroup As clsRowGroup) As clsProcOutput
        Dim oRet As New clsProcOutput
        'Conversion of lookup values
        Dim dic = objGroup.dic
        Try
            Dim rGstReg = dtItems.DataSet.Tables("gstreg").Rows(0)
            rGstReg("partialcredit") = If(myUtils.IsInList(myUtils.cStrTN(rXL("partialcredit")), "yes", "y"), 1, 0)
            Me.CalculateDate(rXL, rGstReg, "RegDate")
            Dim rCompany = dtItems.DataSet.Tables("company").Rows(0)
            Me.CalculateDate(rXL, rCompany, "FinStartDate")
            Dim rParty = dtItems.DataSet.Tables("party").Rows(0)
            For Each str2 As String In New String() {"partygstregtype"}
                Dim rrPOS() As DataRow = dsMaster.Tables(str2).Select("Descrip='" & rXL("GstRegTypeDescrip") & "'")
                If rrPOS.Length > 0 Then
                    rParty("GSTRegType") = rrPOS(0)("codeword")
                    rGstReg("GSTRegType") = rrPOS(0)("codeword")
                ElseIf String.IsNullOrEmpty(rXL("GSTIN")) Then
                    rParty("GSTRegType") = "N"
                    rGstReg("GSTRegType") = "N"
                Else
                    rParty("GSTRegType") = "R"
                    rGstReg("GSTRegType") = "R"
                End If
            Next

        Catch ex As Exception
            oRet.AddException(ex)

        End Try
        Return oRet
    End Function

    Public Overrides Function FindVouch(provider As IAppDataProvider, CampusFilter As String, InvoiceNum As String, strInvoiceDate As String, CounterFilter As String) As DataRow

    End Function

    Public Overrides Async Function ExecuteServer(rTask As DataRow, input As clsProcOutput) As Task(Of clsProcOutput)
        Dim Params = JsonConvert.DeserializeObject(Of Dictionary(Of String, String))(myUtils.cStrTN(rTask("infojson")))
        Dim path = Await myFuncs.DownloadIfReqd(myContext, rTask, Params("path"))
        Dim oRet = Await Me.ExecuteImport(path, myUtils.cStrTN(rTask("username")), myUtils.cStrTN(rTask("importfileid")))
        Return oRet
    End Function

    Protected Overrides Function GenerateSQL(provider As IAppDataProvider, objGroup As clsRowGroup, objPortion As clsPortionInfo) As clsCollecString(Of String)
        Dim dicSQL As New clsCollecString(Of String)
        Dim rXL = objGroup.Rows(0), dic = objGroup.dic

        Dim DispNameFilter = String.Format("DispName='{0}'", Me.CleanFilterString(myUtils.cStrTN(rXL("DispName"))))
        Dim GSTINFilter = String.Format("GSTIN='{0}'", Me.CleanFilterString(myUtils.cStrTN(rXL("GSTIN"))))
        Dim PanNumFilter = String.Format("PanNum='{0}'", Me.CleanFilterString(myUtils.cStrTN(rXL("PanNum"))))
        dicSQL.Add("partymain", "Select * from PartyMain where mainpartyid in (select mainpartyid from company where " & PanNumFilter & ")")
        dicSQL.Add("company", "Select * from Company where " & PanNumFilter & "")
        dicSQL.Add("gstreg", "Select * from GSTReg where " & GSTINFilter & "")
        Dim strf1 As String = myUtils.CombineWhere(False, DispNameFilter, "GstRegID in (Select gstregid from GSTReg where " & GSTINFilter & ")")
        dicSQL.Add("campus", $"Select * from campus where " & strf1 & "")
        dicSQL.Add("division", $"Select * from division where companyid in (Select companyid from Company where " & PanNumFilter & ")")
        dicSQL.Add("party", $"Select * from Party where PartyID in (select PartyID from campus where " & strf1 & ")")
        dicSQL.Add("customer", $"Select * from customer where PartyId in (Select PartyId from campus where " & strf1 & ")")
        dicSQL.Add("vendor", $"Select * from vendor where PartyId in (Select PartyId from campus where " & strf1 & ")")

        Return dicSQL

    End Function

    Protected Overrides Function HandleGroupData(provider As IAppDataProvider, objGroup As clsRowGroup, objPortion As clsPortionInfo) As Task(Of clsProcOutput)
        Dim oRet As New clsProcOutput
        Dim rXL = objGroup.Rows(0)
        Dim dic = objGroup.dic

        objGroup.dicSQL.Clear()
        Dim dic2 = Me.GenerateSQL(provider, objGroup, objPortion)
        dic2.CopyTo(objGroup.dicSQL)

        Dim DispName As String = myUtils.cStrTN(rXL("DispName")).Replace("'", "")
        Dim GSTIN As String = myUtils.cStrTN(rXL("GSTIN")).Replace("'", "")
        Dim PanNum As String = myUtils.cStrTN(rXL("PanNum")).Replace("'", "")
        Dim dsDB = provider.objSQLHelper.ExecuteDataset(CommandType.Text, objGroup.dicSQL)
        Me.CheckAddOpInfo(provider, objGroup.dicSQL)

        'taxpayer Import
        myContext.Tables.SetAuto(dsDB.Tables("partymain"), dsDB.Tables("company"), "mainpartyid", "_company")
        myContext.Tables.SetAuto(dsDB.Tables("partymain"), dsDB.Tables("party"), "mainpartyid", "_party")
        myContext.Tables.SetAuto(dsDB.Tables("company"), dsDB.Tables("gstreg"), "companyid")
        myContext.Tables.SetAuto(dsDB.Tables("gstreg"), dsDB.Tables("campus"), "gstregid")
        myContext.Tables.SetAuto(dsDB.Tables("company"), dsDB.Tables("division"), "companyid", "_division")
        myContext.Tables.SetAuto(dsDB.Tables("party"), dsDB.Tables("campus"), "partyid", "_Campus")
        myContext.Tables.SetAuto(dsDB.Tables("party"), dsDB.Tables("customer"), "partyid", "_customer")
        myContext.Tables.SetAuto(dsDB.Tables("party"), dsDB.Tables("vendor"), "partyid", "_vendor")
        myContext.Tables.SetAuto(dsDB.Tables("company"), dsDB.Tables("campus"), "companyid", "_Campus")

        Dim rr1() As DataRow = dsDB.Tables("partymain").Select()
        Dim rr2() As DataRow = dsDB.Tables("party").Select("PanNum='" & PanNum & "'")
        Dim rr3() As DataRow = If(rr2.Length > 0, dsDB.Tables("campus").Select("partyid=" & rr2(0)("partyid")), New DataRow() {})
        Dim rr4() As DataRow = dsDB.Tables("company").Select("PanNum='" & PanNum & "'")
        Dim rr5() As DataRow = dsDB.Tables("gstreg").Select("GSTIN='" & GSTIN & "'")
        Dim rr6() As DataRow = If(rr2.Length > 0, dsDB.Tables("customer").Select("partyid=" & rr2(0)("partyid")), New DataRow() {})
        Dim rr7() As DataRow = If(rr2.Length > 0, dsDB.Tables("vendor").Select("partyid=" & rr2(0)("partyid")), New DataRow() {})
        Dim rr8() As DataRow = If(rr4.Length > 0, dsDB.Tables("division").Select("companyid=" & rr4(0)("companyid")), New DataRow() {})

        Dim rPartyMain, rParty, rPartySubType, rdivision, rcompany, rGstReg, rcustomer, rvendor As DataRow
        Dim UpdateAddress As Boolean = False
        If rr1.Length > 0 Then
            rPartyMain = rr1(0)
            If myUtils.IsInList(myUtils.cStrTN(rXL("HOCampus")), "y", "yes") Then UpdateAddress = True
        Else
            rPartyMain = myContext.Tables.AddNewRow(dsDB.Tables("partymain"))
            UpdateAddress = True
        End If
        If UpdateAddress Then
            rPartyMain("countrycode") = "IN"
            rPartyMain("MPName") = rXL("CompName")
            rPartyMain("Address") = rXL("SelAddress")
            rPartyMain("partytype") = "A"
            rPartyMain("PinCode") = rXL("SelPinCode")
            rPartyMain("City") = rXL("SelCity")
            rPartyMain("State") = rXL("SelState")

        End If
        rPartyMain("TurnOver") = rXL("TurnOver")
        Dim rrSub() As DataRow = dsMaster.Tables("Su").Select("subdivisioncode='" & rXL("SelState") & "'")
        If rrSub.Length > 0 Then
            rPartyMain("state") = rrSub(0)("subdivisionname")
            rPartyMain("statecode") = rrSub(0)("subdivisioncode")
        End If
        Dim rrSub1() As DataRow = dsMaster.Tables("co").Select("country='" & rXL("SelCountry") & "'")
        If rrSub1.Length > 0 Then
            rPartyMain("country") = rXL("SelCountry")
            rPartyMain("countrycode") = rrSub1(0)("countrycode")
        End If

        'party
        If rr2.Length > 0 Then
            rParty = rr2(0)
        Else
            rParty = myContext.Tables.AddNewRow(dsDB.Tables("party"))
            For Each str2 As String In New String() {"partygstregtype"}
                Dim rrGstRegType() As DataRow = dsMaster.Tables(str2).Select("Descrip='" & rXL("GSTRegTypeDescrip") & "'")
                If rrGstRegType.Length > 0 Then
                    rParty("GSTRegType") = rrGstRegType(0)("codeword")
                ElseIf String.IsNullOrEmpty(GSTIN) Then
                    rParty("GSTRegType") = "N"
                Else
                    rParty("GSTRegType") = "R"
                End If
            Next
            rParty("gstin") = GSTIN
            rParty("mainpartyid") = rPartyMain("mainpartyid")
            rParty("PartyName") = rXL("CompName")
            Dim objNum As New clsVoucherNum(provider)
            rParty("partycode") = objNum.GetNewSerialNo("PartyID", myUtils.cStrTN(rPartyMain("partytype")), rParty)
        End If
        rParty("SelAddress") = rXL("SelAddress")
        rParty("SelPinCode") = rXL("SelPinCode")
        rParty("SelCity") = rXL("SelCity")
        rParty("Pannum") = rXL("Pannum")
        If rrSub.Length > 0 Then
            rParty("selstate") = rrSub(0)("subdivisionname")
            rParty("selstatecode") = rrSub(0)("subdivisioncode")
        End If
        If rrSub1.Length > 0 Then
            rParty("selcountry") = rXL("SelCountry")
            rParty("selcountrycode") = rrSub1(0)("countrycode")
        End If

        Dim rTA = myFuncs2.FindTaxAreaRow(dsMaster.Tables("taxarea"), myUtils.cStrTN(rXL("SelState")))
        If rTA Is Nothing Then
            oRet.AddError("State not found")
        Else
            rParty("taxareaid") = rTA("taxareaid")
            rParty("SelState") = rTA("Descrip")
            rParty("SelStateCode") = rTA("TaxAreaCode")
        End If
        myFuncsBase.SetPartyNameAddress(rPartyMain, dsDB.Tables("party"), False)


        'company
        If rr4.Length > 0 Then
            rcompany = rr4(0)
        Else
            rcompany = myContext.Tables.AddNewRow(dsDB.Tables("company"))
            rcompany("PanNum") = PanNum
            rcompany("mainpartyid") = rPartyMain("mainpartyid")
            rcompany("compname") = rXL("compname")
        End If

        'gstreg
        If rr5.Length > 0 Then
            rGstReg = rr5(0)
        Else
            rGstReg = myContext.Tables.AddNewRow(dsDB.Tables("gstreg"))
            rGstReg("gstin") = GSTIN
            rGstReg("companyid") = rcompany("companyid")
        End If

        rGstReg("GSTNUserId") = rXL("GSTNUserId")
        rGstReg("GstRegType") = rParty("GSTRegType")
        rGstReg("TurnOver") = rXL("TurnOver")
        Me.CalculateDate(rXL, rGstReg, "RegDate")

        Dim rTA1 = myFuncs2.FindTaxAreaRow(dsMaster.Tables("taxarea"), myUtils.cStrTN(rXL("SelState")))
        If rTA1 Is Nothing Then
            oRet.AddError("State not found")
        Else
            rGstReg("taxareaid") = rTA1("taxareaid")
        End If


        'campus
        If rr3.Length > 0 Then
            rPartySubType = rr3(0)
        Else
            rPartySubType = myContext.Tables.AddNewRow(dsDB.Tables("campus"))
            rPartySubType("partyid") = rParty("partyid")
            rPartySubType("Campuscode") = rParty("partycode")
            rPartySubType("CompanyID") = rcompany("CompanyID")
            rPartySubType("gstregid") = rGstReg("gstregid")
        End If
        rPartySubType("DispName") = rXL("DispName")
        rPartySubType("TCName") = rXL("DispName")
        For Each str1 As String In New String() {"campustype"}
            Dim rrPOS() As DataRow = dsMaster.Tables(str1).Select("Descrip='" & myUtils.cStrTN(rXL(str1)) & "'")
            If rrPOS.Length > 0 Then
                rPartySubType(str1) = rrPOS(0)("codeword")
            Else
                rPartySubType(str1) = DBNull.Value
            End If
        Next

        'Divisionid
        If rr8.Length = 0 Then
            'create division
            rdivision = myContext.Tables.AddNewRow(dsDB.Tables("division"))
            rdivision("CompanyID") = rcompany("CompanyID")
            rdivision("divisioncode") = "Default"
            rdivision("divisionname") = "Default"
        End If

        If rPartySubType IsNot Nothing AndAlso rdivision IsNot Nothing Then
            'Dim rrDivision() As DataRow = dsMaster.Tables("division").Select("companyid=" & rPartySubType("companyid"))
            rPartySubType("divisioncodelist") = rdivision("divisioncode")
        End If

        rPartySubType("importfileid") = Me.ImportFileID

        'customer
        If rr6.Length > 0 Then
            rcustomer = rr6(0)
        Else
            rcustomer = myContext.Tables.AddNewRow(dsDB.Tables("customer"))
            rcustomer("partyid") = rParty("partyid")
            rcustomer("CustomerCode") = rParty("partycode")
        End If

        'vendor
        If rr7.Length > 0 Then
            rvendor = rr7(0)
        Else
            rvendor = myContext.Tables.AddNewRow(dsDB.Tables("vendor"))
            rvendor("partyid") = rParty("partyid")
            rvendor("VendorCode") = rParty("partycode")
        End If

        Try
            Dim Dated As DateTime = rGstReg("regdate")
            While Dated < Now.Date
                Dim rPP As DataRow = oMaster.rPostPeriod(Dated)
                Dim rPPGst As DataRow = oMaster.GstRegPPRow(rGstReg("gstregid"), rPP("postperiodid"))
                Dated = Dated.AddMonths(1)
            End While
        Catch ex As Exception
            Trace.WriteLine(ex.Message)
        End Try

        oRet.Data = dsDB
        Return Task.FromResult(oRet)
    End Function

    Protected Overrides Function GenerateFilter() As String
        Return "isnull(dispname,'')<>''"
    End Function

    Protected Friend Overridable Function GenerateSort(TableName As String) As String
        Return "isheadoffice desc"
    End Function

End Class
