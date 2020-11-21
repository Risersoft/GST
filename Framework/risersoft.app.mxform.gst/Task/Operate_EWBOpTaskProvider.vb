Imports GSTN.API.Library.Models.EWB
Imports Newtonsoft.Json
Imports risersoft.app.dataporter
Imports risersoft.shared
Imports risersoft.app.dataporter.basExtension3
Imports Microsoft.Extensions.Logging

Public Class Operate_EWBOpTaskProvider
    Inherits ImportTaskProviderGstBase
    Public Overrides Property DocName As String = "Ewaybill"
    Public Overrides Property DocType As String = "EWB"

    Public Overrides Property TemplateName As String = "Ewaybill-UCE"
    Public Overrides Property TemplateFunctionName As String = "GstListEWBTemplate()"
    Public Sub New(controller As clsAppController)
        MyBase.New(controller)

    End Sub
    Public Overrides ReadOnly Property IsApiTask As Boolean = True


    Public Overrides Async Function ExecuteServer(rTask As DataRow, input As clsProcOutput) As Task(Of clsProcOutput)
        Dim Params = JsonConvert.DeserializeObject(Of Dictionary(Of String, String))(myUtils.cStrTN(rTask("infojson")))
        Dim filepath = Await myFuncs.DownloadIfReqd(myContext, rTask, Params("path"))
        Return Await Me.ExecuteOperate(filepath, myUtils.cStrTN(rTask("filename")), myUtils.cStrTN(rTask("username")))
    End Function
    Public Async Function ExecuteOperate(InputFileName As String, OutputFileName As String, username As String) As Task(Of clsProcOutput)

        Dim oRet As New clsProcOutput
        Try
            Dim conn As New clsSCMSExcel(myContext)
            Dim lst As New List(Of String) From {{InputFileName}}
            oRet = Me.ReadData(conn, Nothing, lst)
            oMaster.GetDataset2(False)
            Me.PopulateMaster()
            Dim oProc As New clsGSTNEwayBill(myContext)
            Dim lst2 As New List(Of DataTable)
            For Each dt1 As DataTable In Me.GetTablesToImport(oRet.Data)
                lst2.Add(dt1)
                For Each r1 As DataRow In dt1.Select("isnull(gstin,'') <> ''")
                    Dim oRet2 As New clsProcOutput
                    Dim rGstReg = Me.FindGSTIN(r1("gstin").ToString)
                    If rGstReg Is Nothing Then
                        oRet2.AddError("GSTIN Not found")
                    Else
                        Try
                            Dim rTA = myFuncs2.FindTaxAreaRow(dsMaster.Tables("taxarea"), myUtils.cStrTN(r1("fromstate")))

                            Select Case myUtils.cStrTN(r1("action")).Trim.ToLower
                                Case "update"
                                    Dim info As New EWBUpdVehRequestInfo
                                    SyncObjectFromRow2(info, r1)
                                    If Not rTA Is Nothing Then info.fromState = rTA("tincode")
                                    Dim result2 = oProc.Update(rGstReg("gstregid"), info)
                                    If result2.HttpStatusCode = 200 Then
                                        r1("vehupddate") = result2.Data.vehUpdDate
                                    Else
                                        oRet2.AddError(result2.Message)
                                    End If
                                Case "extend"
                                    Dim info As New EWBExtendRequestInfo
                                    SyncObjectFromRow2(info, r1)
                                    If Not rTA Is Nothing Then info.FromState = rTA("tincode")
                                    Dim result2 = oProc.Extend(rGstReg("gstregid"), info)
                                    If result2.HttpStatusCode = 200 Then
                                        r1("Updateddate") = result2.Data.UpdatedDate
                                        r1("validupto") = result2.Data.ValidUpto
                                    Else
                                        oRet2.AddError(result2.Message)
                                    End If

                                Case "cancel"
                                    Dim info As New EWBCancelRequestInfo
                                    SyncObjectFromRow2(info, r1)
                                    Dim result2 = oProc.Cancel(rGstReg("gstregid"), info)
                                    If result2.HttpStatusCode = 200 Then
                                        r1("canceldate") = result2.Data.cancelDate
                                    Else
                                        oRet2.AddError(result2.Message)
                                    End If
                            End Select
                        Catch ex As Exception
                            oRet2.AddException(ex)
                            myContext.Logger.LogInformation(ex.Message)
                        End Try
                    End If
                    r1("errortext") = oRet2.Message
                Next
            Next

            oRet = myFuncs.GenerateExcel(myContext, lst2, OutputFileName)
            Await myFuncs2.UploadMail(myContext, oRet, username)

        Catch ex As Exception
            oRet.AddException(ex)
        End Try



        Return oRet

    End Function


    Public Overrides Function FindVouch(provider As IAppDataProvider, CampusFilter As String, InvoiceNum As String, strInvoiceDate As String, CounterFilter As String) As DataRow
        Throw New NotImplementedException()
    End Function

    Public Overrides Function TryImportRowGroup(provider As IAppDataProvider, objGroup As clsRowGroup, objPortion As clsPortionInfo) As Task(Of clsProcOutput)
        Throw New NotImplementedException()
    End Function

    Public Overrides Function ExecutePreValidation(provider As IAppDataProvider, rInv As DataRow, dtItems As DataTable, rXL As DataRow, objGroup As clsRowGroup) As clsProcOutput
        Throw New NotImplementedException()
    End Function

    Protected Overrides Function GenerateSQL(provider As IAppDataProvider, objGroup As clsRowGroup, objPortion As clsPortionInfo) As clsCollecString(Of String)
        Throw New NotImplementedException()
    End Function
End Class
