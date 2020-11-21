Imports Newtonsoft.Json
Imports risersoft.shared
Imports System.IO
Imports risersoft.shared.dal
Imports risersoft.app.dataporter
Imports System.Reflection
Imports risersoft.app.mxent
Imports risersoft.API.GSTN.GSTR2
Imports risersoft.shared.cloud
Imports risersoft.shared.portable.Models.Auth
Public Class Import_DelTaskProvider
    Inherits clsTaskProviderBase
    Protected Friend oMaster As clsMasterDataFICO

    Public Sub New(controller As clsAppController)
        MyBase.New(controller)
        oMaster = New clsMasterDataFICO(myContext)

    End Sub

    Public Overrides ReadOnly Property IsApiTask As Boolean = True

    Public Overrides Function ExecuteServer(rTask As DataRow, input As clsProcOutput) As Task(Of clsProcOutput)
        Dim ImportFileId As String = myUtils.cStrTN(rTask("importfileid"))
        Dim oProc As clsGSTDocBase, oproc2 As clsGSTPartyBase
        Dim sql2 = "select DocType, ImportFileId from ImportFile where ImportFileId = '" & ImportFileId & "'"
        Dim dt1 As DataTable = myContext.DataProvider.objSQLHelper.ExecuteDataset(CommandType.Text, sql2).Tables(0)
        For Each r1 As DataRow In dt1.Select()
            Select Case myUtils.cStrTN(r1("DocType")).Trim.ToLower
                Case "pc"
                    oProc = New clsGSTAdvanceBase(myContext, "PC", "GSTR1")
                Case "pv"
                    oProc = New clsGSTAdvanceBase(myContext, "PV", "GSTR2")
                Case "is", "ip", "gstr2a", "gstr1"
                    oProc = New clsGSTInvoiceSale(Me.myContext)
                Case "customer"
                    oproc2 = New clsGSTCustomerParty(myContext)
                Case "vendor"
                    oproc2 = New clsGSTVendorParty(myContext)
            End Select
        Next

        If oProc IsNot Nothing Then
            Dim oRet = oProc.DeleteVouchImport(ImportFileId.ToString) +
            oProc.DeleteCPVouchImport(ImportFileId.ToString)
            Return Task.FromResult(oRet)

        End If

    End Function


End Class
