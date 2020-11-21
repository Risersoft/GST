Imports System.Reflection
Imports risersoft.app.mxent
Imports risersoft.shared
Imports System.Globalization
Imports risersoft.API.GSTN
Imports risersoft.API.GSTN.Auth
Imports risersoft.shared.Extensions
Imports risersoft.shared.cloud

Public MustInherit Class clsGSTNTrackedAPIBase
    Protected Friend myContext As IProviderContext
    Public oMaster As clsMasterDataFICO
    Public API, ReturnCode As String
    Protected Friend DefaultGSP As Func(Of String, String)
    Public ForceEnv As String = ""
    Protected objLock As New Object
    Public Sub New(context As IProviderContext, API As String, ReturnCode As String)
        myContext = context
        oMaster = New clsMasterDataFICO(myContext)
        Me.API = API
        Me.ReturnCode = ReturnCode
        Me.DefaultGSP = Function(x)
                            Return "GSTN"
                        End Function

    End Sub


    Public Function GetFromTime(GstRegID As Integer, ReturnPeriodID As Integer, TransType As String, TransSubType As String) As String
        Dim from_time As String = "", strf1 As String
        If Not String.IsNullOrEmpty(TransSubType) Then strf1 = "TransSubType='" & TransSubType & "'"
        Dim rTrans As DataRow = Me.GetLastTransactionRow(GstRegID, ReturnPeriodID, TransType, myUtils.CombineWhere(False, strf1, "responseerror is null"))
        If Not rTrans Is Nothing Then from_time = Format(CDate(rTrans("dated")).AddDays(-1), "dd-MM-yyyy")
        Return from_time
    End Function
    Public Function GetLastTransactionRow(GstRegID As Integer, ReturnPeriodID As Integer, TransType As String, strFilter As String) As DataRow
        SyncLock objLock
            Dim strf1 As String = myUtils.CombineWhere(False, "ReturnKey='" & Me.ReturnCode & "'", "transtype='" & TransType & "'", "GstRegID=" & GstRegID, "ReturnPeriodid=" & ReturnPeriodID, "API='" & Me.API & "'", strFilter)
            Dim sql As String = "select top 1 * from GSTNTransaction where  " & strf1 & "  order by dated desc"
            Dim dt1 As DataTable = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql).Tables(0)
            If dt1.Rows.Count > 0 Then Return dt1.Rows(0)
        End SyncLock
    End Function
    Public Function SaveTransaction(GstRegID As Integer, ReturnPeriodID As Integer, TransType As String, TransSubType As String, dicParams As Dictionary(Of String, String), Optional ApiTaskID? As Guid = Nothing) As DataRow
        SyncLock objLock
            Dim sql As String = "Select * from GSTNTransaction where 0=1"
            Dim dt1 As DataTable = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql).Tables(0)
            Dim nr As DataRow = myContext.Tables.AddNewRow(dt1)
            nr("gstregid") = If(GstRegID > 0, GstRegID, DBNull.Value)
            nr("returnperiodid") = If(ReturnPeriodID > 0, ReturnPeriodID, DBNull.Value)
            nr("transtype") = TransType
            nr("TransSubType") = TransSubType
            nr("dated") = TaskProviderFactory.FindLocalTime
            nr("API") = Me.API
            nr("gsp") = Me.GetGspConfig().Item1
            nr("userid") = myContext.Police.UniqueUserID
            nr("returnkey") = ReturnCode
            nr("apitaskid") = IIf(ApiTaskID IsNot Nothing AndAlso ApiTaskID.Value <> Guid.Empty, ApiTaskID, myContext.Controller.TaskInfo.ApiTaskID)
            nr("dbschedtaskid") = myContext.Controller.TaskInfo.DBSchedTaskID
            For Each kvp In dicParams
                If nr.Table.Columns.Contains(kvp.Key) Then nr(kvp.Key) = kvp.Value
            Next
            If dicParams.ContainsKey("txn") Then nr("gstntransactionid") = dicParams("txn") Else nr("gstntransactionid") = System.Guid.NewGuid.ToString
            myContext.Provider.objSQLHelper.SaveResults(dt1, sql)
            Return nr
        End SyncLock
    End Function
    Public Overridable Function PopulateDataset(Of TModel)(Model As TModel, ds As DataSet) As DataSet
        For Each prop In Model.GetType.GetProperties
            Dim objValue = prop.GetValue(Model)
            Trace.WriteLine("Property Name:" & prop.Name)
            If (Not objValue Is Nothing) Then
                If ds.Tables.Contains(prop.Name) Then
                    If (prop.PropertyType.GetInterfaces.Contains(GetType(IList))) Then
                        Me.PopulateDataTable(objValue, prop, ds.Tables(prop.Name))
                    ElseIf Not myUtils.AttributableTypes.Contains(prop.PropertyType) Then
                        Me.PopulateDataTable(objValue, ds.Tables(prop.Name))
                    End If
                End If
            End If
        Next
        Return ds

    End Function
    Public Overridable Sub PopulateDataTable(Of TModel)(Model As TModel, dt As DataTable)
        For Each prop In Model.GetType.GetProperties
            Trace.WriteLine("Property Name:" & prop.Name)
            If (prop.PropertyType.GetInterfaces.Contains(GetType(IList))) Then
                Me.PopulateDataTable(prop.GetValue(Model), prop, dt)
            ElseIf Not myUtils.AttributableTypes.Contains(prop.PropertyType) Then
                Me.PopulateDataTable(prop.GetValue(Model), dt)
            End If
        Next

    End Sub
    Public Function PopulateDataTable(Of TModel)(objValue As TModel, objInfo As PropertyInfo, dtDest As DataTable) As List(Of DataRow)
        Dim lst As New List(Of DataRow)
        If Not objValue Is Nothing Then
            Dim oProc As New clsPOCOConverter(myContext), dt As DataTable
            If Not dtDest.Columns.Contains("num") Then dtDest.Columns.Add("num", GetType(Integer))
            Dim t2 As Type = objInfo.PropertyType
            If TypeOf objValue Is IList Then
                Dim t3 = t2.GetGenericArguments()(0)
                dt = oProc.GenerateTable(t3, objValue)
            Else
                dt = oProc.GenerateRow(objValue, False, "").Table
            End If
            For Each col As DataColumn In dt.Columns
                If dtDest.Columns.Contains(col.ColumnName) AndAlso col.DataType Is GetType(String) AndAlso dtDest.Columns(col.ColumnName).DataType Is GetType(DateTime) Then
                    For Each r1 As DataRow In dt.Select
                        If Not String.IsNullOrEmpty(myUtils.cStrTN(r1(col.ColumnName))) Then
                            r1(col.ColumnName) = Format(DateTime.ParseExact(myUtils.cStrTN(r1(col.ColumnName)), "dd-MM-yyyy", CultureInfo.InvariantCulture), "dd-MMM-yyyy")
                        End If

                    Next
                End If
            Next
            lst = myUtils.CopyRows(dt.Select, dtDest,, False)
        End If
        Return lst
    End Function
    Protected Friend Sub SetPropertyValue(Of TModel)(Model As TModel, prop As PropertyInfo, t2 As Type, dt1 As DataTable)
        Dim oProc As New clsPOCOConverter(myContext)
        If (prop.PropertyType.GetInterfaces.Contains(GetType(IList))) Then
            Dim t3 = t2.GetGenericArguments()(0)
            Dim lst = oProc.GenerateObjectList(t3, dt1)
            If lst.Count > 0 Then
                prop.SetValue(Model, lst)
            End If
        ElseIf dt1.Rows.Count > 0 Then
            Dim obj = oProc.GenerateObject(t2, dt1.Rows(0))
            prop.SetValue(Model, obj)
        End If

    End Sub
    Public Overridable Sub PopulateObject(Of TModel)(Model As TModel, ds As DataSet)
        For Each dt1 As DataTable In ds.Tables
            Trace.WriteLine("Payload: Table name= " & dt1.TableName & ", Row count=" & dt1.Rows.Count)
            Dim prop As PropertyInfo = Model.GetType.FindProperty(dt1.TableName)
            If (Not prop Is Nothing) Then
                Dim t2 As Type = prop.PropertyType
                Me.SetPropertyValue(Model, prop, t2, dt1)
            End If
        Next

    End Sub



    Public Function PartySubTypeRow(GSTIN As String, dt As DataTable) As DataRow
        Dim rr() As DataRow = dt.Select("gstin='" & GSTIN & "'")
        If rr.Length > 0 Then Return rr(0)
    End Function
    Public Function SaveToken(userid As String, gstregid As Integer, token As TokenResponseModel, otp As String) As DataRow


        If (Not token Is Nothing) AndAlso (Not String.IsNullOrEmpty(token.auth_token)) Then
            SyncLock objLock
                'https://groups.google.com/forum/#!topic/gst-suvidha-provider-gsp-discussion-group/8tkccf8Artk
                If token.expiry > 360 Then token.expiry = 360       'Token is expiring after 360 mins.

                Dim sql As String = "select * from gstntoken where 0=1"
                Dim dt1 As DataTable = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql).Tables(0)
                Dim nr As DataRow = myContext.Tables.AddNewRow(dt1)
                nr("gstntokenid") = System.Guid.NewGuid.ToString("D")
                nr("userid") = userid
                nr("gstregid") = If(gstregid > 0, gstregid, DBNull.Value)
                nr("authtoken") = token.auth_token
                nr("sek") = token.decryptSek
                nr("obtainedon") = TaskProviderFactory.FindLocalTime
                nr("expiry") = token.expiry
                nr("otp") = otp
                nr("expiryon") = TaskProviderFactory.FindLocalTime.AddMinutes(token.expiry)
                nr("gsp") = Me.GetGspConfig().Item1
                nr("API") = Me.API
                nr("ipaddr") = GSTNConstants.publicip
                myContext.Provider.objSQLHelper.SaveResults(dt1, sql)

                Return nr


            End SyncLock
        End If
    End Function
    Public Function GetActiveToken(userid As String, GstRegID As Integer, BufferMins As Integer) As DataRow
        SyncLock objLock
            Dim Dated1 As DateTime = TaskProviderFactory.FindLocalTime.AddMinutes(BufferMins)
            Dim Dated2 As DateTime = TaskProviderFactory.FindLocalTime.AddMinutes(-5)
            Dim config = Me.GetGspConfig()
            Dim sql As String = String.Format("select * from gstntoken where /*userid='{0}' and*/ isnull(gstregid,0)={1} 
            and (expiryon>'{2}' or obtainedon>'{3}') and gsp='{4}' and /*ipaddr='{5}' and*/ API='{6}' and (clearedon is null) order by obtainedon desc",
            userid, GstRegID, Format(Dated1, "dd-MMM-yyyy HH:mm"), Format(Dated2, "dd-MMM-yyyy HH:mm"), config.Item1, GSTNConstants.publicip, Me.API)
            Dim dt1 As DataTable = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql).Tables(0)
            Dim r1 As DataRow = If(dt1.Rows.Count > 0, dt1.Rows(0), Nothing)
            Return r1
        End SyncLock

    End Function

    Public Function GetActiveToken(userid As String, GstRegID As Integer) As DataRow
        Return Me.GetActiveToken(userid, GstRegID, 60)
    End Function
    Public MustOverride Function GetAuthClientFromToken(r1 As DataRow, rGstReg As DataRow) As IGSTNAuthProvider
    Public MustOverride Function CheckToken(GstRegID As Integer) As clsProcOutput

    Protected Friend Function GetGspConfig() As Tuple(Of String, String)
        Dim Gsp As String = myContext.Provider.GetSystemOption("gsp").Result
        Dim Env As String = myUtils.Coalesce(ForceEnv, myContext.Provider.GetSystemOption("env").Result, "test")

        If String.IsNullOrEmpty(Gsp) Then Gsp = Me.DefaultGSP(Env)
        Return New Tuple(Of String, String)(Gsp, Env)
    End Function




End Class
