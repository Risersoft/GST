Imports System.Configuration
Imports System.IO
Imports System.Runtime.Serialization
Imports GSTN.API.Library.Models.GstNirvana
Imports Newtonsoft.Json
Imports risersoft.API.GSTN
Imports risersoft.app.mxent
Imports risersoft.shared
Imports risersoft.shared.cloud
Imports risersoft.shared.cloud.common
Imports risersoft.shared.dal

<DataContract>
Public Class frmGstImportVouchModel
    Inherits clsFormDataModel
    Dim myVueSummary, myVueTaxSummary, myVueError As clsViewModel

    Protected Overrides Sub InitViews()
        myView = Me.GetViewModel("File")
        myVueSummary = Me.GetViewModel("Summary")
        myVueTaxSummary = Me.GetViewModel("TaxSummary")
        myVueError = Me.GetViewModel("Error")
    End Sub

    Public Sub New(context As IProviderContext)
        MyBase.New(context)
        Me.InitViews()
        Me.InitForm()

    End Sub

    Private Sub InitForm()
        Dim Sql As String = myFuncsBase.CodeWordSQL("Validation", "DocType", "")
        Me.AddLookupField("DocType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql), "DocType").TableName)

    End Sub

    Public Overrides Function PrepForm(oView As clsViewModel, ByVal prepMode As EnumfrmMode, ByVal prepIDX As String, Optional ByVal strXML As String = "") As Boolean
        Dim sql, str1 As String, dic As New clsCollecString(Of String)
        Me.FormPrepared = False
        If prepMode = EnumfrmMode.acEditM Then
            Dim oRet As clsProcOutput = Me.GetRowLock(prepMode, "ImportFileID", prepIDX)
            If oRet.Success Then
                sql = "Select * from ImportFile where ImportFileID = '" & myUtils.cStrTN(prepIDX) & "'"
                Me.InitData(sql, "", oView, prepMode, prepIDX, strXML)
            End If
        End If


        Me.BindDataTable(myUtils.cStrTN(prepIDX), myUtils.cStrTN(myRow("DocType")))

        myView.MainGrid.MainConf("formatxml") = "<COL KEY=""FileName"" CAPTION=""File Name""/><COL KEY=""FileTime"" CAPTION=""File Date""/><COL KEY=""FileSizeKB"" CAPTION=""File Size(KBs)""/><COL KEY=""DataRowCount"" CAPTION=""Row Count""/><COL KEY=""DataRecordCount"" CAPTION=""Record Count""/><COL KEY=""DataErrorCount"" CAPTION=""Error Count""/><COL KEY=""LastRunTime"" CAPTION=""Last Run Time""/>"
        myView.MainGrid.BindGridData(Me.dsForm, 1)
        myView.MainGrid.QuickConf("", True, "2-1-1-0.5-0.5-0.5-1")

        myVueError.MainGrid.MainConf("formatxml") = "<COL KEY=""VouchNum"" CAPTION=""Number""/><COL KEY=""Dated"" CAPTION=""Date""/><COL KEY=""TotalAmount"" CAPTION=""Value""/><COL KEY=""CTIN"" CAPTION=""Counter Party""/><COL KEY=""ErrorCSV"" CAPTION=""Error""/><COL KEY=""WarningCSV"" CAPTION=""Warning""/>"
        myVueError.MainGrid.BindGridData(Me.dsForm, 2)
        myVueError.MainGrid.QuickConf("", True, "2-1-1-2-1-1")

        myVueSummary.MainGrid.MainConf("formatxml") = "<COL KEY ="" GSTIN, State, ret_pd, RowCount, RecordCount, ErrorCount""/><COL KEY=""ret_pd"" CAPTION=""Return Period""/><COL KEY=""RowCount"" CAPTION=""Total Row Count""/><COL KEY=""RecordCount"" CAPTION=""Total Record Count""/><COL KEY=""ErrorCount"" CAPTION=""Total Error Count""/>"
        myVueSummary.MainGrid.BindGridData(Me.dsForm, 3)
        myVueSummary.MainGrid.QuickConf("", True, "0.8-2-1-1-1-1-0-0-0-0-0")

        myVueTaxSummary.MainGrid.MainConf("formatxml") = "<COL KEY ="" GSTIN, State, ret_pd, TXVAL, IAMT, CAMT, SAMT""/><COL KEY=""ret_pd"" CAPTION=""Return Period""/><COL KEY=""txval"" CAPTION=""TXVAL""/><COL KEY=""IAMT"" CAPTION=""IGST""/><COL KEY=""CAMT"" CAPTION=""CGST""/><COL KEY=""SAMT"" CAPTION=""SGST""/>"
        myVueTaxSummary.MainGrid.BindGridData(Me.dsForm, 3)
        myVueTaxSummary.MainGrid.QuickConf("", True, "0.8-2-1-0-0-0-1-1-1-1-0")


        Me.FormPrepared = True
        Return Me.FormPrepared
    End Function

    Public Overrides Function Validate() As Boolean
        Me.InitError()

        Return Me.CanSave
    End Function

    Protected Friend Overloads Function GenerateData(ByVal ImportFileID As String, ByVal DocType As String) As DataSet
        Dim dic As New clsCollecString(Of String), oRet As New clsProcOutput
        dic.Add("file", "Select ImportFileID, RootImportFileID, FileName, FileTime, FileSizeKB, DataRowCount, DataRecordCount, DataErrorCount, LastRunTime from ImportFile where ImportFileID = '" & ImportFileID & "' or RootImportFileID ='" & ImportFileID & "'")
        dic.Add("error", String.Format("Select ImportFileVouchID, ImportFileID, VouchNum, Dated, TotalAmount, CTIN, ErrorCSV, WarningCSV from ImportFileVouch where ImportFileID = '" & ImportFileID & "'"))

        Dim Type = DocType

        If Type.ToLower = "v" OrElse Type.ToLower = "vendor" Then
            dic.Add("count", String.Format("select Count(*) from vendor where ImportFileID= '" & ImportFileID & "'"))
        ElseIf Type.ToLower = "is" OrElse Type.ToLower = "ip" OrElse Type.ToLower = "gstr1" OrElse Type.ToLower = "gstr6ip" Then
            dic.Add("count", String.Format("Select count(*) from Invoice where importfileid= '" & ImportFileID & "'"))
        ElseIf Type.ToLower = "pc" OrElse Type.ToLower = "pv" Then
            dic.Add("count", String.Format("Select count(*) from GstAdvanceVouch where importfileid= '" & ImportFileID & "'"))
        ElseIf Type.ToLower = "gstr2a" Then
            dic.Add("count", String.Format("Select count(*) from CPInvoice where importfileid= '" & ImportFileID & "'"))
        ElseIf Type.ToLower = "hsn" Then
            dic.Add("count", String.Format("Select count(*) from HSNSac where importfileid= '" & ImportFileID & "'"))
        ElseIf Type.ToLower = "isd" Then
            dic.Add("count", String.Format("Select count(*) from ISDinvoice where importfileid= '" & ImportFileID & "'"))
        ElseIf Type.ToLower = "customer" Then
            dic.Add("count", String.Format("Select count(*) from Customer where importfileid= '" & ImportFileID & "'"))
        ElseIf Type.ToLower = "tp" Then
            dic.Add("count", String.Format("Select count(*) from Campus where importfileid= '" & ImportFileID & "'"))
        ElseIf Type.ToLower = "role" Then
            dic.Add("count", String.Format("Select count(*) from DBRole where importfileid= '" & ImportFileID & "'"))
        ElseIf Type.ToLower = "chl" Then
            dic.Add("count", String.Format("Select count(*) from Challan where importfileid= '" & ImportFileID & "'"))
        ElseIf Type.ToLower = "ewb" Then
            dic.Add("count", String.Format("Select count(*) from Ewaybill where importfileid= '" & ImportFileID & "'"))
        ElseIf Type.ToLower = "tcs" Then
            dic.Add("count", String.Format("Select count(*) from tcs where importfileid= '" & ImportFileID & "'"))
        ElseIf Type.ToLower = "tds" Then
            dic.Add("count", String.Format("Select count(*) from tds where importfileid= '" & ImportFileID & "'"))
        ElseIf Type.ToLower = "recon" Then
            dic.Add("count", String.Format("Select count(*) from invoice where 0=1"))
        ElseIf Type.ToLower = "defer" Then
            dic.Add("count", String.Format("Select count(*) from invoice where 0=1"))
        End If

        Dim ds = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, dic)

        Dim sql As String = "Select SummaryJson from ImportFile where ImportFileID = '" & ImportFileID & "'"
        Dim dt2 = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql).Tables(0)
        If myUtils.cStrTN(dt2.Rows(0)("SummaryJson")).Trim.Length > 0 Then
            Dim ImpInfo = JsonConvert.DeserializeObject(Of GstImportInfo)(dt2.Rows(0)("SummaryJson"))
            Dim oProc As New clsPOCOConverter(myContext)
            Dim dt1 = oProc.GenerateTable(ImpInfo.GSTIN_List)
            myUtils.AddTable(ds, dt1, dt1.TableName)
        Else
            Dim dt1 = New DataTable("Summary")
            myUtils.AddTable(ds, dt1, dt1.TableName)
        End If

        Return ds

    End Function

    Private Sub BindDataTable(ByVal ImportFileID As String, ByVal DocType As String)
        Dim ds = Me.GenerateData(ImportFileID, DocType)
        For i As Integer = 0 To ds.Tables.Count - 1
            myUtils.AddTable(Me.dsForm, ds, ds.Tables(i).TableName, i)
        Next
    End Sub


    Public Overrides Function GenerateParamsOutput(dataKey As String, Params As List(Of clsSQLParam)) As clsProcOutput
        Dim ReturnTaskID As String = myContext.SQL.ParamValue("@ApiTaskID", Params)
        Dim ImportFileID As String = myContext.SQL.ParamValue("@ImportFileID", Params)
        Dim DocType As String = myContext.SQL.ParamValue("@DocType", Params)
        Dim queueName = myContext.Controller.CalcQueueName
        Dim oRet As New clsProcOutput


        If myUtils.IsInList(dataKey, "payloadstatus") AndAlso Not String.IsNullOrWhiteSpace(ReturnTaskID) Then
            oRet = QueueTaskProvider.GetTaskStatus(myContext, ReturnTaskID)
        Else
            Select Case dataKey.Trim.ToLower
                Case "executeagain"
                    Dim sql2 = "select top(1) * from ApiTask where ImportFileID = '" & ImportFileID & "' order by submittedon desc"
                    Dim dt1 As DataTable = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql2).Tables(0)
                    Dim dicParams As New Dictionary(Of String, String)
                    oRet = TaskProviderFactory.ExecuteAgain(myContext, dt1.Rows(0)("ApiTaskID").ToString, queueName)
                    If oRet.Success Then oRet.Description = oRet.GetCalcRows("task")(0)("apitaskid").ToString

                Case "delete"
                    Dim dicParams As New Dictionary(Of String, String) From {{"ImportFileID", ImportFileID.ToString}}
                    queueName = myContext.Controller.CalcQueueName
                    oRet = TaskProviderFactory.CheckAddTask(myContext, "import", "del", 0, Params, queueName, dicParams)
                    If oRet.Success Then oRet.Description = oRet.GetCalcRows("task")(0)("apitaskid").ToString

                Case "getrecordcount"
                    oRet.Data = Me.GenerateData(ImportFileID, DocType)

            End Select

        End If

        Return oRet
    End Function

End Class

