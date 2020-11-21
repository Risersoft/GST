Imports Infragistics.Win.UltraWinGrid
Imports risersoft.app.mxform.gst
Imports risersoft.shared.Extensions

Public Class frmGstInvoicePurch
    Inherits frmMax
    Friend fItem As frmInvoiceItemPurch
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

        fItem = New frmInvoiceItemPurch
        fItem.AddToPanel(Me.UltraExpandableGroupBoxPanel2, True, System.Windows.Forms.DockStyle.Fill)
        fItem.Enabled = False
        fItem.fMat = Me
    End Sub

    Public Overrides Function PrepForm(oView As clsWinView, ByVal prepMode As EnumfrmMode, ByVal prepIdx As String, Optional ByVal strXML As String = "") As Boolean
        Dim POSTaxAreaID As Integer
        Me.FormPrepared = False
        Dim objModel As frmGstInvoicePurchModel = Me.InitData("frmGstInvoicePurchModel", oview, prepMode, prepIdx, strXML)
        If Me.BindModel(objModel, oview) Then
            If myUtils.cValTN(myRow("ConsigneeID")) > 0 Then cmb_ConsigneeID.Value = myUtils.cValTN(myRow("ConsigneeID"))
            Me.cmb_VendorID.Value = myRow("VendorID")
            If Me.cmb_VendorID.SelectedRow IsNot Nothing Then Me.txt_CTIN.Text = myUtils.cStrTN(Me.cmb_VendorID.SelectedRow.Cells("gstin").Value)

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
            HandleIsSEZ(myUtils.cStrTN(myRow("is_sez")))

            fItem.UltraTabControl1.Tabs("Details").Selected = True

            If myUtils.cValTN(myRow("CDNInvoiceID")) > 0 Then
                cmb_RCHRG.ReadOnly = True
                cmb_POSTaxAreaID.ReadOnly = True
            End If

            If Not myUtils.IsInList(myUtils.cStrTN(Me.cmb_TransactionType.Value), "Sales", "Purchase") Then
                cmb_rsn.Visible = True
                LabelReason.Visible = True
            Else
                cmb_rsn.Visible = False
                LabelReason.Visible = False
            End If

            HideTab(myUtils.cValTN(myRow("ShipFromTaxAreaID")), myUtils.cStrTN(myRow("TransactionType")))

            If (Not IsNothing(cmb_CampusID.SelectedRow)) Then HideISDTab(myUtils.cStrTN(cmb_CampusID.SelectedRow.Cells("GstRegType").Value))

            Me.FormPrepared = True
        End If
        Return Me.FormPrepared
    End Function

    Public Overrides Function BindModel(NewModel As clsFormDataModel, oView As clsView) As Boolean
        If MyBase.BindModel(NewModel, oView) Then
            myView.PrepEdit(Me.Model.GridViews("ItemList"))
            myWinSQL.AssignCmb(Me.dsCombo, "Campus", "", Me.cmb_CampusID)
            dvParty = myWinSQL.AssignCmb(Me.dsCombo, "Vendor", "", Me.cmb_VendorID, , 2)
            dvDivision = myWinSQL.AssignCmb(Me.dsCombo, "Division", "", Me.cmb_DivisionID, , 2)
            myWinSQL.AssignCmb(Me.dsCombo, "TransactionType", "", Me.cmb_TransactionType)
            myWinSQL.AssignCmb(Me.dsCombo, "PartyGSTReg", "", Me.cmb_PartyGSTRegType)
            myWinSQL.AssignCmb(Me.dsCombo, "POS", "", Me.cmb_ShipFromTaxAreaID)
            myWinSQL.AssignCmb(Me.dsCombo, "POS", "", Me.cmb_ShipToTaxAreaID)
            Me.cmb_IsForeignCurrency.ValueList = Me.Model.ValueLists("Currency").ComboList
            myWinSQL.AssignCmb(Me.dsCombo, "Reason", "", Me.cmb_rsn)
            myWinSQL.AssignCmb(Me.dsCombo, "ReturnPeriod", "", Me.cmb_ReturnPeriodID)
            myWinSQL.AssignCmb(Me.dsCombo, "POS", "", Me.cmb_POSTaxAreaID)
            Me.cmb_RCHRG.ValueList = Me.Model.ValueLists("RCHRG").ComboList
            Me.cmb_rfndelg.ValueList = Me.Model.ValueLists("rfndelg").ComboList
            Me.cmb_sec7act.ValueList = Me.Model.ValueLists("sec7act").ComboList
            Me.cmb_Diff_Percent.ValueList = Me.Model.ValueLists("Diff_Percent").ComboList
            Me.cmb_p_gst.ValueList = Me.Model.ValueLists("PreGST").ComboList
            Me.cmb_is_sez.ValueList = Me.Model.ValueLists("IsSEZ").ComboList
            myWinSQL.AssignCmb(Me.dsCombo, "Party", "", Me.cmb_ConsigneeID)
            Me.cmb_ISCreditISD.ValueList = Me.Model.ValueLists("ISCreditISD").ComboList

            fItem.BindModel(NewModel)
            Return True
        End If
        Return False
    End Function

    Public Overrides Function VSave() As Boolean
        Me.InitError()
        VSave = False
        cm.EndCurrentEdit()
        Me.CheckTaxCredit(myWinUtils.DataRowFromGridRow(cmb_CampusID.SelectedRow), myRow.Row, Me.dsForm.Tables("invoiceitemGst"))
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

    Public Sub CheckTaxCredit(rGstReg As DataRow, rInv As DataRow, dtInvItem As DataTable)
        If cmb_CampusID.Value > 0 Then
            If (Not myUtils.cBoolTN(rGstReg("partialcredit"))) Then
                For Each rInvItem As DataRow In dtInvItem.Select("isnull(invoiceid,0) in (0," & myUtils.cValTN(rInv("invoiceid")) & ")")
                    If myUtils.IsInList(myUtils.cStrTN(rInvItem("elg")), "NO") Then
                        rInvItem("tx_i") = 0
                        rInvItem("tx_s") = 0
                        rInvItem("tx_c") = 0
                        rInvItem("tx_cs") = 0
                    Else
                        rInvItem("tx_i") = rInvItem("iamt")
                        rInvItem("tx_s") = rInvItem("samt")
                        rInvItem("tx_c") = rInvItem("camt")
                        rInvItem("tx_cs") = rInvItem("csamt")
                    End If
                Next
            End If
        End If
    End Sub

    Private Sub CalculateTax(r1 As DataRow)
        'r1("TXVal") = Math.Round(myUtils.cValTN(r1("Qty")) * myUtils.cValTN(r1("BasicRate")), 2)
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

        'txt_VAL.Value = TotalAmt
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
        If (Not IsNothing(cmb_CampusID.SelectedRow)) Then HideISDTab(myUtils.cStrTN(cmb_CampusID.SelectedRow.Cells("GSTRegType").Value))
    End Sub

    Private Sub cmb_PartyID_Leave(sender As Object, e As EventArgs) Handles cmb_VendorID.Leave, cmb_VendorID.AfterCloseUp
        If (Not IsNothing(cmb_VendorID.SelectedRow)) Then txt_CTIN.Text = myUtils.cStrTN(cmb_VendorID.SelectedRow.Cells("GSTIN").Value)
    End Sub

    Private Sub HideTab(ShipFromTaxAreaID As Integer, TransactionType As String)
        UltraTabControl1.Tabs("IMP").Visible = ((Not IsNothing(cmb_ShipFromTaxAreaID.SelectedRow)) AndAlso myUtils.IsInList(myUtils.cStrTN(myUtils.cStrTN(cmb_ShipFromTaxAreaID.SelectedRow.Cells("Taxareacode").Value)), "OTH"))
        UltraTabControl1.Tabs("CDN").Visible = (myUtils.IsInList(myUtils.cStrTN(TransactionType), "DNP", "CNP"))
    End Sub

    Private Sub HideISDTab(GstRegType As String)
        If GstRegType = "ISD" Then
            cmb_ISCreditISD.Visible = False
            UltraLabelISD.Visible = False
            UltraLabelRCHRG.Visible = False
            cmb_RCHRG.Visible = False
            UltraPanel2.Visible = False
            UltraPanel1.Visible = False
            fItem.UltraPanel2.Visible = False
            fItem.UltraLabelQTY.Visible = False
            fItem.txt_Qty.Visible = False
        End If
    End Sub

    Private Sub cmb_TransactionType_Leave(sender As Object, e As EventArgs) Handles cmb_TransactionType.Leave, cmb_TransactionType.AfterCloseUp
        HideTab(Me.cmb_ShipFromTaxAreaID.Value, Me.cmb_TransactionType.Value)
    End Sub

    Private Sub cmb_ShipFromTaxAreaID_Leave(sender As Object, e As EventArgs) Handles cmb_ShipFromTaxAreaID.Leave, cmb_ShipFromTaxAreaID.AfterCloseUp
        HideTab(Me.cmb_ShipFromTaxAreaID.Value, Me.cmb_TransactionType.Value)
    End Sub

    Private Sub cmb_is_sez_Leave(sender As Object, e As EventArgs) Handles cmb_is_sez.Leave, cmb_is_sez.AfterCloseUp
        HandleIsSEZ(myUtils.cStrTN(cmb_is_sez.Value))
    End Sub

    Private Sub HandleIsSEZ(IzSez As String)
        If Not myUtils.IsInList(myUtils.cStrTN(IzSez), "") Then
            If myUtils.IsInList(myUtils.cStrTN(IzSez), "Y") Then
                txt_stin.ReadOnly = False
            Else
                txt_stin.ReadOnly = True
                txt_stin.Text = String.Empty
            End If
        Else
            txt_stin.Text = String.Empty
            txt_stin.ReadOnly = True
        End If
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

    Private Sub cmb_IsForeignCurrency_Leave(sender As Object, e As EventArgs) Handles cmb_IsForeignCurrency.Leave, cmb_IsForeignCurrency.AfterCloseUp
        If myUtils.IsInList((cmb_IsForeignCurrency.Value), "True") Then
            txt_ex_rt.ReadOnly = False
        Else
            txt_ex_rt.ReadOnly = True
        End If
    End Sub

End Class