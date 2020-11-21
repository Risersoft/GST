Public Class frmGstPurchase

End Class

'**********************************
'Imports Infragistics.Win.UltraWinGrid
'Imports risersoft.app.mxform.gst
'Imports risersoft.shared.Extensions

'Public Class frmGstInvoicePurch
'    Inherits frmMax
'    Friend fItem As frmInvoiceItemPurch
'    Dim dvDivision, dvParty As DataView, OrgInvoiceType As String = ""
'    Dim oSort As clsWinSort
'    Dim myVueTXPD As New clsWinView

'    Public Sub New()
'        MyBase.New()
'        InitializeComponent()
'        Me.InitForm()
'    End Sub

'    Public Sub InitForm()
'        WinFormUtils.SetButtonConf(Me.btnOK, Me.btnCancel, Me.btnSave)
'        myView.SetGrid(UltraGridItemList)
'        myVueTXPD.SetGrid(UltraGridTXPD)

'        fItem = New frmInvoiceItemPurch
'        fItem.AddToPanel(Me.UltraExpandableGroupBoxPanel2, True, System.Windows.Forms.DockStyle.Fill)
'        fItem.Enabled = False
'        fItem.fMat = Me
'    End Sub

'    Public Overrides Function PrepForm(oView As clsWinView, ByVal prepMode As EnumfrmMode, ByVal prepIdx As String, Optional ByVal strXML As String = "") As Boolean
'        Me.FormPrepared = False
'        Dim objModel As frmGstInvoicePurchModel = Me.InitData("frmGstInvoicePurchModel", oview, prepMode, prepIdx, strXML)
'        If Me.BindModel(objModel, oview) Then
'            If myUtils.cValTN(myRow("ConsigneeID")) > 0 Then cmb_ConsigneeID.Value = myUtils.cValTN(myRow("ConsigneeID"))
'            Me.cmb_VendorID.Value = myRow("VendorID")
'            If Me.cmb_VendorID.SelectedRow IsNot Nothing Then Me.txt_CTIN.Text = myUtils.cStrTN(Me.cmb_VendorID.SelectedRow.Cells("gstin").Value)
'            myRow("POSTaxAreaID") = HandleConsigneeID()

'            If Me.Model.DatasetCollection.ContainsKey("OrgInvoice") Then
'                UltraPanelAmndment.Visible = True
'                Dim dt As DataTable = Me.Model.DatasetCollection("OrgInvoice").Tables(0)
'                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
'                    lblInvoiceNo.Text = "Invoice No.  " & " - " & dt.Rows(0)("InvoiceNum")
'                    lblInvoiceDate.Text = "Invoice Date" & " - " & dt.Rows(0)("InvoiceDate")
'                    lblPartyName.Text = "Party Name" & " - " & dt.Rows(0)("PartyName")
'                    lblPartyGSTIN.Text = "Party GSTIN" & " - " & dt.Rows(0)("CTIN")

'                    OrgInvoiceType = myUtils.cStrTN(dt.Rows(0)("GstInvoiceType"))
'                End If
'            Else
'                UltraPanelAmndment.Visible = False
'            End If

'            Me.SetControlsValue(Me.UltraTabControl1.Tabs(0).TabPage)
'            oSort = New clsWinSort(myView, Me.btnUp, Me.btnDown, Nothing, "SortIndex")
'            oSort.renumber()

'            HandleCampus()
'            HandleGstInvoiceType(myUtils.cStrTN(myRow("GstInvoiceType")), myUtils.cValTN(myRow("vendorid")))
'            HandleIsSEZ(myUtils.cStrTN(myRow("is_sez")))

'            fItem.UltraTabControl1.Tabs("Details").Selected = True

'            If myUtils.IsInList(myUtils.cStrTN(Me.cmb_GstInvoiceType.Value), "IMPG") Then
'                cmb_RCHRG.Value = "N"
'                cmb_RCHRG.ReadOnly = True
'            End If

'            If myUtils.cValTN(myRow("CDNInvoiceID")) > 0 Then
'                cmb_RCHRG.ReadOnly = True
'                cmb_sply_ty.ReadOnly = True
'                cmb_POSTaxAreaID.ReadOnly = True
'            End If

'            If cmb_RCHRG.Value = "N" OrElse myUtils.cValTN(myRow("CDNInvoiceID")) > 0 Then
'                UltraTabControl1.Tabs("TXPD").Visible = False
'            End If

'            Me.FormPrepared = True
'        End If
'        Return Me.FormPrepared
'    End Function

'    Public Overrides Function BindModel(NewModel As clsFormDataModel, oView As clsView) As Boolean
'        If MyBase.BindModel(NewModel, oView) Then
'            myView.PrepEdit(Me.Model.GridViews("ItemList"))
'            myVueTXPD.PrepEdit(Me.Model.GridViews("TaxPaid"))
'            myWinSQL.AssignCmb(Me.dsCombo, "Campus", "", Me.cmb_CampusID)
'            dvParty = myWinSQL.AssignCmb(Me.dsCombo, "Vendor", "", Me.cmb_VendorID, , 2)
'            dvDivision = myWinSQL.AssignCmb(Me.dsCombo, "Division", "", Me.cmb_DivisionID, , 2)
'            myWinSQL.AssignCmb(Me.dsCombo, "GstInvoiceType", "", Me.cmb_GstInvoiceType)
'            myWinSQL.AssignCmb(Me.dsCombo, "SupplyType", "", Me.cmb_sply_ty)
'            myWinSQL.AssignCmb(Me.dsCombo, "B2BType", "", Me.cmb_inv_typ)
'            myWinSQL.AssignCmb(Me.dsCombo, "POS", "", Me.cmb_POSTaxAreaID)
'            Me.cmb_RCHRG.ValueList = Me.Model.ValueLists("RCHRG").ComboList
'            Me.cmb_Diff_Percent.ValueList = Me.Model.ValueLists("Diff_Percent").ComboList
'            Me.cmb_IsAmendment.ValueList = Me.Model.ValueLists("IsAmendment").ComboList
'            myWinSQL.AssignCmb(Me.dsCombo, "CDNType", "", Me.cmb_ntty)
'            Me.cmb_p_gst.ValueList = Me.Model.ValueLists("PreGST").ComboList
'            Me.cmb_is_sez.ValueList = Me.Model.ValueLists("IsSEZ").ComboList
'            myWinSQL.AssignCmb(Me.dsCombo, "Party", "", Me.cmb_ConsigneeID)

'            fItem.BindModel(NewModel)
'            Return True
'        End If
'        Return False
'    End Function

'    Public Overrides Function VSave() As Boolean
'        Me.InitError()
'        VSave = False
'        cm.EndCurrentEdit()
'        CalculateTotalAmount()
'        If fItem.VSave And Me.ValidateData() Then
'            If Me.SaveModel() Then
'                Return True
'            End If
'        Else
'            Me.SetError()
'        End If
'        Me.Refresh()
'    End Function

'    Private Sub CalculateTax(r1 As DataRow)
'        If myUtils.cValTN(r1("rt")) > 0 AndAlso myUtils.cValTN(r1("TXVal")) > 0 Then
'            Dim TaxAmt As Double = (myUtils.cValTN(r1("TXVal")) * myUtils.cValTN(r1("RT")) * myUtils.cValTN(myRow("Diff_Percent"))) / 10000
'            If myUtils.IsInList(myUtils.cStrTN(cmb_sply_ty.Value), "INTER") Then
'                r1("IAMT") = TaxAmt
'                r1("CAMT") = 0
'                r1("SAMT") = 0
'            Else
'                r1("CAMT") = TaxAmt / 2
'                r1("SAMT") = TaxAmt / 2
'                r1("IAMT") = 0
'            End If
'        Else
'            If myUtils.cValTN(r1("rt")) = 0 Then
'                r1("IAMT") = 0
'                r1("CAMT") = 0
'                r1("SAMT") = 0
'                r1("CSAMT") = 0
'            End If
'        End If

