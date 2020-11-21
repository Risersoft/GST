Imports risersoft.shared
Imports risersoft.shared.cloud
Imports risersoft.shared.cloud.common
Imports System.Runtime.Serialization
Imports System.Configuration
Imports Newtonsoft.Json

<DataContract>
Public Class frmGSTNGSTR1Model
    Inherits clsFormDataModel
    Dim myViewGSTReg, myViewHistory As clsViewModel

    Protected Overrides Sub InitViews()
        myViewGSTReg = Me.GetViewModel("GSTReg")
        myViewHistory = Me.GetViewModel("History")
        myView = Me.GetViewModel("Invoice")
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
    End Sub

    Public Overrides Function PrepForm(oView As clsViewModel, ByVal prepMode As EnumfrmMode, ByVal prepIDX As String, Optional ByVal strXML As String = "") As Boolean
        Dim Sql As String
        Me.FormPrepared = False
        If Not prepIDX Is Nothing Then
            prepMode = EnumfrmMode.acEditM
            Sql = "Select CompanyID,CompName,PANNum from Company Where CompanyID = " & prepIDX
            Me.InitData(Sql, "returnperiodid", oView, prepMode, prepIDX, strXML)

            Dim ReturnPeriodID As Integer = myFuncs2.ValidatedPostPeriodID(myContext, "ReturnPeriodID", myUtils.cValTN(Me.vBag("returnperiodid")), Me.dsCombo.Tables("postperiod"))
            Me.BindDataTable(myUtils.cValTN(prepIDX), ReturnPeriodID)

            myViewGSTReg.MainGrid.MainConf("formatxml") = "<COL KEY=""Descrip"" CAPTION=""State""/><COL KEY=""Txval"" CAPTION=""Taxable Value""/><COL KEY=""Iamt"" CAPTION=""IGST""/><COL KEY=""camt"" CAPTION=""CGST""/><COL KEY=""SAMT"" CAPTION=""SGST""/><COL KEY=""csamt"" CAPTION=""CESS""/>"
            myViewGSTReg.MainGrid.BindGridData(Me.dsForm, 1)
            myViewGSTReg.MainGrid.QuickConf("", True, "1.5-1-1-1-1-1-1-1-0")
            myViewGSTReg.MainGrid.PrepEdit("<BAND INDEX=""0""><COL KEY=""sysIncl""/></BAND>", EnumEditType.acEditOnly)


            myView.MainGrid.MainConf("formatxml") = "<COL KEY=""SectionName"" CAPTION=""InvoiceType""/><COL KEY=""ret_pd"" CAPTION=""Date""/><COL KEY=""Txval"" CAPTION=""Taxable Value""/><COL KEY=""Iamt"" CAPTION=""IGST""/><COL KEY=""camt"" CAPTION=""CGST""/><COL KEY=""SAMT"" CAPTION=""SGST""/><COL KEY=""csamt"" CAPTION=""CESS""/>"
            myView.MainGrid.BindGridData(Me.dsForm, 2)
            myView.MainGrid.QuickConf("", True, "1-1-1-1-1-1-1-1-1")

            myViewHistory.MainGrid.BindGridData(Me.dsForm, 4)
            myViewHistory.MainGrid.QuickConf("", True, "1-1-1-1-1-0-0-0-0")

            Me.FormPrepared = True
        End If
        Return Me.FormPrepared
    End Function
    Protected Friend Overloads Function GenerateData(ByVal CompanyID As Integer, ReturnPeriodID As Integer) As DataSet
        Dim dic As New clsCollecString(Of String)
        dic.Add("gstreg", "Select GSTRegID, GSTIN, Descrip, convert(money,0) As val, convert(money,0) As iamt, convert(money,0) As camt, convert(money,0) As samt, convert(money,0) As csamt, convert(money,0) As txval,CompName from GstListGSTReg() where  CompanyID = " & CompanyID)


        dic.Add("detail", String.Format("select gstregid, SectionName, ret_pd, count(InvoiceID) As InvoiceCount, 
                          sum(isnull(Factor*FinalTxVal,0)) as Txval, sum(isnull(factor*Factor2*FinalIAMT,0)) as Iamt, sum(isnull(factor*Factor2*FinalCAMT,0)) As camt, sum(isnull(factor*Factor2*FinalSAMT,0)) As samt,
                            sum(isnull(factor*Factor2*FinalCSAMT,0)) As csamt,sum(isnull(val,0)) as val from GstListInvoice2() 
                            where Doctype in ('IS') and isnull(StatusNum,0)=2  and CancelDate is NULL and CompanyID={0} and ReturnPeriodID={1}
                            group by SectionName, ret_pd, gstregid, CompanyID, ReturnPeriodID
                             union all
                                                    select gstregid, SectionName, ret_pd, count(GstAdvanceVouchID) As InvoiceCount, 
                                                    sum(isnull(Factor*FinalAD_AMT,0)) as Txval, sum(isnull(factor*Factor2*FinalIAMT,0)) as Iamt, sum(isnull(factor*Factor2*FinalCAMT,0)) As camt, sum(isnull(factor*Factor2*FinalSAMT,0)) As samt,
                                                    sum(isnull(factor*Factor2*FinalCSAMT,0)) As csamt,sum( isnull(val,0) ) as val from GstListAdvanceVouch() 
                                                    where Doctype in ('PC') and isnull(StatusNum,0)=2  and CancelDate is NULL and CompanyID={0} and ReturnPeriodID={1}
                      group by SectionName, ret_pd, gstregid, CompanyID, ReturnPeriodID", CompanyID, ReturnPeriodID))

        dic.Add("return", String.Format("select * from postperiod where postperiodid ={0}", ReturnPeriodID))
        dic.Add("TaskHistoryData", "SELECT top 50 ApiTaskID, TenantID, ActionType,ActionSubType, SubmittedOn, CompletedOn, Status, FileName, ImportFileID, AppBarJson, Success FROM ApiTask WHERE ParentID = " & CompanyID & " and ActionType='GSTR1' AND Success=1 order by submittedon desc")

        Dim ds = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, dic)
        For Each r1 As DataRow In ds.Tables("gstreg").Select
            For Each str1 As String In New String() {"val", "iamt", "camt", "samt", "csamt", "txval"}
                r1(str1) = Math.Round(myUtils.cValTN(ds.Tables("detail").Compute("sum(" & str1 & ")", "gstregid=" & r1("gstregid"))), 2)
            Next
        Next

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

    Public Overrides Function VSave() As Boolean
        VSave = False
        If Me.Validate Then
            VSave = True
        End If
    End Function

    Public Overrides Function GenerateParamsOutput(dataKey As String, Params As List(Of clsSQLParam)) As clsProcOutput
        Dim GstRegID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@gstregid", Params))
        Dim CompanyID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@companyid", Params))
        Dim ReturnPeriodID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@ReturnPeriodID", Params))
        Dim ReturnTaskID As String = myContext.SQL.ParamValue("@ApiTaskID", Params)
        Dim InfoJsonAPITaskID As String = myContext.SQL.ParamValue("@UploadType", Params)
        Dim GstRegM As String = myUtils.cStrTN(myContext.SQL.ParamValue("@gstregm", Params))
        Dim queueName = myContext.Controller.CalcQueueName
        Dim oRet As New clsProcOutput
        If myUtils.IsInList(dataKey, "dbsumm") Then
            If CompanyID = 0 Then
                oRet.AddError("Company not selected")
            Else
                Select Case dataKey.Trim.ToLower
                    Case "dbsumm"
                        oRet.Data = Me.GenerateData(CompanyID, ReturnPeriodID)
                        Dim provider As New clsDBUserFilterProvider(myContext, False)
                        provider.AddUpdValueRow("ReturnPeriodID", ReturnPeriodID)
                        provider.SaveDBUserFilter()
                        oRet.Message = "Return Peroid change successful"
                End Select
            End If
        ElseIf myUtils.IsInList(dataKey, "infojson") AndAlso Not String.IsNullOrWhiteSpace(InfoJsonAPITaskID) Then
            oRet.Description = myContext.Provider.objSQLHelper.ExecuteScalar(CommandType.Text, "SELECT INFOJSON FROM APITASK WHERE APITaskID ='" & InfoJsonAPITaskID.ToString & "'").ToString
        ElseIf myUtils.IsInList(dataKey, "payloadstatus") AndAlso Not String.IsNullOrWhiteSpace(returnTaskID) Then
            oRet = QueueTaskProvider.GetTaskStatus(myContext, ReturnTaskID)
        ElseIf myUtils.IsInList(dataKey, "uploadm") Then
            If String.IsNullOrWhiteSpace(GstRegM) Then
                oRet.AddError("GSTIN not selected")
            Else
                Dim dicParams As New Dictionary(Of String, String)
                oRet = TaskProviderFactory.CheckAddTask(myContext, "gstr1", dataKey, CompanyID, Params, queueName, dicParams)
                If oRet.Success Then oRet.Description = oRet.GetCalcRows("task")(0)("apitaskid").ToString
            End If
        Else
            If GstRegID = 0 Then
                oRet.AddError("Please select GSTIN")
            Else
                Select Case dataKey.Trim.ToLower
                    Case "otp"
                        Dim oProc As New clsGSTNReturnGSTR1(myContext)
                        oRet = oProc.CheckToken(GstRegID)
                    Case "token"
                        Dim oProc As New clsGSTNReturnGSTR1(myContext)
                        Dim OTP As String = myUtils.cStrTN(myContext.SQL.ParamValue("@otp", Params))
                        oRet = oProc.GetTokenOutput(oProc.GetToken(GstRegID, OTP))
                    Case "summary"
                        Dim oProc As New clsGSTNReturnGSTR1(myContext)
                        Dim rToken = oProc.GetActiveToken(myContext.Police.UniqueUserID, GstRegID)
                        If rToken Is Nothing Then
                            oRet.AddError("Active token not found. Pl connect and generate token")
                        Else
                            oRet = oProc.GetJsonSummary(GstRegID, ReturnPeriodID)
                        End If
                    Case "submit"
                        Dim oProc As New clsGSTNReturnGSTR1(myContext)
                        oRet = oProc.SubmitReturn(GstRegID, ReturnPeriodID)
                        If oRet.Success Then
                            Dim calcRows = oRet.GetCalcRows("task")
                            If calcRows.Count > 0 Then
                                oRet.Description = calcRows(0)("apitaskid").ToString
                            End If
                        End If
                    Case "signdata"
                        Dim forcefresh As Boolean = False
                        Dim oProc As New clsGSTNReturnGSTR1(myContext)
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
                        Dim oProc As New clsGSTNReturnGSTR1(myContext)
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
                    Case "deleteinvoice", "deletepayment", "deleteimportonline"
                        Dim dicParams As New Dictionary(Of String, String)
                        oRet = TaskProviderFactory.CheckAddTask(myContext, "gstr1", dataKey, CompanyID, Params, queueName, dicParams)
                        If oRet.Success Then oRet.Description = oRet.GetCalcRows("task")(0)("apitaskid").ToString
                    Case "reconcile"
                        Dim filename As String = myContext.Controller.CalcRLSId.ToString & "-GSTR1-Reconcile-" & Replace(Now.ToLongTimeString, ":", "").Replace(" ", "") & ".xlsx"
                        Dim dicParams As New Dictionary(Of String, String)
                        dicParams("filename") = filename
                        oRet = TaskProviderFactory.CheckAddTask(myContext, "gstr1", dataKey, CompanyID, Params, queueName, dicParams)
                        If oRet.Success Then oRet.Description = oRet.GetCalcRows("task")(0)("apitaskid").ToString
                    Case "payload"
                        Dim filename As String = myContext.Controller.CalcRLSId.ToString & "-GSTR1-" & Replace(Now.ToLongTimeString, ":", "").Replace(" ", "") & ".zip"
                        Dim dicParams As New Dictionary(Of String, String)
                        dicParams("filename") = filename
                        oRet = TaskProviderFactory.CheckAddTask(myContext, "gstr1", dataKey, CompanyID, Params, queueName, dicParams)
                        If oRet.Success Then oRet.Description = oRet.GetCalcRows("task")(0)("apitaskid").ToString
                    Case "excel"
                        Dim filename As String = myContext.Controller.CalcRLSId.ToString & "-GSTR1-" & Replace(Now.ToLongTimeString, ":", "").Replace(" ", "") & ".xlsx"
                        Dim dicParams As New Dictionary(Of String, String)
                        dicParams("filename") = filename
                        oRet = TaskProviderFactory.CheckAddTask(myContext, "gstr1", dataKey, CompanyID, Params, queueName, dicParams)
                        If oRet.Success Then oRet.Description = oRet.GetCalcRows("task")(0)("apitaskid").ToString
                    Case "pdf"
                        Dim filename As String = myContext.Controller.CalcRLSId.ToString & "-GSTR1-" & Replace(Now.ToLongTimeString, ":", "").Replace(" ", "") & ".pdf"
                        Dim dicParams As New Dictionary(Of String, String)
                        dicParams("filename") = filename
                        oRet = TaskProviderFactory.CheckAddTask(myContext, "gstr1", dataKey, CompanyID, Params, queueName, dicParams)
                        If oRet.Success Then oRet.Description = oRet.GetCalcRows("task")(0)("apitaskid").ToString
                    Case "template", "gstn_error"
                        Dim filename As String = myContext.Controller.CalcRLSId.ToString & "-GSTR1-" & Replace(Now.ToLongTimeString, ":", "").Replace(" ", "") & ".xlsx"
                        Dim dicParams As New Dictionary(Of String, String)
                        dicParams("filename") = filename
                        oRet = TaskProviderFactory.CheckAddTask(myContext, "gstr1", dataKey, CompanyID, Params, queueName, dicParams)
                        If oRet.Success Then oRet.Description = oRet.GetCalcRows("task")(0)("apitaskid").ToString
                    Case Else
                        Dim oProc As New clsGSTNReturnGSTR1(myContext)
                        Dim rToken = oProc.GetActiveToken(myContext.Police.UniqueUserID, GstRegID)
                        If rToken Is Nothing Then
                            oRet.AddError("Active token not found. Pl connect and generate token")
                        Else
                            Dim dicParams As New Dictionary(Of String, String)
                            oRet = TaskProviderFactory.CheckAddTask(myContext, "gstr1", dataKey, CompanyID, Params, queueName, dicParams)
                            'If oRet.Success Then oRet.JsonData = New With {.success = True, .ApiTaskID = oRet.GetCalcRows("task")(0)("apitaskid")}
                            If oRet.Success Then oRet.Description = oRet.GetCalcRows("task")(0)("apitaskid").ToString
                        End If
                End Select

            End If
        End If
        Return oRet
    End Function

    Public Overrides Function GenerateDataParamsOutput(dataKey As String, ds As DataSet, Params As List(Of clsSQLParam)) As clsProcOutput
        Dim GstRegID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@gstregid", Params))
        Dim ReturnPeriodID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@ReturnPeriodID", Params))
        Dim oRet As New clsProcOutput
        Select Case dataKey.Trim.ToLower
            Case "import"
                Dim oProc As New clsGSTNReturnGSTR1(myContext)
                oRet = oProc.UpdateDownloadedDataCP(GstRegID, ReturnPeriodID, Nothing).Result
        End Select
        Return oRet
    End Function
End Class
