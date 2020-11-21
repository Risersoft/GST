Imports Newtonsoft.Json
Imports risersoft.app.mxengg
Imports risersoft.shared
Imports risersoft.shared.cloud.common

Public Class OTPTaskProvider
    Inherits clsTaskProviderBase
    'OTP Management Task Provider
    Public Overrides ReadOnly Property IsApiTask As Boolean = False
    Protected Friend fileProvider As clsFileProviderClientBase
    Public Sub New(controller As clsAppController)
        MyBase.New(controller)
    End Sub

    Public Overrides Function ExecuteServer(rTask As DataRow, input As clsProcOutput) As Task(Of clsProcOutput)
        Dim oRet As New clsProcOutput
        Try
            Dim sql As String = "select * from gstreg"
            Dim dt1 As DataTable = myContext.DataProvider.objSQLHelper.ExecuteDataset(CommandType.Text, sql).Tables(0)

            Dim oProc As New clsGSTNReturnGSTR1(myContext)
            For Each r1 As DataRow In dt1.Select
                Try
                    oRet.AddMessage($"Checking GSTIN {r1("GSTIN")}")
                    Dim rToken = oProc.GetActiveToken(myContext.Police.UniqueUserID.ToString, r1("gstregid"), 2)
                    If (rToken Is Nothing) Then
                        oRet.AddMessage($"Active token not found")
                    Else
                        Dim ExpiryMins As Decimal = DateDiff(DateInterval.Minute, TaskProviderFactory.FindLocalTime, rToken("ExpiryOn"))
                        oRet.AddMessage($"Active token found with expiry in {ExpiryMins} mins")
                        If ExpiryMins <= 60 Then
                            oRet.AddMessage($"Getting refresh token")
                            oProc.RefreshToken(rToken("gstregid"))
                        End If
                    End If
                Catch ex As Exception
                    oRet.AddException(ex)
                End Try
            Next


        Catch ex As Exception
            oRet.AddException(ex)
        End Try
        Return Task.FromResult(oRet)
    End Function

End Class
