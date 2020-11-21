Imports risersoft.app.shared
Imports Infragistics.Win.UltraWinGrid
Imports risersoft.shared.Extensions

Public Class frmInvoiceItemSale
    Inherits frmMax
    Dim dv As DataView
    Friend fMat As frmGstInvoiceSale
    Public Sub New()
        MyBase.New()
        InitializeComponent()
        Me.InitForm()
    End Sub

    Private Sub InitForm()

    End Sub

    Public Overloads Function BindModel(NewModel As clsFormDataModel) As Boolean
        myWinSQL.AssignCmb(NewModel.dsCombo, "TY", "", Me.cmb_TY)
        dv = myWinSQL.AssignCmb(NewModel.dsCombo, "HsnSac", "", Me.cmb_Hsn_Sc,, 2)
        myWinSQL.AssignCmb(NewModel.dsCombo, "GstTax", "", Me.cmb_GstTaxType)
        myWinSQL.AssignCmb(NewModel.dsCombo, "uqclist", "", Me.cmb_UQC)
        Me.cmb_I_RT.ValueList = fMat.Model.ValueLists("gstrate1").ComboList
        Me.cmb_C_RT.ValueList = fMat.Model.ValueLists("gstrate2").ComboList
        Me.cmb_S_RT.ValueList = fMat.Model.ValueLists("gstrate2").ComboList

        Return True
    End Function

    Public Overloads Function PrepForm(ByVal r1 As DataRow, IdField As String) As Boolean
        Me.FormPrepared = False
        If Me.BindData(r1) Then
            HandleTY(myUtils.cStrTN(myRow("TY")))
            If frmMode = EnumfrmMode.acAddM Then
                If myUtils.NullNot(myRow("GstTaxType")) Then myRow("GstTaxType") = "Taxable"
            End If
            Me.FormPrepared = True
        End If
        Return Me.FormPrepared
    End Function

    Public Overrides Function VSave() As Boolean
        Me.InitError()
        VSave = False
        If Not cm Is Nothing Then cm.EndCurrentEdit()
        If Me.CanSave Then
            VSave = True
        End If
    End Function

    Private Sub txt_Qty_Leave(sender As Object, e As EventArgs) Handles txt_Qty.Leave
        CalculateTaxableVal()
    End Sub

    Private Sub txt_BasicRate_Leave(sender As Object, e As EventArgs) Handles txt_BasicRate.Leave
        CalculateTaxableVal()
    End Sub

    Private Sub txt_ChargeBeforeTax_Leave(sender As Object, e As EventArgs) Handles txt_ChargeBeforeTax.Leave
        CalculateTaxableVal()
    End Sub

    Private Sub txt_DiscountBeforeTax_Leave(sender As Object, e As EventArgs) Handles txt_DiscountBeforeTax.Leave
        CalculateTaxableVal()
    End Sub

    Private Sub CalculateTaxableVal()
        txt_TXVal.Value = Math.Round(myUtils.cValTN(txt_Qty.Value) * myUtils.cValTN(txt_BasicRate.Value) + (myUtils.cValTN(txt_ChargeBeforeTax.Value) - myUtils.cValTN(txt_DiscountBeforeTax.Value)), 2)
        CalculateTaxVal()
    End Sub

    Private Sub cmb_I_RT_Leave(sender As Object, e As EventArgs) Handles cmb_I_RT.Leave, cmb_I_RT.AfterCloseUp
        CalculateTaxVal()
    End Sub

    Private Sub cmb_C_RT_Leave(sender As Object, e As EventArgs) Handles cmb_C_RT.Leave, cmb_C_RT.AfterCloseUp
        CalculateTaxVal()
    End Sub

    Private Sub cmb_S_RT_Leave(sender As Object, e As EventArgs) Handles cmb_S_RT.Leave, cmb_S_RT.AfterCloseUp
        CalculateTaxVal()
    End Sub

    Private Sub txt_Cess_Rt_Leave(sender As Object, e As EventArgs) Handles txt_Cess_Rt.Leave
        CalculateTaxVal()
    End Sub

    Private Sub CalculateTaxVal()
        Dim TaxAmt As Double = myUtils.cValTN(txt_TXVal.Value)
        txt_IAMT.Value = Math.Round(TaxAmt * myUtils.cValTN(cmb_I_RT.Value) / 100, 2)
        txt_CAMT.Value = Math.Round(TaxAmt * myUtils.cValTN(cmb_C_RT.Value) / 100, 2)
        txt_SAMT.Value = Math.Round(TaxAmt * myUtils.cValTN(cmb_S_RT.Value) / 100, 2)
        txt_CSAMT.Value = Math.Round(TaxAmt * myUtils.cValTN(txt_Cess_Rt.Value) / 100, 2)
    End Sub

    Private Sub HandleTY(Ty As String)
        If Not myUtils.IsInList(myUtils.cStrTN(Ty), "") Then
            dv.RowFilter = "TY = '" & myUtils.cStrTN(Ty) & "'"
        Else
            dv.RowFilter = "0=1"
        End If
    End Sub

    Private Sub cmb_TY_Leave(sender As Object, e As EventArgs) Handles cmb_TY.Leave, cmb_TY.AfterCloseUp
        HandleTY(myUtils.cStrTN(cmb_TY.Value))
        If (IsNothing(cmb_Hsn_Sc.SelectedRow)) Then cmb_Hsn_Sc.Value = DBNull.Value
    End Sub

    Private Sub cmb_Hsn_Sc_Leave(sender As Object, e As EventArgs) Handles cmb_Hsn_Sc.Leave, cmb_Hsn_Sc.AfterCloseUp
        If Not myUtils.NullNot(cmb_Hsn_Sc.Value) Then
            Dim r1 As DataRow = myWinUtils.DataRowFromGridRow(Me.cmb_Hsn_Sc.SelectedRow)
            txt_Hsn_Desc.Value = r1("Description")
            myRow("Hsn_Desc") = txt_Hsn_Desc.Value
        End If
    End Sub

    Private Sub txt_Hsn_Desc_Leave(sender As Object, e As EventArgs) Handles txt_Hsn_Desc.Leave
        If Not (IsNothing(txt_Hsn_Desc.Value)) Then
            myRow("Hsn_Desc") = txt_Hsn_Desc.Value
        End If
    End Sub

End Class
