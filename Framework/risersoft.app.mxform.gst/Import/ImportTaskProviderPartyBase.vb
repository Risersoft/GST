Imports GSTN.API.Library.Models.GstNirvana
Imports risersoft.app.mxent
Imports risersoft.shared

Public MustInherit Class ImportTaskProviderPartyBase
    Inherits ImportTaskProviderGstBase
    Public Overrides Property DocName As String = "Party"

    Public Sub New(controller As clsAppController)
        MyBase.New(controller)
    End Sub
    Protected Overrides Function GenerateSQL(dicWhere As clsCollecString(Of String)) As clsCollecString(Of String)

    End Function

    Protected Overrides Function GenerateSQL(provider As IAppDataProvider, objGroup As clsRowGroup, objPortion As clsPortionInfo) As clsCollecString(Of String)
        Dim dicSQL As New clsCollecString(Of String)
        Dim rXL = objGroup.Rows(0), dic = objGroup.dic
        Dim PartyType As String = Me.fncPartyType(rXL)
        Dim PartySubTypeTable As String = Me.fncPartySubTypeTable(rXL)

        Dim PanNumFilter = String.Format("PanNum='{0}'", Me.CleanFilterString(myUtils.cStrTN(rXL("PanNum"))))
        Dim SelCityFilter = String.Format("SelCity='{0}'", Me.CleanFilterString(myUtils.cStrTN(rXL("SelCity"))))
        Dim GSTINFilter = String.Format("GSTIN='{0}'", Me.CleanFilterString(myUtils.cStrTN(rXL("GSTIN"))))
        Dim strf1 As String = If(String.IsNullOrEmpty(myUtils.cStrTN(rXL("GSTIN"))),
                            myUtils.CombineWhere(False, PanNumFilter, SelCityFilter), GSTINFilter)
        dicSQL.Add("partymain", $"select * from PartyMain where mainpartyid in (select mainpartyid from party where {strf1}) and partytype='{PartyType}'")
        dicSQL.Add("party", $"select * from Party where {strf1} and partyid in (select partyid from {PartySubTypeTable})")
        dicSQL.Add(PartySubTypeTable, $"Select * from {PartySubTypeTable} where partyid in (Select partyid from party where " & strf1 & ")")
        Return dicSQL

    End Function

    Protected Overrides Function HandleGroupData(provider As IAppDataProvider, objGroup As clsRowGroup, objPortion As clsPortionInfo) As Task(Of clsProcOutput)
        Dim oRet As New clsProcOutput
        Dim rXL = objGroup.Rows(0)
        Dim PartyType As String = Me.fncPartyType(rXL)
        Dim PartySubTypeTable As String = Me.fncPartySubTypeTable(rXL)
        objGroup.dicSQL.Clear()
        Dim dic2 = Me.GenerateSQL(provider, objGroup, objPortion)
        dic2.CopyTo(objGroup.dicSQL)

        Dim PanNum As String = myUtils.cStrTN(rXL("PanNum")).Replace("'", "")
        Dim SelCity As String = myUtils.cStrTN(rXL("SelCity")).Replace("'", "")
        Dim GSTIN As String = myUtils.cStrTN(rXL("GSTIN")).Replace("'", "")
        Dim dsDB = provider.objSQLHelper.ExecuteDataset(CommandType.Text, objGroup.dicSQL)
        Me.CheckAddOpInfo(provider, objGroup.dicSQL)

        'Party Import
        myContext.Tables.SetAuto(dsDB.Tables("partymain"), dsDB.Tables("party"), "mainpartyid")
        myContext.Tables.SetAuto(dsDB.Tables("party"), dsDB.Tables(PartySubTypeTable), "partyid")

        Dim rr1() As DataRow = dsDB.Tables("partymain").Select()
        Dim str2 As String = If(String.IsNullOrEmpty(GSTIN), "PanNum='" & PanNum & "' and SelCity='" & SelCity & "'", "GSTIN='" & GSTIN & "'")
        Dim rr2() As DataRow = dsDB.Tables("party").Select(str2)
        Dim rr3() As DataRow = If(rr2.Length > 0, dsDB.Tables(PartySubTypeTable).Select("partyid=" & rr2(0)("partyid")), New DataRow() {})

        Dim rPartyMain, rParty, rPartySubType As DataRow
        If rr1.Length > 0 Then
            rPartyMain = rr1(0)
        Else
            rPartyMain = myContext.Tables.AddNewRow(dsDB.Tables("partymain"))
            rPartyMain("partytype") = PartyType
            If PartyType = "V" Then
                rPartyMain("PartySubType") = "MS"
            End If

            rPartyMain("Phnum") = rXL("SelPhnum")
            rPartyMain("Email") = rXL("SelEmail")

        End If

        rPartyMain("countrycode") = "IN"
        rPartyMain("MPName") = rXL("PartyName")
        rPartyMain("Address") = rXL("SelAddress")
        rPartyMain("City") = rXL("SelCity")
        rPartyMain("Pincode") = rXL("SelPinCode")

        Dim rrSub() As DataRow = dsMaster.Tables("Su").Select("SubDivisionCode='" & rXL("SelStateCode") & "'")
        If rrSub.Length > 0 Then
            rPartyMain("state") = rXL("SelStateCode")
            rPartyMain("statecode") = rrSub(0)("subdivisioncode")
        Else
            Dim rrSu() As DataRow = dsMaster.Tables("Su").Select("subdivision='" & rXL("SelStateCode") & "'")
            If rrSu.Length > 0 Then
                rPartyMain("state") = rXL("SelStateCode")
                rPartyMain("statecode") = rrSu(0)("subdivisioncode")
            Else
                rPartyMain("state") = DBNull.Value
                rPartyMain("statecode") = DBNull.Value
            End If
        End If

        Dim rrSub1() As DataRow = dsMaster.Tables("co").Select("country='" & rXL("SelCountry") & "'")
        If rrSub1.Length > 0 Then
            rPartyMain("country") = rXL("SelCountry")
            rPartyMain("countrycode") = rrSub1(0)("countrycode")
        Else
            rPartyMain("country") = DBNull.Value
            rPartyMain("countrycode") = DBNull.Value
        End If


        If rr2.Length > 0 Then
            rParty = rr2(0)
        Else
            rParty = myContext.Tables.AddNewRow(dsDB.Tables("party"))
            rParty("mainpartyid") = rPartyMain("mainpartyid")
            rParty("Pannum") = rXL("Pannum")
            Dim objNum As New clsVoucherNum(provider)
            rParty("partycode") = objNum.GetNewSerialNo("PartyID", myUtils.cStrTN(rPartyMain("partytype")), rParty)
        End If

        rParty("gstin") = GSTIN
        rParty("PartyName") = rXL("PartyName")
        rParty("SelAddress") = rXL("SelAddress")
        rParty("SelCity") = rXL("SelCity")
        rParty("SelEmail") = rXL("SelEmail")
        rParty("SelEMailCc") = rXL("SelEMailCc")
        rParty("SelEMailBcc") = rXL("SelEMailBcc")
        rParty("SelPhnum") = rXL("SelPhnum")
        rParty("SelPincode") = rXL("SelPinCode")

        Dim rrSub2() As DataRow = dsMaster.Tables("Su").Select("SubDivisionCode='" & rXL("SelStateCode") & "'")
        If rrSub2.Length > 0 Then
            rParty("selstate") = rXL("SelStateCode")
            rParty("selstatecode") = rrSub2(0)("subdivisioncode")
        Else
            Dim rrSu() As DataRow = dsMaster.Tables("Su").Select("subdivision='" & rXL("SelStateCode") & "'")
            If rrSu.Length > 0 Then
                rParty("selstate") = rXL("SelStateCode")
                rParty("selstatecode") = rrSu(0)("subdivisioncode")
            Else
                rParty("selstate") = DBNull.Value
                rParty("selstatecode") = DBNull.Value
            End If
        End If

        Dim rrSub3() As DataRow = dsMaster.Tables("co").Select("country='" & rXL("SelCountry") & "'")
        If rrSub3.Length > 0 Then
            rParty("selcountry") = rXL("SelCountry")
            rParty("selcountrycode") = rrSub3(0)("countrycode")
        Else
            rParty("selcountry") = DBNull.Value
            rParty("selcountrycode") = DBNull.Value
        End If

        Dim rTA = myFuncs2.FindTaxAreaRow(dsMaster.Tables("taxarea"), myUtils.cStrTN(rXL("SelStateCode")))
        If rTA Is Nothing Then
            oRet.AddError("State not found")
        Else
            rParty("taxareaid") = rTA("taxareaid")
            rParty("SelState") = rTA("Descrip")
            rParty("SelStateCode") = rTA("TaxAreaCode")
        End If
        myFuncsBase.SetPartyNameAddress(rPartyMain, dsDB.Tables("party"), False)

        If rr3.Length > 0 Then
            rPartySubType = rr3(0)
        Else
            rPartySubType = myContext.Tables.AddNewRow(dsDB.Tables(PartySubTypeTable))
            rPartySubType("partyid") = rParty("partyid")
            rPartySubType(PartySubTypeTable & "code") = rParty("partycode")
            rPartySubType("importfileid") = Me.ImportFileID
        End If

        oRet.Data = dsDB
        Return Task.FromResult(oRet)
    End Function

    Protected Overrides Function GenerateFilter() As String
        Return "isnull(pannum,'')<>''"
    End Function

    Public Overrides Async Function TryImportRowGroup(provider As IAppDataProvider, objGroup As clsRowGroup, objPortion As clsPortionInfo) As Task(Of clsProcOutput)
        Dim PartyType As String = Me.fncPartyType(Nothing)
        Dim PartySubTypeTable As String = Me.fncPartySubTypeTable(Nothing)
        Dim oRet = Await Me.HandleGroupData(provider, objGroup, objPortion)
        Dim info As New ImportErrorInfo()
        If oRet.WarningCount > 0 Then info.Voucher.AddWarning(Me.DocumentExistsErrorCode, oRet.WarningMessage)
        'Have a new dataset ready for data to be saved from database and copy level 1 and level 2 rows into it
        Dim dsDB = oRet.Data
        Dim rVouch = dsDB.Tables(0).Select()(0)
        If objGroup.Rows.Count > 1 Then
            oRet.AddError("Multiple Records found from Same PanNum and City")
            info.Voucher.AddError(Me.PreValidationErrorCode, "Multiple Records found from Same PanNum and City")
            Me.UpdateError(objGroup.Rows, info.Voucher)
        Else
            'Do the pre-operations, like getting IDs
            oRet = Me.ExecutePreValidation(provider, rVouch, dsDB.Tables("party"), objGroup.Rows(0), objGroup)
            If oRet.Success Then
                Dim dic = objGroup.dic
                Me.RunValidator(info, objGroup.Rows, rVouch, dsDB, "party", Sub(obj, rItem)
                                                                                If rItem Is Nothing Then
                                                                                    obj.SetValue("GSTUtils", New clsJINTFuncsGSTN(myContext))
                                                                                    For Each kvp In objGroup.dic
                                                                                        obj.AddOrUpdateRow(kvp.Value, kvp.Key)
                                                                                    Next
                                                                                End If
                                                                            End Sub)
                If info.Errorcount = 0 Then
                    'If all OK, go ahead and save. If not OK, add validation errors obtained to a new error datatable with same schema as ds, but with Validation and Warning columns.
                    Dim VouchDescrip = "No. " & rVouch("MPName") & ""
                    Try
                        provider.dbConn.BeginTransaction(provider, Me.GetType.Name, EnumfrmMode.acAddM, "MainPartyID")

                        For Each str1 As String In New String() {"partymain", "party", PartySubTypeTable}
                            provider.objSQLHelper.SaveResults(dsDB.Tables(str1), objGroup.dicSQL(str1), dicOpInfo(str1), False)
                        Next

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
                                                                                                     r1("vouchnum") = objGroup.Rows(0)("pannum")
                                                                                                 End Sub)
                    End If
                    oRet.AddError(info.ErrorDescripTot)
                End If

            Else
                oRet.AddError("Unforeseen error in pre validation")
                info.Voucher.AddError(Me.PreValidationErrorCode, "Unforeseen error in pre validation")
                Me.UpdateError(objGroup.Rows, info.Voucher)
            End If

        End If

        Return oRet

    End Function


End Class
