Imports risersoft.shared
Imports risersoft.app.mxent
Imports System.Runtime.Serialization
Imports System.Data.SqlClient

<DataContract>
Public Class frmEwayBillModel
    Inherits clsFormDataModel

    Protected Overrides Sub InitViews()
        myView = Me.GetViewModel("PartB")
    End Sub

    Public Sub New(context As IProviderContext)
        MyBase.New(context)
        Me.InitViews()
        Me.InitForm()
    End Sub

    Private Sub InitForm()
        Dim sql As String

        sql = "Select CampusID, DispName, CompanyID, CampusType,TaxAreaCode, DivisionCodeList, GstRegID, CampusCode from mmListCampus()  Order by DispName"
        Me.AddLookupField("CampusID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Campus").TableName)

        sql = myFuncsBase.CodeWordSQL("EWayBill", "SupplyType", "")
        Me.AddLookupField("SupplyType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "SupplyType").TableName)

        sql = myFuncsBase.CodeWordSQL("EWayBill", "SubSupplyType", "")
        Me.AddLookupField("SubSupplyType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "SubSupplyType").TableName)

        sql = myFuncsBase.CodeWordSQL("EWayBill", "DocumentType", "")
        Me.AddLookupField("DocumentType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "DocumentType").TableName)

        sql = "Select VendorID,isNull(TransporterGSTIN,'') + ' | ' + TransporterName from gstlistEWB()"
        Me.AddLookupField("TransporterID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Transporter").TableName)

        sql = myFuncsBase.CodeWordSQL("EWayBill", "TransportationMode", "")
        Me.AddLookupField("TransportationMode", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "TransMode").TableName)

    End Sub

    Public Overrides Function PrepForm(oView As clsViewModel, ByVal prepMode As EnumfrmMode, ByVal prepIDX As String, Optional ByVal strXML As String = "") As Boolean
        Dim sql, str1 As String

        Me.FormPrepared = False
        If prepMode = EnumfrmMode.acAddM Then prepIDX = 0
        sql = "Select * from Ewaybill Where EwayBillID = " & prepIDX
        Me.InitData(sql, "InvoiceID,ODNoteID", oView, prepMode, prepIDX, strXML)

        myRow("doctype") = "INV"
        myRow("EwayBillNum") = "481000066539"
        myRow("EwayBillDate") = "2018-01-29 16:36:00.000"

        Dim dic1 As New clsCollecString(Of String)
        dic1.Add("invoice", "select InvoiceID,InvoiceNum,InvoiceDate,CampusID,DocType from Invoice where Invoiceid = " & myRow("Invoiceid"))
        Me.AddDataSet("invoice", dic1)
        Dim dt As DataTable = DatasetCollection("invoice").Tables(0)
        If frmMode = EnumfrmMode.acAddM Then
            myRow("Campusid") = dt.Rows(0)("Campusid")
            If dt.Rows(0)("DocType") = "IP" Then
                myRow("SupplyType") = "I"
            Else
                myRow("SupplyType") = "O"
            End If
        End If

        sql = "Select EWayBillVehicleID,EWayBillID,TransMode,VehicleNo,FromPlace,EnteredDate,UserGSTINTransin,TripshtNo from EWayBillVehicle where EWayBillID = " & frmIDX & ""
        myView.MainGrid.myCMain("FORMATXML") = "<COL KEY=""TransMode"" CAPTION=""Mode""/><COL KEY=""FromPlace"" CAPTION=""From""/><COL KEY=""UserGSTINTransin"" CAPTION=""Entered By""/><COL KEY=""TripshtNo"" CAPTION=""CEWB No.""/>"
        myView.MainGrid.QuickConf(sql, True, "1-1-1-1-1-1", True)
        str1 = "<BAND INDEX=""0"" TABLE=""EWayBillVehicle"" IDFIELD=""EWayBillVehicleID""><COL KEY="" EWayBillID,TransMode,VehicleNo,FromPlace,EnteredDate,UserGSTINTransin,TripshtNo ""/></BAND>"
        myView.MainGrid.PrepEdit(str1, EnumEditType.acCommandOnly)

        Me.FormPrepared = True
        Return Me.FormPrepared
    End Function

    Public Overrides Function Validate() As Boolean
        Me.InitError()
        'If Me.SelectedRow("CampusID") Is Nothing Then Me.AddError("CampusID", "Must Select Campus")
        'If Me.SelectedRow("SupplyType") Is Nothing Then Me.AddError("SupplyType", "Enter SupplyType")
        'If Me.SelectedRow("SubSupplyType") Is Nothing Then Me.AddError("SubSupplyType", "Enter SubSupplyType")
        Return Me.CanSave
    End Function

    Public Overrides Function VSave() As Boolean
        VSave = False
        If Me.Validate Then
            If Me.CanSave Then
                Try
                    myContext.Provider.dbConn.BeginTransaction(myContext)
                    Select Case myUtils.cStrTN(myRow("doctype")).Trim.ToUpper
                        Case "INV"
                            myRow("documentid") = myRow("invoiceid")
                        Case "CHL"
                            myRow("documentid") = myRow("odnoteid")
                    End Select
                    myContext.Provider.objSQLHelper.SaveResults(myRow.Row.Table, Me.sqlForm)
                    frmIDX = myRow("ewaybillID")
                    myView.MainGrid.SaveChanges(, "ewaybillID=" & frmIDX)
                    frmMode = EnumfrmMode.acEditM
                    myContext.Provider.dbConn.CommitTransaction()
                    VSave = True
                Catch e As Exception
                    myContext.Provider.dbConn.RollBackTransaction()
                    Me.AddError("", e.Message)
                End Try
            End If
        End If
    End Function

    Public Overrides Function GenerateParamsModel(vwState As clsViewState, SelectionKey As String, Params As List(Of clsSQLParam)) As clsViewModel
        Dim Model As clsViewModel = Nothing
        Dim oRet As clsProcOutput = myContext.SQL.ValidateSQLParams(Params)
        If oRet.Success Then
            Select Case SelectionKey.Trim.ToLower
                Case "transporter"
                    Model = myContext.Provider.GenerateSelectionModel(vwState, "<SYS ID=""VendorID""/>", False)
            End Select
        End If
        Return Model
    End Function

End Class
