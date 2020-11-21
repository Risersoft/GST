Imports Newtonsoft.Json
Imports risersoft.shared
Imports System.IO
Imports risersoft.shared.dal
Imports risersoft.app.dataporter
Imports System.Reflection
Imports risersoft.app.mxent
Imports GSTN.API.Library.Models.GstNirvana

Public Class Import_ISTaskProvider
    Inherits ImportTaskProviderInvoiceBase(Of clsGSTInvoiceSale)

    Public Overrides Property RowsNotMatchingCode As Integer = 212
    Public Overrides Property PreValidationErrorCode As Integer = 218
    Public Overrides Property DocType As String = "IS"

    Public Overrides Property TemplateName As String = "InvoiceSale"
    Public Overrides Property TemplateFunctionName As String = "GstListInvoiceTemplate()"


    Public Sub New(controller As clsAppController)
        MyBase.New(controller)
        Me.fncPartyType = Function(rXL)
                              Return "C"
                          End Function
        Me.fncPartySubTypeTable = Function(rXL)
                                      Return "Customer"
                                  End Function

    End Sub

    Public Overrides Async Function ExecuteServer(rTask As DataRow, input As clsProcOutput) As Task(Of clsProcOutput)
        Dim Params = JsonConvert.DeserializeObject(Of Dictionary(Of String, String))(myUtils.cStrTN(rTask("infojson")))
        Dim path = Await myFuncs.DownloadIfReqd(myContext, rTask, Params("path"))
        Dim oRet = Await Me.ExecuteImport(path, myUtils.cStrTN(rTask("username")), myUtils.cStrTN(rTask("importfileid")))
        Return oRet
    End Function




    Public Overrides Function ExecutePreValidation(provider As IAppDataProvider, rInv As DataRow, dtItems As DataTable, rXL As DataRow, objGroup As clsRowGroup) As clsProcOutput
        Dim dic = objGroup.dic
        Dim oRet = MyBase.ExecutePreValidation(provider, rInv, dtItems, rXL, objGroup)
        Dim oProc As New clsGSTInvoiceSale(provider)
        oProc.oMaster = Me.oMaster
        rInv("uniquekey") = oProc.CalcUniqueKey(rXL("gstin"), myUtils.cValTN(rInv("returnperiodid")), rInv("invoicenum"), myUtils.cValTN(rInv("isamendment")))
        oProc.PopulateGstType(rInv, dtItems, myUtils.cStrTN(rXL("shipto")))

        Return oRet

    End Function
    Protected Overrides Sub UpdateItem(rSource As DataRow, rDest As DataRow)
        For Each str1 As String In New String() {"gstTaxtype", "ty"}
            Dim rrPOS() As DataRow = dsMaster.Tables(str1).Select("Descrip='" & myUtils.cStrTN(rSource(str1)) & "'")
            If rrPOS.Length > 0 Then rDest(str1) = rrPOS(0)("codeword")
        Next
        If myUtils.IsInList(myUtils.cStrTN(rDest("ty")), "S") Then
            If myUtils.cValTN(rDest("qty")) = 0 Then rDest("qty") = 1
            If myUtils.cStrTN(rDest("uqc")).Trim.Length = 0 Then rDest("uqc") = "NOS"
        End If
        rDest("basicrate") = If(myUtils.cValTN(rDest("qty")) > 0, myUtils.cValTN(rSource("grossvalue")) / myUtils.cValTN(rDest("qty")), 0)

        'At Time of Import, setting Values Null as 0.
        For Each str1 As String In New String() {"CESS_RT", "I_RT", "C_RT", "S_RT", "IAMT", "CAMT", "SAMT", "CSAMT"}
            rDest(str1) = myUtils.cValTN(rSource(str1))
        Next

        If myUtils.cValTN(rSource("I_RT")) > 0 Then
            rDest("RT") = myUtils.cValTN(rSource("I_RT"))
        Else
            rDest("RT") = myUtils.cValTN(rSource("C_RT")) + myUtils.cValTN(rSource("S_RT"))
        End If


    End Sub

End Class
