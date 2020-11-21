Imports Newtonsoft.Json
Imports risersoft.shared
Imports System.IO
Imports risersoft.shared.dal
Imports risersoft.app.dataporter
Imports System.Reflection
Imports risersoft.app.mxent
Imports GSTN.API.Library.Models.GstNirvana

Public Class Import_IPTaskProvider
    Inherits ImportTaskProviderInvoiceBase(Of clsGSTInvoicePurch)

    Public Overrides Property RowsNotMatchingCode As Integer = 522
    Public Overrides Property PreValidationErrorCode As Integer = 528
    Public Overrides Property DocType As String = "IP"

    Public Overrides Property TemplateName As String = "InvoicePurch"
    Public Overrides Property TemplateFunctionName As String = "GstListInvoiceTemplate()"

    Public Sub New(controller As clsAppController)
        MyBase.New(controller)
        Me.fncPartyType = Function(rXL)
                              Return "V"
                          End Function
        Me.fncPartySubTypeTable = Function(rXL)
                                      Return "Vendor"
                                  End Function
    End Sub

    Public Overrides Async Function ExecuteServer(rTask As DataRow, input As clsProcOutput) As Task(Of clsProcOutput)
        Dim Params = JsonConvert.DeserializeObject(Of Dictionary(Of String, String))(myUtils.cStrTN(rTask("infojson")))
        Dim path = Await myFuncs.DownloadIfReqd(myContext, rTask, Params("path"))
        Me.SetOperation(If(Params.ContainsKey("compare"), myUtils.cBoolTN(Params("compare")), False))
        Dim oRet = Await Me.ExecuteImport(path, myUtils.cStrTN(rTask("username")), myUtils.cStrTN(rTask("importfileid")))
        Return oRet
    End Function


    Public Overrides Function ExecutePreValidation(provider As IAppDataProvider, rInv As DataRow, dtItems As DataTable, rXL As DataRow, objGroup As clsRowGroup) As clsProcOutput
        Dim dic = objGroup.dic
        Dim oRet = MyBase.ExecutePreValidation(provider, rInv, dtItems, rXL, objGroup)
        Dim oProc As New clsGSTInvoicePurch(provider)
        oProc.oMaster = Me.oMaster
        Dim CTIN = If(myUtils.NullNot(rXL("ctin")), System.Guid.NewGuid.ToString, myUtils.cStrTN(rXL("ctin")))
        rInv("uniquekey") = oProc.CalcUniqueKey(CTIN, myUtils.cValTN(rInv("returnperiodid")), rInv("invoicenum"), myUtils.cValTN(rInv("isamendment")))
        oProc.PopulateGstType(rInv, dtItems, myUtils.cStrTN(rXL("shipfrom")))
        Return oRet

    End Function
    Protected Overrides Sub UpdateItem(rSource As DataRow, rDest As DataRow)
        rDest("ITCReversal_4243") = myUtils.IsInList(myUtils.cStrTN(rSource("ITCReversal_4243")), "y", "yes")
        Me.CalculateDate(rSource, rDest, "ITCDate")
        For Each str1 As String In New String() {"gstTaxtype", "ty", "elg"}
            Dim rrPOS() As DataRow = dsMaster.Tables(str1).Select("Descrip='" & myUtils.cStrTN(rSource(str1)) & "'")
            If rrPOS.Length > 0 Then rDest(str1) = rrPOS(0)("codeword")
        Next
        If myUtils.IsInList(myUtils.cStrTN(rDest("ty")), "S") Then
            If myUtils.cValTN(rDest("qty")) = 0 Then rDest("qty") = 1
            If myUtils.cStrTN(rDest("uqc")).Trim.Length = 0 Then rDest("uqc") = "NOS"
        End If

        rDest("basicrate") = If(myUtils.cValTN(rDest("qty")) > 0, myUtils.cValTN(rSource("TXVAL")) / myUtils.cValTN(rDest("qty")), 0)

        'At Time of Import, setting Values Null as 0.
        For Each str1 As String In New String() {"CESS_RT", "I_RT", "C_RT", "S_RT", "IAMT", "CAMT", "SAMT", "CSAMT", "tx_i", "tx_c", "tx_s", "tx_cs"}
            rDest(str1) = myUtils.cValTN(rSource(str1))
        Next

        If myUtils.cValTN(rSource("I_RT")) > 0 Then
            rDest("RT") = myUtils.cValTN(rSource("I_RT"))
        Else
            rDest("RT") = myUtils.cValTN(rSource("C_RT")) + myUtils.cValTN(rSource("S_RT"))
        End If
    End Sub
    Public Overrides Async Function TryCompareRowGroup(provider As IAppDataProvider, objGroup As clsRowGroup, objPortion As clsPortionInfo) As Task(Of clsProcOutput)
        Dim dic As New clsCollecString(Of DataRow), dicSQL As New clsCollecString(Of String)
        Dim oRet = Await Me.HandleGroupData(provider, objGroup, objPortion)
        Dim info As New ImportErrorInfo()
        Dim dsDB = oRet.Data

        If oRet.Success Then
            For Each str1 As String In New String() {"txval"}
                Dim FileValue = objGroup.Rows.Sum(Function(r1) myUtils.cValTN(r1("txval")))
                Dim DBValue = dsDB.Tables("item").Select.Sum(Function(r1) myUtils.cValTN(r1("txval")))
                If FileValue <> DBValue Then
                    oRet.AddError($"Mismatch in {str1}")
                    info.Voucher.AddError(Me.PreValidationErrorCode, $"Mismatch in {str1}")
                End If
            Next
        Else
            info.Voucher.AddError(Me.PreValidationErrorCode, "Voucher not found")
        End If

        If Not oRet.Success Then Me.UpdateError(objGroup.Rows, info.Voucher)

        Return oRet

    End Function

End Class
