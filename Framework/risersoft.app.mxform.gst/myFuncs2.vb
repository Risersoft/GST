Imports System.Configuration
Imports System.Xml
Imports AutoMapper
Imports GSTN.API.Library.Models
Imports GSTN.API.Library.Models.GstNirvana
Imports Newtonsoft.Json
Imports risersoft.API.GSTN
Imports risersoft.app.mxent
Imports risersoft.shared
Imports risersoft.shared.cloud
Imports risersoft.shared.dal
Imports risersoft.shared.Extensions
Imports risersoft.shared.portable.Models.Auth

Public Class myFuncs2
    Public Shared Function InitializeMapper() As IConfigurationProvider
        Dim config = New MapperConfiguration(Sub(cfg)
                                                 cfg.CreateMap(Of Invoice, GstSaleInvoiceInfo)()
                                                 cfg.CreateMap(Of InvoiceItemGst, GstSaleInvoiceItemInfo)()
                                                 cfg.CreateMap(Of GstSaleInvoiceInfo, Invoice)()
                                                 cfg.CreateMap(Of GstSaleInvoiceItemInfo, InvoiceItemGst)()
                                                 cfg.CreateMap(Of Party, GstPartyInfo)()
                                                 cfg.CreateMap(Of GstPartyInfo, Party)()
                                                 cfg.CreateMap(Of EWB.GenerateEWBInfo, EwayBill)()
                                                 cfg.CreateMap(Of EWB.GenerateEWBInfo, EWayBillVehicle)()
                                             End Sub)
        Return config
    End Function
    Public Shared Function IsFinalPP(context As IProviderContext, GstRegID As Integer, rPP As DataRow, ReturnField As String) As Boolean
        Dim oMasterFICO As New clsMasterDataFICO(context)
        Dim isfinal As Boolean = myUtils.cBoolTN(rPP("IsFinal"))
        If Not isfinal Then
            'Ensure entry in gstregpp
            Dim nr As DataRow = oMasterFICO.GstRegPPRow(GstRegID, rPP("PostPeriodID"))
            isfinal = myUtils.IsInList(myUtils.cStrTN(nr(ReturnField)), "S", "F")
        End If
        Return isfinal
    End Function
    Public Shared Sub SetDivision(context As IProviderContext, fRow As DataRow, rParent As DataRow, CampusID As Integer)
        If Not context.IsProviderClient Then
            If CampusID > 0 Then
                Dim ds = myFuncs2.FindDivisionList(context, fRow, CampusID).Data
                If (Not ds Is Nothing) AndAlso (ds.Tables.Count > 0) Then
                    If ds.Tables(0).Select("divisionid=" & myUtils.cValTN(rParent("divisionid"))).Length = 0 Then
                        rParent("divisionid") = ds.Tables(0).Rows(0)("divisionid")
                    End If
                Else
                    rParent("divisionid") = DBNull.Value
                End If
            Else
                rParent("divisionid") = DBNull.Value
            End If
        End If

    End Sub

    Public Shared Function FindDivisionList(context As IProviderContext, ObjectRow As DataRow, CampusID As Integer) As clsProcOutput
        Dim oRet As New clsProcOutput
        Dim str1 As String = context.AppModel.strFilterDBAppUser(context, ObjectRow, "DivisionID")
        Dim sql As String = "select companyid,divisioncodelist from campus where campusid=" & CampusID
        Dim dt1 As DataTable = context.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql).Tables(0)
        If dt1.Rows.Count > 0 Then
            Dim CodeList As New List(Of String)
            CodeList.AddRange(Split(myUtils.cStrTN(dt1.Rows(0)("DivisionCodeList")), ","))
            Dim strf1 As String = myUtils.MakeCSV2(",", "'0'", True, CodeList.ToArray)
            Dim str2 As String = "DivisionCode in (" & strf1 & ")"
            Dim strFilter As String = myUtils.CombineWhere(False, str1, str2, "CompanyID=" & myUtils.cValTN(dt1.Rows(0)("companyid")))
            sql = "select Divisionid, (DivisionCode + '-' + DivisionName) as division,DivisionCode From division where " & strFilter
            oRet.Data = context.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql)


            If oRet.Data.Tables(0).Rows.Count = 0 Then
                sql = "select top 1 Divisionid, (DivisionCode + '-' + DivisionName) as division,DivisionCode From division where CompanyID=" & myUtils.cValTN(dt1.Rows(0)("companyid"))
                oRet.Data = context.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql)

            End If

        Else
            oRet.AddError("Campus not found")
        End If
        Return oRet
    End Function
    Public Shared Function ValidPostPeriod(context As IProviderContext, VouchDate As Date, PPfinal As Boolean, GstRegID As Integer, ReturnField As String) As clsProcOutput
        Dim oMasterFICO As New clsMasterDataFICO(context)
        Dim rPostPeriod As DataRow = oMasterFICO.rPostPeriod(VouchDate)
        Dim oRet As New clsProcOutput
        If PPfinal Then
            oRet.AddError("Original Data Post Period is finalized")
        ElseIf IsNothing(rPostPeriod) Then
            oRet.AddError("Select Valid Date, Post Period Corresponding to this date is not Defined")
        Else
            oRet.ID = myUtils.cValTN(rPostPeriod("PostPeriodID"))
            Dim isFinal As Boolean = myFuncs2.IsFinalPP(context, GstRegID, rPostPeriod, ReturnField)
            If isFinal Then oRet.AddError("New Data PostPeriod is finalized")
        End If

        Return oRet
    End Function
    Public Shared Sub ValidateSupplyType(context As IProviderContext, f As clsFormDataModel, CPField As String)

        If f.SelectedRow("sply_ty") Is Nothing Then
            f.AddError("sply_ty", "Please select Supply Type")
        ElseIf f.SelectedRow("CampusID") Is Nothing Then
            f.AddError("CampusID", "Please select Campus")
        ElseIf (f.SelectedRow(CPField & "ID") Is Nothing) AndAlso (f.SelectedRow("postaxareaid") Is Nothing) Then
            f.AddError(CPField & "ID", "Please select " & CPField & " or Place of Supply")
        Else
            Dim SupplierTaxArea, RecieverTaxArea As String
            If myUtils.IsInList(CPField, "Vendor") Then
                SupplierTaxArea = myUtils.cStrTN(f.SelectedRow(CPField & "ID")("TaxAreaCode"))
                If (Not IsNothing(f.SelectedRow("postaxareaid"))) Then
                    RecieverTaxArea = myUtils.cStrTN(f.SelectedRow("postaxareaid")("TaxAreaCode"))
                Else
                    RecieverTaxArea = myUtils.cStrTN(f.SelectedRow("campusid")("TaxAreaCode"))
                End If

            Else
                SupplierTaxArea = myUtils.cStrTN(f.SelectedRow("campusid")("TaxAreaCode"))
                If (Not IsNothing(f.SelectedRow("postaxareaid"))) Then
                    RecieverTaxArea = myUtils.cStrTN(f.SelectedRow("postaxareaid")("TaxAreaCode"))
                Else
                    RecieverTaxArea = myUtils.cStrTN(f.SelectedRow(CPField & "ID")("TaxAreaCode"))
                End If
            End If

            If myUtils.IsInList(SupplierTaxArea, RecieverTaxArea) AndAlso (Not myUtils.IsInList(SupplierTaxArea, "IMP")) Then
                If myUtils.IsInList(f.myRow("sply_ty"), "INTER") Then f.AddError("sply_ty", "Place of Supply and Tax Area Code do not match. Please select correct supply type")
            Else
                If myUtils.IsInList(f.myRow("sply_ty"), "INTRA") Then f.AddError("sply_ty", "Place of Supply and Tax Area Code do not match. Please select correct supply type")
            End If
        End If
    End Sub

    Shared Function FilterTimeDependent(Dated As Date, StartField As String, EndField As String, AddMonth As Integer) As String
        Dim str1 As String = "0=1"
        If Not myUtils.NullNot(Dated) Then str1 = "(" & StartField & " Is NULL Or " & StartField & " <= '" & Format(Dated, "dd-MMM-yyyy") & "') and (" & EndField & " Is NULL or " & EndField & " >= '" & Format(DateAdd("m", -AddMonth, Dated), "dd-MMM-yyyy") & "')"
        Return str1
    End Function

    Shared Function FieldFilter(context As IProviderContext, fRow As DataRow, dated As Date, StartField As String, EndField As String, IDField As String) As String
        Dim str As String = ""
        Dim str1 As String = context.AppModel.strFilterDBAppUser(context, fRow, IDField)
        Dim Str2 As String = FilterTimeDependent(dated, StartField, EndField, 0)
        str = myUtils.CombineWhere(False, str1, Str2)
        Return str
    End Function
    Public Shared Function AddUpdParty(context As IProviderContext, PartyType As String, PartySubType As String, Gstin As String, TrdName As String, Addr1 As String, Addr2 As String, place As String, pincode As String, taxareaid As Integer, ByRef rPartySubType As DataRow) As clsProcOutput
        Dim rParty As DataRow
        Dim oRet As New clsProcOutput
        If Not GSTUtils.ValidateGSTIN(Gstin) AndAlso (Not GSTUtils.ValidateUIN(Gstin)) Then oRet.AddError("Invalid GSTIN")

        If oRet.Success Then
            Dim TinCode As Integer = myUtils.cValTN(Left(Gstin, 2))
            Dim PAN As String = Gstin.Substring(2, Gstin.Length - 5)

            Dim dic As New clsCollecString(Of String), sql As String = ""
            dic.Add("partymain", String.Format("select * from partymain where mainpartyid in (select mainpartyid from party where pannum='{0}' or gstin='{1}') and partytype in ('A','{2}') and isnull(partysubtype,'') in ('','{3}')", PAN, Gstin, PartyType, PartySubType))
            dic.Add("party", String.Format("select * from party where gstin='{0}'", Gstin))
            Select Case PartyType.Trim.ToUpper
                Case "V"
                    sql = String.Format("select * from vendor where partyid in (select partyid from party where gstin='{0}')", Gstin)
                Case "C"
                    sql = String.Format("select * from customer where partyid in (select partyid from party where gstin='{0}')", Gstin)
            End Select
            dic.Add(PartyType, sql)
            Dim dsDB As DataSet = context.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, dic)
            context.Tables.SetAuto(dsDB.Tables("partymain"), dsDB.Tables("party"), "mainpartyid")
            context.Tables.SetAuto(dsDB.Tables("party"), dsDB.Tables(PartyType), "partyid")


            Dim rr1() As DataRow = dsDB.Tables("partymain").Select()
            Dim rPartyMain As DataRow
            If rr1.Length > 0 Then
                rPartyMain = rr1(0)
            Else
                rPartyMain = context.Tables.AddNewRow(dsDB.Tables("partymain"))
                rPartyMain("mpname") = TrdName
                rPartyMain("partytype") = PartyType
                rPartyMain("partysubtype") = PartySubType
                rPartyMain("address") = Addr1
                rPartyMain("city") = place
                rPartyMain("pincode") = pincode
            End If
            Dim rr2() As DataRow = dsDB.Tables("party").Select("mainpartyid=" & rPartyMain("mainpartyid"))
            If rr2.Length > 0 Then
                rParty = rr2(0)
            Else
                rParty = context.Tables.AddNewRow(dsDB.Tables("party"))
                rParty("partyname") = TrdName
                rParty("gstin") = Gstin
                rParty("pannum") = PAN
                rParty("mainpartyid") = rPartyMain("mainpartyid")
                rParty("seladdress") = Addr1
                rParty("selcity") = place
                rParty("selpincode") = pincode
                If taxareaid > 0 Then rParty("taxareaid") = taxareaid
            End If
            Dim rr3() As DataRow = dsDB.Tables(PartyType).Select("partyid=" & rParty("partyid"))
            If rr3.Length > 0 Then
                rPartySubType = rr3(0)
            Else
                rPartySubType = context.Tables.AddNewRow(dsDB.Tables(PartyType))
                rPartySubType("partyid") = rParty("partyid")
            End If
            For Each str1 As String In New String() {"partymain", "party", PartyType}
                context.Provider.objSQLHelper.SaveResults(dsDB.Tables(str1), dic(str1))
            Next
            oRet.ID = myUtils.cValTN(rPartySubType(0))
        End If

        Return oRet


    End Function
    Public Shared Function FindSupplyTypePurchase(CTIN As String, TPGstRegType As String, rPOS As DataRow, rShipFrom As DataRow) As String
        Dim Sply_Ty As String = "INTRA"

        If String.IsNullOrEmpty(CTIN) Then
            'Party is un-registered
            If (Not rPOS Is Nothing) AndAlso (Not rShipFrom Is Nothing) Then
                Sply_Ty = If(myUtils.cValTN(rPOS("taxareaid")) = myUtils.cValTN(rShipFrom("taxareaid")), "INTRA", "INTER")
            End If
        ElseIf myUtils.IsInList(TPGstRegType, "SEZ", "SEZD") Then
            Sply_Ty = "INTER"
        ElseIf (Not rPOS Is Nothing) Then
            'Party is registered
            Sply_Ty = If(myUtils.IsInList(myUtils.cStrTN(rPOS("tincode")), Left(CTIN, 2)), "INTRA", "INTER")
        End If

        Return Sply_Ty

    End Function
    Public Shared Function FindSupplyTypeSale(rGstReg As DataRow, PartyRegType As String, rPOS As DataRow) As String
        Dim Sply_Ty As String = "INTRA"
        If myUtils.IsInList(PartyRegType, "SEZ", "SEZD") Then
            Sply_Ty = "INTER"
        ElseIf (Not rPOS Is Nothing) AndAlso (Not rGstReg Is Nothing) Then
            If myUtils.cValTN(rGstReg("taxareaid")) = myUtils.cValTN(rPOS("taxareaid")) Then
                Sply_Ty = "INTRA"
            Else
                Sply_Ty = "INTER"
            End If
        End If

        Return Sply_Ty

    End Function
    Public Shared Sub CheckGetJsonData(context As IProviderContext, Model As clsFormDataModel)
        If Model.vBag.ContainsKey("importfilevouchid") Then
            Try
                Dim sql2 As String = "select datajson from importfilevouch where importfilevouchid=" & myUtils.cValTN(Model.vBag("importfilevouchid"))
                Dim dt2 As DataTable = context.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql2).Tables(0)
                Dim ds As DataSet = JsonConvert.DeserializeObject(Of DataSet)(myUtils.cStrTN(dt2.Rows(0)("datajson")))
                myUtils.MergeDataRow(ds.Tables(0).Rows(0), Model.myRow.Row)
                For Each rItem As DataRow In ds.Tables(1).Select
                    myUtils.CopyOneRow(rItem, Model.dsForm.Tables(1))
                Next
            Catch ex As Exception
                Trace.WriteLine("Error while importing file voucher:" & ex.Message)
            End Try
        End If
    End Sub
    Public Shared Function FindTaxAreaRow(dtTA As DataTable, TaxAreaText As String) As DataRow
        Dim rTA As DataRow
        If IsNumeric(TaxAreaText) Then
            Dim rrPOS() As DataRow = dtTA.Select("TinCodenum=" & myUtils.cValTN(TaxAreaText))
            If rrPOS.Length > 0 Then rTA = rrPOS(0)
        Else
            Dim rrPOS() As DataRow = dtTA.Select("TaxAreaCode='" & TaxAreaText & "'")
            If rrPOS.Length > 0 Then
                rTA = rrPOS(0)
            Else
                rrPOS = dtTA.Select("Descrip='" & TaxAreaText & "'")
                If rrPOS.Length > 0 Then rTA = rrPOS(0)
            End If
        End If
        Return rTA
    End Function

    Public Shared Sub CheckPopulateTaxAreaID(rInv As DataRow, rCounter As DataRow, rConsignee As DataRow)
        If myUtils.cValTN(rInv("POSTaxAreaID")) = 0 Then
            If (rConsignee IsNot Nothing) AndAlso myUtils.cValTN(rConsignee("taxareaid")) > 0 Then
                rInv("POSTaxAreaID") = myUtils.cValTN(rConsignee("TaxAreaID"))
            ElseIf (rCounter IsNot Nothing) AndAlso myUtils.cValTN(rCounter("taxareaid")) > 0 Then
                rInv("POSTaxAreaID") = myUtils.cValTN(rCounter("TaxAreaID"))
            Else
                rInv("postaxareaid") = DBNull.Value
            End If
        End If
    End Sub
    Public Shared Function PopulateFilters(oMaster As clsMasterDataFICO, DocType As String, GstRegID As Integer, ReturnPeriodID1 As Integer, ReturnPeriodID2 As Integer, PANLevelMatch As Boolean) As clsCollecString(Of String)
        Return myFuncs2.PopulateFilters(oMaster, DocType, GstRegID, ReturnPeriodID1, ReturnPeriodID2, PANLevelMatch, "matchperiodid")
    End Function

    Public Shared Function PopulateFilters(oMaster As clsMasterDataFICO, DocType As String, GstRegID As Integer, ReturnPeriodID1 As Integer, ReturnPeriodID2 As Integer, PANLevelMatch As Boolean, MatchPeriodField As String) As clsCollecString(Of String)
        Dim dic As New clsCollecString(Of String)
        If PANLevelMatch Then
            Dim rGstReg = oMaster.GstRegRow(GstRegID)
            If rGstReg Is Nothing Then
                dic.Add("tpcampusid", $"0=1")
                dic.Add("cpcampusid", $"0=1")
            Else
                dic.Add("tpcampusid", $"campusid in (select campusid from campus where companyid={rGstReg("companyid")})")
                dic.Add("cpcampusid", $"gstregid in (select gstregid from gstreg where companyid={rGstReg("companyid")})")
            End If
        Else
            dic.Add("tpcampusid", "campusid in (select campusid from campus where GstRegID=" & GstRegID & ")")
            dic.Add("cpcampusid", "gstregid =" & GstRegID)
        End If

        Dim rPP1 As DataRow = oMaster.rPostPeriod(ReturnPeriodID1)
        Dim rPP2 As DataRow = oMaster.rPostPeriod(If(ReturnPeriodID2 > 0, ReturnPeriodID2, ReturnPeriodID1))
        Dim PPCSV1, PPCSV2, PPCSV3 As String

        'and finyearid={1} removed from PPCSV2, ref UBS 2018-19 report
        PPCSV1 = If(rPP1 Is Nothing, "0", myUtils.MakeCSV(oMaster.PostPeriodTable.Select(String.Format("sdate>='{0}' and edate<='{1}'", Format(rPP1("sdate"), "dd-MMM-yyyy"), Format(rPP2("edate"), "dd-MMM-yyyy"))), "postperiodid"))
        PPCSV2 = If(rPP1 Is Nothing, "0", myUtils.MakeCSV(oMaster.PostPeriodTable.Select(String.Format("sdate<'{0}'", Format(rPP1("sdate"), "dd-MMM-yyyy"), rPP1("finyearid"))), "postperiodid"))
        PPCSV3 = If(rPP1 Is Nothing, "0", myUtils.MakeCSV(oMaster.PostPeriodTable.Select(String.Format("sdate>='{0}'", Format(rPP1("sdate"), "dd-MMM-yyyy"))), "postperiodid"))


        Dim strf1 = dic.Add("returnperiodid", "ReturnPeriodID in (" & PPCSV1 & ")")
        dic.Add("matchperiodid", myUtils.CombineWhereOR(False, strf1, If(String.IsNullOrEmpty(MatchPeriodField), "",
                         myUtils.CombineWhere(False, "ReturnPeriodID in (" & PPCSV2 & ")",
                                              If(myUtils.IsInList(MatchPeriodField, "ewbmatchperiodid"), "", "isnull(reconaction,'') not in ('lapse','ineligible')"),
                        myUtils.CombineWhereOR(False, $"{MatchPeriodField} is null", $"{MatchPeriodField} in (" & PPCSV3 & ")")))))

        dic.Add("tpinvoiceid", myUtils.CombineWhere(False, dic("tpcampusid"), "DocType='" & DocType & "'",
                                                         "gstinvoicetype in ('B2B','CDN')", dic("matchperiodid")))
        dic.Add("ewaybillid", myUtils.CombineWhere(False, dic("tpcampusid"), dic("matchperiodid")))
        dic.Add("cpinvoiceid", myUtils.CombineWhere(False, dic("cpcampusid"),
                                                        "DocType='" & DocType & "'", dic("matchperiodid")))
        Return dic

    End Function
    Public Shared Function GetDashBoardSettings(context As IProviderContext, vwState As clsViewState, DashboardXML As String) As DashboardSettingModel
        Dim myXML As New XmlDocument
        myXML.LoadXml(myUtils.ProperXML(DashboardXML))
        Dim oNode As XmlNode = myXML.SelectSingleNode("/ROOT/CUSTOMDASHBOARD")
        Dim settings As New DashboardSettingModel
        settings.DeSerializeAttributes(oNode)
        If Not String.IsNullOrEmpty(settings.FieldName) Then
            Dim fRow = context.AppModel.GetFilterFieldRow(settings.FieldName)
            Dim FieldValue As Integer = myUtils.cValTN(myFuncs.ParamValue(vwState, fRow("filterkey")))
            Dim dt1 As DataTable = context.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, "select * from dashboardsetting").Tables(0)
            Dim sr = myFuncs2.FindRow(context, dt1, settings.FieldName, FieldValue)
            settings = JsonConvert.DeserializeObject(Of DashboardSettingModel)(myUtils.cStrTN(sr("layoutjson")))
        End If
        Return settings
    End Function
    Public Shared Function FindRow(context As IProviderContext, dt1 As DataTable, FieldName As String, FieldValue As Integer) As DataRow
        Dim sr, rr() As DataRow
        If myUtils.IsInList(FieldName, "tenantid", "") Then
            rr = dt1.Select("isnull(fieldname,'') in ('','tenantid')")
        ElseIf myUtils.IsInList(FieldName, "companyid") Then
            rr = dt1.Select($"fieldname='companyid' and fieldvalue={FieldValue}")
            If rr.Length = 0 Then rr = dt1.Select("isnull(fieldname,'') in ('','tenantid')")
        ElseIf myUtils.IsInList(FieldName, "gstregid") Then
            rr = dt1.Select($"fieldname='gstregid' and fieldvalue={FieldValue}")
            If rr.Length = 0 Then
                Dim oMaster As New clsMasterDataFICO(context)
                Dim rGstReg = oMaster.GstRegRow(FieldValue)
                If Not rGstReg Is Nothing Then rr = dt1.Select($"fieldname='companyid' and fieldvalue={rGstReg("companyid")}")
            End If
            If rr.Length = 0 Then rr = dt1.Select("isnull(fieldname,'') in ('','tenantid')")
        End If
        If (Not rr Is Nothing) AndAlso rr.Length > 0 Then Return rr(0)
    End Function
    Public Shared Function FindRows(oMaster As clsMasterDataFICO, dt1 As DataTable, FieldName As String, FieldValue As Integer, strFilter As String) As DataRow()
        Dim rr() As DataRow
        If myUtils.IsInList(FieldName, "tenantid", "") Then
            rr = dt1.Select(myUtils.CombineWhere(False, strFilter, "gstregid is null and companyid is null"))
        ElseIf myUtils.IsInList(FieldName, "companyid") Then
            rr = dt1.Select(myUtils.CombineWhere(False, strFilter, $"companyid={FieldValue}"))
            If rr.Length = 0 Then rr = dt1.Select(myUtils.CombineWhere(False, strFilter, "gstregid is null and companyid is null"))
        ElseIf myUtils.IsInList(FieldName, "gstregid") Then
            rr = dt1.Select(myUtils.CombineWhere(False, strFilter, $"gstregid={FieldValue}"))
            If rr.Length = 0 Then
                Dim rGstReg = oMaster.GstRegRow(FieldValue)
                If Not rGstReg Is Nothing Then rr = dt1.Select(myUtils.CombineWhere(False, strFilter, $"companyid={FieldValue}"))
            End If
            If rr.Length = 0 Then rr = dt1.Select(myUtils.CombineWhere(False, strFilter, "gstregid is null and companyid is null"))
        End If
        Return rr
    End Function
    Public Shared Function GetParamGstRegID(context As IProviderContext) As String
        Dim dic As New clsCollecString(Of String)
        context.GetAppInfo.PopulateAppBarDict(dic)
        Dim sql As String = "select * from gstreg " & myUtils.CombineWhere(True, If(dic.Exists("cid"), "companyid=" & myUtils.cValTN(dic("cid")), ""))
        Dim dt1 As DataTable = context.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql).Tables(0)
        Return dt1.Rows(0)("gstregid")

    End Function
    Public Shared Function GetInvoiceNumberDynamicPart(dtSettings As DataTable, oMaster As clsMasterDataFICO, GstRegID As Integer, DocNature As String, InvoiceNum As String) As clsProcOutput(Of Long)
        Dim oRet As New clsProcOutput(Of Long)
        If String.IsNullOrWhiteSpace(InvoiceNum) Then
            oRet.AddError("Empty Invoice Number!")
        Else
            Try
                Dim rr() As DataRow = myFuncs2.FindRows(oMaster, dtSettings, "gstregid", GstRegID, $"documentnature='{DocNature}'")
                For Each r1 As DataRow In rr
                    Dim TemplateId = Convert.ToInt32(r1("InvoiceNumberTemplateId"))
                    Dim Prefix = r1("Prefix").ToString
                    Dim Suffix = r1("Suffix").ToString
                    Dim DynamicPart As String = String.Empty

                    If Not String.IsNullOrWhiteSpace(Prefix) AndAlso Not String.IsNullOrWhiteSpace(Suffix) Then
                        If InvoiceNum.StartsWith(Prefix) AndAlso InvoiceNum.EndsWith(Suffix) Then
                            DynamicPart = InvoiceNum.Remove(0, Prefix.Length)

                            If Not String.IsNullOrWhiteSpace(DynamicPart) AndAlso DynamicPart.EndsWith(Suffix) Then
                                DynamicPart = DynamicPart.Remove(DynamicPart.Length - Suffix.Length)
                            Else
                                DynamicPart = String.Empty
                            End If
                        End If
                    Else
                        If Not String.IsNullOrWhiteSpace(Prefix) Then
                            If InvoiceNum.StartsWith(Prefix) Then
                                DynamicPart = InvoiceNum.Remove(0, Prefix.Length)
                            End If
                        Else
                            If Not String.IsNullOrWhiteSpace(Suffix) Then
                                If InvoiceNum.EndsWith(Suffix) Then
                                    DynamicPart = InvoiceNum.Remove(InvoiceNum.Length - Suffix.Length)
                                End If
                            End If
                        End If
                    End If

                    If Not String.IsNullOrWhiteSpace(DynamicPart) AndAlso IsNumeric(DynamicPart) Then
                        oRet.Result = Convert.ToInt64(DynamicPart)
                        oRet.ID = TemplateId

                        Exit For
                    End If
                Next
            Catch ex As Exception
                oRet.AddException(ex)
            End Try

        End If

        If oRet.ID = 0 OrElse oRet.Result = 0 Then oRet.AddError("Dynamic part not found")
        Return oRet
    End Function

    Public Shared Function GetInvoiceNumberDynamicPart(provider As IAppDataProvider, GstRegID As Integer, DocNature As String, InvoiceNum As String) As clsProcOutput(Of Long)
        Dim dt As DataTable = provider.objSQLHelper.ExecuteDataset(CommandType.Text, $"SELECT InvoiceNumberTemplateId,DocumentNature, CompanyId, GSTRegId, Prefix, Suffix FROM GstDocNumTemplate where documentnature='{DocNature}'").Tables(0)
        Dim oMaster = New clsMasterDataFICO(provider)
        Dim oRet = myFuncs2.GetInvoiceNumberDynamicPart(dt, oMaster, GstRegID, DocNature, InvoiceNum)
        Return oRet
    End Function
    Public Shared Function ValidatedPostPeriodID(context As IProviderContext, FieldName As String, CurrentPostPeriodID As Integer, dt As DataTable) As Integer
        Dim provider As New clsDBUserFilterProvider(context, False)
        Dim nr = provider.FindFieldRow(FieldName)
        Dim DBUserPostPeriodID As Integer = If(nr Is Nothing, 0, myUtils.cValTN(nr("fieldvalue")))
        Dim PostPeriodID As Integer = CInt(dt.Select("", "sdate desc")(0)("postperiodid"))
        For Each PPID In New Integer() {DBUserPostPeriodID, CurrentPostPeriodID}
            Dim rr() As DataRow = dt.Select("postperiodid=" & PPID)
            If rr.Length > 0 Then
                PostPeriodID = PPID
                Exit For
            End If
        Next
        Return PostPeriodID
    End Function

    Public Shared Async Function UploadMail(context As IProviderContext, oRet As clsProcOutput, username As String) As Task(Of Boolean)
        Dim client = context.App.objAppExtender.FileProviderClient(context, ConfigurationManager.AppSettings("StorageContainerName"))
        Dim BlobName = Await client.UpLoadAsync(oRet.Description, System.IO.Path.GetFileName(oRet.Description), "")
        Dim FileUrl2 = Await client.GetDownloadUri(BlobName)


        Dim mailer As New MailModuleBase(context)
        Dim MailMessage = String.Format("Your excel file has been generated and is available on {0}", FileUrl2.Result.Uri)
        Dim mailerRet = Await mailer.SendGenericMail(username, "Convert 2 Excel", MailMessage)
        If mailerRet.Success Then
            oRet.AddMessage("Sent Message: " & MailMessage)
        Else
            Dim mailerMessage = String.Format("Message from SendGenericMailMandrill Message='{0}' StackTrace='{1}'", mailerRet.Message, mailerRet.StackTrace)
            oRet.AddMessage(mailerMessage)
        End If
        Return True
    End Function

    Public Shared Function AutoGenerateGstDocNumSeries(context As IProviderContext, GstRegID As Integer, ReturnPeriodID As Integer) As clsProcOutput(Of List(Of DocNumSeriesModel))
        Dim oRet As New clsProcOutput(Of List(Of DocNumSeriesModel))
        oRet.Result = New List(Of DocNumSeriesModel)
        Try
            Dim sql As String = "SELECT * FROM(
                                    SELECT InvoiceNumberTemplateId AS TemplateId, InvoiceNumDynamicPart AS DynamicPart, CASE WHEN CancelDate IS NULL THEN 0 ELSE 1 END AS IsCancelled FROM Invoice INV INNER JOIN Campus CAMP ON INV.CampusID = CAMP.CampusID WHERE InvoiceNumberTemplateId IS NOT NULL AND InvoiceNumDynamicPart IS NOT NULL AND Camp.GSTRegID =" & GstRegID & " AND INV.ReturnPeriodID=" & ReturnPeriodID & "
                                    UNION
                                    SELECT InvoiceNumberTemplateId AS TemplateId, VouchNumDynamicPart AS DynamicPart, 0 AS IsCancelled from GstAdvanceVouch as Vouch INNER JOIN Campus CAMP ON Vouch.CampusID = CAMP.CampusID WHERE InvoiceNumberTemplateId IS NOT NULL AND VouchNumDynamicPart IS NOT NULL AND Camp.GSTRegID =" & GstRegID & " AND Vouch.ReturnPeriodID=" & ReturnPeriodID & "
                                    ) as V
                                    ORDER BY V.TemplateId, V.DynamicPart"

            Dim dt As DataTable = context.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql).Tables(0)

            If dt.Rows.Count > 0 Then
                For Each TemplateGroup In dt.Rows.Cast(Of DataRow).Select(Function(x) New With {
                            .TemplateId = Convert.ToInt32(x("TemplateId")),
                            .DynamicPart = Convert.ToInt64(x("DynamicPart")),
                            .IsCancelled = Convert.ToBoolean(x("IsCancelled"))
                        }).GroupBy(Function(x) x.TemplateId)
                    Dim TemplateId = TemplateGroup.Key
                    Dim Invoices = TemplateGroup.OrderBy(Function(x) x.DynamicPart).ToList
                    Dim FromDynamicPart = Invoices.First().DynamicPart
                    Dim ToDynamicPart = Invoices.Last().DynamicPart
                    Dim TotCount = (ToDynamicPart - FromDynamicPart) + 1
                    Dim CancelledCount = Invoices.Where(Function(x) x.IsCancelled).LongCount
                    Dim IssuedCount = Invoices.LongCount - CancelledCount
                    Dim MissingCount = TotCount - CancelledCount - IssuedCount

                    oRet.Result.Add(New DocNumSeriesModel With {
                        .GSTRegID = GstRegID,
                        .ReturnPeriodID = ReturnPeriodID,
                                .InvoiceNumberTemplateId = TemplateId,
                                .NumFrom = FromDynamicPart,
                                .NumTo = ToDynamicPart,
                                .TotCount = TotCount,
                                .CancelledCount = CancelledCount,
                                .IssuedCount = IssuedCount,
                                .MissingCount = MissingCount
                            })
                Next
            End If

        Catch ex As Exception
            oRet.AddException(ex)
        End Try

        Return oRet
    End Function
    Public Shared Function CalculateGstDocNumSeriesDiff(context As IProviderContext, GstRegID As Integer, ReturnPeriodID As Integer) As clsProcOutput(Of List(Of DocNumSeriesDiffModel))
        Dim oRet As New clsProcOutput(Of List(Of DocNumSeriesDiffModel))
        oRet.Result = New List(Of DocNumSeriesDiffModel)
        Try

            Dim oMaster As New clsMasterDataFICO(context)
            Dim rPP = oMaster.rPostPeriod(ReturnPeriodID)
            Dim rPP2 = oMaster.PostPeriodTable.Select("sdate<'" & Format(rPP("sdate"), "dd-MMM-yyyy") & "'", "sdate desc")(0)

            Dim dic As New clsCollecString(Of String)
            dic.Add("curr", $"select * from GstListDocNumSeries() where gstregid={GstRegID} and returnPeriodID={ReturnPeriodID}")
            dic.Add("prev", $"select * from GstListDocNumSeries() where gstregid={GstRegID} and returnPeriodID={rPP2("postperiodid")}")
            Dim ds = context.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, dic)


            For Each r1 As DataRow In ds.Tables("curr").Select
                Dim model = New DocNumSeriesDiffModel With {.InvoiceNumberSeriesId = r1("invoicenumberseriesid"),
                                .InvoiceNumberTemplateId = r1("invoicenumberTemplateId")}
                model.DocumentNatureDescrip = myUtils.cStrTN(r1("documentnaturedescrip"))
                model.Prefix = myUtils.cStrTN(r1("prefix"))
                model.Suffix = myUtils.cStrTN(r1("suffix"))
                model.CurrentPeriodStart = r1("NumFrom")
                model.GSTRegID = r1("gstregid")
                model.ReturnPeriodID = r1("returnperiodid")

                Dim rr() As DataRow = ds.Tables("prev").Select($"invoicenumbertemplateid={r1("invoicenumbertemplateid")} and NumTo<" & r1("NumFrom"))
                If rr.Length > 0 Then
                    model.LastPeriodEnd = rr(0)("NumTo")
                End If
                oRet.Result.Add(Model)

            Next

        Catch ex As Exception
            oRet.AddException(ex)
        End Try

        Return oRet
    End Function

    Public Shared Function Recalculate(context As IProviderContext, GstRegID As String, ReturnPeriodID As String) As clsProcOutput
        Dim oRet As New clsProcOutput
        Try
            Dim dt As DataTable = context.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, "SELECT invoiceid, transactiontype, isamendment, doctype, rchrg,invoicenum, invoicenumbertemplateid, invoicenumdynamicpart from gstlistinvoice2() WHERE GSTRegID =" & myUtils.cValTN(GstRegID) & " AND ReturnPeriodID=" & myUtils.cValTN(ReturnPeriodID)).Tables(0)
            If dt.Rows.Count > 0 Then
                Dim SaleProvider = New clsGSTInvoiceSale(context)
                Dim PurchaseProvider = New clsGSTInvoicePurch(context)

                For Each dr In dt.Rows
                    If dr IsNot Nothing AndAlso myUtils.IsInList(dr("DocType"), "IS", "IP") Then
                        If myUtils.IsInList(dr("DocType"), "IS") Then
                            SaleProvider.UpdateInvoiceNumberDynamicPart(GstRegID, dr)
                        Else
                            PurchaseProvider.UpdateInvoiceNumberDynamicPart(GstRegID, dr)
                        End If
                    End If
                Next

                context.Provider.objSQLHelper.SaveResults(dt, "SELECT invoiceid,  invoicenumbertemplateid, invoicenumdynamicpart FROM Invoice")
            End If

            dt = context.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, "SELECT Vouch.* FROM GstAdvanceVouch Vouch INNER JOIN Campus CAMP ON Vouch.CampusID = CAMP.CampusID WHERE Camp.GSTRegID ='" & GstRegID.ToString & "' AND Vouch.ReturnPeriodID='" & ReturnPeriodID.ToString & "'").Tables(0)
            If dt.Rows.Count > 0 Then
                Dim PCProvider = New clsGSTAdvanceBase(context, "PC", "GSTR1")
                Dim PVProvider = New clsGSTAdvanceBase(context, "PV", "GSTR1")

                For Each dr In dt.Rows
                    If dr IsNot Nothing AndAlso myUtils.IsInList(dr("DocType"), "PC", "PV") Then
                        If myUtils.IsInList(dr("DocType"), "PC") Then
                            PCProvider.UpdateInvoiceNumberDynamicPart(GstRegID, dr)
                        Else
                            PVProvider.UpdateInvoiceNumberDynamicPart(GstRegID, dr)
                        End If
                    End If
                Next

                context.Provider.objSQLHelper.SaveResults(dt, "SELECT * FROM GstAdvanceVouch")
            End If

        Catch ex As Exception
            oRet.AddException(ex)
        End Try
        Return oRet
    End Function

    Public Shared Function AddChartStaticList(context As IProviderContext) As List(Of DashletSettingModel)
        Dim lstDashlet = New List(Of DashletSettingModel)


        For Each r1 As DataRow In context.AppModel.ViewDefs.Select("Visualization  In ('Grid+Chart','Chart','HTML')")
            Dim dashlet = New DashletSettingModel
            dashlet.Code = r1("cvwkey")
            dashlet.Name = r1("strview")
            dashlet.Visualization = r1("visualization")
            If myUtils.IsInList(dashlet.Visualization, "html") Then
                dashlet.ChartType = "HTML"
            Else
                Dim Model = myUtils.DeSerialize(Of clsChartModel)(myUtils.cStrTN(r1("ChartXML")))
                dashlet.ChartType = Model.ChartType
            End If
            dashlet.ImageFile = myFuncs2.CalculateImage(dashlet.ChartType)
            lstDashlet.Add(dashlet)
        Next

        Return lstDashlet
    End Function
    Public Shared Function CalculateImage(chartType As String) As String
        Dim ImageFile As String = ""

        If myUtils.IsInList(chartType, "column") Then
            ImageFile = "column-chart.png"
        ElseIf myUtils.IsInList(chartType, "pie") Then
            ImageFile = "pie-chart-icon-vector.jpg"
        ElseIf myUtils.IsInList(chartType, "linesymbols") Then
            ImageFile = "LineChart.jpg"
        ElseIf myUtils.IsInList(chartType, "html") Then
            ImageFile = "gstreturnstatus.png"
        Else
            ImageFile = "KPIs.png"
        End If
        Return ImageFile
    End Function
    Public Shared Async Function SendVendorEmails(context As IProviderContext, oProc As clsGSTNReturnBase, dicFilter As clsCollecString(Of String), FileName As String) As Task(Of clsProcOutput)
        Dim oRet As New clsProcOutput

        Try
            Dim VendorMailMatchCode = Await context.Provider.GetSystemOption("Vendormailmatchcode")
            Dim lstMatchCode As New List(Of String)
            If String.IsNullOrEmpty(VendorMailMatchCode) Then
                lstMatchCode.AddRange(New String() {"TO", "CO", "MM"})
            Else
                lstMatchCode.AddRange(Split(VendorMailMatchCode, ",").Select(Function(x) String.Join("", x.Where(Function(c) Char.IsLetter(c)))))
            End If
            Dim strf2 As String = "matchcode in (" & myUtils.MakeCSV2(",", "", True, lstMatchCode.ToArray) & ")"
            Dim vw As clsDummyView = Await oProc.PrepareView("listpurchinvoicematch", myUtils.CombineWhere(False, dicFilter("cpinvoiceid"), strf2))
            Dim dtVendor As DataTable = context.Data.SelectDistinct(vw.mainGrid.myDS.Tables(0), "vendorid",,, "vendorid is not null")


            Dim mailer As New MailModuleBase(context)
            Dim VendorMailHTML = Await context.Provider.GetSystemOption("Vendormailhtml")
            Dim strFormattedHTML As String = If(String.IsNullOrEmpty(VendorMailHTML), mailer.ReadHtmlTemplate("VendorEmailTemplate.html"), VendorMailHTML)
            Dim fileProvider = context.App.objAppExtender.FileProviderClient(context, ConfigurationManager.AppSettings("StorageContainerName"))
            Dim xlProvider As New XLTaskProvider(context.Controller)

            For Each r1 As DataRow In dtVendor.Select("isnull(selemail,'')<>''")
                Dim strHTML = strFormattedHTML
                Dim dic As New clsCollecString(Of String)
                dic.Add("Vendor", r1("partyname").ToString & "(" & r1("ctin").ToString & ")")
                dic.Add("PartyName", r1("partyname").ToString)
                dic.Add("CTIN", r1("ctin").ToString)
                dic.Add("Publisher", context.Controller.CalcPublisher)
                Dim ParamList As List(Of String) = XMLUtils.PrepSubstList(strFormattedHTML, "$").Distinct.ToList
                For Each prm As String In ParamList
                    If dic.ContainsKey(prm) Then
                        strHTML = Replace(strHTML, "$" & prm & "$", dic(prm))
                    Else
                        strHTML = Replace(strHTML, "$" & prm & "$", "")
                    End If
                Next

                Dim vw2 As clsDummyView = Await oProc.PrepareView("listpurchinvoicematch", myUtils.CombineWhere(False, dicFilter("cpinvoiceid"), context.SQL.GenerateSQLWhere(r1, "vendorid"), strf2))
                Dim NewFileName = System.IO.Path.GetFileNameWithoutExtension(FileName) & "_" & r1("vendorid").ToString & System.IO.Path.GetExtension(FileName)
                Dim oRet2 = xlProvider.ExecuteExport(vw2, NewFileName)
                Dim BlobName = Await fileProvider.UpLoadAsync(oRet2.Description, NewFileName, "")
                Dim FileUrl2 = Await fileProvider.GetDownloadUri(BlobName)
                Dim attach = New MailAttachment("xlsx", "Details.xlsx", oRet2.Description, FileUrl2.Result.ToString)
                oRet = oRet + Await mailer.SendGenericMail(myUtils.cStrTN(r1("selemail")), myUtils.cStrTN(r1("selemailcc")), myUtils.cStrTN(r1("selemailbcc")), "Discrepancy in GST Tax invoice details", strHTML, attach)
            Next
        Catch ex As Exception
            context.Logger.LogInformation(ex.Message & vbCrLf & ex.StackTrace)
            oRet.AddException(ex)
        End Try
        Return oRet
    End Function
End Class
