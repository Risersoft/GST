Imports System.Collections.Concurrent
Imports System.Configuration
Imports System.IO.Compression
Imports System.Net
Imports System.Threading
Imports GSTN.API.Library.Models.GstNirvana
Imports Newtonsoft.Json
Imports risersoft.API.GSTN
Imports risersoft.API.GSTN.Auth
Imports risersoft.shared
Imports risersoft.shared.cloud.common
Imports risersoft.shared.dal

Public Class clsGSTNReturnBase
    Inherits clsGSTNTrackedAPIBase
    Public DocType As String
    Protected Friend dicSubType As New clsCollecString(Of String)
    Public Overridable Property CDNRApiAction As String = ""
    Public Overridable Property fncInvoiceFilter As Func(Of String) = Function()
                                                                          Dim str1 As String = "isnull(lastupdated,getdate()) > isnull((select dated from gstntransaction where isnull(statuscode,'')='P' and gstntransactionid = lasttransactionid),'1799-1-1')"
                                                                          Return str1
                                                                      End Function
    Public Sub New(context As IProviderContext, DocType As String, ReturnCode As String)
        MyBase.New(context, "TaxPayer", ReturnCode)
        dicSubType.Add("b2b", "inv_typ")
        dicSubType.Add("cdn", "ntty")
        dicSubType.Add("cdnur", "ntty")
        dicSubType.Add("exp", "exp_typ")
        dicSubType.Add("b2c", "b2c_typ")
        Me.DocType = DocType
        'no field for b2bur, impg, imps
    End Sub
    Public Function PrepareGSTRPayloadSQLCP(GstRegID As Integer, ReturnPeriodID As Integer) As clsCollecString(Of String)
        Dim strf1 As String = "actionflag in ('A','R')"
        Dim strf2 As String = "GstRegID=" & GstRegID
        Dim strf3 As String = "ReturnPeriodID=" & ReturnPeriodID
        Dim strf4 As String = "postingdate > isnull((select dated from gstntransaction where isnull(statuscode,'')='P' and gstntransactionid = actiontransactionid),'1799-1-1')"

        Dim strFilterInv As String = myUtils.CombineWhere(False, strf1, strf2, strf3, strf4)
        Dim strFilterSumm As String = myUtils.CombineWhere(False, strf2, strf3)
        Dim dic As New clsCollecString(Of String)
        dic.Add("b2b", "select distinct invoiceid, ctin, actionflag as flag, chksum, inum, idt from 
                            accListCPinvoiceItem() " & myUtils.CombineWhere(True, strFilterInv, "gstinvoicetype='b2b' and DocType='" & DocType & "' and isnull(isamendment,0)=0") & " group by invoiceid, ctin, inum, idt, actionflag, chksum")
        dic.Add("cdnr", "select distinct invoiceid, ctin, actionflag as flag, chksum, nt_num, nt_dt from 
                            accListcpinvoiceItem() " & myUtils.CombineWhere(True, strFilterInv, "gstInvoiceType='CDN' and DocType='" & DocType & "' and isnull(isamendment,0)=0") & " group By invoiceid, ctin, nt_num, nt_dt, actionflag, chksum")
        Return dic

    End Function

    Protected Friend Function GenerateFilter(dic As clsCollecString(Of List(Of Integer)), fieldName As String) As String
        Dim IDList As New List(Of Integer)
        If dic.ContainsKey(fieldName) Then IDList = dic(fieldName)
        If IDList.Count = 0 Then IDList.Add(0)       'when no invoice is there
        Dim strFilterInv As String = fieldName & " In (" & myUtils.MakeCSV(IDList, ",") & ")"
        Return strFilterInv
    End Function

    Public Function OnGstrUpload(GstRegID As Integer, ReturnPeriodID As Integer, TransType As String, dicParams As Dictionary(Of String, String), UploadType As String, dicID As clsCollecString(Of List(Of Integer))) As DataRow
        Dim nrTrans As DataRow = Me.SaveTransaction(GstRegID, ReturnPeriodID, TransType, UploadType, dicParams)

        Select Case UploadType.Trim.ToUpper
            Case "UM"
                Dim dic As New clsCollecString(Of String)
                dic.Add("inv", "select invoiceid, LastTransactionId,ActionFlag from invoice where " & Me.GenerateFilter(dicID, "invoiceid"))
                Dim ds As DataSet = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, dic)

                For Each str1 As String In New String() {"inv"}
                    For Each r1 As DataRow In ds.Tables(str1).Select
                        r1("LastTransactionId") = nrTrans("gstntransactionid")
                    Next
                    myContext.Provider.objSQLHelper.SaveResults(ds.Tables(str1), dic(str1))
                Next
            Case "AR"
                Dim dic As New clsCollecString(Of String)
                dic.Add("inv", "select cpinvoiceid, ActionTransactionId,ActionFlag from cpinvoice where " & Me.GenerateFilter(dicID, "cpinvoiceid"))
                Dim ds As DataSet = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, dic)

                For Each str1 As String In New String() {"inv"}
                    For Each r1 As DataRow In ds.Tables(str1).Select
                        r1("LastTransactionId") = nrTrans("gstntransactionid")
                    Next
                    myContext.Provider.objSQLHelper.SaveResults(ds.Tables(str1), dic(str1))
                Next
            Case "DEL"
                Dim dic As New clsCollecString(Of String)
                dic.Add("inv", "select * from GstnAction where " & Me.GenerateFilter(dicID, "gstnactionid"))
                Dim ds As DataSet = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, dic)

                For Each str1 As String In New String() {"inv"}
                    For Each r1 As DataRow In ds.Tables(str1).Select
                        r1("LastTransactionId") = nrTrans("gstntransactionid")
                    Next
                    myContext.Provider.objSQLHelper.SaveResults(ds.Tables(str1), dic(str1))
                Next
            Case "CLR"
                Dim dic As New clsCollecString(Of String)
                dic.Add("inv", $"select invoiceid, LastTransactionId,ActionFlag from invoice where campusid in (select campusid from campus where gstregid={GstRegID}) and returnperiodid={ReturnPeriodID}")
                Dim ds As DataSet = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, dic)

                For Each str1 As String In New String() {"inv"}
                    For Each r1 As DataRow In ds.Tables(str1).Select
                        r1("LastTransactionId") = DBNull.Value
                    Next
                    myContext.Provider.objSQLHelper.SaveResults(ds.Tables(str1), dic(str1))
                Next
        End Select




        Return nrTrans

    End Function


    Public Function OnGSTRAction(GstRegID As Integer, ReturnPeriodID As Integer, ReturnKey As String, Status As String) As Boolean

        Dim rPP As DataRow = oMaster.rPostPeriod(ReturnPeriodID)
        Dim rGstReg As DataRow = oMaster.GstRegRow(GstRegID)
        If (Not rPP Is Nothing) AndAlso (Not rGstReg Is Nothing) Then
            Dim nr As DataRow = oMaster.GstRegPPRow(GstRegID, ReturnPeriodID)
            If nr.Table.Columns.Contains(ReturnKey) Then
                nr(ReturnKey) = Status
            End If
            Dim IsPending As Boolean = False
            Do
                If (Not IsPending) Then IsPending = Not myUtils.IsInList(myUtils.cStrTN(nr("GSTR3B")), "F")
                nr("ispending") = IsPending
                rPP = oMaster.rPostPeriod(CDate(rPP("sdate")).AddMonths(1))
                If Not rPP Is Nothing Then
                    nr = oMaster.GstRegPPRow(GstRegID, rPP("PostPeriodID"))
                End If
            Loop While (rPP IsNot Nothing)


            myContext.Provider.objSQLHelper.SaveResults(nr.Table, "select * from gstregpp where 0=1")
            Return True

        End If


    End Function

    Protected Friend Overridable Function GetAuthClient(gsp As String, gstin As String, userid As String, env As String, IPAddr As String) As IGSTNAuthProvider
        Dim client As GSTNAuthClient
        Select Case gsp.Trim.ToLower
            Case "kpmg"
                client = New KpmgGspAuthClient(gstin.Trim, userid.Trim, env.Trim, IPAddr)
            Case Else
                client = New GSTNAuthClient(gstin.Trim, userid.Trim, gsp.Trim, env.Trim, IPAddr)
        End Select
        client.Init()
        Return client
    End Function

    Public Overrides Function GetAuthClientFromToken(rToken As DataRow, rGstReg As DataRow) As IGSTNAuthProvider
        If Not rToken Is Nothing Then
            Dim token As New TokenResponseModel
            token.auth_token = rToken("authtoken")
            token.expiry = rToken("expiry")
            Dim config = Me.GetGspConfig()
            Dim client As IGSTNAuthProvider = Me.GetAuthClient(config.Item1, rGstReg("gstin"), rGstReg("gstnuserid"), config.Item2, rToken("IPAddr"))
            client.token = token
            client.DecryptedKey = Convert.FromBase64String(rToken("sek"))
            Return client
        End If
    End Function


    Public Function SendOTP(rGstReg As DataRow) As GSTNResult(Of OTPResponseModel)
        If Not rGstReg Is Nothing Then
            Dim config = Me.GetGspConfig()
            Dim client = Me.GetAuthClient(config.Item1, rGstReg("gstin"), rGstReg("gstnuserid"), config.Item2, GSTNConstants.publicip)
            Trace.WriteLine("Getting OTP from " & config.Item1)
            Dim model = client.RequestOTP()
            Dim nrTrans = Me.SaveTransaction(rGstReg("GstRegID"), 0, "OTPREQUEST", "", client.dicParams)
            Return model
        End If
    End Function
    Public Overridable Function PrepareReconcileViews(strFilter As String) As Task(Of List(Of clsDummyView))

    End Function
    Public Overridable Function PrepareSummaryViews(CampusFilter As String, PeriodFilter As String, SummaryType As String) As Task(Of List(Of clsDummyView))

    End Function


    Public Overridable Function PrepareGSTRPayloadSQLDel(GstRegID As Integer, ReturnPeriodID As Integer) As clsCollecString(Of String)

    End Function
    Public Overridable Function PrepareGSTRPayloadSQLUp(GstRegID As Integer, ReturnPeriodID As Integer) As clsCollecString(Of String)

    End Function
    Public Overridable Function PrepareGSTRAPayloadSQL(GstRegID As Integer, ReturnPeriodID As Integer) As clsCollecString(Of String)

    End Function
    Public Overridable Function GenerateSQL(GstRegID As Integer, ReturnPeriodID As Integer, UploadType As String) As clsCollecString(Of String)
        Dim dic As clsCollecString(Of String)
        Select Case UploadType.Trim.ToUpper
            Case "UM"
                dic = Me.PrepareGSTRPayloadSQLUp(GstRegID, ReturnPeriodID)
            Case "DEL"
                dic = Me.PrepareGSTRPayloadSQLDel(GstRegID, ReturnPeriodID)
            Case "AR"
                dic = Me.PrepareGSTRPayloadSQLCP(GstRegID, ReturnPeriodID)
        End Select
        Return dic
    End Function

    Protected Friend Function CreateParamsJson(GstRegID As Integer, ReturnPeriodID As Integer) As String
        Dim Params = Me.CreateParams(GstRegID, ReturnPeriodID)
        Dim json = JsonConvert.SerializeObject(Params)
        Return json
    End Function
    Protected Friend Function CreateParams(GstRegID As Integer, ReturnPeriodID As Integer) As List(Of clsSQLParam)
        Dim Params As New List(Of clsSQLParam)
        Params.Add(New clsSQLParam("@APITASKID", myContext.Controller.TaskInfo.ApiTaskID.ToString, GetType(Guid), False))
        Params.Add(New clsSQLParam("@GstRegID", GstRegID, GetType(Integer), False))
        Params.Add(New clsSQLParam("@ReturnPeriodID", ReturnPeriodID, GetType(Integer), False))

        Return Params
    End Function
    Protected Friend Function GetTransTypeFromTable(TableName As String) As String
        Dim str1 As String = TableName.ToUpper

        If myUtils.IsInList(str1, "TXPD") Then
            str1 = "TXP"
        ElseIf myUtils.IsInList(str1, "TXPDA") Then
            str1 = "TXPA"
        ElseIf myUtils.IsInList(str1, "HSN") Then
            str1 = "HSNSUM"
        ElseIf myUtils.IsInList(str1, "doc_issue") Then
            str1 = "DOCISS"
        End If

        Return str1
    End Function
    Public Function CreateStatusTask(rUploadTask As DataRow, GstRegID As Integer, ReturnPeriodID As Integer) As clsProcOutput

        Dim dic = New Dictionary(Of String, String) From {{"InfoJson", Me.CreateParamsJson(GstRegID, ReturnPeriodID)}}
        Dim filename As String = myContext.Controller.CalcRLSId.ToString & "-" & Me.ReturnCode & "-" & Replace(Now.ToLongTimeString, ":", "").Replace(" ", "") & ".zip"
        dic.Add("filename", filename)
        dic.Add("userid", rUploadTask("userid").ToString)
        Dim rTask As DataRow = TaskProviderFactory.CreateApiTask(myContext.Provider, Me.ReturnCode, "STATUS", rUploadTask("parentid"), dic)
        Dim queueName = myContext.Controller.CalcQueueName
        Dim oRet2 = TaskProviderFactory.Enqueue(myContext.Provider, rTask, queueName)

        Return oRet2
    End Function

    Public Overrides Function CheckToken(GstRegID As Integer) As clsProcOutput
        Dim oRet As New clsProcOutput(Of GSTNResult(Of OTPResponseModel))
        Dim nr = Me.GetActiveToken(myContext.Police.UniqueUserID, GstRegID)
        If nr Is Nothing Then
            Dim rGstReg As DataRow = oMaster.GstRegRow(GstRegID)
            If Not rGstReg Is Nothing Then
                Dim Model = Me.SendOTP(rGstReg)
                oRet.JsonData = New With {.status = Model.HttpStatusCode, .token = 0}
            End If
        Else
            oRet.JsonData = New With {.status = 200, .token = 1}
        End If
        Return oRet

    End Function

    Public Function GetToken(GstRegID As Integer, OTP As String) As GSTNResult(Of TokenResponseModel)
        Dim model As GSTNResult(Of TokenResponseModel)
        Dim rGstReg As DataRow = oMaster.GstRegRow(GstRegID)
        If Not rGstReg Is Nothing Then
            Dim config = Me.GetGspConfig()
            Dim client = Me.GetAuthClient(config.Item1, rGstReg("gstin"), rGstReg("gstnuserid"), config.Item2, GSTNConstants.publicip)
            model = client.RequestToken(OTP)
            Dim nr = Me.SaveTransaction(GstRegID, 0, "AUTHTOKEN", "", client.dicParams)
            If (Not model Is Nothing) AndAlso (Not model.Data Is Nothing) Then
                model.Data.decryptSek = Convert.ToBase64String(client.DecryptedKey)
                Dim nr2 = Me.SaveToken(myContext.Police.UniqueUserID, GstRegID, model.Data, OTP)
            End If
        End If
        Return model
    End Function
    Public Function GetTokenOutput(model As GSTNResult(Of TokenResponseModel)) As clsProcOutput
        Dim oRet As New clsProcOutput
        If (Not model Is Nothing) AndAlso (Not model.Data Is Nothing) AndAlso (Not String.IsNullOrEmpty(model.Data.auth_token)) Then
            oRet.JsonData = New With {.status = HttpStatusCode.OK, .data = model.Data.status_cd, .ExpiryMins = model.Data.expiry / 60}
        ElseIf Not model Is Nothing Then
            oRet.JsonData = New With {.status = model.HttpStatusCode}
        Else
            oRet.JsonData = New With {.status = HttpStatusCode.InternalServerError}
        End If
        Return oRet
    End Function
    Protected Friend Function GetSignRow(rGstPP As DataRow, forcefresh As Boolean) As DataRow
        Dim sql As String = "select * from gstnsign where gstregid=" & rGstPP("GstRegID") & " and ReturnPeriodID=" & rGstPP("ReturnPeriodID") & " order by dated desc"
        Dim dt1 As DataTable = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql).Tables(0)
        Dim nr As DataRow
        If (forcefresh) OrElse (dt1.Rows.Count = 0) Then
            nr = myUtils.CopyOneRow(rGstPP, dt1)
            nr("dated") = Now
            nr("returnkey") = ReturnCode
            nr("provider") = "eMudhra"
            nr("gstnsignid") = System.Guid.NewGuid.ToString
            nr("transactionid") = System.Guid.NewGuid.ToString
        Else
            nr = dt1.Rows(0)
        End If
        Return nr
    End Function

    Public Async Function GenerateExcel(CampusFilter As String, PeriodFilter As String, filename As String, SummaryType As String) As Task(Of clsProcOutput)
        Dim oRet As New clsProcOutput
        Try
            Dim lst = Await Me.PrepareSummaryViews(CampusFilter, PeriodFilter, SummaryType)
            Dim lst2 = lst.Select(Of DataTable)(Function(vw)
                                                    Dim dt1 = vw.mainGrid.Model.ToVisibleTable(, True)
                                                    dt1.TableName = vw.Model.myCMain.strView
                                                    Return dt1
                                                End Function).ToList

            oRet = myFuncs.GenerateExcel(myContext, lst2, filename)
        Catch ex As Exception
            oRet.AddError(ex.Message)
        End Try
        Return oRet

    End Function
    Public Function GenerateExcel(lst As List(Of clsDummyView), filename As String) As clsProcOutput
        Dim oRet As New clsProcOutput
        Try
            Dim lst2 = lst.Select(Of DataTable)(Function(vw)
                                                    Try
                                                        Dim dt1 = vw.mainGrid.Model.ToVisibleTable(, True)
                                                        dt1.TableName = vw.Model.myCMain.strView
                                                        Return dt1
                                                    Catch ex As Exception
                                                        myContext.Logger.logInformation($"Error in {vw.Model.myCMain.strView}: " & ex.Message)
                                                    End Try
                                                End Function).Where(Function(x) x IsNot Nothing).ToList

            oRet = myFuncs.GenerateExcel(myContext, lst2, filename)
        Catch ex As Exception
            oRet.AddError(ex.Message)
        End Try
        Return oRet

    End Function
    Public Overridable Function ImportJson(filePath As String, CounterParty As Boolean) As clsProcOutput

    End Function

    Public Function RefreshToken(GstRegID As Integer) As GSTNResult(Of TokenResponseModel)
        Dim model As GSTNResult(Of TokenResponseModel)
        Dim rGstReg As DataRow = Me.oMaster.GstRegRow(GstRegID)
        Dim rToken = Me.GetActiveToken(myContext.Police.UniqueUserID, GstRegID, 2)
        Dim auth = Me.GetAuthClientFromToken(rToken, rGstReg)
        model = auth.RefreshToken()
        Dim nr = Me.SaveTransaction(GstRegID, 0, "REFRESHTOKEN", "", auth.dicParams)
        If (Not model Is Nothing) AndAlso (Not model.Data Is Nothing) Then
            model.Data.decryptSek = Convert.ToBase64String(auth.DecryptedKey)
            Dim nr2 = Me.SaveToken(myContext.Police.UniqueUserID, GstRegID, model.Data, "")
        End If

        Return model
    End Function

    Protected Friend Function GenerateIDList(dt1 As DataTable, ParamArray arrColKey() As String) As clsCollecString(Of List(Of Integer))
        Dim dic As New clsCollecString(Of List(Of Integer))
        For Each ColKey In arrColKey
            If dt1.Columns.Contains(ColKey) AndAlso dt1.Columns(ColKey).DataType Is GetType(Integer) Then
                Dim lst As New List(Of Integer)
                For Each r1 As DataRow In dt1.Select
                    lst.Add(myUtils.cValTN(r1(ColKey)))
                Next
                dic.Add(ColKey, lst)
            End If

        Next
        Return dic
    End Function
    Protected Friend Function RecordCountExceedsLimit(dic As clsCollecString(Of String), limit As Integer) As Boolean
        Dim total As Integer = 0, LimitCrossed As Boolean = False
        For Each kvp In dic
            Try
                Dim sql As String = "select count(*) from (" & kvp.Value & ") as t1"
                Dim ds1 As DataSet = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql)
                total = total + myUtils.cValTN(ds1.Tables(0).Rows(0)(0))
            Catch ex As Exception
                Trace.WriteLine(ex.Message)
            End Try
            If total > limit Then
                LimitCrossed = True
                Exit For
            End If
        Next
        Return LimitCrossed
    End Function

    Public Function AmendmentFilterInv(rPP As DataRow, GstRegID As Integer) As String
        If (rPP IsNot Nothing) Then
            Dim strfa1 As String = String.Format("(
(ReturnPeriodID ={0} and GstregID = {1} And rootinvoiceid Is Not null) 
Or 
(
(invoiceid Not in (select isnull(originvoiceid,0) from invoice inner join postperiod on invoice.returnperiodid = postperiod.postperiodid where edate <= '{2}')) 
And 
(rootReturnPeriodID in (select isnull(rootInvoice.ReturnPeriodID,0) from Invoice inner join Invoice as RootInvoice on invoice.RootInvoiceid = Rootinvoice.invoiceid where invoice.ReturnPeriodID = {0} And gstregid = {1}))
And
(sdate < '{3}')
And
(GstregID = {1})
)
)", rPP("postperiodid"), GstRegID, Format(rPP("edate"), "dd-MMM-yyyy"), Format(rPP("sdate"), "dd-MMM-yyyy"))
            Return strfa1
        End If
    End Function
    Public Function AmendmentFilterPY(ReturnPeriodID As Integer, GstRegID As Integer) As String
        Dim strfa1 As String = String.Format("(
(ReturnPeriodID ={0} and GstregID = {1} and rootvouchID is not null) 
or 
(
(GstAdvanceVouchID not in (select isnull(OrigVouchID,0) from GstAdvanceVouch where (select edate from postperiod where PostPeriodID=returnperiodid) <= (select edate from postperiod where PostPeriodID = {0}))) 
and 
(rootReturnPeriodID in (select isnull(rootReturnPeriodID,0) from GstListAdvanceVouchItem() where ReturnPeriodID = {0} and gstregid = {1}))
and
(select sdate from postperiod where PostPeriodID=returnperiodid)  < (select sdate from postperiod where PostPeriodID = {0})
And
(GstregID = {1})
)
)", ReturnPeriodID, GstRegID)
        Return strfa1
    End Function
    Public Overridable Async Function PrepareViewApiTask(vwKey As String, ApiTaskID As String, Optional vwName As String = "") As Task(Of clsDummyView)
        Dim dvw As New clsDummyView(EnumViewMode.acNormalM, myContext.Controller)
        Dim strTag As String = String.Format("<VIEW KEY=""{0}"">", vwKey) & vbCrLf &
                                   String.Format("<MODROW><SQLWHERE2>APITASKID='{0}'</SQLWHERE2></MODROW>", ApiTaskID) & vbCrLf &
                                   "</VIEW>"
        Await myContext.Controller.ProcessTag(dvw, strTag)
        If String.IsNullOrEmpty(vwName) Then dvw.Model.myCMain.strView = dvw.Model.vwKey Else dvw.Model.myCMain.strView = vwName
        Return dvw
    End Function
    Public Overridable Async Function PrepareViewGSTIN(vwKey As String, GstRegID As Integer, ReturnPeriodID As Integer, Optional vwName As String = "") As Task(Of clsDummyView)
        Return Await Me.PrepareViewGSTIN(vwKey, GstRegID, ReturnPeriodID, ReturnPeriodID, vwName)
    End Function
    Public Overridable Async Function PrepareViewGSTIN(vwKey As String, GstRegID As Integer, ReturnPeriodID1 As Integer, ReturnPeriodID2 As Integer, Optional vwName As String = "") As Task(Of clsDummyView)
        Dim dvw As New clsDummyView(EnumViewMode.acNormalM, myContext.Controller)
        Dim rPP1 As DataRow = oMaster.rPostPeriod(ReturnPeriodID1)
        Dim rPP2 As DataRow = oMaster.rPostPeriod(If(ReturnPeriodID2 > 0, ReturnPeriodID2, ReturnPeriodID1))
        Dim strf1 As String = String.Format("sdate>='{0}' and edate<='{1}'", Format(rPP1("sdate"), "dd-MMM-yyyy"), Format(rPP2("edate"), "dd-MMM-yyyy"))
        Dim PPIDCSV As String = myUtils.MakeCSV(rPP1.Table.Select(strf1), "postperiodid")
        Dim sql As String = myUtils.CombineWhere(False, "ReturnPeriodID In (" & PPIDCSV & ")", String.Format("GstRegID={0}", GstRegID))
        Return Await Me.PrepareView(vwKey, sql, vwName)
    End Function
    Public Overridable Async Function PrepareViewPAN(vwKey As String, GstRegID As Integer, ReturnPeriodID1 As Integer, ReturnPeriodID2 As Integer, Optional vwName As String = "") As Task(Of clsDummyView)
        Dim rGstReg As DataRow = oMaster.GstRegRow(GstRegID)
        Dim rPP1 As DataRow = oMaster.rPostPeriod(ReturnPeriodID1)
        Dim rPP2 As DataRow = oMaster.rPostPeriod(If(ReturnPeriodID2 > 0, ReturnPeriodID2, ReturnPeriodID1))
        Dim strf1 As String = String.Format("sdate>='{0}' and edate<='{1}'", Format(rPP1("sdate"), "dd-MMM-yyyy"), Format(rPP2("edate"), "dd-MMM-yyyy"))
        Dim PPIDCSV As String = myUtils.MakeCSV(rPP1.Table.Select(strf1), "postperiodid")

        Dim sql As String = myUtils.CombineWhere(False, "ReturnPeriodID In (" & PPIDCSV & ")", String.Format("CompanyID={0}", rGstReg("companyid")))
        Return Await Me.PrepareView(vwKey, sql, vwName)
    End Function
    Public Overridable Async Function PrepareView(vwKey As String, dic As clsCollecString(Of String), Optional vwName As String = "") As Task(Of clsDummyView)
        Dim sql As String = myUtils.CombineWhere(False, dic("ReturnPeriodID"), dic("campusid"))
        Return Await Me.PrepareView(vwKey, sql, vwName)
    End Function

    Public Overridable Async Function PrepareView(vwKey As String, strFilter As String, Optional vwName As String = "") As Task(Of clsDummyView)
        Dim dvw As New clsDummyView(EnumViewMode.acNormalM, myContext.Controller)
        Dim strTag As String = String.Format("<VIEW KEY=""{0}"">", vwKey) & vbCrLf &
                                   "<MODROW><SQLWHERE2>" & strFilter & "</SQLWHERE2><APPLIFILTERS></APPLIFILTERS></MODROW>" & vbCrLf &
                                   "</VIEW>"
        Await myContext.Controller.ProcessTag(dvw, strTag)
        If String.IsNullOrEmpty(vwName) Then dvw.Model.myCMain.strView = dvw.Model.vwKey Else dvw.Model.myCMain.strView = vwName
        Return dvw
    End Function

End Class