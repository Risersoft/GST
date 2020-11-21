Imports risersoft.shared
Imports risersoft.app.mxent
Imports System.Runtime.Serialization
Imports Newtonsoft.Json

<DataContract>
Public Class frmGstPartyMainModel
    Inherits risersoft.app.mxform.frmPartyMainModel

    Public Sub New(context As IProviderContext)
        MyBase.New(context)
    End Sub
    Public Overrides Function GenerateParamsOutput(dataKey As String, Params As List(Of clsSQLParam)) As clsProcOutput
        Dim GSTIN As String = myUtils.cStrTN(myContext.SQL.ParamValue("@gstin", Params))
        Dim oRet As New clsProcOutput
        Select Case dataKey.Trim.ToLower
            Case "search"
                Try
                    Dim oProc As New clsGSTNPublic(myContext)
                    oRet.JsonData = JsonConvert.SerializeObject(oProc.SearchGSTIN(GSTIN))
                Catch ex As Exception
                    oRet.JsonData = JsonConvert.SerializeObject(ex)
                End Try
        End Select

        Return oRet
    End Function
    Public Overrides Function DeleteEntity(EntityKey As String, ID As Integer, AcceptWarning As Boolean) As clsProcOutput
        Dim oRet As New clsProcOutput
        Try
            Select Case EntityKey.Trim.ToLower
                Case "customer"
                    Dim oProc As New clsGSTCustomerParty(myContext)
                    oRet = oProc.DeleteVouch(ID, AcceptWarning)
                Case "vendor"
                    Dim oProc As New clsGSTVendorParty(myContext)
                    oRet = oProc.DeleteVouch(ID, AcceptWarning)
            End Select

        Catch ex As Exception
            oRet.AddException(ex)
        End Try


        Return oRet
    End Function

End Class