'    End Sub

'    Private Sub CalculateTotalAmount()
'        Dim TotalAmt As Decimal = 0
'        cm.EndCurrentEdit()
'        For Each r1 As DataRow In myView.mainGrid.myDv.Table.Select
'            CalculateTax(r1)
'            If myUtils.cStrTN(myRow("RCHRG")) = "Y" Then
'                TotalAmt = myUtils.cValTN(TotalAmt) + myUtils.cValTN(r1("TXVal"))
'            Else
'                TotalAmt = myUtils.cValTN(TotalAmt) + myUtils.cValTN(r1("TXVal")) + myUtils.cValTN(r1("CSAMT")) + myUtils.cValTN(r1("IAMT")) + myUtils.cValTN(r1("CAMT")) + myUtils.cValTN(r1("SAMT"))
'            End If
'        Next

'        txt_VAL.Value = TotalAmt
'    End Sub

'    Private Sub UltraGridItemList_AfterRowActivate(sender As Object, e As EventArgs) Handles UltraGridItemList.AfterRowActivate
'        Me.InitError()
'        myView.mainGrid.myGrid.UpdateData()

'        Dim r1 As DataRow = myWinUtils.DataRowFromGridRow(myView.mainGrid.myGrid.ActiveRow)
'        fItem.PrepForm(r1, "InvoiceItemGstID")

'        fItem.Enabled = True
'    End Sub

'    Private Sub UltraGridItemList_BeforeRowDeactivate(sender As Object, e As ComponentModel.CancelEventArgs) Handles UltraGridItemList.BeforeRowDeactivate
'        If fItem.VSave Then
'        Else
'            e.Cancel = True
'        End If
'    End Sub


'    Private Sub HandleCampus()
'        dvDivision.RowFilter = FilterDivision(Me.Controller, cmb_CampusID, Nothing)
'        If frmMode = EnumfrmMode.acAddM AndAlso cmb_DivisionID.Rows.Count = 1 Then cmb_DivisionID.Value = myUtils.cValTN(cmb_DivisionID.Rows(0).Cells("DivisionID").Value)
'        If cmb_DivisionID.SelectedRow Is Nothing Then cmb_DivisionID.Value = DBNull.Value

'        If Not IsNothing(cmb_CampusID.SelectedRow) AndAlso (Not IsNothing(cmb_GstInvoiceType.SelectedRow)) AndAlso (Not IsNothing(cmb_sply_ty.SelectedRow)) Then
'            EnableCtlCampus(True)
'        Else
'            EnableCtlCampus(False)
'        End If
'    End Sub

'    Public Function DivisionFilter(cmb1 As UltraCombo, cmb2 As UltraCombo) As String
'        Dim CodeList As New List(Of String)
'        If Not IsNothing(cmb1) AndAlso Not IsNothing(cmb1.SelectedRow) Then CodeList.AddRange(Split(myUtils.cStrTN(cmb1.SelectedRow.Cells("DivisionCodeList").Value), ","))
'        If Not IsNothing(cmb2) AndAlso Not IsNothing(cmb2.SelectedRow) Then CodeList.AddRange(Split(myUtils.cStrTN(cmb2.SelectedRow.Cells("DivisionCodeList").Value), ","))
'        Dim str1 As String = myUtils.MakeCSV2(",", "'0'", True, CodeList.ToArray)
'        Dim str2 As String = "DivisionCode in (" & str1 & ")"

'        Return str2
'    End Function

'    Public Function FilterDivision(Controller As clsWinController, cmb1 As Infragistics.Win.UltraWinGrid.UltraCombo, cmb2 As Infragistics.Win.UltraWinGrid.UltraCombo) As String
'        Dim str As String = "0=1"
'        Dim str1 As String = Controller.AppModel.strFilterDBAppUser("DivisionID")
'        Dim str2 As String = DivisionFilter(cmb1, cmb2)
'        If str1.Trim.Length > 0 OrElse str2.Trim.Length > 0 Then str = myUtils.CombineWhere(False, str1, str2)
'        Return str
'    End Function

'    Private Sub EnableCtlCampus(Bool As Boolean)
'        If myView.mainGrid.myDv.Table.Select.Length > 0 Then btnAddSerial.Enabled = True Else btnAddSerial.Enabled = Bool
'        If myView.mainGrid.myDv.Table.Select.Length > 0 Then btnDelItem.Enabled = True Else btnDelItem.Enabled = Bool
'    End Sub

'    Private Sub cmb_campusid_Leave(sender As Object, e As EventArgs) Handles cmb_CampusID.Leave, cmb_CampusID.AfterCloseUp
'        HandleCampus()
'    End Sub

'    Private Sub cmb_PartyID_Leave(sender As Object, e As EventArgs) Handles cmb_VendorID.Leave, cmb_VendorID.AfterCloseUp
'        If (Not IsNothing(cmb_VendorID.SelectedRow)) Then txt_CTIN.Text = myUtils.cStrTN(cmb_VendorID.SelectedRow.Cells("GSTIN").Value)
'    End Sub

'    Private Sub cmb_GstInvoiceType_Leave(sender As Object, e As EventArgs) Handles cmb_GstInvoiceType.Leave, cmb_GstInvoiceType.AfterCloseUp
'        If Not IsNothing(cmb_CampusID.SelectedRow) AndAlso (Not IsNothing(cmb_GstInvoiceType.SelectedRow)) AndAlso (Not IsNothing(cmb_sply_ty.SelectedRow)) Then
'            EnableCtlCampus(True)
'        Else
'            EnableCtlCampus(False)
'        End If

'        HandleGstInvoiceType(myUtils.cStrTN(cmb_GstInvoiceType.Value), 0)
'        If myUtils.IsInList(myUtils.cStrTN(Me.cmb_GstInvoiceType.Value), "IMPG") Then
'            cmb_RCHRG.Value = "N"
'            cmb_RCHRG.ReadOnly = True
'        End If

'        If frmMode = EnumfrmMode.acAddM AndAlso myUtils.IsInList(myUtils.cStrTN(Me.cmb_GstInvoiceType.Value), "B2B") Then
'            cmb_inv_typ.Value = "R"
'        End If

'    End Sub

'    Private Sub HandleGstInvoiceType(GstInvoiceType As String, VendorID As Integer)
'        If Not myUtils.IsInList(myUtils.cStrTN(GstInvoiceType), "") Then
'            If myUtils.IsInList(myUtils.cStrTN(GstInvoiceType), "B2B", "CDN") Then
'                dvParty.RowFilter = myUtils.CombineWhereOR(False, "len(isnull(gstin,''))>0", "vendorid=" & VendorID)
'            Else
'                dvParty.RowFilter = myUtils.CombineWhereOR(False, "len(isnull(gstin,''))=0", "vendorid=" & VendorID)
'            End If
'            HideTab(GstInvoiceType)
'        Else
'            HideTab(GstInvoiceType)
'            dvParty.RowFilter = "0=1"
'        End If
'        If (IsNothing(cmb_VendorID.SelectedRow)) Then
'            cmb_VendorID.Value = DBNull.Value
'            txt_CTIN.Value = DBNull.Value
'        End If
'    End Sub

'    Private Sub HideTab(GstInvoiceType As String)
'        UltraTabControl1.Tabs("IMPG").Visible = (myUtils.IsInList(myUtils.cStrTN(GstInvoiceType), "IMPG"))
'        UltraTabControl1.Tabs("B2B").Visible = (myUtils.IsInList(myUtils.cStrTN(GstInvoiceType), "B2B"))
'        UltraTabControl1.Tabs("CDN").Visible = (myUtils.IsInList(myUtils.cStrTN(GstInvoiceType), "CDN", "CDNUR"))

'        cmb_RCHRG.ReadOnly = False
'        If myUtils.IsInList(myUtils.cStrTN(GstInvoiceType), "IMPG", "IMPS") Then
'            cmb_sply_ty.Value = "INTER"
'            cmb_sply_ty.ReadOnly = True
'        Else
'            cmb_sply_ty.ReadOnly = False
'        End If
'    End Sub

