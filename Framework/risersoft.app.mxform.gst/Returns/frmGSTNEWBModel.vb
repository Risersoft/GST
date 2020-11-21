Imports Newtonsoft.Json
Imports risersoft.shared
Imports risersoft.shared.cloud
Imports risersoft.shared.cloud.common
Imports risersoft.API.GSTN.GSTR3B
Imports System.Configuration
Imports System.Runtime.Serialization
Imports Microsoft.Owin.Infrastructure

<DataContract>
Public Class frmGSTNEWBModel
    Inherits clsFormDataModel
    Dim myViewGSTReg, myViewHistory As clsViewModel

    Protected Overrides Sub InitViews()
        myViewGSTReg = Me.GetViewModel("GSTReg")
        myView = Me.GetViewModel("Invoice")
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

        Sql = "Select PostPeriodID, ret_pd, sdate from PostPeriod  Order by sdate desc"
        Me.AddLookupField("ReturnPeriodID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql), "PostPeriod").TableName)
    End Sub

    Public Overrides Function PrepForm(oView As clsViewModel, ByVal prepMode As EnumfrmMode, ByVal prepIDX As String, Optional ByVal strXML As String = "") As Boolean
        Dim Sql As String
        Me.FormPrepared = False
        If Not prepIDX Is Nothing Then
            prepMode = EnumfrmMode.acAddM
            Sql = "Select CompanyID,CompName,PANNum  from Company Where CompanyID = " & prepIDX
            Me.InitData(Sql, "returnperiodid", oView, prepMode, prepIDX, strXML)


            Dim ReturnPeriodID As Integer = myUtils.cValTN(Me.vBag("returnperiodid"))
            Me.BindDataTable(myUtils.cValTN(prepIDX), ReturnPeriodID)

            myViewGSTReg.MainGrid.MainConf("formatxml") = "<COL KEY=""Descrip"" CAPTION=""State""/><COL KEY=""TXVAL"" CAPTION=""Taxable Value""/><COL KEY=""Iamt"" CAPTION=""IGST""/><COL KEY=""camt"" CAPTION=""CGST""/><COL KEY=""SAMT"" CAPTION=""SGST""/><COL KEY=""csamt"" CAPTION=""CESS""/>"
            myViewGSTReg.MainGrid.BindGridData(Me.dsForm, 1)
            myViewGSTReg.MainGrid.QuickConf("", True, "1.5-1-1-1-1-1-1")

            myView.MainGrid.MainConf("formatxml") = "<COL KEY=""EwayBillStatus"" CAPTION=""Status""/><COL KEY=""ret_pd"" CAPTION=""Date""/><COL KEY=""EwayBillCount"" CAPTION=""Count""/><COL KEY=""TXVAL"" CAPTION=""Taxable Value""/><COL KEY=""Iamt"" CAPTION=""IGST""/><COL KEY=""camt"" CAPTION=""CGST""/><COL KEY=""SAMT"" CAPTION=""SGST""/><COL KEY=""csamt"" CAPTION=""CESS""/>"
            myView.MainGrid.BindGridData(Me.dsForm, 2)
            myView.MainGrid.QuickConf("", True, "1-0-1-1-1-1-1-1-1")

            myViewHistory.MainGrid.BindGridData(Me.dsForm, 4)
            myViewHistory.MainGrid.QuickConf("", True, "1-1-1-0-1-1")

            Me.FormPrepared = True
        End If
        Return Me.FormPrepared
    End Function

    Protected Friend Overloads Function GenerateData(ByVal CompanyID As Integer, ReturnPeriodID As Integer) As DataSet
        Dim dic As New clsCollecString(Of String)
        dic.Add("gstreg", "Select GSTRegID, GSTIN, Descrip, convert(money,0) As txval, convert(money,0) As iamt, convert(money,0) As camt, convert(money,0) As samt, convert(money,0) As csamt from GstListGSTReg() where  CompanyID = " & CompanyID)

        dic.Add("detail", String.Format("select Gstregid,ret_pd,Gstin,EwayBillStatus,count(EwayBillid) as EwayBillCount,
        sum(Isnull(taxableAmount,0)) as Txval,sum(isnull(igstValue,0)) as Iamt,sum(isnull(cgstValue,0)) as camt,
         sum(isnull(sgstValue,0)) as Samt,sum(isnull(cessValue,0)) as CSamt from GstListEWB()
                            where CompanyID={0} and ReturnPeriodID={1}
                            group by gstregid,ret_pd,gstin, EwayBillStatus, CompanyID, ReturnPeriodID", CompanyID, ReturnPeriodID))
        dic.Add("return", String.Format("select * from postperiod where postperiodid ={0}", ReturnPeriodID))
        dic.Add("TaskHistoryData", "SELECT ApiTaskID, TenantID, FileName, ImportFileID, ActionType,ActionSubType, SubmittedOn, AppBarJson, CompletedOn, Status FROM ApiTask WHERE ParentID = " & CompanyID & " and ActionType='Ewb' AND Success = 1")
        Dim ds = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, dic)
        Me.AddDataSet("ds", dic)

        For Each r1 As DataRow In ds.Tables("gstreg").Select
            For Each str1 As String In New String() {"iamt", "camt", "samt", "csamt", "txval"}
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

    Public Overrides Function GenerateParamsOutput(dataKey As String, Params As List(Of clsSQLParam)) As clsProcOutput
        Dim GstRegID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@gstregid", Params))
        Dim ReturnPeriodID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@ReturnPeriodID", Params))
        Dim CompanyID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@companyid", Params))
        Dim ReturnTaskID As String = myContext.SQL.ParamValue("@ApiTaskID", Params)
        Dim queueName = myContext.Controller.CalcQueueName
        Dim InfoJsonAPITaskID As String = myContext.SQL.ParamValue("@UploadType", Params)

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
                Dim oProc As New clsGSTNEwayBill(myContext)
                Select Case dataKey.Trim.ToLower
                    Case "payloadstatus"
                        oRet = QueueTaskProvider.GetTaskStatus(myContext, ReturnTaskID)
                    Case "generate"
                        Dim filename As String = myContext.Controller.CalcRLSId.ToString & $"-EWB-{dataKey}-" & Replace(Now.ToLongTimeString, ":", "").Replace(" ", "") & ".zip"
                        Dim dicParams As New Dictionary(Of String, String)
                        dicParams("filename") = filename
                        oRet = TaskProviderFactory.CheckAddTask(Of List(Of clsSQLParam))(myContext, "ewb", dataKey, CompanyID, Params, queueName, dicParams)
                        If oRet.Success Then oRet.Description = oRet.GetCalcRows("task")(0)("apitaskid").ToString
                    Case Else
                        Dim filename As String = myContext.Controller.CalcRLSId.ToString & $"-EWB-{dataKey}-" & Replace(Now.ToLongTimeString, ":", "").Replace(" ", "") & ".xlsx"
                        Dim dicParams As New Dictionary(Of String, String)
                        dicParams("filename") = filename
                        oRet = TaskProviderFactory.CheckAddTask(Of List(Of clsSQLParam))(myContext, "ewb", dataKey, CompanyID, Params, queueName, dicParams)
                        If oRet.Success Then oRet.Description = oRet.GetCalcRows("task")(0)("apitaskid").ToString
                End Select
            End If
        End If
        Return oRet
    End Function

    Public Overrides Function VSave() As Boolean
        VSave = False
        If Me.Validate Then
            VSave = True
        End If
    End Function
End Class
