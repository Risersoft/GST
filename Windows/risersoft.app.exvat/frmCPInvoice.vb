Imports Infragistics.Win.UltraWinGrid
Imports risersoft.app.mxform
Imports risersoft.app.mxform.gst
Imports risersoft.shared.Extensions

Public Class frmCPInvoice
    Inherits frmMax
    Dim dvParty As DataView
    Dim oSort As clsWinSort

    Public Sub New()
        MyBase.New()
        InitializeComponent()
        Me.InitForm()
    End Sub

    Public Sub InitForm()
        WinFormUtils.SetButtonConf(Me.btnOK, Me.btnCancel, Me.btnSave)
        myView.SetGrid(UltraGridItemList)
    End Sub

    Public Overrides Function PrepForm(oView As clsWinView, ByVal prepMode As EnumfrmMode, ByVal prepIdx As String, Optional ByVal strXML As String = "") As Boolean
        Me.FormPrepared = False
        Dim objModel As frmCPInvoiceModel = Me.InitData("frmCPInvoiceModel", oview, prepMode, prepIdx, strXML)
        If Me.BindModel(objModel, oview) Then
            If myUtils.cValTN(myRow("OINum")) > 0 Then
                UltraPanelAmndment.Visible = True
                lblInvoiceNo.Text = "Invoice No.  " & " - " & myRow("OINum")
                lblInvoiceDate.Text = "Invoice Date" & " - " & myRow("OIdt")
                If Me.cmb_VendorID.SelectedRow IsNot Nothing Then lblPartyName.Text = myUtils.cStrTN(cmb_VendorID.SelectedRow.Cells("PartyName").Value)
                lblPartyGSTIN.Text = "Party GSTIN" & " - " & myRow("CTIN")
            Else
                UltraPanelAmndment.Visible = False
            End If


            If Not myUtils.NullNot(Me.cmb_CustomerID.Value) Then
                cmb_CustomerID.Visible = True
                LabelCustomer.Visible = True
                cmb_VendorID.Visible = False
                LabelVendor.Visible = False
            Else
                cmb_CustomerID.Visible = False
                LabelCustomer.Visible = False
                cmb_VendorID.Visible = True
                LabelVendor.Visible = True
            End If

            Dim dt1 As DataTable = Me.Model.DatasetCollection("GSTNAction").Tables(0)
            If dt1.Rows.Count > 0 AndAlso dt1.Rows(0)("ActionFlag") = "P" Then
                btnUnmark.Visible = True
                btnMark.Visible = False
            Else
                btnUnmark.Visible = False
                btnMark.Visible = True
            End If

            Me.FormPrepared = True
        End If
        Return Me.FormPrepared
    End Function

    Public Overrides Function BindModel(NewModel As clsFormDataModel, oView As clsView) As Boolean
        If MyBase.BindModel(NewModel, oView) Then
            myView.PrepEdit(Me.Model.GridViews("ItemList"))
            myWinSQL.AssignCmb(Me.dsCombo, "GstReg", "", Me.cmb_GstRegID)
            dvParty = myWinSQL.AssignCmb(Me.dsCombo, "Customer", "", Me.cmb_CustomerID, , 2)
            dvParty = myWinSQL.AssignCmb(Me.dsCombo, "Vendor", "", Me.cmb_VendorID, , 2)
            myWinSQL.AssignCmb(Me.dsCombo, "B2BType", "", Me.cmb_inv_typ)
            myWinSQL.AssignCmb(Me.dsCombo, "POS", "", Me.cmb_POSTaxAreaID)
            Me.cmb_RCHRG.ValueList = Me.Model.ValueLists("RCHRG").ComboList
            Me.cmb_IsAmendment.ValueList = Me.Model.ValueLists("IsAmendment").ComboList
            myWinSQL.AssignCmb(Me.dsCombo, "CDNType", "", Me.cmb_ntty)

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

    Private Sub btnMark_Click(sender As Object, e As EventArgs) Handles btnMark.Click
        If Me.VSave() Then
            Dim oRet As clsProcOutput = Me.GenerateIDOutput("mark", frmIDX)
            If oRet.Success Then
                Me.PrepForm(Me.pView, Me.frmMode, Me.frmIDX, Me.Controller.Data.VarBagXML(Me.vBag))
            End If
            MsgBox(oRet.Message, MsgBoxStyle.Information, myWinApp.Vars("appname"))
        End If
    End Sub

    Private Sub btnUnmark_Click(sender As Object, e As EventArgs) Handles btnUnmark.Click
        If Me.VSave() Then
            Dim oRet As clsProcOutput = Me.GenerateIDOutput("unmark", frmIDX)
            If oRet.Success Then
                Me.PrepForm(Me.pView, Me.frmMode, Me.frmIDX, Me.Controller.Data.VarBagXML(Me.vBag))
            End If
            MsgBox(oRet.Message, MsgBoxStyle.Information, myWinApp.Vars("appname"))
        End If
    End Sub

End Class
