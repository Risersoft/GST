Imports risersoft.app.mxform.gst
Imports Infragistics.Win.UltraWinGrid
Imports risersoft.app.mxent
Imports risersoft.app.mxform
Imports risersoft.shared.Extensions

Public Class frmEwayBill
    Inherits frmMax

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        Me.InitForm()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Dim myVueVehicle As New clsWinView, dvParty As DataView

    Private Sub InitForm()
        myView.SetGrid(Me.UltraGridItem)
        myVueVehicle.SetGrid(Me.UltraGridPart)
        WinFormUtils.SetButtonConf(Me.btnOK, Me.btnCancel, Me.btnSave)
    End Sub

    Public Overrides Function PrepForm(oView As clsWinView, ByVal prepMode As EnumfrmMode, ByVal prepIdx As String, Optional ByVal strXML As String = "") As Boolean
        Me.FormPrepared = False
        Dim objModel As frmEwayBillModel = Me.InitData("frmEwayBillModel", oview, prepMode, prepIdx, strXML)
        If Me.BindModel(objModel, oview) Then
            myWinSQL.AssignCmb(Me.dsCombo, "Campus", "", Me.cmb_CampusID)
            myWinSQL.AssignCmb(Me.dsCombo, "MovementType", "", Me.cmb_transactiontype)
            myWinSQL.AssignCmb(Me.dsCombo, "SubSupplyType", "", Me.cmb_SubSupplyType)
            myWinSQL.AssignCmb(Me.dsCombo, "DocumentType", "", Me.cmb_DocType)
            myWinSQL.AssignCmb(Me.dsCombo, "Transporter", "", Me.cmb_TransVendorID)
            myWinSQL.AssignCmb(Me.dsCombo, "StateCode", "", Me.cmb_fromStateCode)
            myWinSQL.AssignCmb(Me.dsCombo, "StateCode", "", Me.cmb_toStateCode)
            myWinSQL.AssignCmb(Me.dsCombo, "StateCode", "", Me.cmb_actfromStateCode)
            myWinSQL.AssignCmb(Me.dsCombo, "StateCode", "", Me.cmb_acttoStateCode)
            myWinSQL.AssignCmb(Me.dsCombo, "SupplyType", "", Me.cmb_SupplyType)
            dvParty = myWinSQL.AssignCmb(Me.dsCombo, "Customer", "", Me.cmb_CustomerID, , 2)
            dvParty = myWinSQL.AssignCmb(Me.dsCombo, "Vendor", "", Me.cmb_VendorID, , 2)
            myWinSQL.AssignCmb(Me.dsCombo, "ReturnPeriod", "", Me.cmb_ReturnPeriodID)

            If prepMode = EnumfrmMode.acEditM AndAlso Not myUtils.NullNot(myRow("ewaybillnum")) Then
                btnGenerate.Enabled = False
            End If

            Me.cmb_transactiontype.Value = myUtils.cValTN(myRow("transactiontype"))

            Me.cmb_VendorID.Value = myUtils.cValTN(myRow("VendorID"))
            Me.cmb_CustomerID.Value = myUtils.cValTN(myRow("CustomerID"))
            Me.cmb_CampusID.Value = myUtils.cValTN(myRow("CampusID"))

            If Me.cmb_VendorID.SelectedRow IsNot Nothing Then
                PopulateBillFromBillToVendor(myUtils.cStrTN(myRow("SupplyType")), myUtils.cValTN(myRow("VendorID")), myUtils.cValTN(myRow("CampusID")))
            ElseIf Me.cmb_CustomerID.SelectedRow IsNot Nothing Then
                PopulateBillFromBillToCustomer(myUtils.cStrTN(myRow("SupplyType")), myUtils.cValTN(myRow("CustomerID")), myUtils.cValTN(myRow("CampusID")))
            End If

            If Not myUtils.NullNot(myRow("ewaybillnum")) Then
                WinFormUtils.SetReadOnly(UltraPanel11, True, False)
                WinFormUtils.SetReadOnly(UltraTabControl3, True, False)
                WinFormUtils.SetReadOnly(UltraTabControl4, True, False)
                WinFormUtils.SetReadOnly(UltraTabControl2.Tabs("Item").TabPage, True, False)
                For Each gr In myView.mainGrid.myGrid.Rows
                    gr.Activation = Activation.NoEdit
                Next
            End If

            Me.FormPrepared = True
        End If
        Return Me.FormPrepared
    End Function

    Public Overrides Function BindModel(NewModel As clsFormDataModel, oView As clsView) As Boolean
        If MyBase.BindModel(NewModel, oView) Then
            myVueVehicle.PrepEdit(Me.Model.GridViews("PartB"))
            myView.PrepEdit(Me.Model.GridViews("Items"), btnAddItem, btnDeleteItem)

            Return True
        End If
        Return False
    End Function

    Public Overrides Function VSave() As Boolean
        Me.InitError()
        VSave = False
        cm.EndCurrentEdit()
        If Me.ValidateData() Then
            If Me.SaveModel() Then
                Return True
            End If
        Else
            Me.SetError()
        End If
        Me.Refresh()
    End Function

    Private Sub cmb_TransactionType_Leave(sender As Object, e As EventArgs) Handles cmb_transactiontype.Leave, cmb_transactiontype.AfterCloseUp
        UltraTabControl3.Tabs("DISP").Visible = ((Not IsNothing(cmb_transactiontype.SelectedRow)) AndAlso myUtils.IsInList(myUtils.cValTN(cmb_transactiontype.Value), "3", "4"))
        UltraTabControl4.Tabs("SHIP").Visible = ((Not IsNothing(cmb_transactiontype.SelectedRow)) AndAlso myUtils.IsInList(myUtils.cValTN(cmb_transactiontype.Value), "2", "4"))
    End Sub

    Private Sub PopulateBillFromBillToCustomer(SupplyType As String, CustomerID As Integer, CampusID As Integer)
        If Me.cmb_CampusID.SelectedRow IsNot Nothing AndAlso Me.cmb_CustomerID.SelectedRow IsNot Nothing AndAlso (Not myUtils.NullNot(myRow("SupplyType"))) Then
            Me.txt_fromGstin.Text = myUtils.cStrTN(cmb_CampusID.SelectedRow.Cells("GSTIN").Value)
            Me.txt_toGstin.Text = myUtils.cStrTN(cmb_CustomerID.SelectedRow.Cells("GSTIN").Value)
            Me.textfromTrdName.Text = myUtils.cStrTN(cmb_CampusID.SelectedRow.Cells("DispName").Value)
            Me.textToTrdName.Text = myUtils.cStrTN(cmb_CustomerID.SelectedRow.Cells("PartyName").Value)
        End If
    End Sub

    Private Sub PopulateBillFromBillToVendor(SupplyType As String, VendorID As Integer, CampusID As Integer)
        If Me.cmb_CampusID.SelectedRow IsNot Nothing AndAlso Me.cmb_VendorID.SelectedRow IsNot Nothing AndAlso (Not myUtils.NullNot(myRow("SupplyType"))) Then
            Me.txt_fromGstin.Text = myUtils.cStrTN(cmb_VendorID.SelectedRow.Cells("GSTIN").Value)
            Me.txt_toGstin.Text = myUtils.cStrTN(cmb_CampusID.SelectedRow.Cells("GSTIN").Value)
            Me.textfromTrdName.Text = myUtils.cStrTN(cmb_VendorID.SelectedRow.Cells("PartyName").Value)
            Me.textToTrdName.Text = myUtils.cStrTN(cmb_CampusID.SelectedRow.Cells("DispName").Value)
        End If
    End Sub

    Private Sub cmb_CustomerID_Leave(sender As Object, e As EventArgs) Handles cmb_CustomerID.Leave, cmb_CustomerID.AfterCloseUp
        PopulateBillFromBillToCustomer(cmb_SupplyType.Value, Me.cmb_CustomerID.Value, Me.cmb_CampusID.Value)
    End Sub

    Private Sub cmb_VendorID_Leave(sender As Object, e As EventArgs) Handles cmb_VendorID.Leave, cmb_VendorID.AfterCloseUp
        PopulateBillFromBillToVendor(cmb_SupplyType.Value, Me.cmb_VendorID.Value, Me.cmb_CampusID.Value)
    End Sub

    Private Sub cmb_CampusID_Leave(sender As Object, e As EventArgs) Handles cmb_CampusID.Leave, cmb_CampusID.AfterCloseUp
        If (Not IsNothing(cmb_SupplyType.SelectedRow)) AndAlso cmb_SupplyType.Value = "O" Then
            PopulateBillFromBillToCustomer(cmb_SupplyType.Value, Me.cmb_CustomerID.Value, Me.cmb_CampusID.Value)
        Else
            PopulateBillFromBillToVendor(cmb_SupplyType.Value, Me.cmb_VendorID.Value, Me.cmb_CampusID.Value)
        End If
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If myVueVehicle.mainGrid.myDS.Tables(0).Select("entereddate is null").Length > 0 Then
            MsgBox("Transportation Details Already Exists... You need to cancel First detail", MsgBoxStyle.Information, myWinApp.Vars("AppName"))
        Else
            Dim f As New frmEwayBillPartB
            f.fMat = Me
            If f.PrepForm(myVueVehicle, EnumfrmMode.acAddM, "", "") Then f.ShowDialog()
        End If
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        Dim f As New frmEwayBillPartB
        f.fMat = Me
        If f.PrepForm(Me.myVueVehicle, EnumfrmMode.acEditM, "") Then f.ShowDialog()
    End Sub

    Private Sub UltraGridPart_AfterRowActivate(sender As Object, e As EventArgs) Handles UltraGridPart.AfterRowActivate
        Dim r1 As DataRow = myWinUtils.DataRowFromGridRow(myVueVehicle.mainGrid.myGrid.ActiveRow)
        If Not myUtils.NullNot(r1("entereddate")) Then
            btnEdit.Enabled = False
        Else
            btnEdit.Enabled = True
        End If

    End Sub

    Private Sub btnGenerate_Click(sender As Object, e As EventArgs) Handles btnGenerate.Click
        If Me.VSave() Then
            Dim Params As New List(Of clsSQLParam)
            Params.Add(New clsSQLParam("@gstregid", Me.Model.SelectedRow("campusid")("gstregid"), GetType(Integer), False))
            Params.Add(New clsSQLParam("@ewaybillid", frmIDX, GetType(Integer), False))
            Dim oRet = Me.GenerateParamsOutput("generate", Params)
            If oRet.Success Then
                Me.PrepForm(pView, frmMode, frmIDX, Me.Controller.Data.VarBagXML(Me.vBag))
            Else
                MsgBox(oRet.Message,, myWinApp.Vars("appname"))
            End If
        End If
    End Sub

    Private Sub btnCancelEWB_Click(sender As Object, e As EventArgs) Handles btnCancelEWB.Click
        myRow("CancelDate") = Now.Date
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Me.Controller.PrintingPress.ShowSpecPrint(myView.Model, "crpewaybill", "", frmIDX)
    End Sub

    Private Sub btnSelect_Click(sender As Object, e As EventArgs) Handles btnSelect.Click
        Dim Params As New List(Of clsSQLParam)
        Params.Add(New clsSQLParam("@ewaybillID", frmIDX, GetType(Integer), False))
        Dim rr() As DataRow = Me.AdvancedSelect("transporter", Params)
        If Not rr Is Nothing AndAlso rr.Length > 0 Then
            cmb_TransVendorID.Value = myUtils.cValTN(rr(0)("VendorID"))
        End If
    End Sub

    Private Sub UltraGridItem_AfterCellUpdate(sender As Object, e As CellEventArgs) Handles UltraGridItem.AfterCellUpdate
        'If myUtils.IsInList(e.Cell.Column.Key, "RT", "Cess_Rt", "TXVAL") Then
        '    Dim r1 As DataRow = myWinUtils.DataRowFromGridRow(Me.cmb_CampusID.SelectedRow)
        '    Dim r2 As DataRow = myWinUtils.DataRowFromGridRow(Me.cmb_CustomerID.SelectedRow)
        '    Dim r3 As DataRow = myWinUtils.DataRowFromGridRow(Me.cmb_VendorID.SelectedRow)
        '    If Me.cmb_SupplyType.Value = "O" Then
        '        If Not r1 Is Nothing AndAlso Not r2 Is Nothing Then
        '            If r1("TaxAreaCode") = r2("TaxAreaCode") Then
        '                e.Cell.Row.Cells("CAMT").Value = Math.Round(myUtils.cValTN(e.Cell.Row.Cells("RT").Value) * myUtils.cValTN(e.Cell.Row.Cells("TXVAL").Value) / 100, 2) / 2
        '                e.Cell.Row.Cells("SAMT").Value = Math.Round(myUtils.cValTN(e.Cell.Row.Cells("RT").Value) * myUtils.cValTN(e.Cell.Row.Cells("TXVAL").Value) / 100, 2) / 2
        '                e.Cell.Row.Cells("IAMT").Value = 0
        '            Else
        '                e.Cell.Row.Cells("IAMT").Value = Math.Round(myUtils.cValTN(e.Cell.Row.Cells("RT").Value) * myUtils.cValTN(e.Cell.Row.Cells("TXVAL").Value) / 100, 2)
        '                e.Cell.Row.Cells("CAMT").Value = 0
        '                e.Cell.Row.Cells("SAMT").Value = 0
        '            End If
        '        End If
        '    Else
        '        If Not r1 Is Nothing AndAlso Not r3 Is Nothing > 0 Then
        '            If r1("TaxAreaCode") = r3("TaxAreaCode") Then
        '                e.Cell.Row.Cells("CAMT").Value = Math.Round(myUtils.cValTN(e.Cell.Row.Cells("RT").Value) * myUtils.cValTN(e.Cell.Row.Cells("TXVAL").Value) / 100, 2) / 2
        '                e.Cell.Row.Cells("SAMT").Value = Math.Round(myUtils.cValTN(e.Cell.Row.Cells("RT").Value) * myUtils.cValTN(e.Cell.Row.Cells("TXVAL").Value) / 100, 2) / 2
        '                e.Cell.Row.Cells("IAMT").Value = 0
        '            Else
        '                e.Cell.Row.Cells("IAMT").Value = Math.Round(myUtils.cValTN(e.Cell.Row.Cells("RT").Value) * myUtils.cValTN(e.Cell.Row.Cells("TXVAL").Value) / 100, 2)
        '                e.Cell.Row.Cells("CAMT").Value = 0
        '                e.Cell.Row.Cells("SAMT").Value = 0
        '            End If
        '        End If
        '    End If
        '        e.Cell.Row.Cells("CSAMT").Value = Math.Round(myUtils.cValTN(e.Cell.Row.Cells("Cess_Rt").Value) * myUtils.cValTN(e.Cell.Row.Cells("TXVAL").Value) / 100, 2)
        'End If

    End Sub

    Private Sub cmb_SupplyType_Leave(sender As Object, e As EventArgs) Handles cmb_SupplyType.Leave, cmb_SupplyType.AfterCloseUp
        If cmb_SupplyType.Value = "O" Then
            cmb_VendorID.Visible = False
            UltraLabelVendor.Visible = False
            cmb_CustomerID.Visible = True
            UltraLabelCustomer.Visible = True
        Else
            cmb_CustomerID.Visible = False
            UltraLabelCustomer.Visible = False
            cmb_VendorID.Visible = True
            UltraLabelVendor.Visible = True
        End If
    End Sub