'    Private Sub cmb_sply_ty_Leave(sender As Object, e As EventArgs) Handles cmb_sply_ty.Leave, cmb_sply_ty.AfterCloseUp
'        If Not IsNothing(cmb_CampusID.SelectedRow) AndAlso (Not IsNothing(cmb_GstInvoiceType.SelectedRow)) AndAlso (Not IsNothing(cmb_sply_ty.SelectedRow)) Then
'            EnableCtlCampus(True)
'        Else
'            EnableCtlCampus(False)
'        End If
'        CalculateTotalAmount()
'    End Sub

'    Private Sub cmb_is_sez_Leave(sender As Object, e As EventArgs) Handles cmb_is_sez.Leave, cmb_is_sez.AfterCloseUp
'        HandleIsSEZ(myUtils.cStrTN(cmb_is_sez.Value))
'    End Sub

'    Private Sub HandleIsSEZ(IzSez As String)
'        If Not myUtils.IsInList(myUtils.cStrTN(IzSez), "") Then
'            If myUtils.IsInList(myUtils.cStrTN(IzSez), "Y") Then
'                txt_stin.ReadOnly = False
'            Else
'                txt_stin.ReadOnly = True
'                txt_stin.Text = String.Empty
'            End If
'        Else
'            txt_stin.Text = String.Empty
'            txt_stin.ReadOnly = True
'        End If
'    End Sub

'    Private Sub cmb_ConsigneeID_Leave(sender As Object, e As EventArgs) Handles cmb_ConsigneeID.Leave, cmb_ConsigneeID.AfterCloseUp
'        Me.cmb_POSTaxAreaID.Value = HandleConsigneeID()
'    End Sub

'    Private Function HandleConsigneeID() As Integer
'        Dim POSTaxAreaID As Integer
'        If myUtils.cValTN(cmb_ConsigneeID.Value) > 0 Then
'            POSTaxAreaID = myUtils.cValTN(cmb_ConsigneeID.SelectedRow.Cells("TaxAreaID").Value)
'            cmb_POSTaxAreaID.ReadOnly = True
'        Else
'            POSTaxAreaID = myUtils.cValTN(myRow("POSTaxAreaID"))
'            cmb_POSTaxAreaID.ReadOnly = False
'        End If
'        Return POSTaxAreaID
'    End Function

'    Private Sub btnAddTXPD_Click(sender As Object, e As EventArgs) Handles btnAddTXPD.Click
'        Dim gr As UltraGridRow
'        gr = myVueTXPD.mainGrid.ButtonAction("add")
'    End Sub

'    Private Sub btnDeleteTXPD_Click(sender As Object, e As EventArgs) Handles btnDeleteTXPD.Click
'        myVueTXPD.mainGrid.ButtonAction("del")
'    End Sub

'    Private Sub UltraGridTXPD_AfterCellUpdate(sender As Object, e As CellEventArgs) Handles UltraGridTXPD.AfterCellUpdate
'        If myUtils.IsInList(e.Cell.Column.Key, "RT", "AD_AMT") Then

'            If myUtils.IsInList(cmb_sply_ty.Value, "INTER") Then
'                e.Cell.Row.Cells("IAMT").Value = myUtils.cValTN(e.Cell.Row.Cells("RT").Value) * myUtils.cValTN(e.Cell.Row.Cells("AD_AMT").Value) / 100
'            Else
'                Dim taxAmt As Decimal = myUtils.cValTN(e.Cell.Row.Cells("RT").Value) * myUtils.cValTN(e.Cell.Row.Cells("AD_AMT").Value) / 100
'                e.Cell.Row.Cells("CAMT").Value = taxAmt / 2
'                e.Cell.Row.Cells("SAMT").Value = taxAmt / 2
'            End If
'        End If
'    End Sub

'    Private Sub btnAddSerial_Click(sender As Object, e As EventArgs) Handles btnAddSerial.Click
'        If myView.mainGrid.myDv.Count = 0 OrElse fItem.VSave Then
'            Dim gr As UltraGridRow
'            gr = myView.mainGrid.myGrid.DisplayLayout.Bands(0).AddNew()
'            gr.Cells("SortIndex").Value = myUtils.MaxVal(myView.mainGrid.myDv.Table, "SortIndex") + 1
'            myView.mainGrid.UpdateData()
'            oSort.renumber()

'            fItem.Focus()
'        End If
'    End Sub

'    Private Sub btnDelItem_Click(sender As Object, e As EventArgs) Handles btnDelItem.Click
'        myView.mainGrid.ButtonAction("del")
'        If myView.mainGrid.myDv.Table.Select.Length = 0 Then
'            fItem.FormPrepared = False
'            oSort.renumber()
'            fItem.Enabled = False
'            EnableCtlCampus(True)
'        End If
'    End Sub
'End Class



'******************************************************
'Model Code

'Imports risersoft.shared
'Imports risersoft.app.mxent
'Imports System.Runtime.Serialization
'Imports System.Data.SqlClient

'<DataContract>
'Public Class frmGstInvoicePurchModel
'    Inherits clsFormDataModel
'    Dim PPFinal As Boolean = False
'    Dim myVueTXPD As clsViewModel

'    Protected Overrides Sub InitViews()
'        myView = Me.GetViewModel("ItemList")
'        myVueTXPD = Me.GetViewModel("TaxPaid")
'    End Sub

'    Public Sub New(context As IProviderContext)
'        MyBase.New(context)
'        Me.InitViews()
'        Me.InitForm()
'    End Sub

'    Private Sub InitForm()
'        Dim sql As String
'        sql = "Select CampusID, DispName, CompanyID, CampusType,TaxAreaCode, DivisionCodeList, GstRegID, PartialCredit from mmListCampus()  Order by DispName"
'        Me.AddLookupField("CampusID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Campus").TableName)

'        sql = "SELECT  VendorID, PartyName, TaxAreaCode, GSTRegType, GSTIN, VendorCode, TaxAreaID FROM  PurListVendor() Order By PartyName"
'        Me.AddLookupField("VendorID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Vendor").TableName)




'        sql = myFuncsBase.CodeWordSQL("Invoice", "B2BType", "")
'        Me.AddLookupField("inv_typ", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "B2BType").TableName)

'        sql = myFuncsBase.CodeWordSQL("Invoice", "CDNType", "codeword in ('C','D')")
'        Me.AddLookupField("ntty", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "CDNType").TableName)

'        sql = "select TaxAreaID, Descrip, TaxAreaCode from TaxArea Order by Descrip"
'        Me.AddLookupField("POSTaxAreaID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "POS").TableName)

'        sql = "Select PartyID, PartyName, TaxAreaID from Party order by PartyName"
'        Me.AddLookupField("ConsigneeID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Party").TableName)

'        sql = myFuncsBase.CodeWordSQL("Invoice", "SupplyType", "")
'        Me.AddLookupField("sply_ty", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "SupplyType").TableName)

'        sql = myFuncsBase.CodeWordSQL("Invoice", "ZeroTax", "Tag2 = 'IP'")
'        Me.AddLookupField("InvoiceItemGst", "ZeroTax_Type", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "ZeroTax").TableName)

'        sql = "Select  Divisionid, (DivisionCode + '-' + DivisionName) as division,DivisionCode from Division order by DivisionCode"
'        Me.AddLookupField("DivisionID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Division").TableName)

'        Dim vlist As New clsValueList
'        vlist.Add("Y", "Yes")
'        vlist.Add("N", "No")
'        Me.ValueLists.Add("RCHRG", vlist)
'        Me.AddLookupField("RCHRG", "RCHRG")

'        Me.ValueLists.Add("IsSEZ", vlist)
'        Me.AddLookupField("Is_SEZ", "IsSEZ")

'        Me.ValueLists.Add("PreGST", vlist)
'        Me.AddLookupField("PreGST", "PreGST")

