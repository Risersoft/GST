Imports System.Collections.Concurrent
Imports System.Configuration
Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Threading
Imports GSTN.API.Library.Models.GstNirvana
Imports Microsoft.Owin.Infrastructure
Imports Newtonsoft.Json
Imports risersoft.API.GSTN
Imports risersoft.app.mxent
Imports risersoft.shared
Imports risersoft.shared.cloud

Public MustInherit Class ImportTaskProviderGstBase
    Inherits ImportTaskProviderBase(Of GstImportInfo)
    Public fncPartyType As Func(Of DataRow, String)
    Public fncPartySubTypeTable As Func(Of DataRow, String)
    Dim dicHSN As ConcurrentDictionary(Of String, DataRow), dicParty As ConcurrentDictionary(Of String, DataSet)

    Public Sub New(controller As clsAppController)
        MyBase.New(controller)
        dicHSN = New ConcurrentDictionary(Of String, DataRow)
        dicParty = New ConcurrentDictionary(Of String, DataSet)
    End Sub


    Public Overrides Sub PopulateMaster()
        oMaster.GetDataset2(False)

        Dim dicSQL As New clsCollecString(Of String)
        dicSQL.Add("campus", "select * from Campus")
        dicSQL.Add("division", "select * from division")
        dicSQL.Add("gstreg", "select * from gstreg")
        dicSQL.Add("taxarea", "select *, dbo.val(tincode) as tincodenum from taxarea")
        dicSQL.Add("co", "Select distinct countryname As country, countrycode + ' - ' + countryname, countrycode, countryname from unlocodesub where countrycode ='IN' order by countryname")
        dicSQL.Add("su", "select distinct subdivisionname as subdivision, subdivisioncode + ' - ' + subdivisionname, countrycode, SubDivisionCode, SubDivisionName from unlocodesub where countrycode='IN' order by countrycode, subdivisionname")
        dicSQL.Add("GstTaxType", myFuncsBase.CodeWordSQL("invoice", "gsttax", ""))
        dicSQL.Add("documentType", myFuncsBase.CodeWordSQL("invoice", "documentType", ""))
        dicSQL.Add("TransactionType", myFuncsBase.CodeWordSQL("invoice", "TransactionType", ""))
        dicSQL.Add("partygstRegType", myFuncsBase.CodeWordSQL("party", "gstRegType", ""))
        dicSQL.Add("rsn", myFuncsBase.CodeWordSQL("invoice", "Reason", ""))
        dicSQL.Add("ty", myFuncsBase.CodeWordSQL("invoice", "ty", ""))
        dicSQL.Add("elg", myFuncsBase.CodeWordSQL("invoice", "ELG", ""))
        dicSQL.Add("rule", "select * from validationrule where doctype='" & Me.DocType & "' and isnull(isdisabled,0)=0")
        dicSQL.Add("options", "select * from systemoptions")
        dicSQL.Add("users", "select * from users")
        dicSQL.Add("hsn", "select * from hsnsac")
        dicSQL.Add("TransactionCategory", myFuncsBase.CodeWordSQL("invoice", "TransactionCategory", ""))
        dicSQL.Add("Template", $"SELECT InvoiceNumberTemplateId, CompanyId, GSTRegId, Prefix, Suffix, DocumentNature FROM GstDocNumTemplate")
        dicSQL.Add("section", "select * from gstrsection")
        dicSQL.Add("campustype", myFuncsBase.CodeWordSQL("company", "CampusType", ""))

        dsMaster = myContext.DataProvider.objSQLHelper.ExecuteDataset(CommandType.Text, dicSQL)

    End Sub
    Protected Overrides Function UniqueFieldList(DocType As String, TableName As String) As List(Of String)
        Dim lst1 As New List(Of String)
        Select Case DocType.Trim.ToLower
            Case "ip"
                lst1 = New List(Of String)(New String() {"gstin", "ctin", "invoicenum"})
            Case "is", "isd"
                lst1 = New List(Of String)(New String() {"gstin", "invoicenum"})
            Case "pv", "pc"
                lst1 = New List(Of String)(New String() {"gstin", "vouchnum"})
            Case "tp"
                lst1 = New List(Of String)(New String() {"pannum", "gstin", "dispname"})
            Case "customer", "vendor"
                lst1 = New List(Of String)(New String() {"pannum", "selcity"})
            Case "hsn"
                lst1 = New List(Of String)(New String() {"ServiceType", "code"})
            Case "role"
                lst1 = New List(Of String)(New String() {"rolename"})
            Case "ewb"
                lst1 = New List(Of String)(New String() {"DocNum"})
            Case "chl"
                If TableName = myUtils.cStrTN("Goods Sent back from Job Worker") Then
                    lst1 = New List(Of String)(New String() {"Chnum", "Chdt", "OrigChNum", "InvoiceNum", "InvoiceDate"})
                ElseIf TableName = myUtils.cStrTN("Goods Sent to Job Worker") Then
                    lst1 = New List(Of String)(New String() {"Chnum", "Chdt"})
                End If
            Case "tds"
                lst1 = New List(Of String)(New String() {"TDSCertificateNum", "ReturnPeriod", "CTIN"})
            Case "tcs"
                lst1 = New List(Of String)(New String() {"TCSCertificateNum"})
            Case "recon"
                lst1 = New List(Of String)(New String() {"TPInvoiceNum", "CPInvoiceNum"})
            Case "defer"
                lst1 = New List(Of String)(New String() {"gstin", "invoicenum"})


        End Select
        Return lst1
    End Function


    Public Overrides Sub AddRecord(info As GstImportInfo, objGroup As clsRowGroup)
        Dim rPP = If(objGroup.dic.ContainsKey("returnperiod"), objGroup.dic("returnperiod"), Nothing)
        Dim ret_pd As String = If(rPP Is Nothing, "", rPP("ret_pd").ToString)
        If objGroup.Rows(0).Table.Columns.Contains("txval") Then
            info.AddRecord(myUtils.cStrTN(objGroup.Rows(0)("GSTIN")), ret_pd, objGroup.Rows.Count, objGroup.Output.Success,
                           myContext.Tables.GetColSum(objGroup.Rows.ToArray, "txval"),
                            myContext.Tables.GetColSum(objGroup.Rows.ToArray, "iamt"),
                            myContext.Tables.GetColSum(objGroup.Rows.ToArray, "camt"),
                            myContext.Tables.GetColSum(objGroup.Rows.ToArray, "samt"),
                            myContext.Tables.GetColSum(objGroup.Rows.ToArray, "csamt"))
        Else
            info.AddRecord(myUtils.cStrTN(objGroup.Rows(0)("GSTIN")), ret_pd, objGroup.Rows.Count, objGroup.Output.Success)
        End If
    End Sub
    Public Overridable Overloads Function GenerateTemplate(CampusFilter As String, PeriodFilter As String, fileName As String, strFilter As String) As clsProcOutput
        Return Me.GenerateTemplate(CampusFilter, PeriodFilter, fileName, strFilter, "")
    End Function
    Public Overridable Overloads Function GenerateTemplate(CampusFilter As String, PeriodFilter As String, fileName As String, strFilter As String, SheetName As String) As clsProcOutput
        Dim Sql = String.Format("select * from {0}", TemplateFunctionName) & myUtils.CombineWhere(True,
                                 CampusFilter, PeriodFilter, $"doctype='{DocType}'", strFilter)
        Return Me.GenerateTemplate(fileName, Sql, SheetName)
    End Function


    Public Overrides Function RunValidator(info As ImportErrorInfo, RowGroup As List(Of DataRow), rInvoice As DataRow, dsDB As DataSet, ItemTableName As String, UpdateObjects As Action(Of clsJintValidator, DataRow)) As Boolean
        'Run the JINTValidator
        Dim oProc As New clsJintValidator(myContext)
        If dsMaster.Tables("options").Select.Length > 0 Then
            oProc.AddOrUpdateRow(dsMaster.Tables("options").Rows(0), "")
        End If
        oProc.AddOrUpdateRow(rInvoice, "")
        oProc.SetValue("RowCount", RowGroup.Count)
        oProc.SetValue("GSTUtils", New clsJINTFuncsGSTN(myContext))
        oProc.objFuncs.dtItems = dsDB.Tables(ItemTableName)
        UpdateObjects(oProc, Nothing)
        info.Voucher.AdoptResult(oProc.ValidateRules(dsMaster.Tables("rule"), "V", 0))
        If String.IsNullOrEmpty(ItemTableName) Then
            If info.Voucher.Errors.Count > 0 OrElse info.Voucher.Warnings.Count > 0 Then
                SyncLock RowGroup(0).Table.Rows.SyncRoot
                    RowGroup(0)("errorcode") = info.Voucher.ErrorCodeCSV("")
                    RowGroup(0)("errortext") = info.Voucher.ErrorTextCSV("")
                    RowGroup(0)("warningcode") = info.Voucher.WarningCodeCSV("")
                    RowGroup(0)("warningtext") = info.Voucher.WarningTextCSV("")
                End SyncLock
            End If
        Else
            For i As Integer = 0 To RowGroup.Count - 1
                Dim r1 As DataRow = RowGroup(i)
                Dim nr As DataRow = dsDB.Tables(ItemTableName).Select()(i)
                oProc.AddOrUpdateRow(nr, ItemTableName)
                UpdateObjects(oProc, nr)
                Dim info2 As New ImportErrorLevelInfo
                info2.AdoptResult(oProc.ValidateRules(dsMaster.Tables("rule"), "I", i + 1))
                info.Items.Add(info2)
                If info.Voucher.Errors.Count > 0 OrElse info.Voucher.Warnings.Count > 0 OrElse info2.Errors.Count > 0 OrElse info2.Warnings.Count > 0 Then
                    SyncLock r1.Table.Rows.SyncRoot
                        r1("errorcode") = myUtils.MakeCSV(",", info.Voucher.ErrorCodeCSV(""), info2.ErrorCodeCSV(""))
                        r1("errortext") = myUtils.MakeCSV(",", info.Voucher.ErrorTextCSV(""), info2.ErrorTextCSV(""))
                        r1("warningcode") = myUtils.MakeCSV(",", info.Voucher.WarningCodeCSV(""), info2.WarningCodeCSV(""))
                        r1("warningtext") = myUtils.MakeCSV(",", info.Voucher.WarningTextCSV(""), info2.WarningTextCSV(""))
                    End SyncLock
                End If
            Next
        End If
        Return (info.Errorcount = 0)
    End Function

    Protected Overrides Function BuildFileUrl(authority As String, rootFile As String, currFile As String) As String
        Dim str1 = String.Format("http://{0}/frmGstImportVouch/FileDetails/{1}/{2}", authority, rootFile, currFile)
        Dim dic2 = myContext.GetAppInfo.PopulateAppBarDict(New clsCollecString(Of String))
        str1 = WebUtilities.AddQueryString(str1, dic2)
        Return str1
    End Function
    Public Function FindHSN(provider As IAppDataProvider, rItem As DataRow) As DataRow
        Dim nr As DataRow
        Dim Code As String = myUtils.cStrTN(rItem("hsn_sc"))
        Try
            If Not String.IsNullOrEmpty(Code) Then
                dicHSN.TryGetValue(Code, nr)
                If nr Is Nothing Then
                    For i As Integer = 1 To 5
                        Try
                            Dim dic As New clsCollecString(Of String)
                            dic.Add("hsn", String.Format("select * from hsnsac where code='{0}'", Code))
                            Dim dt1 As DataTable = provider.objSQLHelper.ExecuteDataset(CommandType.Text, dic).Tables(0)
                            Me.CheckAddOpInfo(provider, dic)
                            If dt1.Rows.Count > 0 Then
                                nr = dt1.Rows(0)
                            Else
                                nr = provider.Tables.AddNewRow(dt1)
                                nr("code") = Code
                                nr("description") = If(myUtils.cStrTN(rItem("hsn_desc")).Trim.Length > 0, rItem("hsn_desc"), myUtils.cStrTN(rItem("description")))
                                nr("txrt") = myUtils.cValTN(rItem("I_RT")) + myUtils.cValTN(rItem("C_RT")) + myUtils.cValTN(rItem("S_RT"))
                                nr("ty") = rItem("ty")
                                provider.objSQLHelper.SaveResults(dt1, dic("hsn"), dicOpInfo("hsn"))
                            End If
                            Exit For
                        Catch ex As Exception
                            'May have unique constraint error
                            myContext.Logger.logInformation("Could not get/save HsnSac:" & Code & ", Exception: ex.Message")
                        End Try
                        Thread.Sleep(10)
                    Next
                    If nr IsNot Nothing Then dicHSN.AddOrUpdate(Code, nr, Function(x, y) nr)
                End If

            End If
        Catch ex As Exception
            myContext.Logger.logInformation("Could not Find HsnSac:" & Code & ", Exception: ex.Message")
        End Try

        Return nr
    End Function
    Public Function FindGSTIN(GSTIN As String) As DataRow
        Dim rrGstReg() As DataRow = dsMaster.Tables("gstreg").Select("gstin='" & GSTIN & "'")
        If rrGstReg.Length > 0 Then Return rrGstReg(0)
    End Function
    Public Function FindGstRegID(GstRegID As Integer) As DataRow
        Dim rrGstReg() As DataRow = dsMaster.Tables("gstreg").Select("GstRegID=" & GstRegID)
        If rrGstReg.Length > 0 Then Return rrGstReg(0)
    End Function
    Public Function FindCampus(CampusID As Integer) As DataRow
        Dim rrCampus() As DataRow = dsMaster.Tables("campus").Select("campusid=" & CampusID)
        If rrCampus.Length > 0 Then Return rrCampus(0)
    End Function
    Public Function FindGstRegCampus(GstRegID As Integer) As DataRow
        Dim rrCampus() As DataRow = dsMaster.Tables("campus").Select("gstregid=" & GstRegID)
        If rrCampus.Length > 0 Then Return rrCampus(0)
    End Function

    Public Overridable Async Function GetMasterData(provider As IAppDataProvider, objGroup As clsRowGroup, objPortion As clsPortionInfo) As Task(Of Boolean)
        Dim dic2 As New clsCollecString(Of String)
        Dim rXL = objGroup.Rows(0), dic = objGroup.dic
        Dim rGstReg As DataRow = Me.FindGSTIN(myUtils.cStrTN(rXL("gstin")))
        Dim PartyType As String = Me.fncPartyType(rXL)
        Dim PartySubTypeTable As String = Me.fncPartySubTypeTable(rXL)
        If rGstReg IsNot Nothing Then
            dic.Add("gstreg", rGstReg)
            'Campusid
            Dim rCampus As DataRow = Me.FindGstRegCampus(myUtils.cValTN(rGstReg("gstregid")))
            'Divisionid
            If rCampus IsNot Nothing Then
                dic.Add("campus", rCampus)
                Dim rrDivision() As DataRow = dsMaster.Tables("division").Select("companyid=" & rCampus("companyid"))
                If rrDivision.Length > 0 Then dic.Add("division", rrDivision(0))
            End If

        End If
        'Customerid / Vendorid
        Dim oRet = Await Me.GetOrAddParty(provider, myUtils.cStrTN(rXL("ctin")), myUtils.cStrTN(rXL("PartyGstRegType")), myUtils.cStrTN(rXL("partyname")), "", myUtils.cStrTN(rXL("PartyLocation")), PartySubTypeTable, PartyType, If(myUtils.IsInList(PartyType, "V"), "MS", ""), dic2)
        If oRet.Success Then
            Dim ds As DataSet
            SyncLock objLock
                ds = oRet.Data.Copy
            End SyncLock
            Dim dt1 As DataTable = provider.Data.outerJoin(ds, "party")
            dic.Add("party", dt1.Rows(0))
        End If

        Dim rPostPeriod = Me.FindReturnPeriod(objGroup, rXL, "returnperiod", rGstReg, "GstRegPP")
        Return True
    End Function
    Protected Friend Overridable Function FindReturnPeriod(objGroup As clsRowGroup, rXL As DataRow, FieldName As String, rGstReg As DataRow, GstRegPPObjectName As String) As DataRow
        Dim rPostPeriod As DataRow, ChangeSource As Boolean = False
        Dim arr() As String = Split(myUtils.cStrTN(rXL(FieldName)), "-")
        Try
            If arr.Length = 2 Then
                rPostPeriod = oMaster.rPostPeriod(New Date(arr(1), arr(0), 1))
            ElseIf IsNumeric(myUtils.cStrTN(rXL(FieldName))) Then
                Dim Dated = DateTime.FromOADate(myUtils.cValTN(rXL(FieldName)))
                rPostPeriod = oMaster.rPostPeriod(Dated)
                ChangeSource = True
            Else
                Dim Dated = Me.DateFromString(myUtils.cStrTN(rXL(FieldName)))
                rPostPeriod = oMaster.rPostPeriod(Dated)
                ChangeSource = True
            End If
            If Not rPostPeriod Is Nothing Then
                If ChangeSource Then
                    SyncLock (objGroup.Rows(0).Table.Rows.SyncRoot)
                        For Each r1 As DataRow In objGroup.Rows
                            r1(FieldName) = Format(rPostPeriod("sdate"), "MM-yyyy")
                        Next
                    End SyncLock

                End If
                objGroup.dic.Add(FieldName, rPostPeriod)
                If (rGstReg IsNot Nothing) AndAlso (Not String.IsNullOrEmpty(GstRegPPObjectName)) Then
                    Dim rGstPP = oMaster.GstRegPPRow(rGstReg("gstregid"), myUtils.cValTN(rPostPeriod("PostPeriodid")))
                    objGroup.dic.Add(GstRegPPObjectName, rGstPP)
                End If
            End If
        Catch ex As Exception
            myContext.Logger.logInformation("Could Not Get Posting Period for " & myUtils.cStrTN(rXL(FieldName)) & ": " & ex.Message)
        End Try
        Return rPostPeriod
    End Function
    Public Overridable Function ExecutePreValidationMaster(rVouch As DataRow, dtItems As DataTable, rXL As DataRow, dic As clsCollecString(Of DataRow)) As clsProcOutput
        Dim oRet As New clsProcOutput
        Dim PartySubTypeTable = Me.fncPartySubTypeTable(rXL)
        Try
            If dic.ContainsKey("campus") Then rVouch("campusid") = dic("Campus")("campusid")
            If rVouch.Table.Columns.Contains("divisionid") AndAlso dic.ContainsKey("division") Then rVouch("divisionid") = dic("division")("divisionid")
            If dic.ContainsKey("party") Then rVouch(PartySubTypeTable & "id") = dic("party")(PartySubTypeTable & "id")
            If dic.ContainsKey("returnperiod") Then rVouch("returnperiodid") = dic("returnperiod")("postperiodid")
            If (rVouch.Table.Columns.Contains("StatusNum")) Then rVouch("StatusNum") = 2

            'POSTaxAreaID
            For Each str1 As String In New String() {"pos"}
                If rXL.Table.Columns.Contains(str1) Then
                    Dim rPOS = myFuncs2.FindTaxAreaRow(dsMaster.Tables("taxarea"), myUtils.cStrTN(rXL(str1)))
                    If rPOS IsNot Nothing Then
                        rVouch(str1 & "taxareaid") = rPOS("taxareaid")
                        dic.Add(str1, rPOS)
                    End If
                End If
            Next

            If (rVouch.Table.Columns.Contains("partygstregtype")) Then
                If String.IsNullOrEmpty(myUtils.cStrTN(rVouch("partygstregtype"))) Then
                    rVouch("partygstRegType") = If(String.IsNullOrEmpty(myUtils.cStrTN(rXL("CTIN"))), "N", "R")
                End If
            End If

            'DocType
            If (rVouch.Table.Columns.Contains("doctype")) Then rVouch("doctype") = Me.DocType

        Catch ex As Exception
            oRet.AddException(ex)
        End Try
        Return oRet
    End Function



    Public MustOverride Function FindVouch(provider As IAppDataProvider, CampusFilter As String, InvoiceNum As String, strInvoiceDate As String, CounterFilter As String) As DataRow

    Protected Friend Function ExecuteGetOrAddParty(provider As IAppDataProvider, GSTIN As String, RegType As String, PartyName As String, TaxAreaCode As String, PartyLocation As String, PartySubTypeTable As String, PartyType As String, PartySubType As String, dic As clsCollecString(Of String)) As clsProcOutput
        Dim oRet As New clsProcOutput
        Dim str1 As String = String.Format("gstin='{0}' and mainpartyid in (select mainpartyid from partymain where partytype in ('A','{1}') and isnull(partysubtype,'') in ('','{2}'))", GSTIN, PartyType, PartySubType)
        Dim dicSQL As New clsCollecString(Of String)
        dicSQL.Add("partymain", String.Format("select * from partymain where mainpartyid in (select mainpartyid from party where gstin='{0}') and partytype in ('A','{1}') and isnull(partysubtype,'') in ('','{2}')", GSTIN, PartyType, PartySubType))
        dicSQL.Add("party", "select * from party where " & str1)
        Select Case PartySubTypeTable.Trim.ToLower
            Case "vendor"
                dicSQL.Add("vendor", "select * from vendor where partyid in (select partyid from party where " & str1 & ")")
            Case "customer"
                dicSQL.Add("customer", "select * from customer where partyid in (select partyid from party where " & str1 & ")")
        End Select
        Me.CheckAddOpInfo(provider, dicSQL)
        Dim dsParty = provider.objSQLHelper.ExecuteDataset(CommandType.Text, dicSQL)
        Dim rr1() As DataRow = dsParty.Tables("partymain").Select()
        Dim rr2() As DataRow = dsParty.Tables("party").Select("gstin='" & GSTIN & "'")
        Dim rr3() As DataRow = If(rr2.Length > 0, dsParty.Tables(PartySubTypeTable).Select("partyid=" & rr2(0)("partyid")), New DataRow() {})
        If rr1.Length > 0 AndAlso rr2.Length > 0 AndAlso rr3.Length > 0 Then
            oRet.ID = rr3(0)(PartySubTypeTable & "ID")
            oRet.Data = dsParty
        Else
            SyncLock (objLock)
                'get again to avoid creating multiple parties with same GSTIN
                dsParty = provider.objSQLHelper.ExecuteDataset(CommandType.Text, dicSQL)
                myContext.Tables.SetAuto(dsParty.Tables("partymain"), dsParty.Tables("party"), "mainpartyid")
                If dsParty.Tables.Contains("customer") Then
                    'Sale Import
                    myContext.Tables.SetAuto(dsParty.Tables("party"), dsParty.Tables("customer"), "partyid")
                Else
                    'Purchase import
                    myContext.Tables.SetAuto(dsParty.Tables("party"), dsParty.Tables("vendor"), "partyid")
                End If

                rr1 = dsParty.Tables("partymain").Select()
                rr2 = dsParty.Tables("party").Select("gstin='" & GSTIN & "'")
                rr3 = If(rr2.Length > 0, dsParty.Tables(PartySubTypeTable).Select("partyid=" & rr2(0)("partyid")), New DataRow() {})

                Dim rPartyMain, rParty, rPartySubType As DataRow
                If rr1.Length > 0 Then
                    rPartyMain = rr1(0)
                Else
                    rPartyMain = myContext.Tables.AddNewRow(dsParty.Tables("partymain"))
                    rPartyMain("countrycode") = "IN"
                    rPartyMain("MPName") = PartyName
                    Dim rrSub() As DataRow = dsMaster.Tables("Su").Select("subdivisioncode='" & TaxAreaCode & "'")
                    If rrSub.Length > 0 Then
                        rPartyMain("state") = rrSub(0)("subdivisionname")
                        rPartyMain("statecode") = TaxAreaCode
                    End If
                    If Not String.IsNullOrEmpty(PartyLocation) Then rPartyMain("address") = PartyLocation
                    rPartyMain("partytype") = PartyType
                    rPartyMain("partysubtype") = PartySubType
                    For Each kvp In dic
                        If rPartyMain.Table.Columns.Contains(kvp.Key) Then rPartyMain(kvp.Key) = kvp.Value
                    Next
                    provider.objSQLHelper.SaveResults(dsParty.Tables("partymain"), dicSQL("partymain"), dicOpInfo("partymain"))
                End If


                If rr2.Length > 0 Then
                    rParty = rr2(0)
                Else
                    rParty = myContext.Tables.AddNewRow(dsParty.Tables("party"))
                    For Each str2 As String In New String() {"partygstregtype"}
                        Dim rrPOS() As DataRow = dsMaster.Tables(str2).Select("Descrip='" & RegType & "'")
                        If rrPOS.Length > 0 Then
                            rParty("GSTRegType") = rrPOS(0)("codeword")
                        ElseIf String.IsNullOrEmpty(GSTIN) Then
                            rParty("GSTRegType") = "N"
                        Else
                            rParty("GSTRegType") = "R"
                        End If
                    Next
                    rParty("gstin") = GSTIN
                    rParty("mainpartyid") = rPartyMain("mainpartyid")
                    rParty("PartyName") = PartyName
                    If Not String.IsNullOrEmpty(PartyLocation) Then rParty("seladdress") = PartyLocation
                    Dim rTA = myFuncs2.FindTaxAreaRow(dsMaster.Tables("taxarea"), TaxAreaCode)
                    If rTA IsNot Nothing Then rParty("taxareaid") = rTA("taxareaid")
                    Dim objNum As New clsVoucherNum(provider)
                    rParty("partycode") = objNum.GetNewSerialNo("PartyID", myUtils.cStrTN(rPartyMain("partytype")), rParty)
                    For Each kvp In dic
                        If rParty.Table.Columns.Contains(kvp.Key) Then rParty(kvp.Key) = kvp.Value
                        If rParty.Table.Columns.Contains("Sel" & kvp.Key) Then rParty("Sel" & kvp.Key) = kvp.Value
                    Next
                    provider.objSQLHelper.SaveResults(dsParty.Tables("party"), dicSQL("party"), dicOpInfo("party"))
                End If


                If rr3.Length > 0 Then
                    rPartySubType = rr3(0)
                Else
                    rPartySubType = myContext.Tables.AddNewRow(dsParty.Tables(PartySubTypeTable))
                    rPartySubType("importfileid") = Me.ImportFileID
                    rPartySubType("partyid") = rParty("partyid")
                    rPartySubType(PartySubTypeTable & "code") = rParty("partycode")
                    provider.objSQLHelper.SaveResults(dsParty.Tables(PartySubTypeTable), dicSQL(PartySubTypeTable), dicOpInfo(PartySubTypeTable))
                End If
                oRet.ID = rPartySubType(PartySubTypeTable & "ID")
                oRet.Data = dsParty
            End SyncLock
        End If

        Return oRet
    End Function
    Public Async Function GetOrAddParty(provider As IAppDataProvider, GSTIN As String, RegType As String, PartyName As String, TaxAreaCode As String, PartyLocation As String, PartySubTypeTable As String, PartyType As String, PartySubType As String, dic As clsCollecString(Of String)) As Task(Of clsProcOutput)
        Dim oRet As New clsProcOutput
        If (String.IsNullOrEmpty(GSTIN)) Then
            oRet.AddError("Unregistered Party")
        ElseIf (Not GSTUtils.ValidateGSTIN(GSTIN)) AndAlso (Not GSTUtils.ValidateUIN(GSTIN)) Then
            oRet.AddError("Invalid GSTIN")
        End If

        If oRet.Success Then
            Dim ds As DataSet
            Dim cont = dicParty.TryGetValue(GSTIN, ds)
            If cont Then
                oRet.Data = ds.Copy
            Else
                If String.IsNullOrEmpty(RegType) Then
                    RegType = If(String.IsNullOrEmpty(GSTIN), "N", "R")
                End If
                If (String.IsNullOrEmpty(TaxAreaCode)) Then
                    TaxAreaCode = myUtils.cValTN(Left(GSTIN, 2))
                End If

                If String.IsNullOrEmpty(PartyName.Trim) Then PartyName = GSTIN
                For i As Integer = 1 To 5
                    Try
                        oRet = Me.ExecuteGetOrAddParty(provider, GSTIN, RegType, PartyName, TaxAreaCode, PartyLocation, PartySubTypeTable, PartyType, PartySubType, dic)
                        Exit For
                    Catch ex As Exception
                        'Threading / Locking error while GetorAddParty
                        oRet.AddException(ex)
                        myContext.Logger.logInformation(ex.Message)
                    End Try
                    Await Task.Delay(10)
                Next
                If oRet.Success Then
                    dicParty.AddOrUpdate(GSTIN, oRet.Data, Function(x, y) oRet.Data)
                End If
            End If
        Else
            myContext.Logger.logInformation(oRet.Message)
        End If

        Return oRet


    End Function

    Protected Overrides Async Function CreateData(provider As IAppDataProvider, Groups As List(Of clsRowGroup), objPortion As clsPortionInfo) As Task(Of Boolean)
        Dim info As clsSaveOpInfo
        Dim InvoiceDescrip = $"Invoice {Me.DocType} for Import File {Me.ImportFileID.ToString} from rows {objPortion.StartRow} to {objPortion.EndRow}"

        Dim TryCount As Integer = 0, success As Boolean = False

        While (Not success) AndAlso (TryCount < 5)
            Try
                TryCount = TryCount + 1
                myContext.Logger.logInformation($"Begin CreateData portion item {InvoiceDescrip} Try Count = {TryCount}")


                objPortion.dicWhereGet.Clear()
                For Each grp In Groups
                    Dim rXL = grp.Rows(0)
                    grp.dic.Clear()
                    Await Me.GetMasterData(provider, grp, objPortion)
                    grp.dicWhere = Me.GenerateSQL(provider, grp, objPortion)
                    grp.dicSQL = Me.GenerateSQL(grp.dicWhere)
                    grp.dsDB = New DataSet
                Next

                Dim firstgrp = Groups.First, dicWhere2 As New clsCollecString(Of String)
                For Each kvp In firstgrp.dicWhere
                    Dim lst = Groups.Select(Function(x) x.dicWhere(kvp.Key)).ToList
                    objPortion.dicWhereGet.Add(kvp.Key, myUtils.CombineWhereOR(False, lst.ToArray))
                    dicWhere2.Add(kvp.Key, "0=1")
                Next
                objPortion.dicSQLSave = Me.GenerateSQL(dicWhere2)
                objPortion.dicSQLGet = Me.GenerateSQL(objPortion.dicWhereGet)
                Me.CheckAddOpInfo(provider, objPortion.dicSQLSave)
                info = Me.dicOpInfo(firstgrp.dicSQL.GetIndexKey(0))

                objPortion.IDField = info.AutoIDColumn
                objPortion.dsTot = provider.objSQLHelper.ExecuteDataset(CommandType.Text, objPortion.dicSQLSave)
                Dim ds = provider.objSQLHelper.ExecuteDataset(CommandType.Text, objPortion.dicSQLGet(0))
                Dim dt1 = myUtils.AddTable(objPortion.dsTot, ds.Tables(0), objPortion.dsTot.Tables(0).TableName)

                objPortion.IDValueCSV = myUtils.MakeCSV(dt1.Select, info.AutoIDColumn)
                For Each grp In Groups
                    Dim dtg1 = myUtils.AddTable(grp.dsDB, dt1.Clone, dt1.TableName)
                    myUtils.CopyRows(dt1.Select(grp.dicWhere(dt1.TableName)), dtg1)
                    grp.IDValueCSV = myUtils.MakeCSV(dtg1.Select, info.AutoIDColumn)
                Next

                For i As Integer = 1 To objPortion.dicSQLGet.Count - 1
                    Dim TableName = objPortion.dicSQLGet.GetIndexKey(i)
                    If Not myUtils.EndsWith(TableName, "_0") Then
                        objPortion.dicWhereGet.AddUpd(TableName, info.AutoIDColumn & " IN (" & objPortion.IDValueCSV & ")")
                        For Each grp In Groups
                            grp.dicWhere.AddUpd(TableName, info.AutoIDColumn & " IN (" & grp.IDValueCSV & ")")
                        Next
                    End If

                Next

                objPortion.dicSQLGet = Me.GenerateSQL(objPortion.dicWhereGet)
                For i As Integer = 1 To objPortion.dicSQLGet.Count - 1
                    Dim TableName = objPortion.dicSQLGet.GetIndexKey(i)
                    Dim ds2 = provider.objSQLHelper.ExecuteDataset(CommandType.Text, objPortion.dicSQLGet(i))
                    Dim dt2 = myUtils.AddTable(objPortion.dsTot, ds2.Tables(0), TableName)
                    For Each grp In Groups
                        Dim dtg2 = myUtils.AddTable(grp.dsDB, dt2.Clone, dt2.TableName)
                        myUtils.CopyRows(dt2.Select(grp.dicWhere(i)), dtg2)
                    Next
                Next

                Groups.ForEach(Sub(x) x.dsDB.AcceptChanges())

                myContext.Logger.logInformation($"End CreateData portion item {InvoiceDescrip}")
                success = True
            Catch ex As Exception
                myContext.Logger.logInformation($"Error while executing portion {objPortion.StartRow} - {objPortion.EndRow}:" & ex.Message & ex.StackTrace)
                success = False
            End Try
        End While




        Return success
    End Function
    Protected Overrides Async Function HandleGroupData(provider As IAppDataProvider, objGroup As clsRowGroup, objPortion As clsPortionInfo) As Task(Of clsProcOutput)
        Dim oRet As New clsProcOutput, dsDB As DataSet
        Dim rXL = objGroup.Rows(0)
        Dim dic = objGroup.dic
        If UpdateBatchSize = 1 Then
            Await Me.GetMasterData(provider, objGroup, objPortion)
            objGroup.dicSQL.Clear()
            Dim dic2 = Me.GenerateSQL(provider, objGroup, objPortion)
            dic2.CopyTo(objGroup.dicSQL)
            dsDB = provider.objSQLHelper.ExecuteDataset(CommandType.Text, objGroup.dicSQL)
            Me.CheckAddOpInfo(provider, objGroup.dicSQL)
            oRet.Data = dsDB
        Else
            dsDB = objGroup.dsDB
        End If


        If IsCompareOperation Then
            oRet.Success = (dsDB.Tables("vouch").Rows.Count > 0)
        Else
            Dim nr As DataRow
            If dsDB.Tables("vouch").Rows.Count > 0 Then
                'For before return period- returnperiodbefore, usable when file is uploaded for editing.
                Dim rPP = oMaster.rPostPeriod(myUtils.cValTN(dsDB.Tables("vouch").Rows(0)("returnperiodid")))
                dic.Add("returnperiodbefore", rPP)

                nr = dsDB.Tables("vouch").Rows(0)
                If myUtils.IsInList(nr("importfileid").ToString, Me.ImportFileID.ToString) AndAlso (objGroup.Rows.Count = dsDB.Tables("item").Select.Length) Then
                    'this file is running again and the invoice need not be imported again
                Else
                    For Each dt1 As DataTable In dsDB.Tables
                        myUtils.DeleteRows(dt1.Select)
                    Next
                    oRet.AddWarning("Duplicate Record")
                    nr = Nothing
                End If
            End If

            If nr Is Nothing Then
                nr = myUtils.CopyOneRow(rXL, dsDB.Tables("vouch"))
                nr("importfileid") = Me.ImportFileID
                For Each r1 As DataRow In objGroup.Rows
                    Dim nr2 As DataRow = myUtils.CopyOneRow(r1, dsDB.Tables("item"))
                    Me.UpdateItem(r1, nr2)
                Next

            End If
        End If
        Return oRet
    End Function
    Protected Overrides Sub PopulateImportFileRow(ImpInfo As GstImportInfo, rFile As DataRow)
        If ImpInfo Is Nothing Then
            rFile("datarowcount") = 0
            rFile("datarecordcount") = 0
            rFile("dataerrorcount") = 0
        Else
            rFile("SummaryJson") = JsonConvert.SerializeObject(ImpInfo)
            rFile("datarowcount") = ImpInfo.GSTIN_List.Sum(Function(x) x.RowCount)
            rFile("datarecordcount") = ImpInfo.GSTIN_List.Sum(Function(x) x.RecordCount)
            rFile("dataerrorcount") = ImpInfo.GSTIN_List.Sum(Function(x) x.ErrorCount)
        End If

    End Sub
    Protected Overrides Function CombineOutput(lst As List(Of clsProcOutput(Of GstImportInfo))) As GstImportInfo
        Dim ImpInfo As New GstImportInfo
        For Each lstitem In lst
            For Each info2 In lstitem.Result.GSTIN_List
                Dim newInfo = ImpInfo.AddInfo(info2)
                If (Not newInfo Is Nothing) AndAlso (String.IsNullOrEmpty(newInfo.State)) Then
                    Dim rr() As DataRow = dsMaster.Tables("taxarea").Select("TINCODE='" & Left(newInfo.GSTIN, 2) & "'")
                    If rr.Length > 0 Then newInfo.State = rr(0)("taxareacode")
                End If
            Next
        Next
        Return ImpInfo
    End Function

    Protected Overrides Function SaveData(provider As IAppDataProvider, Groups As List(Of clsRowGroup), objPortion As clsPortionInfo) As Task(Of Boolean)
        Dim PortionDescrip = $"Invoice {Me.DocType} for Import File {Me.ImportFileID.ToString} from rows {objPortion.StartRow} to {objPortion.EndRow}"
        Dim TryCount As Integer = 0, success As Boolean = False

        While (Not success) AndAlso (TryCount < 5)
            Try
                TryCount = TryCount + 1
                myContext.Logger.logInformation($"Begin SaveData portion {PortionDescrip}, Try count={TryCount}")
                Dim dic As New clsCollection(Of DataRow, clsRowGroup)
                Dim dsDB = objPortion.dsTot.Clone
                Dim rel = provider.Tables.SetAuto(dsDB.Tables("vouch"), dsDB.Tables("item"), objPortion.IDField)
                Dim IDValue As Integer = -9000000
                For Each grp In Groups
                    If grp.Output.Success Then
                        For Each str1 As String In New String() {"vouch", "item"}
                            Dim tbl = dsDB.Tables(str1)
                            For Each r1 As DataRow In grp.dsDB.Tables(str1).Rows
                                If r1.RowState = DataRowState.Deleted Then
                                    tbl.ImportRow(r1)
                                Else
                                    If myUtils.NullNot(r1(objPortion.IDField)) Then r1(objPortion.IDField) = IDValue
                                    tbl.ImportRow(r1)
                                    Dim nr As DataRow = tbl.Rows(tbl.Rows.Count - 1)
                                    dic.Add(nr, grp)
                                End If
                            Next
                        Next
                        IDValue = IDValue + 1
                    End If
                Next

                provider.dbConn.BeginTransaction(provider, Me.GetType.Name, EnumfrmMode.acAddM, "ImportFileID", Me.ImportFileID.ToString)
                For Each str1 As String In New String() {"item", "vouch"}
                    provider.objSQLHelper.SaveResults(dsDB.Tables(str1), objPortion.dicSQLSave(str1), dicOpInfo(str1), True, "0=1")
                    myContext.Logger.logInformation($"Row count for SaveData portion {PortionDescrip} Table {str1} try count {TryCount} = " & dsDB.Tables(str1).Select.Length)
                Next

                provider.objSQLHelper.SaveResults(dsDB.Tables("vouch"), objPortion.dicSQLSave("vouch"), dicOpInfo("vouch"))
                myContext.Logger.logInformation($"Milestone completed vouch of SaveData portion {PortionDescrip}")

                For Each r1 As DataRow In dsDB.Tables("vouch").GetErrors
                    Dim objGroup = dic(r1)
                    objGroup.Output.AddError(r1.RowError)
                    Me.UpdateErrorMsg(objGroup.Rows, Me.DatabaseTransactionErrorCode, r1.RowError)
                    myUtils.RemoveRows(r1.GetChildRows(rel))
                Next
                provider.objSQLHelper.SaveResults(dsDB.Tables("item"), objPortion.dicSQLSave("item"), dicOpInfo("item"))

                For Each r2 As DataRow In dsDB.Tables("item").GetErrors
                    myContext.Logger.logInformation($"Error in item row of SaveData portion {PortionDescrip} = {r2.RowError}, Row Data = {myUtils.RowValuesText(r2)}")
                Next
                provider.dbConn.CommitTransaction(PortionDescrip, Me.ImportFileID.ToString)
                success = True
            Catch ex As Exception
                myContext.Logger.logInformation($"End SaveData portion {PortionDescrip} try count {TryCount} with exception: " & ex.Message)
                provider.dbConn.RollBackTransaction(PortionDescrip, ex.Message, False)
                success = False
            End Try
        End While
        myContext.Logger.logInformation($"End SaveData portion  {PortionDescrip}")
        Return Task.FromResult(True)
    End Function

End Class