End Class




'Imports risersoft.app.mxform.gst
'Imports risersoft.app.mxent
'Imports risersoft.app.mxform
'Imports risersoft.shared.Extensions

'Public Class frmEwayBill
'    Inherits frmMax

'    Public Sub New()
'        ' This call is required by the designer.
'        InitializeComponent()
'        Me.InitForm()
'        ' Add any initialization after the InitializeComponent() call.
'    End Sub

'    Private Sub InitForm()
'        myView.SetGrid(Me.UltraGrid1)
'        WinFormUtils.SetButtonConf(Me.btnOK, Me.btnCancel, Me.btnSave)
'    End Sub

'    Public Overrides Function PrepForm(oView As clsWinView, ByVal prepMode As EnumfrmMode, ByVal prepIdx As String, Optional ByVal strXML As String = "") As Boolean
'        Me.FormPrepared = False
'        Dim objModel As frmEwayBillModel = Me.InitData("frmEwayBillModel", oview, prepMode, prepIdx, strXML)
'        If Me.BindModel(objModel, oview) Then
'            myWinSQL.AssignCmb(Me.dsCombo, "Campus", "", Me.cmb_CampusID)
'            myWinSQL.AssignCmb(Me.dsCombo, "SupplyType", "", Me.cmb_SupplyType)
'            myWinSQL.AssignCmb(Me.dsCombo, "SubSupplyType", "", Me.cmb_SubSupplyType)
'            myWinSQL.AssignCmb(Me.dsCombo, "DocumentType", "", Me.cmb_DocType)
'            myWinSQL.AssignCmb(Me.dsCombo, "Transporter", "", Me.cmb_TransVendorID)
'            myWinSQL.AssignCmb(Me.dsCombo, "StateCode", "", Me.cmb_fromStateCode)
'            myWinSQL.AssignCmb(Me.dsCombo, "StateCode", "", Me.cmb_toStateCode)
'            myWinSQL.AssignCmb(Me.dsCombo, "StateCode", "", Me.cmb_actfromStateCode)
'            myWinSQL.AssignCmb(Me.dsCombo, "StateCode", "", Me.cmb_acttoStateCode)
'            myWinSQL.AssignCmb(Me.dsCombo, "TransactionType", "", Me.cmb_TransactionType)