'        Dim vlist1 As New clsValueList
'        vlist1.Add(False, "Debit/Credit Note")
'        vlist1.Add(True, "Amendment")
'        Me.ValueLists.Add("IsAmendment", vlist1)
'        Me.AddLookupField("IsAmendment", "IsAmendment")

'        Dim vlist2 As New clsValueList
'        vlist2.Add(0, "0")
'        vlist2.Add(5, "5")
'        vlist2.Add(12, "12")
'        vlist2.Add(18, "18")
'        vlist2.Add(28, "28")
'        Me.ValueLists.Add("gstrate", vlist2)
'        Me.AddLookupField("rt", "gstrate")

'        Dim vlistuqc As New clsValueList
'        vlistuqc.Add("BAG", "BAGS")
'        vlistuqc.Add("BAL", "BALE")
'        vlistuqc.Add("BDL", "BUNDLES")
'        vlistuqc.Add("BKL", "BUCKLES")
'        vlistuqc.Add("BOU", "BILLION OF UNITS")
'        vlistuqc.Add("BOX", "BOX")
'        vlistuqc.Add("BTL", "BOTTLES")
'        vlistuqc.Add("BUN", "BUNCHES")
'        vlistuqc.Add("CAN", "CANS")
'        vlistuqc.Add("CBM", "CUBIC METERS")
'        vlistuqc.Add("CCM", "CUBIC CENTIMETER")
'        vlistuqc.Add("CMS", "CENTIMETERS")
'        vlistuqc.Add("CTN", "CARTONS")
'        vlistuqc.Add("DOZ", "DOZENS")
'        vlistuqc.Add("DRM", "DRUMS")
'        vlistuqc.Add("GGK", "GREAT GROSS")
'        vlistuqc.Add("GMS", "GRAMMES")
'        vlistuqc.Add("GRS", "GROSS")
'        vlistuqc.Add("GYD", "GROSS YARDS")
'        vlistuqc.Add("KGS", "KILOGRAMS")
'        vlistuqc.Add("KLR", "KILOLITRE")
'        vlistuqc.Add("KME", "KILOMETRE")
'        vlistuqc.Add("MLT", "MILILITRE")
'        vlistuqc.Add("MTR", "METERS")
'        vlistuqc.Add("MTS", "METRIC TON")
'        vlistuqc.Add("NOS", "NUMBERS")
'        vlistuqc.Add("PAC", "PACKS")
'        vlistuqc.Add("PCS", "PIECES")
'        vlistuqc.Add("PRS", "PAIRS")
'        vlistuqc.Add("QTL", "QUINTAL")
'        vlistuqc.Add("ROL", "ROLLS")
'        vlistuqc.Add("SET", "SETS")
'        vlistuqc.Add("SQF", "SQUARE FEET")
'        vlistuqc.Add("SQM", "SQUARE METERS")
'        vlistuqc.Add("SQY", "SQUARE YARDS")
'        vlistuqc.Add("TBS", "TABLETS")
'        vlistuqc.Add("TGM", "TEN GROSS")
'        vlistuqc.Add("THD", "THOUSANDS")
'        vlistuqc.Add("TON", "TONNES")
'        vlistuqc.Add("TUB", "TUBES")
'        vlistuqc.Add("UGS", "US GALLONS")
'        vlistuqc.Add("UNT", "UNITS")
'        vlistuqc.Add("YDS", "YARDS")
'        vlistuqc.Add("OTH", "OTHERS")
'        Me.ValueLists.Add("uqclist", vlistuqc)
'        Me.AddLookupField("UQC", "uqclist")

'        Dim vlistapplicable As New clsValueList
'        vlistapplicable.Add(65, "65")
'        vlistapplicable.Add(100, "100")
'        Me.ValueLists.Add("Diff_Percent", vlistapplicable)
'        Me.AddLookupField("Diff_Percent", "Diff_Percent")

'        sql = myFuncsBase.CodeWordSQL("Invoice", "TY", "")
'        Me.AddLookupField("InvoiceItemGst", "TY", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "TY").TableName)

'        sql = myFuncsBase.CodeWordSQL("Invoice", "ELG", "")
'        Me.AddLookupField("InvoiceItemGst", "ELG", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "ELG").TableName)

'        sql = "Select Code, Description, Ty from HsnSac "
'        Me.AddLookupField("InvoiceItemGst", "HsnSac", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "HsnSac").TableName)
'    End Sub

'    Public Overrides Function PrepForm(oView As clsViewModel, ByVal prepMode As EnumfrmMode, ByVal prepIDX As String, Optional ByVal strXML As String = "") As Boolean
'        Dim sql, str1 As String, dic As New clsCollecString(Of String)

'        Me.FormPrepared = False
'        If prepMode = EnumfrmMode.acAddM Then prepIDX = 0
'        sql = "Select * from Invoice Where InvoiceID = " & prepIDX
'        Me.InitData(sql, "CDNInvoiceID,OrigInvoiceID", oView, prepMode, prepIDX, strXML)

'        If myUtils.NullNot(myRow("diff_percent")) Then myRow("diff_percent") = 100
'        If myUtils.NullNot(myRow("isamendment")) Then myRow("isamendment") = False
'        If myUtils.NullNot(myRow("rchrg")) Then myRow("rchrg") = "N"

'        dic.Add("my", "Select invoiceid from invoice where originvoiceid = " & frmIDX)
'        dic.Add("orig", "Select invoiceid from invoice where originvoiceid = " & myUtils.cValTN(myRow("OrigInvoiceID")) & " and invoiceid<>" & frmIDX)
'        Me.AddDataSet("AmendVouch", dic)


'        If myUtils.cValTN(myRow("OrigInvoiceID")) > 0 Then
'            myRow("isamendment") = True
'            sql = "Select distinct VendorID, Inv_Typ, Exp_Typ, InvoiceNum,InvoiceDate,PartyName,GstInvoiceType,CTIN,boe_num,boe_dt,sply_ty,CDNInvoiceID,RootInvoiceID,PostPeriodID from gstlistinvoiceitem() where InvoiceID = " & myUtils.cValTN(myRow("OrigInvoiceID"))
'            Me.AddDataSet("OrgInvoice", sql)
'            Dim dt As DataTable = Me.DatasetCollection("OrgInvoice").Tables(0)
'            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
'                Dim OrgInTypeCode As String = myUtils.cStrTN(dt.Rows(0)("GstInvoiceType"))
'                If myUtils.cValTN(dt.Rows(0)("rootinvoiceid")) > 0 Then myRow("rootinvoiceid") = dt.Rows(0)("rootinvoiceid") Else myRow("rootinvoiceid") = myRow("OrigInvoiceID")
'                str1 = "Codeword in ('" & OrgInTypeCode & "') and Tag2 = 'IP'"
'                If frmMode = EnumfrmMode.acAddM Then
'                    myRow("vendorid") = dt.Rows(0)("vendorid")
'                    myRow("cdninvoiceid") = dt.Rows(0)("cdninvoiceid")
'                    myRow("sply_ty") = dt.Rows(0)("sply_ty")
'                End If
'            End If
'        ElseIf myUtils.cValTN(myRow("CDNInvoiceID")) > 0 Then
'            sql = "Select distinct VendorID, Inv_Typ, Exp_Typ, InvoiceNum,InvoiceDate,PartyName,GstInvoiceType,CTIN,boe_num,boe_dt, sply_ty,RCHRG,POSTaxAreaID,PostPeriodID from gstlistinvoiceitem() where InvoiceID = " & myUtils.cValTN(myRow("CDNInvoiceID"))
'            Me.AddDataSet("OrgInvoice", sql)
'            Dim dt As DataTable = Me.DatasetCollection("OrgInvoice").Tables(0)
'            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
'                Dim OrgInTypeCode As String = myUtils.cStrTN(dt.Rows(0)("GstInvoiceType"))
'                If myUtils.IsInList(myUtils.cStrTN(OrgInTypeCode), "B2BUR", "IMPS") Then
'                    str1 = "Codeword in ('CDNUR') and Tag2 = 'IP'"
'                Else
'                    str1 = "Codeword in ('CDN') and Tag2 = 'IP'"
'                End If
'                If frmMode = EnumfrmMode.acAddM Then
'                    myRow("vendorid") = dt.Rows(0)("vendorid")
'                    myRow("sply_ty") = dt.Rows(0)("sply_ty")
'                    myRow("RCHRG") = dt.Rows(0)("RCHRG")
'                    myRow("POSTaxAreaID") = dt.Rows(0)("POSTaxAreaID")
'                End If
'            End If
'        Else
'            str1 = "Codeword not in ('CDN','CDNUR') and Tag2 = 'IP'"
'        End If

