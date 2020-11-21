Imports Microsoft.Extensions.Logging
Imports Newtonsoft.Json
Imports risersoft.app.mxengg
Imports risersoft.shared
Imports risersoft.shared.cloud.common

Public Class PublicAPITaskProvider
    Inherits clsTaskProviderBase
    'Public API Task Provider
    Public Overrides ReadOnly Property IsApiTask As Boolean = False
    Protected Friend fileProvider As clsFileProviderClientBase
    Public Sub New(controller As clsAppController)
        MyBase.New(controller)
    End Sub

    Public Overrides Function ExecuteServer(rTask As DataRow, input As clsProcOutput) As Task(Of clsProcOutput)
        Dim oRet = Me.ExecuteSearch
        Return Task.FromResult(oRet)
    End Function
    Public Function ExecuteSearch() As clsProcOutput
        Dim oRet As New clsProcOutput
        Try

            Dim sql2 = "select * from codewords where codeclass='Party' and codetype='GSTRegType' and tag3 is not null"
            Dim dt2 As DataTable = myContext.DataProvider.objSQLHelper.ExecuteDataset(CommandType.Text, sql2).Tables(0)

            Dim strf1 As String = "(lastupdatedgstn is null) or (lastupdated>lastupdatedgstn)"
            Dim strf2 As String = "partyid not in (select partyid from campus)"
            Dim sql As String = "select * from party where " & myUtils.CombineWhere(False, strf1, strf2, "isnull(GSTIN,'')<>''")
            Dim dt1 As DataTable = myContext.DataProvider.objSQLHelper.ExecuteDataset(CommandType.Text, sql).Tables(0)

            Dim oProc As New clsGSTNPublic(myContext)
            For Each r1 As DataRow In dt1.Select()
                Try
                    Dim result = oProc.SearchGSTIN(r1("gstin"))
                    If result.Data IsNot Nothing Then
                        r1("lastupdatedgstn") = TaskProviderFactory.FindLocalTime
                        r1("searchresult") = JsonConvert.SerializeObject(result.Data)
                        If myUtils.IsInList(myUtils.cStrTN(r1("partyname")), "", myUtils.cStrTN(r1("gstin"))) Then r1("partyname") = result.Data.lgnm
                        Dim rr() = dt2.Select("tag3='" & result.Data.dty & "'")
                        If rr.Length > 0 Then r1("gstregtype") = rr(0)("codeword")
                    End If
                Catch ex As Exception
                    myContext.Logger.LogInformation(ex.Message)
                End Try
            Next
            myContext.DataProvider.objSQLHelper.SaveResults(dt1, sql)

        Catch ex As Exception
            oRet.AddException(ex)
        End Try
        Return oRet
    End Function
End Class
