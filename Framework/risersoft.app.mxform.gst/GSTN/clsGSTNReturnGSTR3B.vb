Imports risersoft.shared
Imports risersoft.API.GSTN.GSTR3B
Imports risersoft.API.GSTN
Imports Newtonsoft.Json
Imports System.Text
Imports System.Web
Imports System.Reflection
Imports risersoft.shared.Extensions
Imports risersoft.API.GSTN.Ledger
Imports GSTN.API.Library.Models.GstNirvana

Public Class clsGSTNReturnGSTR3B
    Inherits clsGSTNTypedReturnBase(Of GSTR3BTotal, GSTR3BTotal, GSTR3BApiClient, GSTR3BApiClient)
    Public Sub New(context As IProviderContext)
        MyBase.New(context, "", "GSTR3B")
    End Sub
    Public Overrides Function PrepareGSTRPayloadSQLUp(GstRegID As Integer, ReturnPeriodID As Integer) As clsCollecString(Of String)
        Dim strf2 As String = "GstRegID=" & GstRegID
        Dim strf3 As String = "ReturnPeriodID=" & ReturnPeriodID

        Dim strFilterSumm As String = myUtils.CombineWhere(False, strf2, strf3)
        Dim dic As New clsCollecString(Of String)
        dic.Add("sup_details", " select gstregid,returnperiodid,gstin, ret_pd,section2,
                sum(txval) as txval,sum(iamt) as iamt,sum(camt) as camt,sum(samt) as samt,sum(csamt) as csamt from gstListGSTR3b()" &
                myUtils.CombineWhere(True, strFilterSumm, "isnull(txval,0)<>0 and tablenum='3.1'") & " group by gstregid,returnperiodid,gstin,ret_pd,section2")

        dic.Add("inter_sup", " select gstregid,returnperiodid,gstin, ret_pd,section2, POSTaxAreaTinCode as pos,
                sum(txval) as txval,sum(iamt) as iamt from gstListGSTR3B() " & myUtils.CombineWhere(True, strFilterSumm, "tablenum='3.2' and isnull(txval,0)<>0") &
                " group by gstregid,returnperiodid,gstin,ret_pd,section2,POSTaxAreaTinCode")

        dic.Add("itc_elg", "select gstregid,returnperiodid,gstin, ret_pd,section2,ty,
                sum(iamt) as iamt,sum(camt) as camt,sum(samt) as samt, sum(csamt) as csamt from gstListGSTR3B() " &
                myUtils.CombineWhere(True, strFilterSumm, "tablenum in ('4A','4B','4C','4D') and (isnull(iamt,0) + isnull(camt,0))<>0") & " group by gstregid,returnperiodid,gstin,ret_pd,section2,ty")

        dic.Add("inward_sup", "select gstregid,returnperiodid,gstin, ret_pd,'isup_details' as section2,ty,sum(InterSuply) as inter,sum(IntraSuply) as intra 
                from gstListGSTR3B() " & myUtils.CombineWhere(True, strFilterSumm, "tablenum='5' and (isnull(intersuply,0) + isnull(intrasuply,0))<>0") &
                " group by ty,gstregid,returnperiodid,gstin,ret_pd")


        Return dic

    End Function

    Public Overrides Sub PopulateObject(Of TModel)(Model As TModel, ds As DataSet)
        Dim Model2 = TryCast(Model, GSTR3BTotal)
        For Each dt1 As DataTable In ds.Tables
            Trace.WriteLine("Payload: Table name= " & dt1.TableName & ", Row count=" & dt1.Rows.Count)
            Dim prop As PropertyInfo = Model.GetType.FindProperty(dt1.TableName)
            If (Not prop Is Nothing) Then
                Dim t2 As Type = prop.PropertyType
                If dt1.Columns.Contains("section2") Then
                    Dim ds2 As New DataSet, cnt As Integer = 0
                    For Each r1 As DataRow In myContext.Data.SelectDistinct(dt1, "Section2").Select
                        Dim dt2 As DataTable = dt1.Clone
                        dt2.TableName = r1("section2")
                        Dim rr = dt1.Select(myContext.SQL.GenerateSQLWhere(r1, "section2"))
                        cnt = cnt + rr.Length
                        myUtils.CopyRows(rr, dt2)
                        ds2.Tables.Add(dt2)
                    Next
                    If cnt > 0 Then
                        Dim obj = Activator.CreateInstance(t2)
                        MyBase.PopulateObject(obj, ds2)
                        prop.SetValue(Model, obj)
                    End If
                Else
                    MyBase.SetPropertyValue(Model, prop, t2, dt1)
                End If
            End If
        Next


    End Sub
    Public Overrides Sub PopulateDataTable(Of TModel)(Model As TModel, dt As DataTable)
        For Each prop In Model.GetType.GetProperties
            Trace.WriteLine("Property Name:" & prop.Name)
            Dim obj = prop.GetValue(Model)
            If obj Is Nothing Then Continue For
            If (prop.PropertyType.GetInterfaces.Contains(GetType(IList))) OrElse dt.Columns.Contains("section2") Then
                Dim RowList = Me.PopulateDataTable(obj, prop, dt)
                If dt.Columns.Contains("section2") Then
                    For Each r1 As DataRow In RowList
                        r1("section2") = prop.Name
                    Next
                End If
            ElseIf Not myUtils.AttributableTypes.Contains(prop.PropertyType) Then
                Me.PopulateDataTable(obj, dt)
            End If

        Next

    End Sub

    Protected Friend Overrides Sub PopulateSignRow(nr As DataRow, payload As String)
        'https://groups.google.com/forum/#!topic/gst-suvidha-provider-gsp-discussion-group/xXqYa_YHv_k



        Dim arr() As String = Split(payload, "tx_pmt:")
        If arr.Length > 1 Then
            Dim PartA = arr(0).Trim
            If myUtils.EndsWith(PartA, ",") Then PartA = Left(PartA, PartA.Length - 1) & "}"
            Dim HashA = EncryptionUtils.convertByteArrayToString(EncryptionUtils.sha256_hash(Convert.ToBase64String(Encoding.UTF8.GetBytes(PartA))))

            Dim PartB = arr(1).Trim
            If myUtils.EndsWith(PartB, "}") Then PartB = Left(PartB, PartB.Length - 1)
            Dim HashB = EncryptionUtils.convertByteArrayToString(EncryptionUtils.sha256_hash(Convert.ToBase64String(Encoding.UTF8.GetBytes(PartB))))


            Dim Base64Payload = Convert.ToBase64String(Encoding.UTF8.GetBytes(HashA & HashB))
            Dim hash = EncryptionUtils.sha256_hash(Base64Payload)
            nr("payload") = Base64Payload
            nr("hashedpayload") = EncryptionUtils.convertByteArrayToString(hash)
        Else
            MyBase.PopulateSignRow(nr, payload)
        End If




    End Sub
    Public Overrides Async Function PrepareSummaryViews(CampusFilter As String, PeriodFilter As String, SummaryType As String) As Task(Of List(Of clsDummyView))
        Dim lst As New List(Of clsDummyView)
        Dim strFilter As String = myUtils.CombineWhere(False, CampusFilter, PeriodFilter)

        Select Case SummaryType.Trim.ToLower
            Case "payment"
                lst.Add(Await PrepareView("ListTaxPayment", strFilter))
            Case "finaltax"
                lst.Add(Await PrepareView("listGSTR3BStateSumm", strFilter))
                lst.Add(Await PrepareView("listGSTR3BSecSumm", strFilter))
            Case "calctax"
                lst.Add(Await PrepareView("listGSTR3BStateCalcSumm", strFilter))
                lst.Add(Await PrepareView("listGSTR3BT31", strFilter))
                lst.Add(Await PrepareView("listGSTR3BT31A", strFilter))
                lst.Add(Await PrepareView("listGSTR3BT31B", strFilter))
                lst.Add(Await PrepareView("listGSTR3BT31C", strFilter))
                lst.Add(Await PrepareView("listGSTR3BT31D", strFilter))
                lst.Add(Await PrepareView("listGSTR3BT31E", strFilter))
                lst.Add(Await PrepareView("listGSTR3BT32", strFilter))
                lst.Add(Await PrepareView("listGSTR3BT32A", strFilter))
                lst.Add(Await PrepareView("listGSTR3BT32B", strFilter))
                lst.Add(Await PrepareView("listGSTR3BT32C", strFilter))
                lst.Add(Await PrepareView("listGSTR3BT4", strFilter))
                lst.Add(Await PrepareView("listGSTR3BT4A1", strFilter))
                lst.Add(Await PrepareView("listGSTR3BT4A2", strFilter))
                lst.Add(Await PrepareView("listGSTR3BT4A3", strFilter))
                lst.Add(Await PrepareView("listGSTR3BT4A5", strFilter))
                lst.Add(Await PrepareView("listGSTR3BT4B", strFilter))
                lst.Add(Await PrepareView("listGSTR3BT4D2", strFilter))
                lst.Add(Await PrepareView("listGSTR3BT5", strFilter))
                lst.Add(Await PrepareView("listGSTR3BT5A", strFilter))
                lst.Add(Await PrepareView("listGSTR3BT5B", strFilter))

        End Select
        Return lst

    End Function
    Public Function Offset(GstRegID As Integer, ReturnPeriodID As Integer, Model As Offsetliability) As clsProcOutput
        Dim oRet As New clsProcOutput
        Dim rGstReg As DataRow = Me.oMaster.GstRegRow(GstRegID)
        Dim rPP As DataRow = Me.oMaster.rPostPeriod(ReturnPeriodID)
        Dim rToken = Me.GetActiveToken(myContext.Police.UniqueUserID, GstRegID)
        Dim auth = Me.GetAuthClientFromToken(rToken, rGstReg)
        Dim client As New GSTR3BApiClient(auth, rGstReg("gstin"), rGstReg("gstnuserid"), rPP("ret_pd"))
        Dim info = client.Offset(Model)
        Dim nrTrans As DataRow = Me.SaveTransaction(GstRegID, ReturnPeriodID, "RETOFFSET", "", client.dicParams)
        oRet.AddDataRow("trans", nrTrans)
        If Not info.Data Is Nothing Then
            If String.IsNullOrEmpty(info.Data.message) Then
                oRet.AddError("Error - could Not upload")
            Else
                oRet.AddMessage(info.Data.message)
            End If
        End If


        Dim str1 As String = JsonConvert.SerializeObject(oRet, Formatting.None,
                                New JsonSerializerSettings With {.NullValueHandling = NullValueHandling.Ignore})
        Return oRet
    End Function
    Protected Friend Function GetLedgerClient(GstRegID As Integer, ReturnPeriodID As Integer) As LedgerApiClient
        Dim rPP As DataRow = Me.oMaster.rPostPeriod(ReturnPeriodID)
        Dim rGstReg As DataRow = Me.oMaster.GstRegRow(GstRegID)
        Dim rToken = Me.GetActiveToken(myContext.Police.UniqueUserID, GstRegID)
        Dim auth = Me.GetAuthClientFromToken(rToken, rGstReg)
        Dim client = New LedgerApiClient(auth, rGstReg("gstin"), rGstReg("gstnuserid"), rPP("ret_pd"))
        Return client
    End Function
    Public Function GetCashITCBalance(GstRegID As Integer, ReturnPeriodID As Integer) As clsProcOutput(Of GSTNResult(Of LedgerCashBalanceITC))
        Dim client = Me.GetLedgerClient(GstRegID, ReturnPeriodID)
        Dim oRet As New clsProcOutput(Of GSTNResult(Of LedgerCashBalanceITC))
        oRet.Result = client.GetLedgerCashBalanceITC
        Dim nrTrans As DataRow = Me.SaveTransaction(GstRegID, ReturnPeriodID, "BAL", "", client.dicParams)
        oRet.AddDataRow("trans", nrTrans)

        Return oRet
    End Function
    Public Function GetCashITCBalanceJson(GstRegID As Integer, ReturnPeriodID As Integer) As clsProcOutput
        Dim oRet = Me.GetCashITCBalance(GstRegID, ReturnPeriodID)
        Dim payload As String = JsonConvert.SerializeObject(oRet.Result, Newtonsoft.Json.Formatting.Indented,
                            New JsonSerializerSettings With {
                                .NullValueHandling = NullValueHandling.Ignore
                            })
        oRet.JsonData = New JsonDataResult With {.message = oRet.Message, .Data = payload, .success = oRet.Success}

        Return oRet
    End Function
    Public Function GetRetLibBalance(GstRegID As Integer, ReturnPeriodID As Integer) As clsProcOutput
        Dim client = Me.GetLedgerClient(GstRegID, ReturnPeriodID)
        Dim oRet As New clsProcOutput
        Dim model = client.GetRetLibBalance
        Dim nrTrans As DataRow = Me.SaveTransaction(GstRegID, ReturnPeriodID, "TAXPAYABLE", "", client.dicParams)
        oRet.AddDataRow("trans", nrTrans)
        Dim payload As String = JsonConvert.SerializeObject(oRet, Newtonsoft.Json.Formatting.Indented,
                            New JsonSerializerSettings With {
                                .NullValueHandling = NullValueHandling.Ignore
                            })
        oRet.JsonData = New JsonDataResult With {.message = oRet.Message, .Data = payload, .success = oRet.Success}
        Return oRet
    End Function
    Public Function GetLibDetails(GstRegID As Integer, ReturnPeriodID As Integer) As clsProcOutput
        Dim client = Me.GetLedgerClient(GstRegID, ReturnPeriodID)
        Dim oRet As New clsProcOutput
        Dim model = client.GetLibDetails
        Dim nrTrans As DataRow = Me.SaveTransaction(GstRegID, ReturnPeriodID, "TAX", "", client.dicParams)
        oRet.AddDataRow("trans", nrTrans)
        Dim payload As String = JsonConvert.SerializeObject(oRet, Newtonsoft.Json.Formatting.Indented,
                            New JsonSerializerSettings With {
                                .NullValueHandling = NullValueHandling.Ignore
                            })
        oRet.JsonData = New JsonDataResult With {.message = oRet.Message, .Data = payload, .success = oRet.Success}
        Return oRet
    End Function
    Public Function GetCashDetail(GstRegID As Integer, ReturnPeriodID As Integer) As clsProcOutput
        Dim rPP As DataRow = Me.oMaster.rPostPeriod(ReturnPeriodID)
        Dim client = Me.GetLedgerClient(GstRegID, ReturnPeriodID)
        Dim oRet As New clsProcOutput
        Dim model = client.GetCashDtl(DateAdd(DateInterval.Month, -1, rPP("sdate")).ToString("dd-MM-yyyy"), CDate(rPP("edate")).ToString("dd-MM-yyyy"))
        Dim nrTrans As DataRow = Me.SaveTransaction(GstRegID, ReturnPeriodID, "CASH", "", client.dicParams)
        oRet.AddDataRow("trans", nrTrans)
        Dim payload As String = JsonConvert.SerializeObject(oRet, Newtonsoft.Json.Formatting.Indented,
                            New JsonSerializerSettings With {
                                .NullValueHandling = NullValueHandling.Ignore
                            })
        'oRet.JsonData = New JsonDataResult With {.message = oRet.Message, .Data = payload, .success = oRet.Success}

        Return oRet
    End Function
    Public Function GetItcDetail(GstRegID As Integer, ReturnPeriodID As Integer) As clsProcOutput
        Dim rPP As DataRow = Me.oMaster.rPostPeriod(ReturnPeriodID)
        Dim client = Me.GetLedgerClient(GstRegID, ReturnPeriodID)
        Dim oRet As New clsProcOutput
        Dim model = client.GetItcDtl(DateAdd(DateInterval.Month, -1, rPP("sdate")).ToString("dd-MM-yyyy"), CDate(rPP("edate")).ToString("dd-MM-yyyy"))
        Dim nrTrans As DataRow = Me.SaveTransaction(GstRegID, ReturnPeriodID, "ITC", "", client.dicParams)
        oRet.AddDataRow("trans", nrTrans)
        Dim payload As String = JsonConvert.SerializeObject(oRet, Newtonsoft.Json.Formatting.Indented,
                            New JsonSerializerSettings With {
                                .NullValueHandling = NullValueHandling.Ignore
                            })
        'oRet.JsonData = New JsonDataResult With {.message = oRet.Message, .Data = payload, .success = oRet.Success}

        Return oRet
    End Function
    Public Function CheckGenerateGSTR3B(GstRegID As Integer, ReturnPeriodID As Integer) As clsProcOutput
        Dim dic As New clsCollecString(Of String), oRet As New clsProcOutput
        Try
            dic.Add("return", String.Format("select * from gstr3b where  gstregid={0} and ReturnPeriodID={1} ", GstRegID, ReturnPeriodID))
            dic.Add("section", "select * from gstrsectiontext where returntype in ('gstr3b') and section is not null and tablenum not in ('3.2')")
            Dim ds = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, dic)
            For Each r1 As DataRow In ds.Tables("section").Select
                Dim rr() As DataRow = ds.Tables("return").Select(myUtils.CombineWhere(False,
                                     "tablenum='" & myUtils.cStrTN(r1("tablenum")) & "'",
                                     "sectionnum='" & myUtils.cStrTN(r1("sectionnum")) & "'")), nr As DataRow
                If rr.Length > 0 Then
                    nr = rr(0)
                Else
                    nr = myContext.Tables.AddNewRow(ds.Tables("return"))
                End If
                nr("gstregid") = GstRegID
                nr("returnperiodid") = ReturnPeriodID
                nr("tablenum") = r1("tablenum")
                nr("sectionnum") = r1("sectionnum")
            Next
            myContext.Provider.objSQLHelper.SaveResults(ds.Tables("return"), dic("return"))

        Catch ex As Exception
            oRet.AddException(ex)
        End Try
        Return oRet
    End Function
    Public Function CheckPopulateGSTR3B(GstRegID As Integer, ReturnPeriodID As Integer) As clsProcOutput
        Dim dic As New clsCollecString(Of String), oRet As New clsProcOutput
        Try
            Dim strFilterSumm = String.Format("gstregid={0} and ReturnPeriodID={1}", GstRegID, ReturnPeriodID)
            dic.Add("return", "select * from gstr3b where " & strFilterSumm)
            dic.Add("calc", "select gstregid,returnperiodid, section,  tablenum, sectionnum,
                sum(txval) as txval,sum(iamt) as iamt,sum(camt) as camt,sum(samt) as samt,sum(csamt) as csamt, 
                sum(intrasuply) as intrasuply, sum(intersuply) as intersuply  from 
                gstListGSTR3bCalc() where " & strFilterSumm & " group by gstregid,returnperiodid, section,  tablenum, sectionnum")

            Dim ds = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, dic)
            myUtils.DeleteRows(ds.Tables("return").Select)

            For Each r1 As DataRow In ds.Tables("calc").Select
                Dim nr = myUtils.CopyOneRow(r1, ds.Tables("return"))
            Next
            myContext.Provider.objSQLHelper.SaveResults(ds.Tables("return"), dic("return"),, "0=1")  'Delete existing rows
            myContext.Provider.objSQLHelper.SaveResults(ds.Tables("return"), dic("return"))
            Me.CheckGenerateGSTR3B(GstRegID, ReturnPeriodID) 'Generate remaining rows.
        Catch ex As Exception
            oRet.AddException(ex)
        End Try
        Return oRet
    End Function
    Public Function PopulateTaxPaymentPAN(CompanyID As Integer, ReturnPeriodID As Integer) As clsProcOutput
        Dim sql = "select GSTRegID, GSTIN, TaxAreaID from GSTReg where companyid = " & CompanyID & " order by gstin"
        Dim dt1 As DataTable = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql).Tables(0)
        Dim oRet As New clsProcOutput
        For Each r1 As DataRow In dt1.Select
            oRet = oRet + Me.PopulateTaxPaymentGSTIN(r1("gstregid"), ReturnPeriodID)
        Next
        Return oRet
    End Function

    Public Function PopulateTaxPaymentGSTIN(GstRegID As Integer, ReturnPeriodID As Integer) As clsProcOutput
        Dim dic As New clsCollecString(Of String), oRet As New clsProcOutput, sortindex As Integer = 0
        Try
            Dim strFilterSumm = String.Format("gstregid={0} and ReturnPeriodID={1}", GstRegID, ReturnPeriodID)
            dic.Add("py", "select * from taxpayment where " & strFilterSumm)
            dic.Add("return", "select * from gstr3b where " & strFilterSumm)
            Dim ds = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, dic)

            Dim oProc As New clsGSTNReturnGSTR3B(myContext)
            Dim oRet1 = oProc.GetSummary(GstRegID, ReturnPeriodID)

            Dim nrTaxOth = Me.GetAddPaymentRow(ds.Tables("py"), GstRegID, ReturnPeriodID, "TAX", "30002", sortindex)
            Dim nrTaxRCM = Me.GetAddPaymentRow(ds.Tables("py"), GstRegID, ReturnPeriodID, "TAX", "30003", sortindex)
            If (oRet1.Result IsNot Nothing) AndAlso (oRet1.Result.tx_pmt.tx_py IsNot Nothing) Then
                Dim objTaxOth = oRet1.Result.tx_pmt.tx_py.Where(Function(x) myUtils.IsInList(x.trans_typ, "30002")).FirstOrDefault
                If objTaxOth IsNot Nothing Then
                    Me.PopulateAmounts(nrTaxOth, "iamt", objTaxOth.igst.tx, objTaxOth.igst.intr, 0, 0, 0)
                    Me.PopulateAmounts(nrTaxOth, "camt", objTaxOth.cgst.tx, objTaxOth.cgst.intr, objTaxOth.cgst.fee, 0, 0)
                    Me.PopulateAmounts(nrTaxOth, "samt", objTaxOth.sgst.tx, objTaxOth.sgst.intr, objTaxOth.sgst.fee, 0, 0)
                    Me.PopulateAmounts(nrTaxOth, "csamt", objTaxOth.cess.tx, objTaxOth.cess.intr, 0, 0, 0)
                End If

                Dim objTaxRCM = oRet1.Result.tx_pmt.tx_py.Where(Function(x) myUtils.IsInList(x.trans_typ, "30003")).FirstOrDefault
                If objTaxRCM IsNot Nothing Then
                    Me.PopulateAmounts(nrTaxRCM, "iamt", objTaxRCM.igst.tx, objTaxRCM.igst.intr, 0, 0, 0)
                    Me.PopulateAmounts(nrTaxRCM, "camt", objTaxRCM.cgst.tx, objTaxRCM.cgst.intr, objTaxRCM.cgst.fee, 0, 0)
                    Me.PopulateAmounts(nrTaxRCM, "samt", objTaxRCM.sgst.tx, objTaxRCM.sgst.intr, objTaxRCM.sgst.fee, 0, 0)
                    Me.PopulateAmounts(nrTaxRCM, "csamt", objTaxRCM.cess.tx, objTaxRCM.cess.intr, 0, 0, 0)
                End If
            End If

            Dim oRet2 = oProc.GetCashITCBalance(GstRegID, ReturnPeriodID)

            Dim nrBalCash = Me.GetAddPaymentRow(ds.Tables("py"), GstRegID, ReturnPeriodID, "BALCASH", "", sortindex)
            Dim nrBalITC = Me.GetAddPaymentRow(ds.Tables("py"), GstRegID, ReturnPeriodID, "BALITC", "", sortindex)
            If oRet2.Result.Data IsNot Nothing Then
                If oRet2.Result.Data.cash_bal IsNot Nothing Then
                    Dim csh = oRet2.Result.Data.cash_bal
                    Me.PopulateAmounts(nrBalCash, "iamt", csh.igst.tx, csh.igst.intr, csh.igst.fee, csh.igst.pen, csh.igst.oth)
                    Me.PopulateAmounts(nrBalCash, "camt", csh.cgst.tx, csh.cgst.intr, csh.cgst.fee, csh.cgst.pen, csh.cgst.oth)
                    Me.PopulateAmounts(nrBalCash, "samt", csh.sgst.tx, csh.sgst.intr, csh.sgst.fee, csh.sgst.pen, csh.sgst.oth)
                    Me.PopulateAmounts(nrBalCash, "csamt", csh.cess.tx, csh.cess.intr, csh.cess.fee, csh.cess.pen, csh.cess.oth)
                End If

                If oRet2.Result.Data.itc_bal IsNot Nothing Then
                    Dim itc = oRet2.Result.Data.itc_bal
                    Me.PopulateAmounts(nrBalITC, "iamt", itc.igst_bal, 0, 0, 0, 0)
                    Me.PopulateAmounts(nrBalITC, "camt", itc.cgst_bal, 0, 0, 0, 0)
                    Me.PopulateAmounts(nrBalITC, "samt", itc.sgst_bal, 0, 0, 0, 0)
                    Me.PopulateAmounts(nrBalITC, "csamt", itc.cess_bal, 0, 0, 0, 0)
                End If
            End If

            Dim nrITC = Me.GetAddPaymentRow(ds.Tables("py"), GstRegID, ReturnPeriodID, "ITC", "", sortindex)
            For Each str1 As String In New String() {"iamt", "camt", "samt", "csamt"}
                For Each r1 As DataRow In ds.Tables("return").Select("SectionNum='4A'")
                    nrITC($"{str1}_tax") = myUtils.cValTN(nrITC($"{str1}_tax")) + myUtils.cValTN(r1(str1))
                Next
                For Each r1 As DataRow In ds.Tables("return").Select("SectionNum='4B'")
                    nrITC($"{str1}_tax") = myUtils.cValTN(nrITC($"{str1}_tax")) - myUtils.cValTN(r1(str1))
                Next
            Next

            Dim nrPDCASH_Oth = Me.GetAddPaymentRow(ds.Tables("py"), GstRegID, ReturnPeriodID, "PDCASH", "30002", sortindex)
            Dim nrPDCASH_RCM = Me.GetAddPaymentRow(ds.Tables("py"), GstRegID, ReturnPeriodID, "PDCASH", "30003", sortindex)
            Dim nrPDITC = Me.GetAddPaymentRow(ds.Tables("py"), GstRegID, ReturnPeriodID, "PDITC", "", sortindex)
            If (oRet1.Result IsNot Nothing) AndAlso (oRet1.Result.tx_pmt IsNot Nothing) Then
                If (oRet1.Result.tx_pmt.pdcash IsNot Nothing) Then
                    Dim objPDCASH_Oth = oRet1.Result.tx_pmt.pdcash.Where(Function(x) myUtils.IsInList(x.trans_typ, "30002")).FirstOrDefault
                    If objPDCASH_Oth IsNot Nothing Then
                        Me.PopulateAmounts(nrPDCASH_Oth, "iamt", objPDCASH_Oth.ipd, objPDCASH_Oth.i_intrpd, 0, 0, 0)
                        Me.PopulateAmounts(nrPDCASH_Oth, "camt", objPDCASH_Oth.cpd, objPDCASH_Oth.c_intrpd, objPDCASH_Oth.c_lfeepd, 0, 0)
                        Me.PopulateAmounts(nrPDCASH_Oth, "samt", objPDCASH_Oth.spd, objPDCASH_Oth.s_intrpd, objPDCASH_Oth.s_lfeepd, 0, 0)
                        Me.PopulateAmounts(nrPDCASH_Oth, "csamt", objPDCASH_Oth.cspd, objPDCASH_Oth.cs_intrpd, 0, 0, 0)
                        nrPDCASH_Oth("liab_ldg_id") = objPDCASH_Oth.liab_ldg_id
                    End If

                    Dim objPDCASH_RCM = oRet1.Result.tx_pmt.pdcash.Where(Function(x) myUtils.IsInList(x.trans_typ, "30003")).FirstOrDefault
                    If objPDCASH_RCM IsNot Nothing Then
                        Me.PopulateAmounts(nrPDCASH_RCM, "iamt", objPDCASH_RCM.ipd, objPDCASH_RCM.i_intrpd, 0, 0, 0)
                        Me.PopulateAmounts(nrPDCASH_RCM, "camt", objPDCASH_RCM.cpd, objPDCASH_RCM.c_intrpd, objPDCASH_RCM.c_lfeepd, 0, 0)
                        Me.PopulateAmounts(nrPDCASH_RCM, "samt", objPDCASH_RCM.spd, objPDCASH_RCM.s_intrpd, objPDCASH_RCM.s_lfeepd, 0, 0)
                        Me.PopulateAmounts(nrPDCASH_RCM, "csamt", objPDCASH_RCM.cspd, objPDCASH_RCM.cs_intrpd, 0, 0, 0)
                        nrPDCASH_RCM("liab_ldg_id") = objPDCASH_RCM.liab_ldg_id
                    End If
                End If
                Dim objPDITC = oRet1.Result.tx_pmt.pditc
                If objPDITC IsNot Nothing Then
                    Me.PopulateAmounts(nrPDITC, "iamt", objPDITC.i_pdi, objPDITC.i_pdc, objPDITC.i_pds)
                    Me.PopulateAmounts(nrPDITC, "camt", objPDITC.c_pdi, objPDITC.c_pdc, 0)
                    Me.PopulateAmounts(nrPDITC, "samt", objPDITC.s_pdi, 0, objPDITC.s_pds)
                    nrPDITC("csamt_cs") = objPDITC.cs_pdcs
                End If
            End If


            Me.AutoOffset(nrTaxOth, nrTaxRCM, nrBalCash, nrBalITC, nrITC, nrPDCASH_Oth, nrPDCASH_RCM, nrPDITC, ds.Tables("return").Select("SectionNum='4A'"))

            Dim nrNetCash = Me.GetAddPaymentRow(ds.Tables("py"), GstRegID, ReturnPeriodID, "NETCASH", "", sortindex)
            For Each column As DataColumn In nrNetCash.Table.Columns
                If column.DataType.FullName = "System.Decimal" Then
                    nrNetCash(column) = myUtils.cValTN(nrPDCASH_Oth(column)) + myUtils.cValTN(nrPDCASH_RCM(column)) - myUtils.cValTN(nrBalCash(column))

                    If myUtils.cValTN(nrNetCash(column)) < 0 Then
                        nrNetCash(column) = 0
                    End If
                End If
            Next

            myContext.Provider.objSQLHelper.SaveResults(ds.Tables("py"), dic("py"))
        Catch ex As Exception
            oRet.AddException(ex)
        End Try
        Return oRet
    End Function

    Public Function SavePditcTaxPaymentGSTIN(GstRegID As Integer, ReturnPeriodID As Integer, Pditc As Pditc) As clsProcOutput
        Dim oRet As New clsProcOutput

        Try
            Dim query = "select * from taxpayment where " & String.Format("GSTRegId={0} and ReturnPeriodID={1} and EntryType='PDITC'", GstRegID, ReturnPeriodID)
            Dim dt = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, query).Tables(0)

            If dt.Rows.Count > 0 Then
                Dim dr = dt.Rows(0)

                dr("Iamt_I") = Pditc.i_pdi
                dr("Iamt_C") = Pditc.i_pdc
                dr("Iamt_S") = Pditc.i_pds
                dr("Camt_I") = Pditc.c_pdi
                dr("Camt_C") = Pditc.c_pdc
                dr("Samt_I") = Pditc.s_pdi
                dr("Samt_S") = Pditc.s_pds
                dr("CsAmt_Cs") = Pditc.cs_pdcs
                dr("IsManual") = 1

                myContext.Provider.objSQLHelper.SaveResults(dt, query)

                oRet = oRet + Me.PopulateTaxPaymentGSTIN(GstRegID, ReturnPeriodID)
            End If
        Catch ex As Exception
            oRet.AddException(ex)
        End Try

        Return oRet
    End Function

    Public Function GetAddPaymentRow(dt1 As DataTable, GstRegID As Integer, ReturnPeriodID As Integer, EntryType As String, TransType As String, ByRef sortindex As Integer) As DataRow
        Dim strFilterSumm = String.Format("gstregid={0} and ReturnPeriodID={1}", GstRegID, ReturnPeriodID)
        Dim strFilter2 = $"isnull(Entrytype,'')='{EntryType}' and isnull(transtype,'')='{TransType}'"
        Dim rr() As DataRow = dt1.Select(myUtils.CombineWhere(False, strFilterSumm, strFilter2)), nr As DataRow
        If rr.Length > 0 Then
            nr = rr(0)
        Else
            nr = myContext.Tables.AddNewRow(dt1)
            nr("gstregid") = GstRegID
            nr("returnperiodid") = ReturnPeriodID
            nr("entrytype") = EntryType
            nr("transtype") = TransType
        End If
        sortindex = sortindex + 1
        nr("sortindex") = sortindex
        Me.UpdateDescription(nr, EntryType, TransType)

        Return nr
    End Function
    Public Function UpdateDescription(nr As DataRow, EntryType As String, TransType As String) As String
        If EntryType = "TAX" AndAlso TransType = "30002" Then
            nr("description") = "TAX Other than reverse charge"
        ElseIf EntryType = "TAX" AndAlso TransType = "30003" Then
            nr("description") = "TAX Reverse charge"
        ElseIf EntryType = "PDCASH" AndAlso TransType = "30002" Then
            nr("description") = "Net amount payable in Cash"
        ElseIf EntryType = "PDCASH" AndAlso TransType = "30003" Then
            nr("description") = "CASH Reverse charge"
        ElseIf EntryType = "BALCASH" Then
            nr("description") = "Cash balance available on GSTN"
        ElseIf EntryType = "BALITC" Then
            nr("description") = "Opening Balance of ITC"
        ElseIf EntryType = "PDITC" Then
            nr("description") = "ITC Utlized (As per rules)"
        ElseIf EntryType = "NETCASH" Then
            nr("description") = "Balance amount of cash payable"
        ElseIf EntryType = "ITC" Then
            nr("description") = "ITC Available for the month"
        End If

        Return myUtils.cStrTN(nr("description"))
    End Function
    Protected Friend Sub PopulateAmounts(nr As DataRow, taxname As String, tax As Decimal, interest As Decimal, fee As Decimal, penalty As Decimal, other As Decimal)
        If nr.Table.Columns.Contains($"{taxname}_tax") Then nr($"{taxname}_tax") = tax
        If nr.Table.Columns.Contains($"{taxname}_interest") Then nr($"{taxname}_interest") = interest
        If nr.Table.Columns.Contains($"{taxname}_fee") Then nr($"{taxname}_fee") = fee
        If nr.Table.Columns.Contains($"{taxname}_penalty") Then nr($"{taxname}_penalty") = penalty
        If nr.Table.Columns.Contains($"{taxname}_other") Then nr($"{taxname}_other") = other

    End Sub
    Protected Friend Sub PopulateAmounts(nr As DataRow, taxname As String, iamt As Decimal, camt As Decimal, samt As Decimal)
        If nr.Table.Columns.Contains($"{taxname}_i") Then nr($"{taxname}_i") = iamt
        If nr.Table.Columns.Contains($"{taxname}_c") Then nr($"{taxname}_c") = camt
        If nr.Table.Columns.Contains($"{taxname}_s") Then nr($"{taxname}_s") = samt

    End Sub
    Protected Friend Sub AutoOffset(nrTaxOth As DataRow, nrTaxRCM As DataRow, nrBalCash As DataRow, nrBalITC As DataRow, nrITC As DataRow, nrPDCashOth As DataRow, nrPDCashRCM As DataRow, nrPDITC As DataRow, section4Arows As DataRow())
        Dim i_pdi As Decimal = 0
        Dim i_pdc As Decimal = 0
        Dim i_pds As Decimal = 0
        Dim c_pdi As Decimal = 0
        Dim c_pdc As Decimal = 0
        Dim s_pdi As Decimal = 0
        Dim s_pds As Decimal = 0
        Dim cs_pdcs As Decimal = 0

        If nrTaxOth IsNot Nothing Then
            Dim tx_py__trans_typ__30002__igst As Decimal = 0
            Dim tx_py__trans_typ__30002__cgst As Decimal = 0
            Dim tx_py__trans_typ__30002__sgst As Decimal = 0
            Dim tx_py__trans_typ__30002__cess As Decimal = 0

            tx_py__trans_typ__30002__igst = myUtils.cValTN(nrTaxOth("Iamt_Tax")) ' + myUtils.cValTN(nrTaxOth("Iamt_Interest")) '+ myutils.cValtn(nrTaxOth("Iamt_Fee"))
            tx_py__trans_typ__30002__cgst = myUtils.cValTN(nrTaxOth("Camt_Tax")) ' + myUtils.cValTN(nrTaxOth("Camt_Interest")) + myUtils.cValTN(nrTaxOth("Camt_Fee"))
            tx_py__trans_typ__30002__sgst = myUtils.cValTN(nrTaxOth("Samt_Tax")) ' + myUtils.cValTN(nrTaxOth("Samt_Interest")) + myUtils.cValTN(nrTaxOth("Samt_fee"))
            tx_py__trans_typ__30002__cess = myUtils.cValTN(nrTaxOth("CSAMT_Tax")) ' + myUtils.cValTN(nrTaxOth("CSAMT_Interest")) '+ myutils.cValtn(nrTaxOth("CSAMT_Fee"))

            Dim igst_bal As Decimal = 0
            Dim cgst_bal As Decimal = 0
            Dim sgst_bal As Decimal = 0
            Dim cess_bal As Decimal = 0

            If nrBalITC IsNot Nothing Then
                igst_bal = myUtils.cValTN(nrTaxOth("Iamt_Tax"))
                cgst_bal = myUtils.cValTN(nrTaxOth("Camt_Tax"))
                sgst_bal = myUtils.cValTN(nrTaxOth("Samt_Tax"))
                cess_bal = myUtils.cValTN(nrTaxOth("CSAMT_Tax"))
            End If

            If section4Arows IsNot Nothing AndAlso section4Arows.Length > 0 Then
                igst_bal += myUtils.cValTN(section4Arows(0)("IAMT"))
                cgst_bal += myUtils.cValTN(section4Arows(0)("CAMT"))
                sgst_bal += myUtils.cValTN(section4Arows(0)("SAMT"))
                cess_bal += myUtils.cValTN(section4Arows(0)("CSAMT"))
            End If

            '(a) IGST ITC shall first be set-off against IGST liability, then CGST liability And if any balance remaining against SGST / UGST liability.
            If igst_bal > 0 Then
                If tx_py__trans_typ__30002__igst > igst_bal Then
                    i_pdi += igst_bal
                    tx_py__trans_typ__30002__igst -= igst_bal
                    igst_bal = 0
                Else
                    i_pdi += tx_py__trans_typ__30002__igst
                    tx_py__trans_typ__30002__igst = 0
                    igst_bal -= tx_py__trans_typ__30002__igst
                End If
            End If
            If igst_bal > 0 Then
                If tx_py__trans_typ__30002__cgst > igst_bal Then
                    i_pdi += igst_bal
                    tx_py__trans_typ__30002__cgst -= igst_bal
                    igst_bal = 0
                Else
                    i_pdi += tx_py__trans_typ__30002__cgst
                    tx_py__trans_typ__30002__cgst = 0
                    igst_bal -= tx_py__trans_typ__30002__cgst
                End If
            End If
            If igst_bal > 0 Then
                If tx_py__trans_typ__30002__sgst > igst_bal Then
                    i_pdi += igst_bal
                    tx_py__trans_typ__30002__sgst -= igst_bal
                    igst_bal = 0
                Else
                    i_pdi += tx_py__trans_typ__30002__sgst
                    tx_py__trans_typ__30002__sgst = 0
                    igst_bal -= tx_py__trans_typ__30002__sgst
                End If
            End If

            '(b) CGST ITC shall first be set-off against CGST liability And then IGST liability (if any)
            If cgst_bal > 0 Then
                If tx_py__trans_typ__30002__cgst > cgst_bal Then
                    i_pdi += cgst_bal
                    tx_py__trans_typ__30002__cgst -= cgst_bal
                    cgst_bal = 0
                Else
                    i_pdi += tx_py__trans_typ__30002__cgst
                    tx_py__trans_typ__30002__cgst = 0
                    cgst_bal -= tx_py__trans_typ__30002__cgst
                End If
            End If
            If cgst_bal > 0 Then
                If tx_py__trans_typ__30002__igst > cgst_bal Then
                    i_pdi += cgst_bal
                    tx_py__trans_typ__30002__igst -= cgst_bal
                    cgst_bal = 0
                Else
                    i_pdi += tx_py__trans_typ__30002__igst
                    tx_py__trans_typ__30002__igst = 0
                    cgst_bal -= tx_py__trans_typ__30002__igst
                End If
            End If

            '(c) SGST ITC shall first be set-off against SGST liability And then IGST liability (if any). However, SGST can be set-off against IGST only when Balance in CGST Is NIL i.e. for payment of IGST exhaust CGST first then SGST
            '(d) UGST ITC shall first be set-off against UGST liability And then IGST liability (if any). However, UGST can be set-off against IGST only when Balance in CGST Is NIL i.e. for payment of IGST exhaust CGST first then UGST
            If sgst_bal > 0 Then
                If tx_py__trans_typ__30002__sgst > sgst_bal Then
                    i_pdi += sgst_bal
                    tx_py__trans_typ__30002__sgst -= sgst_bal
                    sgst_bal = 0
                Else
                    i_pdi += tx_py__trans_typ__30002__sgst
                    tx_py__trans_typ__30002__sgst = 0
                    sgst_bal -= tx_py__trans_typ__30002__sgst
                End If
            End If
            If sgst_bal > 0 Then
                If tx_py__trans_typ__30002__igst > sgst_bal Then
                    i_pdi += sgst_bal
                    tx_py__trans_typ__30002__igst -= sgst_bal
                    sgst_bal = 0
                Else
                    i_pdi += tx_py__trans_typ__30002__igst
                    tx_py__trans_typ__30002__igst = 0
                    sgst_bal -= tx_py__trans_typ__30002__igst
                End If
            End If

            '(e) Set-off of CGST against SGST / UGST Not ALLOWED & vice a versa.
        End If

        If nrPDITC IsNot Nothing Then
            If IsDBNull(nrPDITC("IsManual")) OrElse nrPDITC("IsManual") = False Then
                nrPDITC("Iamt_I") = i_pdi
                nrPDITC("Iamt_C") = i_pdc
                nrPDITC("Iamt_S") = i_pds
                nrPDITC("Camt_I") = c_pdi
                nrPDITC("Camt_C") = c_pdc
                nrPDITC("Samt_I") = s_pdi
                nrPDITC("Samt_S") = s_pds
                nrPDITC("CsAmt_Cs") = cs_pdcs
            End If

            nrPDITC("iamt_tax") = i_pdi + i_pdc + i_pds
            nrPDITC("camt_tax") = c_pdi + c_pdc
            nrPDITC("samt_tax") = s_pdi + s_pds

            If nrPDCashOth IsNot Nothing AndAlso nrTaxOth IsNot Nothing Then
                nrPDCashOth("Iamt_Tax") = myUtils.cValTN(nrTaxOth("Iamt_Tax")) - myUtils.cValTN(nrPDITC("Iamt_tax"))
                nrPDCashOth("Iamt_Interest") = myUtils.cValTN(nrTaxOth("Iamt_Interest"))
                'nrPDCashOth("Iamt_Fee") = myUtils.cValTN(nrTaxOth("Iamt_Fee"))

                nrPDCashOth("Camt_Tax") = myUtils.cValTN(nrTaxOth("Camt_Tax")) - myUtils.cValTN(nrPDITC("Camt_tax"))
                nrPDCashOth("Camt_Interest") = myUtils.cValTN(nrTaxOth("Camt_Interest"))
                nrPDCashOth("Camt_Fee") = myUtils.cValTN(nrTaxOth("Camt_Fee"))

                nrPDCashOth("Samt_Tax") = myUtils.cValTN(nrTaxOth("Samt_Tax")) - myUtils.cValTN(nrPDITC("Samt_tax"))
                nrPDCashOth("Samt_Interest") = myUtils.cValTN(nrTaxOth("Samt_Interest"))
                nrPDCashOth("Samt_Fee") = myUtils.cValTN(nrTaxOth("Samt_Fee"))

                nrPDCashOth("CSAMT_Tax") = myUtils.cValTN(nrTaxOth("CSAMT_Tax")) - myUtils.cValTN(nrPDITC("CsAmt_tax"))
                nrPDCashOth("CSAMT_Interest") = myUtils.cValTN(nrTaxOth("CSAMT_Interest"))
                'nrPDCashOth("CSAMT_Fee") = myUtils.cValTN(nrTaxOth("CSAMT_Fee"))
            End If
        End If

        If nrPDCashRCM IsNot Nothing AndAlso nrTaxRCM IsNot Nothing Then
            For Each column As DataColumn In nrPDCashRCM.Table.Columns
                If column.DataType.FullName = "System.Decimal" Then
                    nrPDCashRCM(column) = nrTaxRCM(column)
                End If
            Next
        End If
    End Sub
    Protected Overrides Function DownloadGSTNTp(GstRegID As Integer, ReturnPeriodID As Integer, client As GSTR3BApiClient, ds As DataSet, Action As String) As Task(Of List(Of GSTNDownload(Of GSTR3BTotal)))
        Dim lstOut As New List(Of GSTNDownload(Of GSTR3BTotal))

        Try
            Dim model1 = client.GetSummary().Data
            Dim nrTrans As DataRow = Me.SaveTransaction(GstRegID, ReturnPeriodID, "RETSUM", "", client.dicParams)
            Dim oRet As New GSTNDownload(Of GSTR3BTotal)
            If Not model1 Is Nothing Then
                'instant response
                Dim result As New GSTNDataFile(Of GSTR3BTotal)
                result.FileName = $"GSTR3B_{nrTrans("gstntransactionid")}.json"
                result.Data = model1
                oRet.Files.Add(result)
            End If
            oRet.GSTRegID = GstRegID
            oRet.ReturnPeriodID = ReturnPeriodID
            oRet.rTrans = nrTrans
            lstOut.Add(oRet)
        Catch ex As Exception
            Trace.WriteLine(ex.Message)
        End Try
        Return Task.FromResult(lstOut)

    End Function
    Public Overrides Function UpdateDownloadedDataTP(GstRegID As Integer, ReturnPeriodID As Integer, results As List(Of GSTR3BTotal)) As Task(Of clsProcOutput(Of GstImportInfoGSTIN))
        Dim rGstReg As DataRow = oMaster.GstRegRow(GstRegID)
        Dim rPP As DataRow = oMaster.rPostPeriod(ReturnPeriodID)
        Dim result As Boolean = myFuncs2.IsFinalPP(myContext, GstRegID, rPP, "GSTR3B")
        Dim oRet As New clsProcOutput(Of GstImportInfoGSTIN)
        If result Then
            oRet.AddError("Period finalized")
        Else
            Try
                Dim sql As String = $"select * from gstr3b where gstregid={GstRegID} and returnperiodid={ReturnPeriodID}"
                Dim dt1 As DataTable = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql).Tables(0)
                myUtils.DeleteRows(dt1.Select)

                Dim dic2 As New clsCollecString(Of String)
                dic2.Add("section", "select * from gstrsectiontext where returntype='gstr3b'")
                dic2.Add("ta", "select * from taxarea")
                Dim dsMaster = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, dic2)

                Dim ds = Me.PopulateDataTP(results)

                For Each dt2 As DataTable In ds.Tables
                    If dt2.Columns.Contains("inter") Then dt2.Columns("inter").ColumnName = "intersuply"
                    If dt2.Columns.Contains("intra") Then dt2.Columns("intra").ColumnName = "intrasuply"
                    For Each r2 As DataRow In dt2.Select
                        Dim strf1 As String = myContext.SQL.GenerateSQLWhere(r2, "ty")
                        Dim strf2 As String = $"JsonProperty='{dt2.TableName}'"
                        If dt2.Columns.Contains("section2") AndAlso myUtils.cStrTN(r2("section2")).Trim.Length > 0 Then
                            strf2 = myUtils.CombineWhereOR(False, strf2, $"JsonProperty='{r2("section2")}'")
                        End If
                        Dim rr() As DataRow = dsMaster.Tables("section").Select(myUtils.CombineWhere(False, strf1, strf2))
                        If rr.Length > 0 Then
                            Dim nr As DataRow = myUtils.CopyOneRow(r2, dt1)
                            nr("tablenum") = rr(0)("tablenum")
                            nr("sectionnum") = rr(0)("sectionnum")
                            nr("gstregid") = GstRegID
                            nr("returnperiodid") = ReturnPeriodID
                            If dt2.Columns.Contains("pos") Then
                                Dim rTA = myFuncs2.FindTaxAreaRow(dsMaster.Tables("ta"), myUtils.cStrTN(r2("pos")))
                                If rTA IsNot Nothing Then nr("postaxareaid") = rTA("taxareaid")
                            End If
                        End If
                    Next
                Next

                myContext.Provider.objSQLHelper.SaveResults(dt1, sql, True)

            Catch ex As Exception
                Trace.WriteLine(ex.Message)
                oRet.AddException(ex)
            End Try
        End If
        Return Task.FromResult(oRet)
    End Function

End Class