'        sql = myFuncsBase.CodeWordSQL("Invoice", "GstTypecode", str1)
'        Me.AddLookupField("GstInvoiceType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "GstInvoiceType").TableName)


'        If frmMode = EnumfrmMode.acAddM Then
'            myRow("InvoiceDate") = Now.Date
'            myRow("PostingDate") = Now.Date
'            If Me.dsCombo.Tables("gstinvoicetype").Rows.Count = 1 Then myRow("gstinvoicetype") = Me.dsCombo.Tables("gstinvoicetype").Rows(0)("codeword")

'        Else
'            Dim oMaster As New clsMasterDataFICO(myContext)
'            Dim rPostPeriod As DataRow = oMaster.rPostPeriod(myUtils.cValTN(myRow("PostPeriodID")))
'            If Not IsNothing(rPostPeriod) Then
'                PPFinal = myFuncs2.IsFinalPP(myContext, Me.SelectedRow("CampusID")("gstregid"), rPostPeriod, "GSTR2")
'            End If
'        End If

'        Me.BindDataTable(myUtils.cValTN(prepIDX))

'        myView.MainGrid.BindGridData(Me.dsForm, 1)
'        myView.MainGrid.QuickConf("", True, ".8-1-2-.8-.8-1-1.2-1-1-1-1-1", True)
'        str1 = "<BAND INDEX = ""0"" TABLE = ""InvoiceItemGst"" IDFIELD=""InvoiceItemGstID""><COL KEY ="" InvoiceItemGstID, InvoiceID,ZeroTax_Type,elg,tx_i,tx_c,tx_s,tx_cs,Description,ty,Qty,SortIndex""/><COL KEY=""Hsn_sc"" CAPTION=""HSN""/><COL KEY=""RT"" CAPTION=""Tax Rate""/><COL KEY=""TXVAL"" CAPTION=""Taxable Value""/><COL KEY=""Uqc"" CAPTION=""Unit Name""/><COL KEY=""BasicRate"" CAPTION=""Basic Rate""/><COL KEY=""CSAMT"" CAPTION=""CESS""/><COL KEY=""IAMT"" CAPTION=""Integrated Tax""/><COL KEY=""CAMT"" CAPTION=""Central Tax""/><COL KEY=""SAMT"" CAPTION=""State Tax""/></BAND>"
'        myView.MainGrid.PrepEdit(str1, EnumEditType.acCommandOnly)

'        myVueTXPD.MainGrid.BindGridData(Me.dsForm, 2)
'        myVueTXPD.MainGrid.QuickConf("", True, "1-1-1-1-1-1", True)
'        Dim str3 As String = "<BAND INDEX=""0"" TABLE = ""AdvanceTaxGST"" IDFIELD=""AdvanceTaxGSTID""><COL KEY ="" AdvanceTaxGSTID, InvoiceID, ZeroTax_type, DiffAD_AMT, DiffIAMT, DiffCAMT, DiffSAMT, DiffCSAMT""/><COL KEY=""AD_AMT"" CAPTION=""Advance Amount""/><COL KEY=""RT"" FORMAT=""00.00"" CAPTION=""Tax Rate""/><COL NOEDIT=""TRUE"" KEY=""IAMT"" CAPTION=""Integrated Tax""/><COL NOEDIT=""TRUE"" KEY=""CAMT"" CAPTION=""Central Tax""/><COL NOEDIT=""TRUE"" KEY=""SAMT"" CAPTION=""State Tax""/><COL KEY=""CSAMT"" CAPTION=""CESS""/></BAND>"
'        myVueTXPD.MainGrid.PrepEdit(str3, EnumEditType.acCommandAndEdit)

'        Me.FormPrepared = True
'        Return Me.FormPrepared
'    End Function

'    Private Sub BindDataTable(ByVal InvoicePurchID As Integer)
'        Dim ds As DataSet, Sql As String

'        Sql = " Select InvoiceItemGstID, InvoiceID,ZeroTax_Type,elg,tx_i,tx_c,tx_s,tx_cs,ty, Hsn_sc,SortIndex, Description, Uqc, Qty, BasicRate, TXVAL, RT, IAMT, CAMT, SAMT, CSAMT from InvoiceItemGst  Where InvoiceID = " & InvoicePurchID & " "
'        Sql = Sql & "; Select AdvanceTaxGstID, InvoiceID, ZeroTax_type, DiffAD_AMT, DiffIAMT, DiffCAMT, DiffSAMT, DiffCSAMT, AD_AMT, RT, IAMT, CAMT, SAMT, CSAMT from AdvanceTaxGst Where invoiceid = " & InvoicePurchID
'        Sql = Sql & "; select * from InvoiceGstCalc where invoiceid = " & InvoicePurchID
'        ds = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql)

'        myUtils.AddTable(Me.dsForm, ds, "InvoiceItemGst", 0)
'        myUtils.AddTable(Me.dsForm, ds, "AdvanceTaxGst", 1)
'        myUtils.AddTable(Me.dsForm, ds, "InvoiceGstCalc", 2)

'    End Sub

'    Public Overrides Function Validate() As Boolean
'        Dim TotalTax As Double
'        Me.InitError()

'        If Me.SelectedRow("GstInvoiceType") Is Nothing Then Me.AddError("GstInvoiceType", "Please select Invoice Type")
'        myFuncs2.ValidateSupplyType(myContext, Me, "Vendor")

'        If Me.SelectedRow("DivisionID") Is Nothing Then Me.AddError("DivisionID", "Please Select Division")
'        If Me.myView.MainGrid.myDV.Table.Select.Length = 0 Then Me.AddError("", "Please Enter Some Transactions")
'        If myRow("InvoiceNum").ToString.Trim.Length = 0 Then Me.AddError("InvoiceNum", "Enter Invoice No.")

'        If Me.SelectedRow("VendorID") IsNot Nothing Then
'            If myUtils.IsInList(myUtils.cStrTN(myRow("GSTInvoiceType")), "B2B") Then
'                If Me.SelectedRow("VendorID")("gstin").ToString.Trim.Length = 0 Then Me.AddError("VendorID", "Please select B2B Vendor")
'                If Me.SelectedRow("inv_typ") Is Nothing Then Me.AddError("inv_typ", "Please select B2B Invoice Type")
'                If Me.SelectedRow("VendorID") Is Nothing Then Me.AddError("VendorID", "Please select Vendor")
'                If myUtils.IsInList(myUtils.cStrTN(myRow("inv_typ")), "SEWP") AndAlso myUtils.IsInList(myUtils.cStrTN(myRow("Sply_Ty")), "INTRA") Then Me.AddError("Sply_Ty", "Please select supply type INTER when invoice type  is B2B-SEWP.")
'                If myUtils.IsInList(myUtils.cStrTN(myRow("inv_typ")), "SEWOP") AndAlso myUtils.IsInList(myUtils.cStrTN(myRow("Sply_Ty")), "INTRA") Then Me.AddError("Sply_Ty", "Please select supply type INTER when invoice type  is B2B-SEWOP.")
'                If myUtils.IsInList(myUtils.cStrTN(myRow("inv_typ")), "CBW") AndAlso myUtils.IsInList(myUtils.cStrTN(myRow("Sply_Ty")), "INTRA") Then Me.AddError("Sply_Ty", "Please select supply type INTER when invoice type  is B2B-CBW.")
'            ElseIf myUtils.IsInList(myUtils.cStrTN(myRow("GSTInvoiceType")), "IMPS", "IMPG", "B2BUR") Then
'                If Me.SelectedRow("VendorID")("gstin").ToString.Trim.Length > 0 Then Me.AddError("CustomerID", "Please select correct Customer")
'            End If
'        End If

