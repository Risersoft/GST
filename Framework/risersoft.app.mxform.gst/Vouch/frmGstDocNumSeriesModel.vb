Imports GSTN.API.Library.Models.GstNirvana
Imports risersoft.app.mxent
Imports risersoft.shared
Imports risersoft.shared.cloud
Imports risersoft.shared.cloud.common
Imports System.Configuration
Imports System.Runtime.Serialization

<DataContract>
Public Class frmGstDocNumSeriesModel
    Inherits clsFormDataModel
    Dim myViewInvoiceSeries As clsViewModel

    Protected Overrides Sub InitViews()
        myView = Me.GetViewModel("GSTReg")
        myViewInvoiceSeries = Me.GetViewModel("Series")
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
    End Sub

    Public Overrides Function PrepForm(oView As clsViewModel, ByVal prepMode As EnumfrmMode, ByVal prepIDX As String, Optional ByVal strXML As String = "") As Boolean
        Dim Sql, str1 As String

        Me.FormPrepared = False
        If prepMode = EnumfrmMode.acAddM Then prepIDX = 0
        Sql = "Select CompanyID,CompName,PANNum from Company Where CompanyID = " & prepIDX
        Me.InitData(Sql, "returnperiodid", oView, prepMode, prepIDX, strXML)

        Dim ReturnPeriodID As Integer = myFuncs2.ValidatedPostPeriodID(myContext, "ReturnPeriodID", myUtils.cValTN(Me.vBag("returnperiodid")), Me.dsCombo.Tables("postperiod"))

        Sql = "Select GSTRegID, Descrip, GSTIN from GstListGSTReg() where  CompanyID = " & frmIDX & ""
        myView.MainGrid.MainConf("formatxml") = "<COL KEY=""Descrip"" CAPTION=""State""/>"
        myView.MainGrid.QuickConf(Sql, True, "2-2", True)
        str1 = "<BAND INDEX=""0"" TABLE=""GSTReg"" IDFIELD=""GSTRegID""><COL KEY="" Descrip, GSTIN ""/></BAND>"
        myView.MainGrid.PrepEdit(str1, EnumEditType.acCommandOnly)

        Sql = "Select InvoiceNumberSeriesId, GSTRegID, ReturnperiodID, InvoiceNumberTemplateId, NumFrom, NumTo, TotCount, CancelledCount, IssuedCount, MissingCount from GstDocNumSeries"
        Me.myViewInvoiceSeries.MainGrid.BindGridData(myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql), 0)
        Me.myViewInvoiceSeries.MainGrid.QuickConf("", True, "2-1-1-1-1-1-1", True)
        str1 = $"<BAND INDEX=""0"" TABLE=""GstDocNumSeries"" IDFIELD=""InvoiceNumberSeriesId""><COL KEY="" NumFrom, NumTo""/><COL KEY=""InvoiceNumberTemplateId"" CAPTION=""Template Number"" LOOKUPSQL = ""Select InvoiceNumberTemplateId, Descrip FROM GstListDocNumTemplate() where companyid={frmIDX}""/><COL KEY=""TotCount"" CAPTION=""Total Count""/><COL KEY=""CancelledCount"" CAPTION=""Cancelled""/><COL KEY=""IssuedCount"" CAPTION=""Issued Count""/><COL KEY=""MissingCount"" CAPTION=""Missing Count""/></BAND>"
        myViewInvoiceSeries.MainGrid.PrepEdit(str1, EnumEditType.acCommandAndEdit)
        Me.myViewInvoiceSeries.MainGrid.myDV.RowFilter = "0=1"


        Me.FormPrepared = True

        Return Me.FormPrepared
    End Function

    Public Overrides Function Validate() As Boolean
        Me.InitError()
        Return Me.CanSave
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
                        oRet.Data = Me.GenerateData(CompanyID, ReturnPeriodID)
                        Dim provider As New clsDBUserFilterProvider(myContext, False)
                        provider.AddUpdValueRow("ReturnPeriodID", ReturnPeriodID)
                        provider.SaveDBUserFilter()
                        oRet.Message = "Return Period change successful"
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
                    Case "auto"
                        Dim oRet2 = myFuncs2.AutoGenerateGstDocNumSeries(myContext, GstRegID, ReturnPeriodID)
                        Dim oProc As New clsPOCOConverter(myContext)
                        Dim dt1 = oProc.GenerateTable(GetType(DocNumSeriesModel), oRet2.Result)
                        oRet.Data = myUtils.MakeDSfromTable(dt1, False)
                    Case "recalculate"
                        oRet = myFuncs2.Recalculate(myContext, GstRegID, ReturnPeriodID)
                End Select
            End If
        End If

        Return oRet
    End Function

    Public Overrides Function VSave() As Boolean
        VSave = False
        If Me.Validate Then
            If Me.CanSave Then
                Dim DocNumTemplateDescrip As String = ""
                Try
                    myContext.Provider.dbConn.BeginTransaction(myContext, Me.Name, Me.frmMode.ToString, "CompanyID", frmIDX)
                    frmMode = EnumfrmMode.acEditM
                    myViewInvoiceSeries.MainGrid.SaveChanges()
                    myContext.Provider.dbConn.CommitTransaction(DocNumTemplateDescrip, frmIDX)
                    VSave = True

                Catch e As Exception
                    myContext.Provider.dbConn.RollBackTransaction(DocNumTemplateDescrip, e.Message)
                    Me.AddException("", e)
                End Try
            End If
        End If
    End Function

End Class
