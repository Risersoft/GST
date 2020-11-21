Imports Infragistics.Win.UltraWinGrid
Imports risersoft.app.mxform.gst
Imports risersoft.shared.Extensions

Public Class frmGstInvoiceSale
    Inherits frmMax
    Friend fItem As frmInvoiceItemSale
    Dim dvDivision, dvParty As DataView, OrgInvoiceType As String = ""
    Dim oSort As clsWinSort

    Public Sub New()
        MyBase.New()
        InitializeComponent()
        Me.InitForm()
    End Sub

    Public Sub InitForm()
        WinFormUtils.SetButtonConf(Me.btnOK, Me.btnCancel, Me.btnSave)
        myView.SetGrid(UltraGridItemList)

        fItem = New frmInvoiceItemSale
        fItem.AddToPanel(Me.UltraExpandableGroupBoxPanel2, True, System.Windows.Forms.DockStyle.Fill)
        fItem.Enabled = False
        fItem.fMat = Me
    End Sub

    Public Overrides Function PrepForm(oView As clsWinView, ByVal prepMode As EnumfrmMode, ByVal prepIdx As String, Optional ByVal strXML As String = "") As Boolean
        Dim POSTaxAreaID As Integer
        Me.FormPrepared = False
        Dim objModel As frmGstInvoiceSaleModel = Me.InitData("frmGstInvoiceSaleModel", oview, prepMode, prepIdx, strXML)
        If Me.BindModel(objModel, oview) Then
            If myUtils.cValTN(myRow("ConsigneeID")) > 0 Then cmb_ConsigneeID.Value = myUtils.cValTN(myRow("ConsigneeID"))
            Me.cmb_CustomerID.Value = myRow("customerid")
            If Me.cmb_CustomerID.SelectedRow IsNot Nothing Then Me.txt_CTIN.Text = myUtils.cStrTN(cmb_CustomerID.SelectedRow.Cells("GSTIN").Value)

            If myUtils.cValTN(myRow("POSTaxAreaID")) = 0 Then myRow("POSTaxAreaID") = DBNull.Value

            If Me.Model.DatasetCollection.ContainsKey("OrgInvoice") Then
                UltraPanelAmndment.Visible = True
                Dim dt As DataTable = Me.Model.DatasetCollection("OrgInvoice").Tables(0)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    lblInvoiceNo.Text = "Invoice No.  " & " - " & dt.Rows(0)("InvoiceNum")
                    lblInvoiceDate.Text = "Invoice Date" & " - " & dt.Rows(0)("InvoiceDate")
                    lblPartyName.Text = "Party Name" & " - " & dt.Rows(0)("PartyName")
                    lblPartyGSTIN.Text = "Party GSTIN" & " - " & dt.Rows(0)("CTIN")
                    OrgInvoiceType = myUtils.cStrTN(dt.Rows(0)("GstInvoiceType"))
                End If
            Else
                UltraPanelAmndment.Visible = False
            End If

            If Me.Model.DatasetCollection.ContainsKey("CDNInvoice") Then
                UltraPanelCDN.Visible = True
                Dim dt As DataTable = Me.Model.DatasetCollection("CDNInvoice").Tables(0)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    lblInvoiceNo1.Text = "Invoice No.  " & " - " & dt.Rows(0)("InvoiceNum")
                    lblInvoiceDate1.Text = "Invoice Date" & " - " & dt.Rows(0)("InvoiceDate")
                    lblPartyName1.Text = "Party Name" & " - " & dt.Rows(0)("PartyName")
                    lblPartyGSTIN1.Text = "Party GSTIN" & " - " & dt.Rows(0)("CTIN")
                    OrgInvoiceType = myUtils.cStrTN(dt.Rows(0)("GstInvoiceType"))
                End If
            Else
                UltraPanelCDN.Visible = False
            End If

            Me.SetControlsValue(Me.UltraTabControl1.Tabs(0).TabPage)
            oSort = New clsWinSort(myView, Me.btnUp, Me.btnDown, Nothing, "LineSNum")
            oSort.renumber()

            HandleCampus()

            fItem.UltraTabControl1.Tabs("Details").Selected = True

            If myUtils.cValTN(myRow("CDNInvoiceID")) > 0 Then
                cmb_RCHRG.ReadOnly = True
                cmb_POSTaxAreaID.ReadOnly = True
            End If

            If Not myUtils.IsInList(myUtils.cStrTN(Me.cmb_TransactionType.Value), "Sales", "Purchase") Then
                cmb_Rsn.Visible = True
                LabelReason.Visible = True
            Else
                cmb_Rsn.Visible = False
                LabelReason.Visible = False
            End If

            Me.cmb_ShipToTaxAreaID.Value = myUtils.cValTN(myRow("ShipToTaxAreaID"))
            UpdateExportDutyType(myUtils.cStrTN(myRow("PartyGSTRegType")))

            HideTab(myUtils.cValTN(myRow("ShipToTaxAreaID")), myUtils.cStrTN(myRow("TransactionType")))

            Me.FormPrepared = True
        End If
        Return Me.FormPrepared
    End Function

    Public Overrides Function BindModel(NewModel As clsFormDataModel, oView As clsView) As Boolean
        If MyBase.BindModel(NewModel, oView) Then
            myView.PrepEdit(Me.Model.GridViews("ItemList"))
            myWinSQL.AssignCmb(Me.dsCombo, "Campus", "", Me.cmb_CampusID)
            dvParty = myWinSQL.AssignCmb(Me.dsCombo, "Customer", "", Me.cmb_CustomerID, , 2)
            dvDivision = myWinSQL.AssignCmb(Me.dsCombo, "Division", "", Me.cmb_DivisionID, , 2)
            myWinSQL.AssignCmb(Me.dsCombo, "TransactionType", "", Me.cmb_TransactionType)
            myWinSQL.AssignCmb(Me.dsCombo, "DocType", "", Me.cmb_DocumentType)
            myWinSQL.AssignCmb(Me.dsCombo, "PartyGSTReg", "", Me.cmb_PartyGSTRegType)
            myWinSQL.AssignCmb(Me.dsCombo, "POS", "", Me.cmb_ShipFromTaxAreaID)
            myWinSQL.AssignCmb(Me.dsCombo, "POS", "", Me.cmb_ShipToTaxAreaID)
            myWinSQL.AssignCmb(Me.dsCombo, "ExportType", "", Me.cmb_ExportDutyType)
            myWinSQL.AssignCmb(Me.dsCombo, "Reason", "", Me.cmb_Rsn)
            myWinSQL.AssignCmb(Me.dsCombo, "ReturnPeriod", "", Me.cmb_ReturnPeriodID)
            myWinSQL.AssignCmb(Me.dsCombo, "POS", "", Me.cmb_POSTaxAreaID)
            Me.cmb_RCHRG.ValueList = Me.Model.ValueLists("RCHRG").ComboList
            Me.cmb_rfndelg.ValueList = Me.Model.ValueLists("rfndelg").ComboList
            Me.cmb_sec7act.ValueList = Me.Model.ValueLists("sec7act").ComboList
            Me.cmb_clmrfnd.ValueList = Me.Model.ValueLists("clmrfnd").ComboList
            Me.cmb_Diff_Percent.ValueList = Me.Model.ValueLists("Diff_Percent").ComboList
            Me.cmb_SaleBondedWH.ValueList = Me.Model.ValueLists("SaleBondedWH").ComboList
            Me.cmb_p_gst.ValueList = Me.Model.ValueLists("PreGST").ComboList
            myWinSQL.AssignCmb(Me.dsCombo, "Party", "", Me.cmb_ConsigneeID)
            fItem.BindModel(NewModel)
            Return True
        End If
        Return False
    End Function

    Public Overrides Function VSave() As Boolean
        Me.InitError()
        VSave = False
        cm.EndCurrentEdit()
        CalculateTotalAmount()
        If fItem.VSave And Me.ValidateData() Then
            If Me.SaveModel() Then
                Return True
            End If
        Else
            Me.SetError()
        End If
        Me.Refresh()
    End Function

    Private Sub CalculateTax(r1 As DataRow)
        r1("TXVal") = Math.Round(myUtils.cValTN(r1("Qty")) * myUtils.cValTN(r1("BasicRate")) + (myUtils.cValTN(r1("ChargeBeforeTax")) - myUtils.cValTN(r1("DiscountBeforeTax"))), 2)
        Dim TaxAmt As Double = myUtils.cValTN(r1("TXVal"))
        r1("IAMT") = Math.Round(TaxAmt * myUtils.cValTN(r1("I_RT")) * myUtils.cValTN(myRow("Diff_Percent")) / 10000, 2)
        r1("CAMT") = Math.Round(TaxAmt * myUtils.cValTN(r1("C_RT")) * myUtils.cValTN(myRow("Diff_Percent")) / 10000, 2)
        r1("SAMT") = Math.Round(TaxAmt * myUtils.cValTN(r1("S_RT")) * myUtils.cValTN(myRow("Diff_Percent")) / 10000, 2)
        r1("CSAMT") = Math.Round(TaxAmt * myUtils.cValTN(r1("Cess_Rt")) / 100, 2)
    End Sub

    Private Sub CalculateTotalAmount()
        Dim TotalAmt As Decimal = 0
        cm.EndCurrentEdit()
        For Each r1 As DataRow In myView.mainGrid.myDv.Table.Select
            CalculateTax(r1)
            If myUtils.cStrTN(myRow("RCHRG")) = "Y" Then
                TotalAmt = myUtils.cValTN(TotalAmt) + myUtils.cValTN(r1("TXVal"))
            Else
                TotalAmt = myUtils.cValTN(TotalAmt) + myUtils.cValTN(r1("TXVal")) + myUtils.cValTN(r1("CSAMT")) + myUtils.cValTN(r1("IAMT")) + myUtils.cValTN(r1("CAMT")) + myUtils.cValTN(r1("SAMT"))
            End If
        Next

        txt_VAL.Value = TotalAmt
    End Sub

    Private Sub UltraGridItemList_AfterRowActivate(sender As Object, e As EventArgs) Handles UltraGridItemList.AfterRowActivate
        Me.InitError()
        myView.mainGrid.myGrid.UpdateData()

        Dim r1 As DataRow = myWinUtils.DataRowFromGridRow(myView.mainGrid.myGrid.ActiveRow)
        fItem.PrepForm(r1, "InvoiceItemGstID")

        fItem.Enabled = True
    End Sub

    Private Sub UltraGridItemList_BeforeRowDeactivate(sender As Object, e As ComponentModel.CancelEventArgs) Handles UltraGridItemList.BeforeRowDeactivate
        If fItem.VSave Then
        Else
            e.Cancel = True
        End If
    End Sub

    Private Sub HandleCampus()
        dvDivision.RowFilter = FilterDivision(Me.Controller, Me.fRow, cmb_CampusID, Nothing)
        If frmMode = EnumfrmMode.acAddM AndAlso cmb_DivisionID.Rows.Count = 1 Then cmb_DivisionID.Value = myUtils.cValTN(cmb_DivisionID.Rows(0).Cells("DivisionID").Value)
        If cmb_DivisionID.SelectedRow Is Nothing Then cmb_DivisionID.Value = DBNull.Value
    End Sub

    Public Function DivisionFilter(cmb1 As UltraCombo, cmb2 As UltraCombo) As String
        Dim CodeList As New List(Of String)
        If Not IsNothing(cmb1) AndAlso Not IsNothing(cmb1.SelectedRow) Then CodeList.AddRange(Split(myUtils.cStrTN(cmb1.SelectedRow.Cells("DivisionCodeList").Value), ","))
        If Not IsNothing(cmb2) AndAlso Not IsNothing(cmb2.SelectedRow) Then CodeList.AddRange(Split(myUtils.cStrTN(cmb2.SelectedRow.Cells("DivisionCodeList").Value), ","))
        Dim str1 As String = myUtils.MakeCSV2(",", "'0'", True, CodeList.ToArray)
        Dim str2 As String = "DivisionCode in (" & str1 & ")"

        Return str2
    End Function

    Public Function FilterDivision(Controller As clsWinController, fRow As DataRow, cmb1 As Infragistics.Win.UltraWinGrid.UltraCombo, cmb2 As Infragistics.Win.UltraWinGrid.UltraCombo) As String
        Dim str As String = "0=1"
        Dim str1 As String = Controller.AppModel.strFilterDBAppUser(Controller, fRow, "DivisionID")
        Dim str2 As String = DivisionFilter(cmb1, cmb2)
        If str1.Trim.Length > 0 OrElse str2.Trim.Length > 0 Then str = myUtils.CombineWhere(False, str1, str2)
        Return str
    End Function

    Private Sub EnableCtlCampus(Bool As Boolean)
        If myView.mainGrid.myDv.Table.Select.Length > 0 Then btnAddSerial.Enabled = True Else btnAddSerial.Enabled = Bool
        If myView.mainGrid.myDv.Table.Select.Length > 0 Then btnDelItem.Enabled = True Else btnDelItem.Enabled = Bool
    End Sub

    Private Sub cmb_campusid_Leave(sender As Object, e As EventArgs) Handles cmb_CampusID.Leave, cmb_CampusID.AfterCloseUp
        HandleCampus()
    End Sub

    Private Sub cmb_PartyID_Leave(sender As Object, e As EventArgs) Handles cmb_CustomerID.Leave, cmb_CustomerID.AfterCloseUp
        If (Not IsNothing(cmb_CustomerID.SelectedRow)) Then txt_CTIN.Text = myUtils.cStrTN(cmb_CustomerID.SelectedRow.Cells("GSTIN").Value)
    End Sub

    Private Sub HideTab(ShipToTaxAreaID As Integer, TransactionType As String)
        UltraTabControl1.Tabs("EXP").Visible = ((Not IsNothing(cmb_ShipToTaxAreaID.SelectedRow)) AndAlso myUtils.IsInList(myUtils.cStrTN(myUtils.cStrTN(cmb_ShipToTaxAreaID.SelectedRow.Cells("Taxareacode").Value)), "OTH"))
        UltraTabControl1.Tabs("CDN").Visible = (myUtils.IsInList(myUtils.cStrTN(TransactionType), "DNS", "CNS"))
    End Sub

    Private Sub cmb_TransactionType_Leave(sender As Object, e As EventArgs) Handles cmb_TransactionType.Leave, cmb_TransactionType.AfterCloseUp
        HideTab(Me.cmb_ShipToTaxAreaID.Value, Me.cmb_TransactionType.Value)
    End Sub

    Private Sub cmb_ConsigneeID_Leave(sender As Object, e As EventArgs) Handles cmb_ConsigneeID.Leave, cmb_ConsigneeID.AfterCloseUp
        Me.cmb_POSTaxAreaID.Value = HandleConsigneeID()
    End Sub

    Private Function HandleConsigneeID() As Integer
        Dim POSTaxAreaID As Integer
        If myUtils.cValTN(cmb_ConsigneeID.Value) > 0 Then
            POSTaxAreaID = myUtils.cValTN(cmb_ConsigneeID.SelectedRow.Cells("TaxAreaID").Value)
            cmb_POSTaxAreaID.ReadOnly = True
        Else
            POSTaxAreaID = myUtils.cValTN(myRow("POSTaxAreaID"))
            cmb_POSTaxAreaID.ReadOnly = False
        End If
        Return POSTaxAreaID
    End Function

    Private Sub btnAddSerial_Click(sender As Object, e As EventArgs) Handles btnAddSerial.Click
        If myView.mainGrid.myDv.Count = 0 OrElse fItem.VSave Then
            Dim gr As UltraGridRow
            gr = myView.mainGrid.myGrid.DisplayLayout.Bands(0).AddNew()
            gr.Cells("LineSNum").Value = myUtils.MaxVal(myView.mainGrid.myDv.Table, "LineSNum") + 1
            myView.mainGrid.UpdateData()
            oSort.renumber()

            fItem.Focus()
        End If
    End Sub

    Private Sub btnDelItem_Click(sender As Object, e As EventArgs) Handles btnDelItem.Click
        myView.mainGrid.ButtonAction("del")
        If myView.mainGrid.myDv.Table.Select.Length = 0 Then
            fItem.FormPrepared = False
            oSort.renumber()
            fItem.Enabled = False
            EnableCtlCampus(True)
        End If
    End Sub

    Private Sub cmb_PartyGSTRegType_Leave(sender As Object, e As EventArgs) Handles cmb_PartyGSTRegType.Leave, cmb_PartyGSTRegType.AfterCloseUp
        UpdateExportDutyType(myUtils.cStrTN(cmb_PartyGSTRegType.Value))
    End Sub

    Private Sub cmb_ShipToTaxAreaID_Leave(sender As Object, e As EventArgs) Handles cmb_ShipToTaxAreaID.Leave, cmb_ShipToTaxAreaID.AfterCloseUp
        UpdateExportDutyType(myUtils.cStrTN(cmb_PartyGSTRegType.Value))
        HideTab(Me.cmb_ShipToTaxAreaID.Value, Me.cmb_TransactionType.Value)
    End Sub

    Private Sub UpdateExportDutyType(PartyGSTRegType As String)
        If ((Not IsNothing(cmb_PartyGSTRegType.SelectedRow)) AndAlso myUtils.IsInList(myUtils.cStrTN(PartyGSTRegType), "SEZ")) OrElse ((Not IsNothing(cmb_ShipToTaxAreaID.SelectedRow)) AndAlso myUtils.IsInList(myUtils.cStrTN(myUtils.cStrTN(cmb_ShipToTaxAreaID.SelectedRow.Cells("Taxareacode").Value)), "OTH")) Then
            cmb_ExportDutyType.ReadOnly = False
        Else
            cmb_ExportDutyType.ReadOnly = True
        End If
    End Sub

End Class