'        If myUtils.IsInList(myUtils.cStrTN(myRow("GstInvoiceType")), "IMPS") Then
'            If myUtils.IsInList(myUtils.cStrTN(myRow("Sply_Ty")), "INTRA") Then Me.AddError("Sply_Ty", "Choose Supply Type INTER. When Invoice Type is IMPS.")
'            For Each r1 As DataRow In dsForm.Tables("InvoiceItemGst").Select
'                If myUtils.cStrTN(r1("ty")) = "G" Then Me.AddError("GstInvoiceType", "Choose Item Type SERVICE. When Invoice Type is IMPS.")
'            Next
'        End If

'        If myUtils.cValTN(myRow("CDNInvoiceID")) > 0 Then
'            If Me.DatasetCollection.ContainsKey("OrgInvoice") Then
'                Dim dt As DataTable = Me.DatasetCollection("OrgInvoice").Tables(0)
'                If myUtils.IsInList(myUtils.cStrTN(dt.Rows(0)("GstInvoiceType")), "IMPS") AndAlso myUtils.IsInList(myUtils.cStrTN(myRow("GstInvoiceType")), "CDNUR") Then
'                    If myUtils.IsInList(myUtils.cStrTN(myRow("Sply_Ty")), "INTRA") Then Me.AddError("Sply_Ty", "Choose Supply Type INTER. When Invoice Type is IMPS.")
'                End If
'            End If
'        End If

'        If myUtils.IsInList(myUtils.cStrTN(myRow("GstInvoiceType")), "IMPG") Then
'            If myUtils.IsInList(myUtils.cStrTN(myRow("RCHRG")), "Y") Then Me.AddError("RCHRG", "Please select RCHRG = NO")
'            If myUtils.IsInList(myUtils.cStrTN(myRow("Sply_Ty")), "INTRA") Then Me.AddError("Sply_Ty", "Choose Supply Type INTER. When Invoice Type is IMPG.")
'            If myUtils.IsInList(myUtils.cStrTN(myRow("boe_num")), "") Then Me.AddError("boe_num", "Please enter boe num.")
'            If myRow("boe_num").ToString.Trim.Length > 0 Then
'                If myRow("boe_num").ToString.Trim.Length < 3 OrElse myRow("boe_num").ToString.Trim.Length > 15 Then Me.AddError("boe_num", "Please Select Bill of Export No. between 3-15 Digit")
'            End If
'            If myUtils.IsInList(myUtils.cStrTN(myRow("boe_dt")), "") Then Me.AddError("boe_dt", "Please select boe date.")
'            If myUtils.IsInList(myUtils.cStrTN(myRow("boe_val")), "") Then Me.AddError("boe_val", "Please enter boe value.")
'            If myUtils.IsInList(myUtils.cStrTN(myRow("port_code")), "") Then Me.AddError("port_code", "Please enter port code")
'            For Each r1 As DataRow In dsForm.Tables("InvoiceItemGst").Select
'                If myUtils.cStrTN(r1("ty")) = "S" Then Me.AddError("GstInvoiceType", "Choose Item Type GOODS. When Invoice Type is IMPG.")
'            Next
'        End If


'        If myUtils.NullNot(myRow("postingdate")) OrElse myRow("PostingDate") < myRow("InvoiceDate") Then
'            Me.AddError("PostingDate", "Posting Date can not be less then Invoice Date.")
'        Else
'            If myRow("PostingDate") > Now.Date Then Me.AddError("PostingDate", "Please Select Valid Posting Date.")
'            If myRow("PostingDate") > CDate(myRow("InvoiceDate")).AddMonths(18) Then Me.AddError("InvoiceDate", "Please Select Valid Invoice Date.")
'            If Not Me.SelectedRow("CampusID") Is Nothing Then
'                Dim oRet = myFuncs2.ValidPostPeriod(myContext, Me.myRow("PostingDate"), PPFinal, Me.SelectedRow("CampusID")("gstregid"), "GSTR2")
'                If oRet.Success Then myRow("postperiodid") = oRet.ID Else Me.AddError("Dated", oRet.Message)
'            End If
'        End If



'        For Each r1 As DataRow In dsForm.Tables("InvoiceItemGst").Select
'            If myUtils.IsInList(myUtils.cStrTN(r1("rt")), "0") AndAlso myUtils.IsInList(myUtils.cStrTN(r1("ZeroTax_Type")), "Comp") Then
'                If Not myUtils.IsInList(myUtils.cStrTN(Me.SelectedRow("vendorid")("GSTRegType")), "C") Then
'                    Me.AddError("VendorID", "Please select vendor Composition")
'                End If
'            End If
'        Next
'        If Me.DatasetCollection.ContainsKey("OrgInvoice") Then
'            Dim dt As DataTable = Me.DatasetCollection("OrgInvoice").Tables(0)
'            If myRow("invoicedate") < dt.Rows(0)("invoicedate") Then Me.AddError("invoicedate", "Date cannot be less than original invoice date")
'            If Not myUtils.MatchRowCols(myRow.Row, dt.Rows(0), "vendorid") Then Me.AddError("vendorid", "Cannot change vendor.")
'            If myUtils.cValTN(myRow("OrigInvoiceID")) > 0 Then
'                If myUtils.cValTN(myRow("postperiodid")) = dt.Rows(0)("postperiodid") Then Me.AddError("invoicedate", "Postperiod cannot be same as original invoice")
'            End If

'        End If
'        If Me.DatasetCollection.ContainsKey("AmendVouch") Then
'            If Me.DatasetCollection("AmendVouch").Tables("my").Rows.Count > 0 Then Me.AddError("PostingDate", "This voucher cannot be edited as its amendment has been posted")
'            If Me.DatasetCollection("AmendVouch").Tables("orig").Rows.Count > 0 Then Me.AddError("PostingDate", "This voucher cannot be saved as a different amendment already exists")
'        End If

'        If myUtils.cValTN(myRow("postperiodid")) = 0 Then Me.AddError("PostingDate", "Please Select Valid Post Period")
'        If myRow("InvoiceDate") > Now.Date Then Me.AddError("InvoiceDate", "Please Select Valid Invoice Date.")
'        myView.MainGrid.CheckValid("elg")

'        If myUtils.cValTN(myRow("ConsigneeID")) > 0 Then
'            myRow("POSTaxAreaID") = myUtils.cValTN(Me.SelectedRow("ConsigneeID")("TaxAreaID"))
'        Else
'            If myUtils.cValTN(myRow("POSTaxAreaID")) = 0 AndAlso Not Me.SelectedRow("VendorID") Is Nothing Then myRow("POSTaxAreaID") = myUtils.cValTN(Me.SelectedRow("VendorID")("TaxAreaID"))
'        End If

'        'If Me.SelectedRow("POSTaxAreaID") Is Nothing Then Me.AddError("POSTaxAreaID", "Please select Place of Supply.")

'        If myContext.Data.SelectDistinct(Me.dsForm.Tables("advancetaxgst"), "rt").Select.Length <> Me.dsForm.Tables("advancetaxgst").Select.Length Then Me.AddError("Amounttot", "Duplicate Rates")
'        If Me.dsForm.Tables("advancetaxgst").Select.Length > 0 Then
'            For Each r As DataRow In Me.dsForm.Tables("InvoiceItemGst").Select
'                TotalTax = myUtils.cValTN(r("CSAMT")) + myUtils.cValTN(r("IAMT")) + myUtils.cValTN(r("CAMT")) + myUtils.cValTN(r("SAMT"))
'            Next
'            For Each r As DataRow In Me.dsForm.Tables("advancetaxgst").Select
'                If myUtils.cValTN(r("AD_AMT")) > TotalTax Then Me.AddError("", "Tax Paid cat not be greater then total tax.")
'            Next
'        End If