'            If myUtils.cValTN(myRow("Invoiceid")) > 0 Then
'                Dim dt As DataTable = Me.Model.DatasetCollection("invoice").Tables(0)
'                If dt.Rows.Count > 0 Then
'                    Me.txt_InvoiceNum.Text = dt.Rows(0)("InvoiceNum")
'                    Me.txt_InvoiceDate.Text = dt.Rows(0)("InvoiceDate")
'                End If
'            Else
'                Dim dt As DataTable = Me.Model.DatasetCollection("challan").Tables(0)
'                If dt.Rows.Count > 0 Then
'                    Me.txt_InvoiceNum.Text = dt.Rows(0)("ChNum")
'                    Me.txt_InvoiceDate.Text = dt.Rows(0)("ChDt")
'                End If
'            End If

'            If prepMode = EnumfrmMode.acEditM AndAlso Not myUtils.NullNot(myRow("ewaybillnum")) Then
'                btnGenerate.Enabled = False
'            End If

'            'cmb_TransactionType.Value = myRow("TransactionType")


'            Me.FormPrepared = True
'        End If
'        Return Me.FormPrepared
'    End Function

'    Public Overrides Function BindModel(NewModel As clsFormDataModel, oView As clsView) As Boolean
'        If MyBase.BindModel(NewModel, oView) Then
'            Dim dt2 As DataTable = Me.Model.DatasetCollection("party").Tables(0)
'            Me.txt_fromGstin.Text = myUtils.cStrTN(dt2.Rows(0)("fromGstin"))
'            Me.txt_toGstin.Text = myUtils.cStrTN(dt2.Rows(0)("toGstin"))
'            Me.textfromTrdName.Text = myUtils.cStrTN(dt2.Rows(0)("fromTrdName"))
'            Me.textToTrdName.Text = myUtils.cStrTN(dt2.Rows(0)("toTrdName"))
'            myView.PrepEdit(Me.Model.GridViews("PartB"))
'            Return True
'        End If
'        Return False
'    End Function

