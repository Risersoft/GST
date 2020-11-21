Imports Newtonsoft.Json
Imports risersoft.shared
Imports risersoft.shared.cloud
Imports risersoft.shared.cloud.common
Imports risersoft.API.GSTN.GSTR3B
Imports System.Configuration
Imports System.Runtime.Serialization
Imports Microsoft.Owin.Infrastructure

<DataContract>
Public Class frmGSTNGSTR3BModel
    Inherits clsFormDataModel
    Dim myViewGSTReg, myViewHistory, myViewPayment, myViewPaymentDetail, myViewITCRev As clsViewModel
    Dim strFormatXML As String
    Protected Overrides Sub InitViews()
        myViewGSTReg = Me.GetViewModel("GSTReg")
        myViewHistory = Me.GetViewModel("History")
        myView = Me.GetViewModel("Invoice")
        myViewPayment = Me.GetViewModel("Payment")
        myViewITCRev = Me.GetViewModel("ITCRev")
        myViewPaymentDetail = Me.GetViewModel("PaymentDetail")
    End Sub

    Public Sub New(context As IProviderContext)
        MyBase.New(context)
        Me.InitViews()
        Me.InitForm()
    End Sub

    Private Sub InitForm()
        Dim Sql As String = "Select GstRegID, GSTIN from GstReg  Order by GSTIN"
        Me.AddLookupField("GstRegID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql), "GstReg").TableName)

        Sql = "Select PostPeriodID, ret_pd, sdate from PostPeriod  Order by sdate desc"
        Me.AddLookupField("ReturnPeriodID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql), "PostPeriod").TableName)
        Dim vlistn As New clsValueList
        vlistn.Add("Add", "Add")
        vlistn.Add("Less", "Less")
        Me.ValueLists.Add("Action", vlistn)
        Me.AddLookupField("ITCRevs", "Action", "Action")
    End Sub

    Public Overrides Function PrepForm(oView As clsViewModel, ByVal prepMode As EnumfrmMode, ByVal prepIDX As String, Optional ByVal strXML As String = "") As Boolean
        Dim Sql As String
        Me.FormPrepared = False
        If Not prepIDX Is Nothing Then
            prepMode = EnumfrmMode.acAddM
            Sql = "Select CompanyID,CompName,PANNum,'' as URL  from Company Where CompanyID = " & prepIDX
            Me.InitData(Sql, "returnperiodid", oView, prepMode, prepIDX, strXML)

            Dim ReturnPeriodID As Integer = myFuncs2.ValidatedPostPeriodID(myContext, "ReturnPeriodID", myUtils.cValTN(Me.vBag("returnperiodid")), Me.dsCombo.Tables("postperiod"))
            Me.BindDataTable(myUtils.cValTN(prepIDX), ReturnPeriodID)

            myViewGSTReg.MainGrid.MainConf("formatxml") = "<COL KEY=""Descrip"" CAPTION=""State""/><COL KEY=""Txval"" CAPTION=""Taxable Value""/><COL KEY=""Iamt"" CAPTION=""IGST""/><COL KEY=""camt"" CAPTION=""CGST""/><COL KEY=""SAMT"" CAPTION=""SGST""/><COL KEY=""csamt"" CAPTION=""CESS""/>"
            myViewGSTReg.MainGrid.BindGridData(Me.dsForm, 1)
            myViewGSTReg.MainGrid.QuickConf("", True, "1.5-1-1-1-1-1-1")

            myView.MainGrid.MainConf("formatxml") = "<COL KEY=""section"" CAPTION=""Section""/><COL KEY=""ret_pd"" CAPTION=""Date""/><COL KEY=""Txval"" CAPTION=""Taxable Value""/><COL KEY=""Iamt"" CAPTION=""IGST""/><COL KEY=""camt"" CAPTION=""CGST""/><COL KEY=""SAMT"" CAPTION=""SGST""/><COL KEY=""csamt"" CAPTION=""CESS""/>"
            myView.MainGrid.BindGridData(Me.dsForm, 2)
            myView.MainGrid.QuickConf("", True, "1-1-1-1-1-1-1-1-1")

            myViewPaymentDetail.MainGrid.MainConf("formatxml") = "<COL KEY=""CTCode"" CAPTION=""Type""/><COL KEY=""Iamt_Tax"" CAPTION=""IGST"" GROUP=""Tax""/><COL KEY=""Camt_Tax"" CAPTION=""CGST"" GROUP=""Tax""/><COL KEY=""Samt_Tax"" CAPTION=""SGST"" GROUP=""Tax""/><COL KEY=""CSAMT_Tax"" CAPTION=""CESS"" GROUP=""Tax""/><COL KEY=""Iamt_Interest"" CAPTION=""IGST"" GROUP=""Interest""/><COL KEY=""Camt_Interest"" CAPTION=""CGST"" GROUP=""Interest""/><COL KEY=""Samt_Interest"" CAPTION=""SGST"" GROUP=""Interest""/><COL KEY=""CSAMT_Interest"" CAPTION=""CESS"" GROUP=""Interest""/><COL KEY=""Camt_Fee"" CAPTION=""CGST"" GROUP=""Fee""/><COL KEY=""Samt_fee"" CAPTION=""SGST"" GROUP=""Fee""/>"
            myViewPaymentDetail.MainGrid.BindGridData(Me.dsForm, 3)
            myViewPaymentDetail.MainGrid.QuickConf("", True, "2-0-1-1-1-1-1-1-1-1-1-1-0")


            myViewITCRev.MainGrid.BindGridData(Me.dsForm, 5)
            myViewITCRev.MainGrid.QuickConf("", True, "1-1-1-1-1-1-1")
            Dim str2 = "<BAND INDEX = ""5"" TABLE = ""ITCRevs"" IDFIELD=""ITCRevsID""><COL KEY =""GstRegID,ReturnPeriodID, RuleNum, RuleSection, Iamt, Camt, Samt, CsAmt""/></BAND>"
            myViewITCRev.MainGrid.MainConf("formatxml") = "<COL KEY=""RuleNum"" CAPTION=""Rule Num""/><COL KEY=""RuleSection"" CAPTION=""Rule Section""/><COL KEY=""Iamt"" CAPTION=""IGST""/><COL KEY=""camt"" CAPTION=""CGST""/><COL KEY=""SAMT"" CAPTION=""SGST""/><COL KEY=""csamt"" CAPTION=""CESS""/>"
            myViewITCRev.MainGrid.PrepEdit(str2, EnumEditType.acCommandAndEdit)

            myViewHistory.MainGrid.BindGridData(Me.dsForm, 6)
            myViewHistory.MainGrid.QuickConf("", True, "1-1-1-0-1-0.5")


            Dim str1 As String = myContext.Data.GenColList(Me.dsForm.Tables("paymentsumm").Columns.Cast(Of DataColumn).ToList, "GSTIN, TAX_30002, TAX_30003, BALCASH, BALITC, ITC, PDCASH_30002, PDCASH_30003, PDITC, NETCASH")
            myViewPayment.MainGrid.MainConf.hidecols = str1
            myViewPayment.MainGrid.MainConf.FormatXML = "<COL KEY=""CTCode"" CAPTION=""Type""/>" & strFormatXML
            myViewPayment.MainGrid.BindGridData(Me.dsForm, 7)
            myViewPayment.MainGrid.QuickConf("", True, "1-1-1-1-1-1-1-1-1")


            Me.FormPrepared = True
        End If
        Return Me.FormPrepared
    End Function

    Protected Friend Overloads Function GenerateData(ByVal CompanyID As Integer, ReturnPeriodID As Integer) As DataSet
        Dim dic As New clsCollecString(Of String)
        dic.Add("gstreg", "Select GSTRegID, GSTIN, Descrip, convert(money,0) As iamt, convert(money,0) As camt, convert(money,0) As samt, convert(money,0) As csamt, convert(money,0) As txval from GstListGSTReg() where  CompanyID = " & CompanyID)
        dic.Add("detail", String.Format("select gstregid, section,ret_pd, sum(txval) as txval,sum(iamt) as iamt,
                            sum(camt) as camt, sum(samt) as samt, sum(csamt) as csamt, sum(Intersuply) as Intersuply, sum(Intrasuply) as Intrasuply from 
                            gstlistgstr3b() where  CompanyID={0} and ReturnPeriodID={1} 
                            and (isnull(txval,0)+ isnull(iamt,0)+ isnull(camt,0)+ isnull(samt,0)+isnull(Intersuply,0)+ isnull(Intrasuply,0)>0)
                            group by section,ret_pd,gstregid", CompanyID, ReturnPeriodID))
        dic.Add("paymentdetail", String.Format("select TaxPaymentID, GSTRegId, CompanyID, Iamt_I, Iamt_C, Iamt_S, Camt_I, Camt_C, Samt_I, Samt_S, CsAmt_Cs, IsManual, Total, EntryType, TransType, CTCode, isnull(Description,'') as Description, Iamt_Tax, Camt_Tax, Samt_Tax, CSAMT_Tax, Iamt_Interest, Camt_Interest, Samt_Interest, CSAMT_Interest, Camt_Fee, Samt_fee, GSTIN from 
                            gstlisttaxpayment() where  CompanyID={0} and ReturnPeriodID={1} order by sortindex", CompanyID, ReturnPeriodID))
        dic.Add("return", String.Format("select * from postperiod where postperiodid ={0}", ReturnPeriodID))
        dic.Add("itcrev", String.Format("select itcrevsid, GstRegID,ReturnPeriodID,[Action], RuleNum, RuleSection, Iamt, Camt, Samt, CsAmt from itcrevs where gstregid in (select gstregid from gstreg where companyid={0}) and ReturnPeriodID={1}", CompanyID, ReturnPeriodID))
        dic.Add("TaskHistoryData", "SELECT ApiTaskID, TenantID, FileName, ImportFileID, ActionType,ActionSubType, SubmittedOn, AppBarJson, CompletedOn, Status FROM ApiTask WHERE ParentID = " & CompanyID & " and ActionType='GSTR3B' AND Success = 1")
        dic.Add("paymentsumm", "Select GSTRegID, GSTIN, Descrip from GstListGSTReg() where  CompanyID = " & CompanyID)
        Dim ds = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, dic)


        Dim oProc As New clsGSTNReturnGSTR3B(myContext)
        For Each r1 As DataRow In ds.Tables("Paymentdetail").Select
            oProc.UpdateDescription(r1, myUtils.cStrTN(r1("entrytype")), myUtils.cStrTN(r1("transtype")))
        Next

        Dim oTrans As New clsModelTrans(myContext)
        'Dim dt1 As DataTable = oTrans.SimpleCross(ds.Tables("paymentdetail"), "<SCROSS PIVOT=""ctcode"" IDFIELD=""GstRegID"" AGGCOL=""total"" CAPTION=""DESCRIPTION""/>")
        Dim dt1 As DataTable = oTrans.SimpleCross(ds.Tables("paymentdetail"), "<SCROSS PIVOT=""ctcode"" IDFIELD=""GstRegID"" AGGCOL=""total"" CAPTION=""DESCRIPTION""/>",, strFormatXML)
        myUtils.AddTable(ds, dt1, "paymentSumm")

        For Each r1 As DataRow In ds.Tables("gstreg").Select
            For Each str1 As String In New String() {"iamt", "camt", "samt", "csamt", "txval"}
                r1(str1) = Math.Round(myUtils.cValTN(ds.Tables("detail").Compute("sum(" & str1 & ")", "gstregid=" & r1("gstregid"))), 2)
            Next
        Next

        If myRow IsNot Nothing Then
            Dim conf = New clsEncryptString(False)
            conf.Add("tag", $"<PARAMS ReturnPeriodID=""{ReturnPeriodID}""/>")

            Dim dic2 As New clsCollecString(Of String)
            dic2.Add("params", conf.ToString)
            myContext.GetAppInfo.PopulateAppBarDict(dic2)

            myRow("URL") = WebUtilities.AddQueryString("/frmGSTR3B/Edit/" & myRow("companyid").ToString, dic2)
        End If

        Return ds
    End Function
    Private Sub BindDataTable(ByVal CompanyID As Integer, ReturnPeriodID As Integer)
        Dim ds = Me.GenerateData(CompanyID, ReturnPeriodID)
        For i As Integer = 0 To ds.Tables.Count - 1
            myUtils.AddTable(Me.dsForm, ds, ds.Tables(i).TableName, i)
        Next
    End Sub

    Public Overrides Function Validate() As Boolean
        Me.InitError()
        Return Me.CanSave
    End Function

    Public Overrides Function GenerateParamsOutput(dataKey As String, Params As List(Of clsSQLParam)) As clsProcOutput
        Dim GstRegID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@gstregid", Params))
        Dim ReturnPeriodID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@ReturnPeriodID", Params))
        Dim CompanyID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@companyid", Params))
        Dim ReturnTaskID As String = myContext.SQL.ParamValue("@ApiTaskID", Params)
        Dim InfoJsonAPITaskID As String = myContext.SQL.ParamValue("@UploadType", Params)

        Dim queueName = myContext.Controller.CalcQueueName

        Dim oRet As New clsProcOutput
        If myUtils.IsInList(dataKey, "dbsumm", "payment") Then
            If CompanyID = 0 Then
                oRet.AddError("Company not selected")
            Else
                Select Case dataKey.Trim.ToLower
                    Case "dbsumm"
                        oRet.Data = Me.GenerateData(CompanyID, ReturnPeriodID)
                        Dim provider As New clsDBUserFilterProvider(myContext, False)
                        provider.AddUpdValueRow("ReturnPeriodID", ReturnPeriodID)
                        provider.SaveDBUserFilter()
                        oRet.Message = "Return Period change successful"
                    Case "payment"
                        Dim oProc As New clsGSTNReturnGSTR3B(myContext)
                        oRet = oProc.PopulateTaxPaymentPAN(CompanyID, ReturnPeriodID)
                        oRet.Data = Me.GenerateData(CompanyID, ReturnPeriodID)
                End Select
            End If
        ElseIf myUtils.IsInList(dataKey, "infojson") AndAlso Not String.IsNullOrWhiteSpace(InfoJsonAPITaskID) Then
            oRet.Description = myContext.Provider.objSQLHelper.ExecuteScalar(CommandType.Text, "SELECT INFOJSON FROM APITASK WHERE APITaskID ='" & InfoJsonAPITaskID.ToString & "'").ToString
        ElseIf myUtils.IsInList(dataKey, "payloadstatus") AndAlso Not String.IsNullOrWhiteSpace(returnTaskID) Then
            oRet = QueueTaskProvider.GetTaskStatus(myContext, ReturnTaskID)
        Else
            If GstRegID = 0 Then
                oRet.AddError("Please select GSTIN")
            Else
                Dim oProc As New clsGSTNReturnGSTR3B(myContext)
                Select Case dataKey.Trim.ToLower
                    Case "otp"
                        oRet = oProc.CheckToken(GstRegID)
                    Case "token"
                        Dim OTP As String = myUtils.cStrTN(myContext.SQL.ParamValue("@otp", Params))
                        oRet = oProc.GetTokenOutput(oProc.GetToken(GstRegID, OTP))
                    Case "summary"
                        oRet = oProc.GetJsonSummary(GstRegID, ReturnPeriodID)
                    Case "cashitcbal"
                        oRet = oProc.GetCashITCBalanceJson(GstRegID, ReturnPeriodID)
                    Case "cashdetail"
                        oRet = oProc.GetCashDetail(GstRegID, ReturnPeriodID)
                    Case "itcdetail"
                        oRet = oProc.GetItcDetail(GstRegID, ReturnPeriodID)
                    Case "retlibbal"
                        oRet = oProc.GetRetLibBalance(GstRegID, ReturnPeriodID)
                    Case "libdetails"
                        oRet = oProc.GetLibDetails(GstRegID, ReturnPeriodID)
                    Case "savepditc"
                        Dim str1 = myUtils.cStrTN(myContext.SQL.ParamValue("@pditc", Params))
                        oRet = oProc.SavePditcTaxPaymentGSTIN(GstRegID, ReturnPeriodID, JsonConvert.DeserializeObject(Of Pditc)(str1))
                        oRet.Data = Me.GenerateData(CompanyID, ReturnPeriodID)
                    Case "offset"
                    Case "newoffset"
                        Dim Data As String = myUtils.cStrTN(myContext.SQL.ParamValue("@offsetdata", Params))
                        Dim Model = JsonConvert.DeserializeObject(Of Offsetliability)(Data)
                        oRet = oProc.Offset(GstRegID, ReturnPeriodID, Model)
                    Case "signdata"
                        Dim forcefresh As Boolean = False
                        Dim nr As DataRow = oProc.GenerateSignData(GstRegID, ReturnPeriodID, forcefresh)

                        If Not nr Is Nothing Then
                            Dim rGst = oProc.oMaster.GstRegRow(GstRegID)
                            Dim toBeSigned As String = nr("hashedpayload")
                            Dim dic = New Dictionary(Of String, Object)()
                            dic.Add("DataHashKey", toBeSigned)
                            dic.Add("AuthPanNumber", rGst("AuthPAN"))
                            oRet.Description = JsonConvert.SerializeObject(dic)
                        End If
                    Case "filesign"
                        Dim forcefresh As Boolean = False
                        Dim nr As DataRow = oProc.GenerateSignData(GstRegID, ReturnPeriodID, forcefresh)
                        If Not nr Is Nothing Then
                            Dim SignedPayload As String = myUtils.cStrTN(myContext.SQL.ParamValue("@sign", Params))
                            If String.IsNullOrWhiteSpace(SignedPayload) Then
                                oRet.AddError("Blank Payload")
                            Else
                                nr("signedpayload") = SignedPayload
                                myContext.Provider.objSQLHelper.SaveResults(nr.Table, "select * from gstnsign where 0=1")
                                oRet = oProc.FileSignedReturn(nr)
                            End If
                        End If
                    Case "submit"
                        oRet = oProc.SubmitReturn(GstRegID, ReturnPeriodID)
                        If oRet.Success Then oRet.Description = oRet.GetCalcRows("task")(0)("apitaskid").ToString
                    Case "payload"
                        Dim filename As String = myContext.Controller.CalcRLSId.ToString & "-GSTR3B-" & Replace(Now.ToLongTimeString, ":", "").Replace(" ", "") & ".zip"
                        'oRet = oProc.GeneratePayload(GstRegID, ReturnPeriodID, "UM", filename)     'For Payload testing.
                        Dim dicParams As New Dictionary(Of String, String)
                        dicParams("filename") = filename
                        oRet = TaskProviderFactory.CheckAddTask(Of List(Of clsSQLParam))(myContext, "gstr3b", dataKey, CompanyID, Params, queueName, dicParams)
                        If oRet.Success Then oRet.Description = oRet.GetCalcRows("task")(0)("apitaskid").ToString
                    Case "excel"
                        Dim filename As String = myContext.Controller.CalcRLSId.ToString & "-GSTR3B-" & Replace(Now.ToLongTimeString, ":", "").Replace(" ", "") & ".xlsx"
                        Dim dicParams As New Dictionary(Of String, String)
                        dicParams("filename") = filename
                        oRet = TaskProviderFactory.CheckAddTask(myContext, "gstr3b", dataKey, CompanyID, Params, queueName, dicParams)
                        If oRet.Success Then oRet.Description = oRet.GetCalcRows("task")(0)("apitaskid").ToString
                    Case Else
                        Dim rToken = oProc.GetActiveToken(myContext.Police.UniqueUserID, GstRegID)
                        If rToken Is Nothing Then
                            oRet.AddError("Active token not found. Pl connect and generate token")
                        Else
                            Dim dicParams As New Dictionary(Of String, String)
                            oRet = TaskProviderFactory.CheckAddTask(Of List(Of clsSQLParam))(myContext, "gstr3b", dataKey, CompanyID, Params, queueName, dicParams)
                            If oRet.Success Then oRet.Description = oRet.GetCalcRows("task")(0)("apitaskid").ToString
                        End If
                End Select
            End If

        End If
        Return oRet
    End Function

    Public Overrides Function VSave() As Boolean
        VSave = False
        Dim SummaryUpload As Boolean = False
        If Me.Validate Then

            If Me.CanSave Then
                Try
                    Dim ReturnPeriodID As Integer = myUtils.cValTN(Me.dsForm.Tables("return").Rows(0)("postperiodid"))
                    myContext.Provider.dbConn.BeginTransaction(myContext, Me.Name, Me.frmMode.ToString, "CompanyID", frmIDX)
                    myUtils.ChangeAll(dsForm.Tables("itcrev").Select, "Returnperiodid=" & ReturnPeriodID)
                    myContext.Provider.objSQLHelper.SaveResults(dsForm.Tables("itcrev"), "select itcrevsid, GstRegID,ReturnPeriodID, RuleNum, RuleSection, Iamt, Camt, Samt, CsAmt, [Action] from itcrevs where 0=1")

                    frmMode = EnumfrmMode.acEditM
                    myContext.Provider.dbConn.CommitTransaction("ITC Reversal updated", frmIDX)
                    VSave = True


                Catch ex2 As Exception
                    myContext.Provider.dbConn.RollBackTransaction("ITC Reversal updated", ex2.Message)
                    Me.AddError("", ex2.Message)
                End Try
            End If
        End If
    End Function

    Public Function GetInfoJSON(apiTaskId As String) As clsProcOutput
        Dim oRet As New clsProcOutput
        Dim ds = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, "SELECT INFOJSON FROM APITASK WHERE APITaskID ='" & apiTaskId.ToString & "'")
        oRet.JsonData = ds
        Return oRet
    End Function
End Class
