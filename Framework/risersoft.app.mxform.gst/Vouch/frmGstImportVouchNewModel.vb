Imports System.IO
Imports System.Runtime.Serialization
Imports Newtonsoft.Json
Imports risersoft.API.GSTN
Imports risersoft.app.mxent
Imports risersoft.shared
Imports risersoft.shared.dal
Imports risersoft.shared.cloud
Imports risersoft.shared.cloud.common
Imports System.Configuration

<DataContract>
Public Class frmGstImportVouchNewModel
    Inherits clsFormDataModel


    Public Sub New(context As IProviderContext)
        MyBase.New(context)
        Me.InitViews()
        Me.InitForm()

    End Sub

    Private Sub InitForm()

    End Sub

    Public Overrides Function PrepForm(oView As clsViewModel, ByVal prepMode As EnumfrmMode, ByVal prepIDX As String, Optional ByVal strXML As String = "") As Boolean
        Dim sql As String
        Me.FormPrepared = False
        If prepMode = EnumfrmMode.acAddM Then prepIDX = Guid.Empty.ToString
        sql = "Select * from ImportFile where ImportFileID = '" & prepIDX & "'"
        Me.InitData(sql, "DocType,Action", oView, prepMode, prepIDX, strXML)

        If Me.vBag("Action") = "Import" Then
            sql = "select codeword, descripshort as Descrip,codeclass,codetype,tag from codewords  WHERE (codeclass='Validation') AND (codetype='DocType') order by codeword"
            Me.AddLookupField("DocType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "DocType").TableName)
        ElseIf Me.vBag("Action") = "Search" Then
            Dim vlistsearch As New clsValueList
            vlistsearch.Add("GSTIN", "GSTIN")
            vlistsearch.Add("PAN", "PAN")
            vlistsearch.Add("Track", "Vendor Return Filling Status Report")
            Me.ValueLists.Add("Search", vlistsearch)
            Me.AddLookupField("DocType1", "Search")
        ElseIf Me.vBag("Action") = "ConvertJson" Then
            Dim vlistconvert As New clsValueList
            vlistconvert.Add("GSTR1", "GSTR1")
            vlistconvert.Add("GSTR2A", "GSTR2A")
            Me.ValueLists.Add("Convert", vlistconvert)
            Me.AddLookupField("DocType1", "Convert")
        End If


        Me.FormPrepared = True
        Return Me.FormPrepared
    End Function

    Public Overrides Function Validate() As Boolean
        Me.InitError()
        Return Me.CanSave
    End Function

    Public Overrides Function GenerateParamsOutput(dataKey As String, Params As List(Of clsSQLParam)) As clsProcOutput
        Dim DocType As String = myContext.SQL.ParamValue("@DocType", Params)
        Dim Action As String = myContext.SQL.ParamValue("@Action", Params)
        Dim serverPath As String = Uri.UnescapeDataString(myContext.SQL.ParamValue("@serverPath", Params))
        Dim Length As String = myContext.SQL.ParamValue("@Length", Params)
        Dim LastWriteTime As DateTime
        DateTime.TryParse(myContext.SQL.ParamValue("@LastWriteTime", Params), LastWriteTime)
        Dim ReturnTaskID As String = myContext.SQL.ParamValue("@ApiTaskID", Params)
        Dim ImportFileID As String = myContext.SQL.ParamValue("@ImportFileID", Params)
        Dim pImportFileID As String = myContext.SQL.ParamValue("@pImportFileID", Params)
        Dim queueName = myContext.Controller.CalcQueueName
        Dim oRet As New clsProcOutput

        If myUtils.IsInList(dataKey, "payloadstatus") AndAlso Not String.IsNullOrWhiteSpace(ReturnTaskID) Then
            oRet = QueueTaskProvider.GetTaskStatus(myContext, ReturnTaskID)
        Else
            Select Case dataKey.Trim.ToLower
                Case "execute"

                    Dim dic As New Dictionary(Of String, String) From {{"path", serverPath}}
                    Dim dicParams As New Dictionary(Of String, String) From {{"InfoJson", JsonConvert.SerializeObject(dic)}, {"ImportFileID", ImportFileID}}

                    If myUtils.IsInList(Action, "convertjson", "search") Then
                        Dim flname As String = myContext.Controller.CalcRLSId.ToString & "-" & Action & "-" & Replace(Now.ToLongTimeString, ":", "").Replace(" ", "") & ".xlsx"
                        dicParams("filename") = flname
                    Else
                        Dim nr = mxform.myFuncs.CreateImportFile(myContext, serverPath, pImportFileID, ImportFileID, Length, New DateTimeOffset(LastWriteTime).ToUnixTimeMilliseconds, DocType)
                    End If

                    Dim rTask As DataRow = TaskProviderFactory.CreateApiTask(myContext.Provider, Action, DocType, 0, dicParams)
                    oRet = TaskProviderFactory.Enqueue(myContext.Provider, rTask, queueName)
                    If oRet.Success Then oRet.Description = rTask("apitaskid").ToString
                    oRet.AddDataRow("task", rTask)

            End Select

        End If

        Return oRet
    End Function

End Class