'    Public Overrides Function VSave() As Boolean
'        Me.InitError()
'        VSave = False
'        cm.EndCurrentEdit()
'        If Me.ValidateData() Then
'            If Me.SaveModel() Then
'                Return True
'            End If
'        Else
'            Me.SetError()
'        End If
'        Me.Refresh()
'    End Function

'    Private Sub cmb_TransactionType_Leave(sender As Object, e As EventArgs) Handles cmb_TransactionType.Leave, cmb_TransactionType.AfterCloseUp
'        UltraTabControl3.Tabs("DISP").Visible = ((Not IsNothing(cmb_TransactionType.SelectedRow)) AndAlso myUtils.IsInList(myUtils.cValTN(cmb_TransactionType.Value), "3", "4"))
'        UltraTabControl4.Tabs("SHIP").Visible = ((Not IsNothing(cmb_TransactionType.SelectedRow)) AndAlso myUtils.IsInList(myUtils.cValTN(cmb_TransactionType.Value), "2", "4"))
'    End Sub

'    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
'        If Me.frmMode = EnumfrmMode.acEditM Then
'            If myView.mainGrid.myDS.Tables(0).Select("entereddate is null").Length > 0 Then
'                MsgBox("Transportation Details Already Exists... You need to cancel First detail", MsgBoxStyle.Information, myWinApp.Vars("AppName"))
'            Else
'                Dim f As New frmEwayBillPartB
'                f.fMat = Me
'                If f.PrepForm(myView, EnumfrmMode.acAddM, "", "") Then f.ShowDialog()
'            End If
'        Else
'            MsgBox("You need to save first before proceeding ..", MsgBoxStyle.Information, myWinApp.Vars("AppName"))
'        End If
'    End Sub