'        If myUtils.IsInList(myUtils.cStrTN(myRow("GSTInvoiceType")), "CDN", "CDNUR") Then
'            If Me.SelectedRow("ntty") Is Nothing Then Me.AddError("ntty", "Please select CDN Invoice Type")
'        End If

'        Return Me.CanSave
'    End Function

'    Public Overrides Function VSave() As Boolean
'        VSave = False

'        If Me.Validate Then
'            myRow("DocType") = "IP"
'            myRow("BillOf") = "C"
'            If frmMode = EnumfrmMode.acAddM Then myRow("StatusNum") = 1
'            Dim oProc As New clsGSTNReturnGSTR2(myContext)
'            myRow("uniquekey") = oProc.CalcUniqueKey(Me.SelectedRow("vendorid")("vendorcode"), myRow("postperiodid"), myRow("invoicenum"), myUtils.cValTN(myRow("isamendment")))
'            Dim rCDNInv = If(Me.DatasetCollection.ContainsKey("OrgInvoice") AndAlso Me.DatasetCollection("OrgInvoice").Tables(0).Rows.Count > 0, Me.DatasetCollection("OrgInvoice").Tables(0).Rows(0), Nothing)
'            Dim oRet = oProc.CheckZeroTax(myRow.Row, rCDNInv, Me.dsForm.Tables("invoiceitemGst"))
'            If Not oRet.Success Then Me.AddError("inv_typ", oRet.Message)
'            oProc.CheckTaxCredit(Me.SelectedRow("CampusID"), myRow.Row, Me.dsForm.Tables("invoiceitemGst"))
'            oProc.CheckSubField(myRow.Row)
'            oProc.CheckSubTypeFields(myRow.Row)
'            Dim rCPInv As DataRow = oProc.CalcInvoiceAction(Me.SelectedRow("CampusID")("gstregid"), myRow("postperiodid"), myRow.Row)
'            For Each r1 As DataRow In dsForm.Tables("InvoiceItemGst").Select
'                If myUtils.cValTN(r1("rt")) > 0 Then r1("ZeroTax_Type") = DBNull.Value
'                If myUtils.cStrTN(r1("elg")) = "NO" Then r1("tx_i") = 0
'            Next


'            If Me.CanSave Then
'                Dim InvoicePurchDescrip As String = " Purhase Invoice No: " & myRow("InvoiceNum").ToString & ", Dt. " & Format(myRow("InvoiceDate"), "dd-MMM-yyyy")
'                Try


'                    myContext.Provider.dbConn.BeginTransaction(myContext, Me.Name, Me.frmMode.ToString, "InvoiceID", frmIDX)
'                    If (frmMode = EnumfrmMode.acEditM) AndAlso (Not oProc.MatchUniqueFields(myRow.Row)) Then
'                        Dim rDel As DataRow = oProc.AddDeleteAction(myRow.Row, Me.SelectedRow("CampusID")("gstregid"), myUtils.cStrTN(Me.SelectedRow("vendorid")("gstin")))
'                    End If
'                    myContext.Provider.objSQLHelper.SaveResults(myRow.Row.Table, "Select * from Invoice Where InvoiceID = " & frmIDX)
'                    frmIDX = myRow("InvoiceID")

'                    myUtils.ChangeAll(dsForm.Tables("InvoiceItemGst").Select, "InvoiceID=" & frmIDX)
'                    myContext.Provider.objSQLHelper.SaveResults(dsForm.Tables("InvoiceItemGst"), "Select InvoiceItemGstID, InvoiceID,SortIndex,ZeroTax_Type,elg,tx_i,tx_c,tx_s,tx_cs,Description,ty, Hsn_sc,TXVAL,RT,IAMT, CAMT,SAMT, CSAMT, Uqc,Qty,BasicRate from InvoiceItemGst")

'                    If (Not rCPInv Is Nothing) Then
'                        myContext.Provider.objSQLHelper.SaveResults(rCPInv.Table, "Select cpinvoiceid, actionflag, matchcode, invoiceid from cpinvoice where 0=1")
'                    End If

'                    oProc.PopulateTxPDA(myRow.Row, dsForm.Tables("AdvanceTaxGst"))
'                    myUtils.ChangeAll(dsForm.Tables("AdvanceTaxGst").Select, "InvoiceID=" & frmIDX)
'                    myContext.Provider.objSQLHelper.SaveResults(dsForm.Tables("AdvanceTaxGst"), "Select AdvanceTaxGstID, InvoiceID, ZeroTax_type, DiffAD_AMT, DiffIAMT, DiffCAMT, DiffSAMT, DiffCSAMT, AD_AMT, RT, IAMT, CAMT, SAMT, CSAMT from AdvanceTaxGst")

'                    oProc.PopulateCalc("ip", frmIDX, myUtils.cValTN(myRow("originvoiceid")), dsForm.Tables("InvoiceGstCalc"))
'                    myUtils.ChangeAll(dsForm.Tables("InvoiceGstCalc").Select, "InvoiceID=" & frmIDX)
'                    myContext.Provider.objSQLHelper.SaveResults(dsForm.Tables("InvoiceGstCalc"), "Select * from InvoiceGstCalc")

'                    frmMode = EnumfrmMode.acEditM
'                    myContext.Provider.dbConn.CommitTransaction(InvoicePurchDescrip, frmIDX)
'                    VSave = True
'                Catch ex As SqlException
'                    myContext.Provider.dbConn.RollBackTransaction(InvoicePurchDescrip, ex.Message)
'                    If ex.Number = 2601 OrElse ex.Number = 2627 Then
'                        Me.AddError("", "Duplicate Invoice")
'                    Else
'                        Me.AddError("", ex.Message)
'                    End If

'                Catch ex2 As Exception
'                    myContext.Provider.dbConn.RollBackTransaction(InvoicePurchDescrip, ex2.Message)
'                    Me.AddError("", ex2.Message)
'                End Try
'            End If
'        End If
'    End Function


'    Public Overrides Function DeleteEntity(EntityKey As String, ID As Integer, AcceptWarning As Boolean) As clsProcOutput
'        Dim oRet As New clsProcOutput
'        Try
'            Select Case EntityKey.Trim.ToLower
'                Case "invoice"
'                    Dim GstRegID As Integer = 0, CTIN As String, dic As New clsCollecString(Of String)
'                    dic.Add(Me.strT, "Select * from Invoice Where InvoiceID = " & ID)
'                    Dim dt1 As DataTable = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, dic).Tables(0)
'                    If dt1.Rows.Count > 0 Then
'                        Dim oMaster As New clsMasterDataFICO(myContext)
'                        Dim rPostPeriod As DataRow = oMaster.rPostPeriod(myUtils.cValTN(dt1.Rows(0)("PostPeriodID")))
'                        GstRegID = Me.SelectedRow(dt1.Rows(0), "CampusID")("gstregid")
'                        CTIN = Me.SelectedRow(dt1.Rows(0), "VendorID")("gstin")
'                        Dim isFinal As Boolean = myFuncs2.IsFinalPP(myContext, GstRegID, rPostPeriod, "GSTR1")
'                        If isFinal Then oRet.AddError("Posting Period Finailzed")
'                    Else
'                        oRet.AddError("Cannot find Invoice")
'                    End If
'                    If oRet.ErrorCount = 0 Then
'                        If AcceptWarning Then
'                            Dim oProc As New clsGSTNReturnGSTR2(myContext)
'                            oProc.AddDeleteAction(dt1.Rows(0), GstRegID, CTIN)
'                            dt1.Rows(0).Delete()
'                            myContext.Provider.objSQLHelper.SaveResults(dt1, dic(0))
'                        ElseIf oRet.WarningCount = 0 Then
'                            oRet.AddWarning("Are you sure ?")
'                        End If
'                    End If
'            End Select

