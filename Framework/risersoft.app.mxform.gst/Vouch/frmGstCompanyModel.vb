Imports risersoft.shared
Imports risersoft.app.mxent
Imports System.Runtime.Serialization
Imports Newtonsoft.Json

<DataContract>
Public Class frmGstCompanyModel
    Inherits risersoft.app.mxform.frmCompanyModel

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
                    Dim obj = oProc.SearchGSTIN(GSTIN)
                    If obj.Data Is Nothing Then
                        oRet.JsonData = New With {.success = False, .message = If(obj.Error Is Nothing, "Could not get from GSTN", obj.ErrorMessage)}
                    Else
                        oRet.JsonData = New With {.success = True, .Data = obj.Data}
                    End If
                Catch ex As Exception
                    oRet.JsonData = New With {.success = False, .message = ex.Message}
                End Try
        End Select

        Return oRet
    End Function


End Class
<DataContract>
Public Class frmGstCampusModel
    Inherits risersoft.app.mxform.frmCampusModel

    Public Sub New(context As IProviderContext)
        MyBase.New(context)
    End Sub
    'This model is required to allow framework to get the security row.
End Class