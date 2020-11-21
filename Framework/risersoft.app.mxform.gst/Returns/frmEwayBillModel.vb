Imports risersoft.shared
Imports risersoft.app.mxent
Imports System.Runtime.Serialization
Imports System.Data.SqlClient
Imports Newtonsoft.Json
Imports System.Net

<DataContract>
Public Class frmEwayBillModel
    Inherits clsFormDataModel
    Dim dicObjects As New clsCollecString(Of DataRow)
    Dim myVueVehicle As clsViewModel

    Protected Overrides Sub InitViews()
        myView = Me.GetViewModel("Items")
        myVueVehicle = Me.GetViewModel("PartB")
    End Sub

    Public Sub New(context As IProviderContext)
        MyBase.New(context)
        Me.InitViews()
        Me.InitForm()
    End Sub

    Private Sub InitForm()
        Dim sql As String

        sql = "Select CampusID, DispName, CompanyID, CampusType,TaxAreaCode, DivisionCodeList, GstRegID, CampusCode, GSTIN from mmListCampus()  Order by DispName"
        Me.AddLookupField("CampusID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Campus").TableName)

        sql = "SELECT  CustomerID, PartyName, TaxAreaCode, GSTIN, TaxAreaID, GSTRegType, RegDate, PartyType FROM  slsListCustomer() Order By PartyName"
        Me.AddLookupField("CustomerID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Customer").TableName)

        sql = "SELECT  VendorID, PartyName, TaxAreaCode, GSTRegType, GSTIN, VendorCode, TaxAreaID, PartyType FROM  PurListVendor() where partysubtype in ('','MS') Order By PartyName"
        Me.AddLookupField("VendorID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Vendor").TableName)

        sql = "SELECT  PostPeriodID, convert(varchar,Month) + ' / ' + convert(varchar,Year) as Month , SDate, EDate, ret_pd FROM postperiod Order By SDate desc"
        Me.AddLookupField("ReturnPeriodID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "ReturnPeriod").TableName)

        sql = myFuncsBase.CodeWordSQL("EWayBill", "SubSupplyType", "")
        Me.AddLookupField("SubSupplyType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "SubSupplyType").TableName)

        sql = myFuncsBase.CodeWordSQL("EWayBill", "SupplyType", "")
        Me.AddLookupField("SupplyType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "SupplyType").TableName)

        sql = myFuncsBase.CodeWordSQL("EWayBill", "TransactionType", "")
        Me.AddLookupField("MovementType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "MovementType").TableName)

        sql = myFuncsBase.CodeWordSQL("EWayBill", "DocumentType", "")
        Me.AddLookupField("DocType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "DocumentType").TableName)

        sql = "Select VendorID,isNull(GSTIN,'') + ' | ' + PartyName from purlistvendor() where partysubtype='TR'"
        Me.AddLookupField("TransVendorID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Transporter").TableName)

        sql = myFuncsBase.CodeWordSQL("EWayBill", "TransportationMode", "")
        myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "TransMode")

        sql = myFuncsBase.CodeWordSQL("EWayBill", "VehicleType", "")
        Me.AddLookupField("VehicleType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "VehicleType").TableName)

        sql = myFuncsBase.CodeWordSQL("EWayBill", "VehicleUpdateReasonCode", "")
        myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "ReasonCode")

        sql = "select dbo.val(TINCode) as code, TinCode + ' | ' + Descrip, TaxAreaCode from TaxArea where dbo.val(TINCode)>0 Order by Descrip"
        Me.AddLookupField("fromStateCode", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "StateCode").TableName)
        Me.AddLookupField("toStateCode", "StateCode")
        Me.AddLookupField("actToStateCode", "StateCode")
        Me.AddLookupField("actFromStateCode", "StateCode")
        Me.AddLookupField("FromState", "StateCode")


        Dim vlist2 As New clsValueList
        vlist2.Add(0, "0")
        vlist2.Add(5, "5")
        vlist2.Add(12, "12")
        vlist2.Add(18, "18")
        vlist2.Add(28, "28")
        Me.ValueLists.Add("gstrate", vlist2)
        Me.AddLookupField("RT", "gstrate")

        sql = "Select * from validationrule where doctype='EWB' and isnull(isdisabled,0)=0"
        myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Rules")

        sql = "Select * from SystemOptions"
        myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "options")

    End Sub

    Public Overrides Function PrepForm(oView As clsViewModel, ByVal prepMode As EnumfrmMode, ByVal prepIDX As String, Optional ByVal strXML As String = "") As Boolean
        Dim sql, str1, strf1 As String

        Me.FormPrepared = False
        If prepMode = EnumfrmMode.acAddM Then prepIDX = 0
        sql = "Select * from Ewaybill Where EwayBillID = " & prepIDX
        Me.InitData(sql, "", oView, prepMode, prepIDX, strXML)

        sql = "Select EWayBillVehicleID,EWayBillID,TransMode,TransDocNo,TransDocDate,VehicleNo,VehicleType,FromPlace,FromState,ReasonCode,EnteredDate,UserGSTINTransin,TripshtNo,ReasonRem from EWayBillVehicle where EWayBillID = " & frmIDX & ""
        myVueVehicle.MainGrid.MainConf("FORMATXML") = "<COL KEY=""TransMode"" CAPTION=""Mode""/><COL KEY=""UserGSTINTransin"" CAPTION=""Entered By""/><COL KEY=""TripshtNo"" CAPTION=""CEWB No.""/><COL KEY=""ReasonRem"" CAPTION=""Remark""/>"
        myVueVehicle.MainGrid.QuickConf(sql, True, "1-1-1-1-1-1-1-2-1-1-1-1.5", True)
        str1 = "<BAND INDEX=""0"" TABLE=""EWayBillVehicle"" IDFIELD=""EWayBillVehicleID""><COL KEY="" EWayBillID,TransDocNo,TransDocDate,VehicleNo,FromPlace,EnteredDate,UserGSTINTransin,TripshtNo,ReasonRem ""/><COL KEY=""FromState"" LOOKUPSQL=""select dbo.val(TINCode) as code, TinCode + ' | ' + Descrip, TaxAreaCode from TaxArea where dbo.val(TINCode)>0 Order by Descrip""/><COL KEY=""TransMode"" LOOKUPSQL=""" & myFuncsBase.CodeWordSQL("EWayBill", "TransportationMode", "") & """/><COL KEY=""ReasonCode"" LOOKUPSQL=""" & myFuncsBase.CodeWordSQL("EWayBill", "VehicleUpdateReasonCode", "") & """/><COL KEY=""VehicleType"" LOOKUPSQL=""" & myFuncsBase.CodeWordSQL("EWayBill", "VehicleType", "") & """/></BAND>"
        myVueVehicle.MainGrid.PrepEdit(str1, EnumEditType.acCommandOnly)

        Me.BindDataTable(myUtils.cValTN(prepIDX))

        myView.MainGrid.BindGridData(Me.dsForm, 1)
        myView.MainGrid.MainConf("FORMATXML") = "<COL KEY=""Description"" CAPTION=""Product""/><COL KEY=""Details"" CAPTION=""Description""/><COL KEY=""Hsn_sc"" CAPTION=""HSN Code""/><COL KEY=""Uqc"" CAPTION=""Product Unit""/><COL KEY=""TXVAL"" CAPTION=""Taxable Amount""/><COL KEY=""RT"" CAPTION=""Rate of Tax""/><COL KEY=""Cess_RT"" CAPTION=""Cess Rate""/>"
        myView.MainGrid.QuickConf("", True, "1-1-1-1-1.5-1-1-1-1-1-1-1-1", True)
        strf1 = "<BAND INDEX=""0"" TABLE=""EWayBillItem"" IDFIELD=""EWayBillItemID""><COL KEY="" EWayBillID,LineSNum,Description,Details,Hsn_sc,Qty,TXVAL,Cess_RT""/><COL KEY=""Uqc"" CAPTION=""UnitName"" LOOKUPSQL=""Select Code, Code + ' - ' + Description from GstUqc""/><COL KEY=""RT"" FORMAT=""00.00"" CAPTION=""Rate"" VLIST=""0|5|12|18|28""/><COL KEY=""IAMT"" CAPTION=""IGST""/><COL KEY=""CAMT"" CAPTION=""CGST""/><COL KEY=""SAMT"" CAPTION=""SGST""/><COL KEY=""CSAMT"" CAPTION=""Cess""/></BAND>"
        myView.MainGrid.PrepEdit(strf1, EnumEditType.acCommandAndEdit)


        Me.FormPrepared = (Me.ErrorList.Count = 0)
        Return Me.FormPrepared
    End Function

    Private Sub BindDataTable(ByVal EwayBillID As Integer)
        Dim ds As DataSet, Sql As String

        Sql = "Select EWayBillItemID,EWayBillID,LineSNum,Description,Details,Hsn_sc,Uqc,Qty,TXVAL,RT,Cess_RT,IAMT,CAMT,SAMT,CSAMT from EWayBillItem where EWayBillID = " & EwayBillID & " "
        ds = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql)
        myUtils.AddTable(Me.dsForm, ds, "EWayBillItem", 0)

    End Sub

    Public Function FindCampusID(CampusID As Integer) As DataRow
        Dim rrCampus() As DataRow = Me.dsCombo.Tables("CampusID").Select("CampusID=" & CampusID)
        If rrCampus.Length > 0 Then Return rrCampus(0)
    End Function

    Public Overrides Function Validate() As Boolean
        Dim oMaster As New clsMasterDataFICO(myContext), LineSNum As Integer = 0
        Me.InitError()

        'Dim oProc As New clsGSTNReturnGSTR1(myContext)
        For Each r As DataRow In myView.MainGrid.myDV.Table.Select("", "linesnum")
            LineSNum = LineSNum + 1
            If myUtils.cValTN(r("IAMT")) > 0 Then
                myRow("Sply_Ty") = "INTER"
            Else
                myRow("Sply_Ty") = "INTRA"
            End If
            r("linesnum") = LineSNum
        Next


        'Validations

        If dicObjects Is Nothing Then dicObjects = New clsCollecString(Of DataRow) Else dicObjects.Clear()
        dicObjects.Add("gstreg", Me.SelectedRow("campusid"))
        dicObjects.Add("party", Me.SelectedRow("customerid"))
        dicObjects.Add("returnperiod", Me.SelectedRow("returnperiodid"))
        If myRow.Row.RowState = DataRowState.Modified Then
            Dim rPP = oMaster.rPostPeriod(myUtils.cValTN(myRow.Row("returnperiodid", DataRowVersion.Original)))
            dicObjects.Add("returnperiodbefore", rPP)
        End If

        If (Not Me.SelectedRow("campusid") Is Nothing) Then
            Dim rGstPP = oMaster.GstRegPPRow(Me.SelectedRow("campusid")("gstregid"), myUtils.cValTN(myRow("returnperiodid")))
            dicObjects.Add("GstRegPP", rGstPP)
        End If

        For Each str1 As String In New String() {"CampusID", "gstreg", "party", "transporter", "returnperiod", "returnperiodbefore", "GstRegPP"}
            If Not dicObjects.Exists(str1) Then dicObjects.Add(str1, Nothing)
        Next

        Dim oProc2 As New clsJintValidator(myContext)
        Dim dt1 = Me.dsCombo.Tables("rules")
        If dsCombo.Tables("options").Select.Length > 0 Then
            oProc2.AddOrUpdateRow(dsCombo.Tables("options").Rows(0), "")
        End If

        Dim rrEWBVeh() As DataRow = myVueVehicle.MainGrid.myDS.Tables(0).Select("", "transdocdate desc")
        Dim lst = oProc2.RunValidator(myRow.Row, Me.dsForm, "EWayBillItem", "item", dt1, Sub(obj, rItem)
                                                                                             If rItem Is Nothing Then
                                                                                                 If rrEWBVeh.Length > 0 Then obj.AddOrUpdateRow(rrEWBVeh(0), "vehicle") Else obj.AddOrUpdateRow(Nothing, "vehicle")
                                                                                                 obj.SetValue("GSTUtils", New clsJINTFuncsGSTN(myContext))
                                                                                                 For Each kvp In dicObjects
                                                                                                     obj.AddOrUpdateRow(kvp.Value, kvp.Key)
                                                                                                 Next

                                                                                             End If
                                                                                         End Sub)
        Me.AddError(lst)

        Return Me.CanSave
    End Function

    Public Overrides Function VSave() As Boolean
        VSave = False
        If Me.Validate Then
            If Me.CanSave Then
                Dim ewaybillDescrip As String = " EwayBillNo: " & myUtils.cStrTN(myRow("EWayBillNum")) & ", Dt. " & myUtils.cDateTN(myRow("EWayBillDate"), DateTime.MinValue)
                Try

                    myContext.Provider.dbConn.BeginTransaction(myContext, Me.Name, Me.frmMode.ToString, "ewaybillID", frmIDX)

                    Dim oProc As New clsGSTEWB(myContext)
                    oProc.PopulateCalc(myUtils.cValTN(myRow("ewaybillID")), myRow.Row, dicObjects("gstreg"), dsForm.Tables("EWayBillItem"), Nothing, Nothing, Nothing, dsCombo)

                    myContext.Provider.objSQLHelper.SaveResults(myRow.Row.Table, Me.sqlForm)
                    frmIDX = myRow("ewaybillID")

                    frmMode = EnumfrmMode.acEditM
                    myVueVehicle.MainGrid.SaveChanges(, "ewaybillID=" & frmIDX)
                    myUtils.ChangeAll(dsForm.Tables("EWayBillItem").Select, "ewaybillID=" & frmIDX)
                    myContext.Provider.objSQLHelper.SaveResults(dsForm.Tables("EWayBillItem"), "Select EWayBillItemID,EWayBillID,LineSNum,Description,Details,Hsn_sc,Uqc,Qty,TXVAL,RT,Cess_RT,IAMT,CAMT,SAMT,CSAMT from EWayBillItem")

                    myRow("lastvehicleid") = myUtils.MaxVal(myVueVehicle.MainGrid.myDS.Tables(0).Select, "ewaybillvehicleid")
                    myContext.Provider.objSQLHelper.SaveResults(myRow.Row.Table, Me.sqlForm)

                    myContext.Provider.dbConn.CommitTransaction(ewaybillDescrip, frmIDX)
                    VSave = True

                Catch ex As SqlException
                    myContext.Provider.dbConn.RollBackTransaction(ewaybillDescrip, ex.Message)
                    Me.AddError("", ex.Message)
                End Try
            End If
        End If
    End Function

    Public Overrides Function GenerateParamsModel(vwState As clsViewState, SelectionKey As String, Params As List(Of clsSQLParam)) As clsViewModel
        Dim Model As clsViewModel = Nothing, sql As String
        Dim oRet As clsProcOutput = myContext.SQL.ValidateSQLParams(Params)
        If oRet.Success Then
            Select Case SelectionKey.Trim.ToLower
                Case "transporter"
                    Model = myContext.Provider.GenerateSelectionModel(vwState, "<SYS ID=""VendorID""/>", False, , "<MODROW><SQLWHERE2>PartySubType = 'TR'</SQLWHERE2></MODROW>")
            End Select
        End If
        Return Model
    End Function

    Public Overrides Function GenerateParamsOutput(dataKey As String, Params As List(Of clsSQLParam)) As clsProcOutput
        Dim oRet As clsProcOutput = myContext.SQL.ValidateSQLParams(Params)
        If oRet.Success Then
            Dim GstRegID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@gstregid", Params))

            Select Case dataKey.Trim.ToLower
                Case "generate"
                    Dim EWayBillID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@ewaybillid", Params))
                    Dim oProc As New clsGSTNEwayBill(myContext)
                    Dim info = oProc.Generate(GstRegID, EWayBillID)
                    oRet.Success = (info.HttpStatusCode = HttpStatusCode.OK)
                    oRet.Message = info.Message
                    oRet.JsonData = New With {.status = 200, .success = oRet.Success, .message = oRet.Message, .Data = JsonConvert.SerializeObject(info.Data)}
                Case "cancel"
                    Dim EWayBillID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@ewaybillid", Params))
                    Dim oProc As New clsGSTNEwayBill(myContext)
                    Dim info = oProc.Cancel(GstRegID, EWayBillID)
                    oRet.Success = (info.HttpStatusCode = HttpStatusCode.OK)
                    oRet.Message = info.Message
                    oRet.JsonData = New With {.status = 200, .success = oRet.Success, .message = oRet.Message, .Data = JsonConvert.SerializeObject(info.Data)}

                Case "update"
                    Dim EWayBillVehicleID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@ewaybillvehicleid", Params))
                    Dim oProc As New clsGSTNEwayBill(myContext)
                    Dim info = oProc.Update(GstRegID, EWayBillVehicleID)
                    oRet.Success = (info.HttpStatusCode = HttpStatusCode.OK)
                    oRet.Message = info.Message
                    oRet.JsonData = New With {.status = 200, .success = oRet.Success, .message = oRet.Message, .Data = JsonConvert.SerializeObject(info.Data)}
            End Select

        End If
        Return oRet

    End Function
End Class




'Imports risersoft.shared
'Imports risersoft.app.mxent
'Imports System.Runtime.Serialization
'Imports System.Data.SqlClient
'Imports Newtonsoft.Json
'Imports System.Net

'<DataContract>
'Public Class frmEwayBillModel
'    Inherits clsFormDataModel

'    Protected Overrides Sub InitViews()
'        myView = Me.GetViewModel("PartB")
'    End Sub

'    Public Sub New(context As IProviderContext)
'        MyBase.New(context)
'        Me.InitViews()
'        Me.InitForm()
'    End Sub

'    Private Sub InitForm()
'        Dim sql As String

'        sql = "Select CampusID, DispName, CompanyID, CampusType,TaxAreaCode, DivisionCodeList, GstRegID, CampusCode from mmListCampus()  Order by DispName"
'        Me.AddLookupField("CampusID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Campus").TableName)

'        sql = myFuncsBase.CodeWordSQL("EWayBill", "SupplyType", "")
'        Me.AddLookupField("SupplyType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "SupplyType").TableName)

'        sql = myFuncsBase.CodeWordSQL("EWayBill", "TransactionType", "")
'        Me.AddLookupField("TransactionType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "TransactionType").TableName)

'        sql = myFuncsBase.CodeWordSQL("EWayBill", "DocumentType", "")
'        Me.AddLookupField("DocType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "DocumentType").TableName)

'        sql = "Select VendorID,isNull(GSTIN,'') + ' | ' + PartyName from purlistvendor() where partysubtype='TR'"
'        Me.AddLookupField("TransVendorID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Transporter").TableName)

'        sql = myFuncsBase.CodeWordSQL("EWayBill", "TransportationMode", "")
'        myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "TransMode")

'        sql = myFuncsBase.CodeWordSQL("EWayBill", "VehicleType", "")
'        Me.AddLookupField("VehicleType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "VehicleType").TableName)

'        sql = "select dbo.val(TINCode) as code, TinCode + ' | ' + Descrip, TaxAreaCode from TaxArea where dbo.val(TINCode)>0 Order by Descrip"
'        Me.AddLookupField("fromStateCode", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "StateCode").TableName)
'        Me.AddLookupField("toStateCode", "StateCode")
'        Me.AddLookupField("actToStateCode", "StateCode")
'        Me.AddLookupField("actFromStateCode", "StateCode")


'    End Sub

'    Public Overrides Function PrepForm(oView As clsViewModel, ByVal prepMode As EnumfrmMode, ByVal prepIDX As String, Optional ByVal strXML As String = "") As Boolean
'        Dim sql, str1 As String

'        Me.FormPrepared = False
'        If prepMode = EnumfrmMode.acAddM Then prepIDX = 0
'        sql = "Select * from Ewaybill Where EwayBillID = " & prepIDX
'        Me.InitData(sql, "InvoiceID,ODNoteID,ChallanID", oView, prepMode, prepIDX, strXML)

'        If prepMode = EnumfrmMode.acAddM Then
'            If myUtils.cValTN(myRow("InvoiceID")) > 0 Then
'                Dim dic2 As New clsCollecString(Of String)
'                dic2.Add("EWB", "select * from Ewaybill where Invoiceid = " & myRow("Invoiceid"))
'                Me.AddDataSet("EWB", dic2)
'                Dim dt1 As DataTable = DatasetCollection("EWB").Tables(0)
'                If dt1.Rows.Count > 0 Then
'                    If myUtils.NullNot(dt1.Rows(0)("ewaybillnum")) Then
'                        Me.AddError("", "This invoice has got an existing EwayBill, without an assigned EwayBill Number.Pl update that ewaybill instead of creating new by going to Ewaybill list.")
'                    End If
'                End If
'            Else
'                Dim dic2 As New clsCollecString(Of String)
'                dic2.Add("EWB", "select * from Ewaybill where ChallanID = " & myRow("ChallanID"))
'                Me.AddDataSet("EWB", dic2)
'                Dim dt1 As DataTable = DatasetCollection("EWB").Tables(0)
'                If dt1.Rows.Count > 0 Then
'                    If myUtils.NullNot(dt1.Rows(0)("ewaybillnum")) Then
'                        Me.AddError("", "This Challan has got an existing EwayBill, without an assigned EwayBill Number.Pl update that ewaybill instead of creating new by going to Ewaybill list.")
'                    End If
'                End If
'            End If
'        End If

'        Dim dic As New clsCollecString(Of String), dt2 As DataTable
'        If myUtils.cValTN(myRow("InvoiceID")) > 0 Then
'            dic.Add("party", "Select * from gstListEwbInv() where Invoiceid = " & myRow("Invoiceid"))
'        Else
'            dic.Add("party", "Select * from gstListEwbChl() where Challanid = " & myRow("Challanid"))
'        End If
'        Me.AddDataSet("party", dic)
'        dt2 = DatasetCollection("party").Tables(0)
'        If dt2.Select.Length > 0 Then
'            myRow("fromaddr1") = dt2.Rows(0)("fromAddr1")
'            myRow("fromaddr2") = dt2.Rows(0)("fromAddr2")
'            myRow("fromPlace") = dt2.Rows(0)("fromPlace")
'            myRow("fromPinCode") = myUtils.cValTN(dt2.Rows(0)("fromPinCode"))
'            myRow("fromStateCode") = dt2.Rows(0)("fromStateCode")
'            myRow("actfromStateCode") = dt2.Rows(0)("actfromStateCode")
'            myRow("toaddr1") = dt2.Rows(0)("toAddr1")
'            myRow("toaddr2") = dt2.Rows(0)("toAddr2")
'            myRow("toPlace") = dt2.Rows(0)("toPlace")
'            myRow("toPinCode") = myUtils.cValTN(dt2.Rows(0)("toPinCode"))
'            myRow("toStateCode") = dt2.Rows(0)("toStateCode")
'            myRow("acttoStateCode") = dt2.Rows(0)("acttoStateCode")
'        End If


'        If myUtils.cValTN(myRow("InvoiceID")) > 0 Then
'            Dim dic1 As New clsCollecString(Of String)
'            dic1.Add("invoice", "select distinct InvoiceID,CampusID,ConsigneeID,InvoiceNum,InvoiceDate,doctype,PartyTaxAddress,CampusTaxAddress,ConsigneeTaxAddress from gstListInvoiceItem() where Invoiceid = " & myRow("Invoiceid"))
'            Me.AddDataSet("invoice", dic1)
'            Dim dt As DataTable = DatasetCollection("invoice").Tables(0)
'            myRow("CampusID") = dt.Rows(0)("CampusID")
'            If frmMode = EnumfrmMode.acAddM Then
'                myRow("doctype") = "INV"
'                If dt.Rows(0)("doctype") = "IP" Then
'                    myRow("SupplyType") = "I"
'                Else
'                    myRow("SupplyType") = "O"
'                End If
'            End If
'            sql = myFuncsBase.CodeWordSQL("EWayBill", "SubSupplyType", "Codeword in (1,2,3,9)")
'            Me.AddLookupField("SubSupplyType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "SubSupplyType").TableName)
'        End If

'        If myUtils.cValTN(myRow("ChallanID")) > 0 Then
'            Dim dic1 As New clsCollecString(Of String)
'            dic1.Add("challan", "select distinct ChallanID,CampusID,ChNum,ChDt,ChallanType,TransactionType from gstListChallanItem() where Challanid = " & myRow("Challanid"))
'            Me.AddDataSet("challan", dic1)
'            Dim dt As DataTable = DatasetCollection("challan").Tables(0)
'            myRow("CampusID") = dt.Rows(0)("CampusID")
'            If frmMode = EnumfrmMode.acAddM Then
'                myRow("doctype") = "CHL"
'                If dt.Rows(0)("ChallanType") = "SENT" Then
'                    myRow("SupplyType") = "O"
'                End If
'                If dt.Rows(0)("ChallanType") = "BACK" AndAlso dt.Rows(0)("TransactionType") = "SENT" Then
'                    myRow("SupplyType") = "O"
'                Else
'                    myRow("SupplyType") = "I"
'                End If
'            End If
'            sql = myFuncsBase.CodeWordSQL("EWayBill", "SubSupplyType", "Codeword in (4,5,6,7,8,9,10,11,12)")
'            Me.AddLookupField("SubSupplyType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "SubSupplyType").TableName)
'        End If

'        sql = "Select EWayBillVehicleID,EWayBillID,TransMode,TransDocNo,TransDocDate,VehicleNo,VehicleType,FromPlace,EnteredDate,UserGSTINTransin,TripshtNo from EWayBillVehicle where EWayBillID = " & frmIDX & ""
'        myView.MainGrid.MainConf("FORMATXML") = "<COL KEY=""TransMode"" CAPTION=""Mode""/><COL KEY=""FromPlace"" CAPTION=""From""/><COL KEY=""UserGSTINTransin"" CAPTION=""Entered By""/><COL KEY=""TripshtNo"" CAPTION=""CEWB No.""/>"
'        myView.MainGrid.QuickConf(sql, True, "1-1-1-1-1-1-1-1-1", True)
'        str1 = "<BAND INDEX=""0"" TABLE=""EWayBillVehicle"" IDFIELD=""EWayBillVehicleID""><COL KEY="" EWayBillID,TransDocNo,TransDocDate,VehicleNo,VehicleType,FromPlace,EnteredDate,UserGSTINTransin,TripshtNo ""/><COL KEY=""TransMode"" LOOKUPSQL=""" & myFuncsBase.CodeWordSQL("EWayBill", "TransportationMode", "") & """/></BAND>"
'        myView.MainGrid.PrepEdit(str1, EnumEditType.acCommandOnly)

'        Me.FormPrepared = (Me.ErrorList.Count = 0)
'        Return Me.FormPrepared
'    End Function

'    Public Overrides Function Validate() As Boolean
'        Me.InitError()

'        Return Me.CanSave
'    End Function

'    Public Overrides Function VSave() As Boolean
'        VSave = False
'        If Me.Validate Then
'            If Me.CanSave Then
'                Dim ewaybillDescrip As String = " EwayBillNo: " & myUtils.cStrTN(myRow("EWayBillNum")) & ", Dt. " & myUtils.cDateTN(myRow("EWayBillDate"), DateTime.MinValue)
'                Try
'                    If myUtils.cValTN(myRow("InvoiceID")) > 0 Then myRow("doctype") = "INV" Else myRow("doctype") = "CHL"

'                    myContext.Provider.dbConn.BeginTransaction(myContext, Me.Name, Me.frmMode.ToString, "ewaybillID", frmIDX)
'                    Select Case myUtils.cStrTN(myRow("doctype")).Trim.ToUpper
'                        Case "INV"
'                            myRow("documentid") = myRow("InvoiceID")
'                        Case "CHL"
'                            myRow("documentid") = myRow("ChallanID")
'                    End Select

'                    myContext.Provider.objSQLHelper.SaveResults(myRow.Row.Table, Me.sqlForm)
'                    frmIDX = myRow("ewaybillID")

'                    frmMode = EnumfrmMode.acEditM
'                    myView.MainGrid.SaveChanges(, "ewaybillID=" & frmIDX)

'                    myRow("lastvehicleid") = myUtils.MaxVal(myView.MainGrid.myDS.Tables(0).Select, "ewaybillvehicleid")
'                    myContext.Provider.objSQLHelper.SaveResults(myRow.Row.Table, Me.sqlForm)

'                    myContext.Provider.dbConn.CommitTransaction(ewaybillDescrip, frmIDX)
'                    VSave = True

'                Catch ex As SqlException
'                    myContext.Provider.dbConn.RollBackTransaction(ewaybillDescrip, ex.Message)
'                    Me.AddError("", ex.Message)
'                End Try
'            End If
'        End If
'    End Function

'    Public Overrides Function GenerateParamsModel(vwState As clsViewState, SelectionKey As String, Params As List(Of clsSQLParam)) As clsViewModel
'        Dim Model As clsViewModel = Nothing, sql As String
'        Dim oRet As clsProcOutput = myContext.SQL.ValidateSQLParams(Params)
'        If oRet.Success Then
'            Select Case SelectionKey.Trim.ToLower
'                Case "transporter"
'                    Model = myContext.Provider.GenerateSelectionModel(vwState, "<SYS ID=""VendorID""/>", False, , "<MODROW><SQLWHERE2>PartySubType = 'TR'</SQLWHERE2></MODROW>")
'            End Select
'        End If
'        Return Model
'    End Function

'    Public Overrides Function GenerateParamsOutput(dataKey As String, Params As List(Of clsSQLParam)) As clsProcOutput
'        Dim oRet As clsProcOutput = myContext.SQL.ValidateSQLParams(Params)
'        If oRet.Success Then
'            Dim GstRegID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@gstregid", Params))

'            Select Case dataKey.Trim.ToLower
'                Case "generate"
'                    Dim EWayBillID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@ewaybillid", Params))
'                    Dim oProc As New clsGSTNEwayBill(myContext)
'                    Dim info = oProc.Generate(GstRegID, EWayBillID)
'                    oRet.Success = (info.HttpStatusCode = HttpStatusCode.OK)
'                    oRet.Message = info.Message
'                    oRet.JsonData = JsonConvert.SerializeObject(info.Data)
'                Case "cancel"
'                    Dim EWayBillID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@ewaybillid", Params))
'                    Dim oProc As New clsGSTNEwayBill(myContext)
'                    Dim info = oProc.Cancel(GstRegID, EWayBillID)
'                    oRet.Success = (info.HttpStatusCode = HttpStatusCode.OK)
'                    oRet.Message = info.Message
'                    oRet.JsonData = JsonConvert.SerializeObject(info.Data)

'                Case "update"
'                    Dim EWayBillVehicleID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@ewaybillvehicleid", Params))
'                    Dim oProc As New clsGSTNEwayBill(myContext)
'                    Dim info = oProc.Update(GstRegID, EWayBillVehicleID)
'                    oRet.Success = (info.HttpStatusCode = HttpStatusCode.OK)
'                    oRet.Message = info.Message
'                    oRet.JsonData = JsonConvert.SerializeObject(info.Data)
'            End Select

'        End If
'        Return oRet
'    End Function
'End Class