'    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
'        Dim f As New frmEwayBillPartB
'        f.fMat = Me
'        If f.PrepForm(Me.myView, EnumfrmMode.acEditM, "") Then f.ShowDialog()
'    End Sub

'    Private Sub btnGenerate_Click(sender As Object, e As EventArgs) Handles btnGenerate.Click
'        If Me.VSave() Then
'            Dim Params As New List(Of clsSQLParam)
'            Params.Add(New clsSQLParam("@gstregid", Me.Model.SelectedRow("campusid")("gstregid"), GetType(Integer), False))
'            Params.Add(New clsSQLParam("@ewaybillid", frmIDX, GetType(Integer), False))
'            Dim oRet = Me.GenerateParamsOutput("generate", Params)
'            If oRet.Success Then
'                Me.PrepForm(pView, frmMode, frmIDX, Me.Controller.Data.VarBagXML(Me.vBag))
'            Else
'                MsgBox(oRet.Message,, myWinApp.Vars("appname"))
'            End If
'        End If
'    End Sub

'    Private Sub btnCancelEWB_Click(sender As Object, e As EventArgs) Handles btnCancelEWB.Click
'        myRow("CancelDate") = Now.Date
'    End Sub

'    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
'        Me.Controller.PrintingPress.ShowSpecPrint(myView.Model, "crpewaybill", "", frmIDX)
'    End Sub

'    Private Sub btnSelect_Click(sender As Object, e As EventArgs) Handles btnSelect.Click
'        Dim Params As New List(Of clsSQLParam)
'        Params.Add(New clsSQLParam("@ewaybillID", frmIDX, GetType(Integer), False))
'        Dim rr() As DataRow = Me.AdvancedSelect("transporter", Params)
'        If Not rr Is Nothing AndAlso rr.Length > 0 Then
'            cmb_TransVendorID.Value = myUtils.cValTN(rr(0)("VendorID"))
'        End If
'    End Sub

'End Class