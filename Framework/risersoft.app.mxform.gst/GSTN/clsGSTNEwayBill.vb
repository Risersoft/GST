Imports risersoft.shared
Imports risersoft.API.GSTN
Imports risersoft.API.GSTN.Auth
Imports GSTN.API.Library.Models.EWB
Imports GSTN.API.Library.Clients
Imports System.Net
Imports GSTN.API.Library.Models.GstNirvana
Imports risersoft.app.mxent
Imports risersoft.app.reports.gst
Imports risersoft.shared.cloud

Public Class clsGSTNEwayBill
    Inherits clsGSTNReturnBase

    Public Sub New(context As IProviderContext)
        MyBase.New(context, "EWB", "")
        Me.API = "EWB"
        Me.DefaultGSP = Function(x)
                            Return "ASPONE"
                        End Function
    End Sub

    Public Overrides Function CheckToken(GstRegID As Integer) As clsProcOutput
        Dim oRet As New clsProcOutput
        Dim nr = Me.GetActiveToken(myContext.Police.UniqueUserID, GstRegID)
        If nr Is Nothing Then
            Dim Model = Me.GetToken(GstRegID)
            If (Model Is Nothing) OrElse (Model.Data Is Nothing) Then
                oRet.AddError("Cannot get token")
            Else
                nr = Me.GetActiveToken(myContext.Police.UniqueUserID, GstRegID)
                oRet.AddDataRow("token", nr)
            End If
        Else
            oRet.AddDataRow("token", nr)
        End If
        Return oRet

    End Function
    Public Overloads Function GetToken(GstRegID As Integer) As GSTNResult(Of TokenResponseModel)
        Dim config = Me.GetGspConfig()
        Dim rGstReg As DataRow = oMaster.GstRegRow(GstRegID)
        Dim client = Me.GetAuthClient(config.Item1, rGstReg("gstin"), myUtils.cStrTN(rGstReg("ewbusername")), config.Item2, myUtils.cStrTN(rGstReg("ewbpassword")))
        Dim model As GSTNResult(Of TokenResponseModel) = client.RequestToken("")
        Me.SaveTransaction(GstRegID, 0, "TOKEN", "", client.dicParams)
        If (model IsNot Nothing) AndAlso (model.Data IsNot Nothing) Then
            Try
                model.Data.decryptSek = Convert.ToBase64String(client.DecryptedKey)
            Catch ex As Exception
                myContext.Logger.logInformation("DecryptSEK not found")
            End Try
            Dim nr = Me.SaveToken(myContext.Police.UniqueUserID, GstRegID, model.Data, "")
        End If
        Return model

    End Function
    Protected Friend Overrides Function GetAuthClient(gsp As String, gstin As String, userid As String, env As String, IPAddr As String) As IGSTNAuthProvider
        Dim client As IGSTNAuthProvider
        Select Case gsp.Trim.ToLower
            Case "kpmg"
                client = New KpmgEWBAuthClient(gstin.Trim, userid.Trim, IPAddr, env.Trim)
            Case Else
                client = New AspOneEWBAuthClient(gstin.Trim, userid.Trim, IPAddr, env.Trim)
        End Select
        client.Init()
        Return client
    End Function
    Protected Friend Function GetClient(provider As IGSTNAuthProvider, gstin As String) As EWBApiClientBase
        Dim client As EWBApiClientBase

        Dim config = Me.GetGspConfig()

        Select Case config.Item1.Trim.ToLower
            Case "kpmg"
                client = New KpmgEWBApiClient(provider, gstin)
            Case Else
                client = New AspOneEWBApiClient(provider, gstin)
        End Select
        Return client
    End Function

    Public Overrides Function GetAuthClientFromToken(r1 As DataRow, rGstReg As DataRow) As IGSTNAuthProvider
        Dim token As New TokenResponseModel
        If Not r1 Is Nothing Then
            token.auth_token = r1("authtoken")
            token.expiry = r1("expiry")

            Dim config = Me.GetGspConfig()
            Dim client = Me.GetAuthClient(config.Item1, rGstReg("gstin"), rGstReg("ewbusername"), config.Item2, rGstReg("ewbpassword"))
            client.token = token
            Try
                client.DecryptedKey = Convert.FromBase64String(r1("sek"))
            Catch ex As Exception
                myContext.Logger.logInformation(ex.Message)
            End Try

            Return client
        End If
    End Function
    Public Function stringValue(obj As Object) As String
        Dim str1 As String
        If TypeOf obj Is DateTime Then
            'https://stackoverflow.com/questions/15893339/check-if-date-time-string-contains-time
            If CType(obj, DateTime).TimeOfDay.TotalSeconds = 0 Then
                If myUtils.NullNot(obj) Then str1 = "" Else str1 = Format(obj, "dd/MM/yyyy")
            Else
                str1 = Format(obj, "dd/MM/yyyy hh:mm:ss tt")
            End If
            str1 = str1.Replace("-", "/")
        Else
            str1 = obj.ToString
        End If
        Return str1
    End Function
    Public Overridable Function DateFromString(str1 As String) As DateTime
        Dim dated = DateTime.ParseExact(str1, New String() {"dd/MM/yyyy", "dd/MM/yyyy hh:mm:ss tt", "dd-MM-yyyy", "dd-MM-yyyy hh:mm:ss tt"}, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None)
        Return dated
    End Function
    Public Function Generate(GstRegID As Integer, EwayBillID As Integer) As GSTNResult(Of EWBPostResponseInfo)
        Dim output As New GSTNResult(Of EWBPostResponseInfo)

        Dim sql2 = $"update ewaybill set lastvehicleid = (select top 1 ewaybillvehicleid from ewaybillvehicle as ebv where ebv.ewaybillid = ewaybill.ewaybillid
                    and ebv.tenantid = ewaybill.tenantid order by createdon desc) where lastvehicleid is null and ewaybillid = {EwayBillID}"
        myContext.Provider.objSQLHelper.ExecuteNonQuery(CommandType.Text, sql2)

        Dim sql As String = "select * from gstlistewbitem() where ewaybillid=" & EwayBillID
        Dim dt1 As DataTable = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql).Tables(0)
        Dim oProc As New clsPOCOConverter(myContext)
        oProc.StringValue = AddressOf stringValue
        If dt1.Rows.Count > 0 Then
            Try
                Dim obj = oProc.GenerateObject(Of GenerateEWBInfo)(dt1)
                obj.totalValue = myUtils.cValTN(dt1.Compute("sum(taxableAmount)", ""))
                output = Me.Generate(GstRegID, obj)
                If output.Data Is Nothing Then
                    dt1.Rows(0)("gstn_error_msg") = "Data not found"
                    myContext.Provider.objSQLHelper.SaveResults(dt1, "select ewaybillid, gstn_error_msg from ewaybill where 0=1")
                Else
                    If (output.Data.ewayBillNo > 0) Then
                        dt1.Rows(0)("ewaybillnum") = output.Data.ewayBillNo
                        dt1.Rows(0)("ewaybilldate") = Me.DateFromString(output.Data.ewayBillDate)
                        dt1.Rows(0)("validupto") = If(String.IsNullOrEmpty(output.Data.validUpto), DBNull.Value, Me.DateFromString(output.Data.validUpto))
                        dt1.Rows(0)("gstn_error_msg") = DBNull.Value
                        myContext.Provider.objSQLHelper.SaveResults(dt1, "select ewaybillid, ewaybillnum, ewaybilldate, validupto, gstn_error_msg from ewaybill where 0=1")
                    Else
                        dt1.Rows(0)("gstn_error_msg") = output.Data.errorCodes
                        myContext.Provider.objSQLHelper.SaveResults(dt1, "select ewaybillid, gstn_error_msg from ewaybill where 0=1")
                    End If
                End If
            Catch ex As Exception
                myContext.Logger.logInformation(ex.Message & ":" & ex.StackTrace)
                dt1.Rows(0)("gstn_error_msg") = ex.Message
                myContext.Provider.objSQLHelper.SaveResults(dt1, "select ewaybillid, gstn_error_msg from ewaybill where 0=1")
            End Try
        End If
        Return output
    End Function

    Public Function Generate(GstRegID As Integer, info As GenerateEWBInfo) As GSTNResult(Of EWBPostResponseInfo)
        Dim output As New GSTNResult(Of EWBPostResponseInfo)
        Dim rGstReg As DataRow = oMaster.GstRegRow(GstRegID)
        Dim oRet = Me.CheckToken(GstRegID)
        If oRet.Success Then
            Dim nr = oRet.GetCalcRows("token")(0)
            Dim client As IGSTNAuthProvider = Me.GetAuthClientFromToken(nr, rGstReg)
            Dim client2 = Me.GetClient(client, rGstReg("gstin"))
            output = client2.Generate(info)
            If client2.dicParams.ContainsKey("ResponsePayload") Then output.Message = client2.dicParams("ResponsePayload")
            Me.SaveTransaction(GstRegID, 0, "GENERATE", "", client2.dicParams)
        Else
            output.Message = oRet.Message
        End If

        Return output
    End Function
    Public Function Cancel(GstRegID As Integer, EwayBillID As Integer) As GSTNResult(Of EWBCancelResponseInfo)
        Dim output As New GSTNResult(Of EWBCancelResponseInfo)
        Dim sql As String = "select * from gstlistewb() where ewaybillid=" & EwayBillID
        Dim dt1 As DataTable = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql).Tables(0)
        Dim oProc As New clsPOCOConverter(myContext)
        oProc.StringValue = AddressOf stringValue
        If dt1.Rows.Count > 0 Then
            Dim obj = oProc.GenerateObject(Of EWBCancelRequestInfo)(dt1)
            output = Me.Cancel(GstRegID, obj)
            If (output.HttpStatusCode = HttpStatusCode.OK) AndAlso String.IsNullOrWhiteSpace(output.Data.errorCodes) Then
                dt1.Rows(0)("canceldate") = Me.DateFromString(output.Data.cancelDate)
                myContext.Provider.objSQLHelper.SaveResults(dt1, "select ewaybillid, canceldate from ewaybill where 0=1")
            End If
        End If
        Return output
    End Function
    Public Function Cancel(GstRegID As Integer, info As EWBCancelRequestInfo) As GSTNResult(Of EWBCancelResponseInfo)
        Dim output As New GSTNResult(Of EWBCancelResponseInfo)
        Dim rGstReg As DataRow = oMaster.GstRegRow(GstRegID)
        Dim oRet = Me.CheckToken(GstRegID)
        If oRet.Success Then
            Dim nr = oRet.GetCalcRows("token")(0)
            Dim client As IGSTNAuthProvider = Me.GetAuthClientFromToken(nr, rGstReg)
            Dim client2 = Me.GetClient(client, rGstReg("gstin"))

            output = client2.Cancel(info)
            If client2.dicParams.ContainsKey("ResponsePayload") Then output.Message = client2.dicParams("ResponsePayload")

            Me.SaveTransaction(GstRegID, 0, "CANCEL", "", client2.dicParams)
        Else
            output.Message = oRet.Message
        End If

        Return output
    End Function
    Public Function Extend(GstRegID As Integer, EwayBillID As Integer) As GSTNResult(Of EWBExtendResponseInfo)
        Dim output As New GSTNResult(Of EWBExtendResponseInfo)
        Dim sql As String = "select * from gstlistewb() where ewaybillid=" & EwayBillID
        Dim dt1 As DataTable = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql).Tables(0)
        Dim oProc As New clsPOCOConverter(myContext)
        oProc.StringValue = AddressOf stringValue
        If dt1.Rows.Count > 0 Then
            Dim obj = oProc.GenerateObject(Of EWBExtendRequestInfo)(dt1)
            output = Me.Extend(GstRegID, obj)
            If (output.HttpStatusCode = HttpStatusCode.OK) AndAlso String.IsNullOrWhiteSpace(output.Data.errorCodes) Then
                dt1.Rows(0)("Updateddate") = Me.DateFromString(output.Data.UpdatedDate)
                dt1.Rows(0)("validupto") = Me.DateFromString(output.Data.ValidUpto)
                myContext.Provider.objSQLHelper.SaveResults(dt1, "select ewaybillid, updateddate,validupto from ewaybill where 0=1")
            End If
        End If
        Return output
    End Function
    Public Function Extend(GstRegID As Integer, info As EWBExtendRequestInfo) As GSTNResult(Of EWBExtendResponseInfo)
        Dim output As New GSTNResult(Of EWBExtendResponseInfo)
        Dim rGstReg As DataRow = oMaster.GstRegRow(GstRegID)
        Dim oRet = Me.CheckToken(GstRegID)
        If oRet.Success Then
            Dim nr = oRet.GetCalcRows("token")(0)
            Dim client As IGSTNAuthProvider = Me.GetAuthClientFromToken(nr, rGstReg)
            Dim client2 = Me.GetClient(client, rGstReg("gstin"))

            output = client2.Extend(info)
            If client2.dicParams.ContainsKey("ResponsePayload") Then output.Message = client2.dicParams("ResponsePayload")

            Me.SaveTransaction(GstRegID, 0, "Extend", "", client2.dicParams)
        Else
            output.Message = oRet.Message
        End If

        Return output
    End Function
    Public Function Reject(GstRegID As Integer, info As EWBRejectRequestInfo) As GSTNResult(Of EWBRejectResponseInfo)
        Dim output As New GSTNResult(Of EWBRejectResponseInfo)
        Dim rGstReg As DataRow = oMaster.GstRegRow(GstRegID)
        Dim oRet = Me.CheckToken(GstRegID)
        If oRet.Success Then
            Dim nr = oRet.GetCalcRows("token")(0)
            Dim client As IGSTNAuthProvider = Me.GetAuthClientFromToken(nr, rGstReg)
            Dim client2 = Me.GetClient(client, rGstReg("gstin"))

            output = client2.Reject(info)
            If client2.dicParams.ContainsKey("ResponsePayload") Then output.Message = client2.dicParams("ResponsePayload")

            Me.SaveTransaction(GstRegID, 0, "Reject", "", client2.dicParams)
        Else
            output.Message = oRet.Message
        End If

        Return output
    End Function
    Public Function Update(GstRegID As Integer, EwayBillVehicleID As Integer) As GSTNResult(Of EWBUpdVehResponseInfo)
        Dim output As New GSTNResult(Of EWBUpdVehResponseInfo), dic As New clsCollecString(Of String)
        Dim sql As String = "select * from ewaybillvehicle where ewaybillvehicleid=" & EwayBillVehicleID
        Dim dt1 As DataTable = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql).Tables(0)
        Dim oProc As New clsPOCOConverter(myContext)
        oProc.StringValue = AddressOf stringValue
        If dt1.Rows.Count > 0 Then
            Dim sql2 As String = "select ewaybillnum,ewaybillid,validUpto from ewaybill where ewaybillid=" & myUtils.cValTN(dt1.Rows(0)("ewaybillid"))
            Dim dt2 As DataTable = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql2).Tables(0)
            If dt2.Rows.Count > 0 Then
                Dim obj = oProc.GenerateObject(Of EWBUpdVehRequestInfo)(dt1)
                obj.ewbNo = dt2.Rows(0)("ewaybillnum")
                If String.IsNullOrWhiteSpace(obj.reasonCode) Then
                    obj.reasonCode = "4"
                    obj.reasonRem = "First Time"
                End If
                output = Me.Update(GstRegID, obj)
                If (output.HttpStatusCode = HttpStatusCode.OK) AndAlso String.IsNullOrWhiteSpace(output.Data.errorCodes) Then
                    dt1.Rows(0)("entereddate") = Me.DateFromString(output.Data.vehUpdDate)
                    myContext.Provider.objSQLHelper.SaveResults(dt1, "select ewaybillvehicleid, entereddate from ewaybillvehicle where 0=1")
                    dt2.Rows(0)("validUpto") = Me.DateFromString(output.Data.validUpto)
                    myContext.Provider.objSQLHelper.SaveResults(dt2, "select ewaybillid, validupto from ewaybill where 0=1")
                End If

            End If


        End If
        Return output
    End Function
    Public Function Update(GstRegID As Integer, info As EWBUpdVehRequestInfo) As GSTNResult(Of EWBUpdVehResponseInfo)
        Dim output As New GSTNResult(Of EWBUpdVehResponseInfo)
        Dim rGstReg As DataRow = oMaster.GstRegRow(GstRegID)
        Dim oRet = Me.CheckToken(GstRegID)
        If oRet.Success Then
            Dim nr = oRet.GetCalcRows("token")(0)
            Dim client As IGSTNAuthProvider = Me.GetAuthClientFromToken(nr, rGstReg)
            Dim client2 = Me.GetClient(client, rGstReg("gstin"))

            output = client2.UpdateVehicle(info)
            If client2.dicParams.ContainsKey("ResponsePayload") Then output.Message = client2.dicParams("ResponsePayload")
            Me.SaveTransaction(GstRegID, 0, "Update", "", client2.dicParams)
        Else
            output.Message = oRet.Message
        End If

        Return output
    End Function
    Public Function GetDetails(GstRegID As Integer, ewbno As Long) As GSTNResult(Of EWBInfo)
        Dim output As New GSTNResult(Of EWBInfo)
        Dim rGstReg As DataRow = oMaster.GstRegRow(GstRegID)
        Dim oRet = Me.CheckToken(GstRegID)
        If oRet.Success Then
            Dim nr = oRet.GetCalcRows("token")(0)
            Dim client As IGSTNAuthProvider = Me.GetAuthClientFromToken(nr, rGstReg)
            Dim client2 = Me.GetClient(client, rGstReg("gstin"))

            output = client2.GetDetails(ewbno)
            Me.SaveTransaction(GstRegID, 0, "GET", "", client2.dicParams)
        Else
            output.Message = oRet.Message
        End If

        Return output
    End Function
    Public Overloads Async Function DownloadGSTNTp(GstRegID As Integer, ReturnPeriodID As Integer, Action As String) As Task(Of clsProcOutput)

        Dim rPP As DataRow = Me.oMaster.rPostPeriod(ReturnPeriodID)
        Dim rGstReg As DataRow = Me.oMaster.GstRegRow(GstRegID)
        Dim rToken = Me.GetActiveToken(myContext.Police.UniqueUserID, GstRegID)
        Dim auth = Me.GetAuthClientFromToken(rToken, rGstReg)
        Dim client = Me.GetClient(auth, rGstReg("gstin"))
        Dim oRet = Await Me.DownloadGSTNTp(GstRegID, ReturnPeriodID, Action, client)
        Return oRet
    End Function

    Protected Friend Overloads Async Function DownloadGSTNTp(GstRegID As Integer, ReturnPeriodID As Integer, Action As String, client As EWBApiClientBase) As Task(Of clsProcOutput)
        Dim oRet As New clsProcOutput
        Dim dic = Me.PrepareGSTRPayloadSQLUp(0, 0)
        Dim ds As DataSet = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, dic)
        Try
            'Dim model1 = client.GetSummary().Data
            Dim model1 As EWBInfo
            Dim nrTrans As DataRow = Me.SaveTransaction(GstRegID, ReturnPeriodID, "RETSUM", "", client.dicParams)
            oRet.AddDataRow("trans", nrTrans)
            If Not model1 Is Nothing Then
                'instant response
                Dim results As New List(Of EWBInfo)
                results.Add(model1)
                oRet.AddMessage("Summary Downloaded")
                If myUtils.IsInList(Action, "clean") Then

                Else
                    oRet = oRet + Await Me.UpdateDownloadedInvoiceTP(GstRegID, ReturnPeriodID, results)
                End If
            End If
        Catch ex As Exception
            myContext.Logger.logInformation(ex.Message)
        End Try
        Return oRet
    End Function
    Public Function PopulateDataTP(results As List(Of EWBInfo)) As DataSet
        Dim dic = Me.PrepareGSTRPayloadSQLUp(0, oMaster.GetPostPeriodID(Now.Date))
        Dim ds2 = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, dic)
        For Each model1 In results
            Me.PopulateDataset(model1, ds2)
        Next
        Return ds2
    End Function

    Public Async Function UpdateDownloadedInvoiceTP(GstRegID As Integer, ReturnPeriodID As Integer, results As List(Of EWBInfo)) As Task(Of clsProcOutput(Of GstImportInfoGSTIN))
        Dim rGstReg As DataRow = oMaster.GstRegRow(GstRegID)
        Dim rPP As DataRow = oMaster.rPostPeriod(ReturnPeriodID)
        Dim result As Boolean = myFuncs2.IsFinalPP(myContext, GstRegID, rPP, "GSTR1")
        Dim oRet As New clsProcOutput(Of GstImportInfoGSTIN)
        If result Then
            oRet.AddError("Period finalized")
        Else
            Dim sql As String = $"select * from gstr3b where gstregid={GstRegID} and returnperiodid={ReturnPeriodID}"
            Dim dt1 As DataTable = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql).Tables(0)

            Dim importer As New Import_GSTR1TaskProvider(myContext.Controller)
            Dim ds = Me.PopulateDataTP(results)
            oRet = Await importer.UpdateDownloadedInvoiceTP(rGstReg, rPP, ds, "")
        End If
        Return oRet
    End Function
    Public Overrides Async Function PrepareReconcileViews(strFilter As String) As Task(Of List(Of clsDummyView))
        Dim lst As New List(Of clsDummyView)

        lst.Add(Await PrepareView("ListEWBInvoiceMatch", strFilter, "E-Way Bill Reconciliation Report"))

        Return lst

    End Function

    Public Function Generate(GstRegID As Integer, ReturnPeriodID As Integer, FileName As String) As clsProcOutput
        Dim oRet As New clsProcOutput
        Dim strf1 As String = myUtils.CombineWhere(False, "isnull(statusnum,0)=2", "canceldate is null and ewbrejecteddate is null",
                                                            $"GstRegID={GstRegID}", $"ReturnPeriodID={ReturnPeriodID}")

        Dim dic As New clsCollecString(Of String)
        dic.Add("generate", "select ewaybillid from gstlistewb() where " & myUtils.CombineWhere(False, strf1, "ewaybillnum is null"))

        Dim ds As DataSet = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, dic), cnt As Integer

        Dim oMaster As New clsMasterDataFICO(myContext)
        Dim rGstPP = oMaster.GstRegPPRow(GstRegID, ReturnPeriodID)
        Dim provider2 As New exvReportDataProvider(myContext)
        Dim vwModel As New clsViewModel(myContext)
        Dim fRow As DataRow = myContext.AppModel.FrmPrnRowKey("crpewaybill", "")


        Dim lstWhere As New List(Of String), lstFile As New List(Of String)
        For Each r1 As DataRow In ds.Tables("generate").Select
            Dim info As GSTNResult(Of EWBPostResponseInfo) = Me.Generate(GstRegID, r1("ewaybillid"))
            cnt = cnt + 1
            lstWhere.Add(myContext.SQL.GenerateSQLWhere(r1, "Ewaybillid"))
            If (0 = 1) Then
                info.Data = New EWBPostResponseInfo With {.ewayBillNo = 12345}
            End If
            If info.Data IsNot Nothing AndAlso info.Data.ewayBillNo > 0 Then
                Dim NewFileName2 = myContext.App.objAppExtender.MapPath("app_data/payload/ewb_" & info.Data.ewayBillNo & ".pdf")
                Dim Model = provider2.GenerateReportModel(myContext.GetAppInfo, fRow, vwModel, r1("ewaybillid"))
                Dim oRet2 = myContext.Controller.PrintingPress.SpecReportFile(vwModel, fRow, Model, NewFileName2)
                lstFile.Add(NewFileName2)
            End If
        Next

        If cnt > 0 Then
            Dim strf2 As String = myUtils.CombineWhereOR(False, lstWhere.ToArray)
            Dim provider = New Import_EWBTaskProvider(myContext.Controller)
            Dim xlsFileName = Replace(FileName, ".zip", ".xlsx")
            oRet = provider.GenerateTemplate($"GstRegID={GstRegID}", $"ReturnPeriodID={ReturnPeriodID}",
                                                     xlsFileName, strf2)
            lstFile.Insert(0, oRet.Description)
            oRet = provider.GenerateZipFile(FileName, lstFile.ToArray)
        End If
        Return oRet
    End Function
End Class
