Imports Newtonsoft.Json
Imports risersoft.shared
Imports GSTN.API.Library.Models.GstNirvana

Public Class Import_CustomerTaskProvider
    Inherits ImportTaskProviderPartyBase

    Public Sub New(controller As clsAppController)
        MyBase.New(controller)
        Me.fncPartyType = Function(rXL)
                              Return "C"
                          End Function
        Me.fncPartySubTypeTable = Function(rXL)
                                      Return "Customer"
                                  End Function
    End Sub

    Public Overrides Property DocType As String = "Customer"

    Public Overrides Property TemplateName As String = "Customer"
    Public Overrides Property TemplateFunctionName As String = "slsListCustomer()"

    Public Overrides Function ExecutePreValidation(provider As IAppDataProvider, rInv As DataRow, dtItems As DataTable, rXL As DataRow, objGroup As clsRowGroup) As clsProcOutput
        Dim oRet As New clsProcOutput
        Dim dic = objGroup.dic
        'Conversion of lookup values
        Try
            Dim rParty = dtItems.DataSet.Tables("party").Rows(0)
            For Each str2 As String In New String() {"partygstregtype"}
                Dim rrPOS() As DataRow = dsMaster.Tables(str2).Select("Descrip='" & rXL("GstRegTypeDescrip") & "'")
                If rrPOS.Length > 0 Then
                    rParty("GSTRegType") = rrPOS(0)("codeword")
                ElseIf String.IsNullOrEmpty(rXL("GSTIN")) Then
                    rParty("GSTRegType") = "N"
                Else
                    rParty("GSTRegType") = "R"
                End If
            Next
            rParty("ITCInElg") = If(myUtils.IsInList(myUtils.cStrTN(rXL("ITCInElg")), "yes", "y"), 1, 0)

        Catch ex As Exception
            oRet.AddException(ex)

        End Try
        Return oRet
    End Function

    Public Overrides Async Function ExecuteServer(rTask As DataRow, input As clsProcOutput) As Task(Of clsProcOutput)
        Dim Params = JsonConvert.DeserializeObject(Of Dictionary(Of String, String))(myUtils.cStrTN(rTask("infojson")))
        Dim path = Await myFuncs.DownloadIfReqd(myContext, rTask, Params("path"))
        Dim oRet = Await Me.ExecuteImport(path, myUtils.cStrTN(rTask("username")), myUtils.cStrTN(rTask("importfileid")))
        Return oRet
    End Function

    Public Overrides Function FindVouch(provider As IAppDataProvider, CampusFilter As String, InvoiceNum As String, strInvoiceDate As String, CounterFilter As String) As DataRow
        Throw New NotImplementedException()
    End Function

End Class
