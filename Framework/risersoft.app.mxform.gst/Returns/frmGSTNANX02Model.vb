Imports Newtonsoft.Json
Imports risersoft.shared
Imports risersoft.shared.cloud
Imports risersoft.shared.cloud.common
Imports System.Configuration
Imports System.Runtime.Serialization

<DataContract>
Public Class frmGSTNANX02Model
    Inherits clsFormDataModel
    Dim myViewGSTReg, myViewITCRev, myViewHistory As clsViewModel

    Protected Overrides Sub InitViews()
        myViewGSTReg = Me.GetViewModel("GSTReg")
        myView = Me.GetViewModel("Invoice")
        myViewITCRev = Me.GetViewModel("ITCRev")
        myViewHistory = Me.GetViewModel("History")
    End Sub

    Public Sub New(context As IProviderContext)
        MyBase.New(context)
        Me.InitViews()
        Me.InitForm()
    End Sub

    Private Sub InitForm()
        Dim Sql As String = "Select GstRegID, GSTIN from GstReg  Order by GSTIN"
        Me.AddLookupField("GstRegID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql), "GstReg").TableName)


        Sql = "Select PostPeriodID, ret_pd,sdate from PostPeriod  Order by sdate desc"
        Me.AddLookupField("ReturnPeriodID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql), "PostPeriod").TableName)
        Me.AddLookupField("ToID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql), "PostPeriod").TableName)
        Me.AddLookupField("FromID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql), "PostPeriod").TableName)


        Dim vlist As New clsValueList
        vlist.Add("Pan", "PAN")
        vlist.Add("Gstin", "GSTIN")
        Me.ValueLists.Add("Operation", vlist)
        Me.AddLookupField("Operation", "Operation")

        Dim vlistc As New clsValueList
        vlistc.Add("Selected", "Selected Period")
        vlistc.Add("FY", "Financial Year")
        vlistc.Add("Custom", "Custom Period")
        Me.ValueLists.Add("Period", vlistc)
        Me.AddLookupField("Period", "Period")

        Dim vlistn As New clsValueList
        vlistn.Add("Add", "Add")
        vlistn.Add("Less", "Less")
        Me.ValueLists.Add("Action", vlistn)
        Me.AddLookupField("Action", "Action")
    End Sub

    Public Overrides Function PrepForm(oView As clsViewModel, ByVal prepMode As EnumfrmMode, ByVal prepIDX As String, Optional ByVal strXML As String = "") As Boolean
        Dim Sql As String

        Me.FormPrepared = False
        If Not prepIDX Is Nothing Then
            prepMode = EnumfrmMode.acAddM
            Sql = "Select CompanyID,CompName,PANNum from Company Where CompanyID = " & prepIDX
            Me.InitData(Sql, "returnperiodid", oView, prepMode, prepIDX, strXML)

            Dim ReturnPeriodID As Integer = myFuncs2.ValidatedPostPeriodID(myContext, "ReturnPeriodID", myUtils.cValTN(Me.vBag("returnperiodid")), Me.dsCombo.Tables("postperiod"))
            Me.BindDataTable(myUtils.cValTN(prepIDX), ReturnPeriodID, "ReturnPeriodID=" & ReturnPeriodID)

            myViewGSTReg.MainGrid.MainConf("formatxml") = "<COL KEY=""Descrip"" CAPTION=""State""/><COL KEY=""Txval"" CAPTION=""Taxable Value""/><COL KEY=""Iamt"" CAPTION=""IGST""/><COL KEY=""camt"" CAPTION=""CGST""/><COL KEY=""SAMT"" CAPTION=""SGST""/><COL KEY=""csamt"" CAPTION=""CESS""/>"
            myViewGSTReg.MainGrid.BindGridData(Me.dsForm, 1)
            myViewGSTReg.MainGrid.QuickConf("", True, "1.5-1-1-1-1-1-1-1")

            myView.MainGrid.MainConf("formatxml") = "<COL KEY=""SectionName"" CAPTION=""InvoiceType""/><COL KEY=""ret_pd"" CAPTION=""Date""/><COL KEY=""Txval"" CAPTION=""Taxable Value""/><COL KEY=""Iamt"" CAPTION=""IGST""/><COL KEY=""camt"" CAPTION=""CGST""/><COL KEY=""SAMT"" CAPTION=""SGST""/><COL KEY=""csamt"" CAPTION=""CESS""/>"
            myView.MainGrid.BindGridData(Me.dsForm, 2)
            myView.MainGrid.QuickConf("", True, "1-1-1-1-1-1-1-1-1")

            myViewITCRev.MainGrid.BindGridData(Me.dsForm, 4)
            myViewITCRev.MainGrid.QuickConf("", True, "1-1-1-1-1-1")
            Dim str2 As String = "<BAND INDEX = ""4"" TABLE = ""ITCRevs"" IDFIELD=""ITCRevsID""><COL KEY =""GstRegID,ReturnPeriodID, RuleNum, RuleSection, Iamt, Camt, Samt, CsAmt""/></BAND>"
            myViewITCRev.MainGrid.MainConf("formatxml") = "<COL KEY=""RuleNum"" CAPTION=""Rule Num""/><COL KEY=""RuleSection"" CAPTION=""Rule Section""/><COL KEY=""Iamt"" CAPTION=""IGST""/><COL KEY=""camt"" CAPTION=""CGST""/><COL KEY=""SAMT"" CAPTION=""SGST""/><COL KEY=""csamt"" CAPTION=""CESS""/>"
            myViewITCRev.MainGrid.PrepEdit(str2, EnumEditType.acCommandAndEdit)

            myViewHistory.MainGrid.BindGridData(Me.dsForm, 6)
            myViewHistory.MainGrid.QuickConf("", True, "1-1-1-1-1-0-0-0-0")

            Me.FormPrepared = True
        End If
        Return Me.FormPrepared
    End Function
    Protected Friend Overloads Function GenerateData(ByVal CompanyID As Integer, PostPeriodID As Integer, strFilter As String) As DataSet
        Dim dic As New clsCollecString(Of String)
        dic.Add("gstreg", "Select GSTRegID, GSTIN, Descrip, convert(money,0) As val, convert(money,0) As iamt, convert(money,0) As camt, convert(money,0) As samt, convert(money,0) As csamt, convert(money,0) As txval from GstListGSTReg() where  CompanyID = " & CompanyID)
        dic.Add("detail", String.Format("Select gstregid, SectionName, ret_pd, count(Invoiceid) As InvoiceCount, 
                 sum(isnull(Factor*txval,0)) as Txval, sum(isnull(factor*Factor2*iamt,0)) as Iamt, sum(isnull(factor*Factor2*camt,0)) As camt, sum(isnull(factor*Factor2*samt,0)) As samt,
                 sum(isnull(factor*Factor2*csamt,0)) As csamt,sum(isnull(val,0)) as val from GstListInvoice() 
                 where isnull(anx02section,'')<>'' and isnull(StatusNum,0)=2  and CancelDate is NULL and CompanyID={0} and {1}
                 group by SectionName, ret_pd, gstregid, CompanyID, ReturnPeriodID", CompanyID, strFilter))
        dic.Add("return", String.Format("select * from postperiod where postperiodid ={0}", PostPeriodID))
        dic.Add("itcrev", String.Format("select itcrevsid, GstRegID,ReturnPeriodID,[Action], RuleNum, RuleSection, Iamt, Camt, Samt, CsAmt from itcrevs where gstregid in (select gstregid from gstreg where companyid={0}) and {1}", CompanyID, strFilter))
        dic.Add("counter", String.Format("select gstregid, gstinvoicetype, ret_pd, count(cpinvoiceid)  As InvoiceCount, sum(val) as val, sum(iamt) As iamt, sum(camt) As camt, sum(samt) As samt,
            sum(csamt) As csamt,sum(txval) As txval from acclistcpinvoice() where doctype='IP' and CompanyID={0} and {1} group by gstinvoicetype, ret_pd, gstregid", CompanyID, strFilter))
        dic.Add("TaskHistoryData", "SELECT top 50 ApiTaskID, TenantID, ActionType,ActionSubType, SubmittedOn, CompletedOn, Status, FileName, ImportFileID, AppBarJson, Success FROM ApiTask WHERE ParentID = " & CompanyID & " and ActionType='GSTR2' AND Success=1 order by submittedon desc")
        Dim ds = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, dic)


        For Each r1 As DataRow In ds.Tables("gstreg").Select
            For Each str1 As String In New String() {"val", "iamt", "camt", "samt", "csamt", "txval"}
                r1(str1) = Math.Round(myUtils.cValTN(ds.Tables("detail").Compute("sum(" & str1 & ")", "gstregid=" & r1("gstregid"))), 2)
            Next
        Next
        ds.AcceptChanges()
        Return ds
    End Function
    Private Sub BindDataTable(ByVal CompanyID As Integer, ReturnPeriodID As Integer, strFilter As String)
        Dim ds = Me.GenerateData(CompanyID, ReturnPeriodID, strFilter)
        For i As Integer = 0 To ds.Tables.Count - 1
            myUtils.AddTable(Me.dsForm, ds, ds.Tables(i).TableName, i)
        Next
    End Sub

    Public Overrides Function Validate() As Boolean
        Me.InitError()
        Return Me.CanSave
    End Function

    Public Overrides Function GenerateDataParamsOutput(dataKey As String, ds As DataSet, Params As List(Of clsSQLParam)) As clsProcOutput
        Dim GstRegID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@gstregid", Params))
        Dim ReturnPeriodID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@ReturnPeriodID", Params))
        Dim oRet As New clsProcOutput
        Select Case dataKey
            Case "import"
                Dim oProc As New clsGSTNReturnGSTR2(myContext)
                oRet = oProc.UpdateDownloadedDataCP(GstRegID, ReturnPeriodID, Nothing).Result
        End Select
        Return oRet
    End Function

    Public Overrides Function GenerateParamsOutput(dataKey As String, Params As List(Of clsSQLParam)) As clsProcOutput
        Dim GstRegID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@gstregid", Params))
        Dim ReturnPeriodID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@ReturnPeriodID", Params))
        Dim ReturnTaskID As String = myContext.SQL.ParamValue("@ApiTaskID", Params)
        Dim CompanyID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@companyid", Params))
        Dim queueName = myContext.Controller.CalcQueueName
        Dim InfoJsonAPITaskID As String = myContext.SQL.ParamValue("@UploadType", Params)

        Dim oRet As New clsProcOutput
        If myUtils.IsInList(dataKey, "dbsumm") Then
            If CompanyID = 0 Then
                oRet.AddError("Company not selected")
            Else
                Select Case dataKey.Trim.ToLower
                    Case "dbsumm"
                        Dim Period As String = myUtils.cStrTN(myContext.SQL.ParamValue("@period", Params))
                        Dim FromID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@from", Params))
                        Dim ToID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@to", Params))
                        Dim oProc As New clsGSTNReturnGSTR2(myContext)
                        Dim oRetPP = myFiltUI.FindPeriodID(oProc.oMaster.PostPeriodTable, Period, ReturnPeriodID, FromID, ToID)
                        Dim dicFilter = myFuncs2.PopulateFilters(oProc.oMaster, oProc.DocType, GstRegID, oRetPP.IDList(0), oRetPP.IDList(1), True)

                        oRet.Data = Me.GenerateData(CompanyID, ReturnPeriodID, dicFilter("ReturnPeriodID"))
                        Dim provider As New clsDBUserFilterProvider(myContext, False)
                        provider.AddUpdValueRow("ReturnPeriodID", ReturnPeriodID)
                        provider.SaveDBUserFilter()
                        oRet.Message = "Return Peroid change successful"
                End Select
            End If
        ElseIf myUtils.IsInList(dataKey, "infojson") AndAlso Not String.IsNullOrWhiteSpace(InfoJsonAPITaskID) Then
            oRet.Description = myContext.Provider.objSQLHelper.ExecuteScalar(CommandType.Text, "SELECT INFOJSON FROM APITASK WHERE APITaskID ='" & InfoJsonAPITaskID.ToString & "'").ToString
        ElseIf myUtils.IsInList(dataKey, "payloadstatus") AndAlso Not String.IsNullOrWhiteSpace(ReturnTaskID) Then
            oRet = QueueTaskProvider.GetTaskStatus(myContext, ReturnTaskID)
        Else
            If GstRegID = 0 Then
                oRet.AddError("Please select GSTIN")
            Else
                Select Case dataKey.Trim.ToLower
                    Case "otp"
                        Dim oProc As New clsGSTNReturnGSTR2(myContext)
                        oRet = oProc.CheckToken(GstRegID)
                    Case "token"
                        Dim oProc As New clsGSTNReturnGSTR2(myContext)
                        Dim OTP As String = myUtils.cStrTN(myContext.SQL.ParamValue("@otp", Params))
                        oRet = oProc.GetTokenOutput(oProc.GetToken(GstRegID, OTP))
                    Case "summary"
                        Dim oProc As New clsGSTNReturnGSTR2(myContext)
                        Dim rToken = oProc.GetActiveToken(myContext.Police.UniqueUserID, GstRegID)
                        If rToken Is Nothing Then
                            oRet.AddError("Active token not found. Pl connect and generate token")
                        Else
                            oRet = oProc.GetJsonSummary(GstRegID, ReturnPeriodID)
                        End If
                    Case "submit"
                        Dim oProc As New clsGSTNReturnGSTR2(myContext)
                        oRet = oProc.SubmitReturn(GstRegID, ReturnPeriodID)
                        If oRet.Success Then oRet.Description = oRet.GetCalcRows("task")(0)("apitaskid").ToString
                    Case "delete"
                        Dim dicParams As New Dictionary(Of String, String)
                        oRet = TaskProviderFactory.CheckAddTask(myContext, "anx02", dataKey, CompanyID, Params, queueName, dicParams)
                        If oRet.Success Then oRet.Description = oRet.GetCalcRows("task")(0)("apitaskid").ToString
                    Case "deletecp"
                        Dim dicParams As New Dictionary(Of String, String)
                        oRet = TaskProviderFactory.CheckAddTask(myContext, "anx02", dataKey, CompanyID, Params, queueName, dicParams)
                        If oRet.Success Then oRet.Description = oRet.GetCalcRows("task")(0)("apitaskid").ToString
                    Case "reconcile", "generate", "email"
                        Dim filename As String = myContext.Controller.CalcRLSId.ToString & "-ANX02-Reconcile-" & Replace(Now.ToLongTimeString, ":", "").Replace(" ", "") & ".xlsx"
                        Dim dicParams As New Dictionary(Of String, String)
                        dicParams("filename") = filename
                        oRet = TaskProviderFactory.CheckAddTask(myContext, "anx02", dataKey, CompanyID, Params, queueName, dicParams)
                        If oRet.Success Then oRet.Description = oRet.GetCalcRows("task")(0)("apitaskid").ToString
                    Case "payload"
                        Dim filename As String = myContext.Controller.CalcRLSId.ToString & "-ANX02-" & Replace(Now.ToLongTimeString, ":", "").Replace(" ", "") & ".zip"
                        Dim dicParams As New Dictionary(Of String, String)
                        dicParams("filename") = filename
                        oRet = TaskProviderFactory.CheckAddTask(myContext, "anx02", dataKey, CompanyID, Params, queueName, dicParams)
                        If oRet.Success Then oRet.Description = oRet.GetCalcRows("task")(0)("apitaskid").ToString
                    Case "excel"
                        Dim filename As String = myContext.Controller.CalcAccountName & "-ANX02-" & Replace(Now.ToLongTimeString, ":", "").Replace(" ", "") & ".xlsx"
                        Dim dicParams As New Dictionary(Of String, String)
                        dicParams("filename") = filename
                        oRet = TaskProviderFactory.CheckAddTask(myContext, "anx02", dataKey, CompanyID, Params, queueName, dicParams)
                        If oRet.Success Then oRet.Description = oRet.GetCalcRows("task")(0)("apitaskid").ToString
                    Case "pdf"
                        Dim filename As String = myContext.Controller.CalcAccountName & "-ANX02-" & Replace(Now.ToLongTimeString, ":", "").Replace(" ", "") & ".pdf"
                        Dim dicParams As New Dictionary(Of String, String)
                        dicParams("filename") = filename
                        oRet = TaskProviderFactory.CheckAddTask(myContext, "anx02", dataKey, CompanyID, Params, queueName, dicParams)
                        If oRet.Success Then oRet.Description = oRet.GetCalcRows("task")(0)("apitaskid").ToString
                    Case "manualdata"
                        Dim CTIN As String = myUtils.cStrTN(myContext.SQL.ParamValue("@CTIN", Params))
                        Dim InvoiceNum As String = myUtils.cStrTN(myContext.SQL.ParamValue("@InvoiceNum", Params))
                        Dim dic As New clsCollecString(Of String)
                        dic.Add("iv", $"select * from gstlistinvoice() where forcecpinvoiceid is null and gstregid={GstRegID} and ctin='{CTIN}' and invoicenum like '%{InvoiceNum}%' ")
                        dic.Add("cp", $"select * from acclistcpinvoice() where gstregid={GstRegID} and ctin='{CTIN}' and invoicenum like '%{InvoiceNum}%' ")
                        oRet.Data = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, dic)
                    Case "manualsave"
                        Dim DataMatch As String = myUtils.cStrTN(myContext.SQL.ParamValue("@DataMatch", Params))
                        Dim lst = JsonConvert.DeserializeObject(Of List(Of ForceMatchData))(DataMatch)
                        If lst.Count > 0 Then
                            Dim strInvoiceIDCSV = myUtils.MakeCSV(lst.Select(Function(x) x.InvoiceID).ToList, ",")
                            Dim sql As String = "select invoiceid, forcecpinvoiceid from invoice where forcecpinvoiceid is null and invoiceid in (" & strInvoiceIDCSV & ")"
                            Dim dt1 As DataTable = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql).Tables(0)
                            For Each r1 As DataRow In dt1.Select
                                r1("forcecpinvoiceid") = lst.Where(Function(x) x.InvoiceID = r1("invoiceid")).FirstOrDefault.CPInvoiceID
                            Next
                            myContext.Provider.objSQLHelper.SaveResults(dt1, sql)
                        End If
                    Case "template"
                        Dim filename As String = myContext.Controller.CalcRLSId.ToString & "-ANX02-" & Replace(Now.ToLongTimeString, ":", "").Replace(" ", "") & ".xlsx"
                        Dim dicParams As New Dictionary(Of String, String)
                        dicParams("filename") = filename
                        oRet = TaskProviderFactory.CheckAddTask(myContext, "anx02", dataKey, CompanyID, Params, queueName, dicParams)
                        If oRet.Success Then oRet.Description = oRet.GetCalcRows("task")(0)("apitaskid").ToString
                    Case Else
                        Dim oProc As New clsGSTNReturnGSTR2(myContext)
                        Dim rToken = oProc.GetActiveToken(myContext.Police.UniqueUserID, GstRegID)
                        If rToken Is Nothing Then
                            oRet.AddError("Active token not found. Pl connect and generate token")
                        Else
                            Dim dicParams As New Dictionary(Of String, String)
                            oRet = TaskProviderFactory.CheckAddTask(myContext, "anx02", dataKey, CompanyID, Params, queueName, dicParams)
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
                    myContext.Provider.objSQLHelper.SaveResults(dsForm.Tables("itcrev"), "select itcrevsid, GstRegID,ReturnPeriodID, RuleNum, RuleSection, Iamt, Camt, Samt, CsAmt ,[Action] from itcrevs where 0=1")

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

End Class

'Public Class ForceMatchData
'    Public Property InvoiceID As Integer
'    Public Property CPInvoiceID As Integer
'End Class
