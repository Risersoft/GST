Imports risersoft.app.mxent
Imports risersoft.shared

Public Class clsGSTPaymentBase
    Inherits clsGSTDocBase
    Dim TolTxVal, TolTxAmt As Decimal, POS As Boolean
    Public Sub New(context As IProviderContext, DocType As String, ReturnField As String)
        MyBase.New(context, DocType, ReturnField, "PaymentID")

    End Sub
    Public Overrides Sub PopulateCalc(IDValue As Integer, rVouch As DataRow, rGstReg As DataRow, dtVouchItem As DataTable, dtCalc As DataTable, rCDN As DataRow, rOrig As DataRow, dsMaster As DataSet)
        Dim rCalc = Me.GetCalcRow(IDValue, rVouch, dtCalc, dtVouchItem, rOrig)
        If myUtils.IsInList(Me.DocType, "PPU") Then
            For Each str1 As String In New String() {"i", "c", "s", "cs"}
                rCalc("tx" & str1 & "summ") = myUtils.cValTN(dtVouchItem.Compute($"sum(tx_{str1})", $"isnull({IDField},0)={IDValue}"))
            Next
        End If
        Dim oProc = Me.GetValidator(IDValue, rVouch, rGstReg, rCDN, rOrig, dsMaster)
        Dim IsTaxable As Boolean = (dtVouchItem.Select("isnull(GstTaxType,'') in ('','taxable')").Length > 0)
        oProc.SetValue("IsTaxable", IsTaxable)

        For Each str1 As String In New String() {"gstr1", "gstr2", "gstr6"}
            rCalc(str1 & "section") = Me.CalculateSection(oProc, str1, dsMaster)
        Next

        For Each rVouchItem As DataRow In dtVouchItem.Select($"isnull({IDField},0)={IDValue}")
            For Each str1 As String In New String() {"gstr3b31", "gstr3b32", "gstr3b4a", "gstr3b4d", "gstr3b5"}
                oProc.AddOrUpdateRow(rVouchItem, "item")
                rVouchItem(str1 & "section") = Me.CalculateSection(oProc, str1, dsMaster)
            Next
        Next
    End Sub

End Class
Public Class clsGSTPaymentPurch
    Inherits clsGSTPaymentBase
    Public Sub New(context As IProviderContext)
        MyBase.New(context, "IP", "GSTR2")
    End Sub


    Protected Friend Overrides Function GetFactor(rVouch As DataRow) As Decimal
        Dim Factor = If(myUtils.IsInList(myUtils.cStrTN(rVouch("GSTInvoiceSubType")), "C"), -1, 1)
        Return Factor
    End Function
End Class
Public Class clsGSTPaymentSale
    Inherits clsGSTPaymentBase
    Public Sub New(context As IProviderContext)
        MyBase.New(context, "IS", "GSTR1")
    End Sub
    Protected Friend Overrides Function GetFactor(rVouch As DataRow) As Decimal
        Dim Factor = If(myUtils.IsInList(myUtils.cStrTN(rVouch("GSTInvoiceSubType")), "D"), -1, 1)
        Return Factor
    End Function
    Protected Friend Overrides Function GetFactor2(rVouch As DataRow) As Decimal
        Dim Factor = If(myUtils.IsInList(myUtils.cStrTN(rVouch("RCHRG")), "Y", "YES"), 0, 1)
        Return Factor
    End Function

End Class