'        Catch ex As Exception
'            oRet.AddException(ex)
'        End Try


'        Return oRet
'    End Function
'End Class


'**************************************************
'GstInvoiceItem

'Imports risersoft.app.shared
'Imports ug = Infragistics.Win.UltraWinGrid
'Imports risersoft.shared.Extensions

'Public Class frmInvoiceItemPurch
'    Inherits frmMax
'    Dim dv As DataView
'    Friend fMat As frmGstInvoicePurch
'    Public Sub New()
'        MyBase.New()
'        InitializeComponent()
'        Me.InitForm()
'    End Sub

'    Private Sub InitForm()

'    End Sub

'    Public Overloads Function BindModel(NewModel As clsFormDataModel) As Boolean
'        myWinSQL.AssignCmb(NewModel.dsCombo, "TY", "", Me.cmb_TY)
'        dv = myWinSQL.AssignCmb(NewModel.dsCombo, "HsnSac", "", Me.cmb_Hsn_Sc,, 2)
'        myWinSQL.AssignCmb(NewModel.dsCombo, "ZeroTax", "", Me.cmb_ZeroTax_Type)
'        myWinSQL.AssignCmb(NewModel.dsCombo, "ELG", "", Me.cmb_Elg)
'        Me.cmb_UQC.ValueList = fMat.Model.ValueLists("uqclist").ComboList
'        Me.cmb_RT.ValueList = fMat.Model.ValueLists("gstrate").ComboList
'        Return True
'    End Function

'    Public Overloads Function PrepForm(ByVal r1 As DataRow, IdField As String) As Boolean
'        Me.FormPrepared = False
'        If Me.BindData(r1) Then
'            HandleTY(myUtils.cStrTN(myRow("TY")))
'            HandleZeroRated(myUtils.cValTN(myRow("rt")))
'            ReadonltCtl(myUtils.cStrTN(fMat.cmb_sply_ty.Value))
'            Me.FormPrepared = True
'        End If
'        Return Me.FormPrepared
'    End Function

'    Public Overrides Function VSave() As Boolean
'        Me.InitError()
'        VSave = False
'        If IsNothing(myRow) Then
'            myWinForms.AddError(Me.txt_Description, "Please Generate Transaction")
'            Exit Function
'        End If

'        cm.EndCurrentEdit()
'        If myUtils.NullNot(Me.txt_Qty.Value) Then myWinForms.AddError(Me.txt_Qty, "Please Enter Qty")
'        If myUtils.NullNot(Me.txt_BasicRate.Value) Then myWinForms.AddError(Me.txt_BasicRate, "Please Enter Basic Rate")
'        If myUtils.NullNot(Me.cmb_RT.Value) Then myWinForms.AddError(Me.cmb_RT, "Please Select Tax Rate")
'        If myUtils.NullNot(Me.txt_Description.Value) Then myWinForms.AddError(Me.txt_Description, "Please Enter Description")
'        If myUtils.NullNot(Me.cmb_TY.Value) Then myWinForms.AddError(Me.cmb_TY, "Please Select Item Type")
'        If myUtils.NullNot(Me.cmb_UQC.Value) Then myWinForms.AddError(Me.cmb_UQC, "Please Select Unit Name")

'        Dim TotalTax As Double = myUtils.cValTN(txt_CSAMT.Text) + myUtils.cValTN(txt_IAMT.Text) + myUtils.cValTN(txt_CAMT.Text) + myUtils.cValTN(txt_SAMT.Text)
'        'If myUtils.cValTN(txt_txpd.Text) > TotalTax Then myWinForms.AddError(txt_txpd, "Tax Paid cat not be greater then total tax.")


'        If myUtils.cValTN(txt_tx_i.Text) > myUtils.cValTN(txt_IAMT.Text) Then myWinForms.AddError(txt_tx_i, "Credit Amount can not be greater then Tax Amount.")
'        If myUtils.cValTN(txt_tx_c.Text) > myUtils.cValTN(txt_CAMT.Text) Then myWinForms.AddError(txt_tx_c, "Credit Amount can not be greater then Tax Amount.")
'        If myUtils.cValTN(txt_tx_s.Text) > myUtils.cValTN(txt_SAMT.Text) Then myWinForms.AddError(txt_tx_s, "Credit Amount can not be greater then Tax Amount.")
'        If myUtils.cValTN(txt_tx_cs.Text) > myUtils.cValTN(txt_CSAMT.Text) Then myWinForms.AddError(txt_tx_cs, "Credit Amount can not be greater then CESS Amount.")

'        If Me.CanSave Then
'            VSave = True
'        End If
'    End Function

'    Private Sub txt_Qty_Leave(sender As Object, e As EventArgs) Handles txt_Qty.Leave
'        CalculateTaxableVal()
'    End Sub

'    Private Sub txt_BasicRate_Leave(sender As Object, e As EventArgs) Handles txt_BasicRate.Leave
'        CalculateTaxableVal()
'    End Sub

'    Private Sub CalculateTaxableVal()
'        txt_TXVal.Value = myUtils.cValTN(txt_Qty.Value) * myUtils.cValTN(txt_BasicRate.Value)
'        CalculateTaxVal()
'    End Sub

'    Private Sub txt_RT_Leave(sender As Object, e As EventArgs) Handles cmb_RT.Leave, cmb_RT.AfterCloseUp
'        CalculateTaxVal()
'        Me.HandleZeroRated(myUtils.cValTN(Me.cmb_RT.Value))
'    End Sub

'    Private Sub CalculateTaxVal()
'        If myUtils.cValTN(cmb_RT.Value) > 0 AndAlso myUtils.cValTN(txt_TXVal.Value) > 0 Then
'            Dim TaxAmt As Double = (myUtils.cValTN(txt_TXVal.Value) * myUtils.cValTN(cmb_RT.Value)) / 100
'            If myUtils.IsInList(myUtils.cStrTN(fMat.cmb_sply_ty.Value), "INTER") Then
'                txt_IAMT.Value = TaxAmt
'            Else
'                txt_CAMT.Value = TaxAmt / 2
'                txt_SAMT.Value = TaxAmt / 2
'            End If
'        Else
'            If myUtils.cValTN(cmb_RT.Value) = 0 Then
'                txt_IAMT.Value = 0
'                txt_CAMT.Value = 0
'                txt_SAMT.Value = 0
'            End If
'        End If
'    End Sub

'    Private Sub HandleTY(Ty As String)
'        If Not myUtils.IsInList(myUtils.cStrTN(Ty), "") Then
'            dv.RowFilter = "TY = '" & myUtils.cStrTN(Ty) & "'"
'        Else
'            dv.RowFilter = "0=1"
'        End If
'    End Sub

'    Private Sub cmb_TY_Leave(sender As Object, e As EventArgs) Handles cmb_TY.Leave, cmb_TY.AfterCloseUp
'        HandleTY(myUtils.cStrTN(cmb_TY.Value))
'        If (IsNothing(cmb_Hsn_Sc.SelectedRow)) Then cmb_Hsn_Sc.Value = DBNull.Value
'    End Sub

'    Private Sub ReadonltCtl(SupplyType)
'        txt_tx_i.ReadOnly = myUtils.IsInList(myUtils.cStrTN(SupplyType), "INTRA")
'        txt_tx_c.ReadOnly = myUtils.IsInList(myUtils.cStrTN(SupplyType), "INTER")
'        txt_tx_s.ReadOnly = myUtils.IsInList(myUtils.cStrTN(SupplyType), "INTER")
'    End Sub
'    Private Sub HandleZeroRated(rt As Decimal)
'        If cmb_RT.Value = 0 Then
'            Me.cmb_ZeroTax_Type.Visible = True
'        Else
'            Me.cmb_ZeroTax_Type.Visible = False
'            Me.cmb_ZeroTax_Type.Value = DBNull.Value
'        End If
'        Me.lblZeroRated.Visible = Me.cmb_ZeroTax_Type.Visible

'    End Sub
'End Class