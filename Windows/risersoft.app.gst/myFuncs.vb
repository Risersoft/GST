Imports risersoft.app.config
Imports risersoft.app.exvat

Public Class myFuncs
    Public DoRefresh As Boolean = False
    Public Shared GetFormFunc As Func(Of String, frmMax)
    Public Shared Function GetForm(DocType As String) As frmMax
        Dim f As frmMax
        Select Case DocType.Trim.ToLower
            Case "pc"
                f = New frmGstPaymentCustomer
            Case "pv"
                f = New frmGstPaymentVendor
            Case "is"
                f = New frmGstInvoiceSale
            Case "ip"
                f = New frmGstInvoicePurch
            Case "customer", "vendor"
                f = New frmGstPartyMain
            Case "tp"
                f = New frmGstCampus
            Case "hsn"
                f = New frmHsnSac
            Case "ewb"
                f = New frmEwayBill

        End Select
        Return f
    End Function


End Class
