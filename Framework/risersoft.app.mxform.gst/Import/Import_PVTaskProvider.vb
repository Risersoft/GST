Imports Newtonsoft.Json
Imports risersoft.shared
Imports System.IO
Imports risersoft.shared.dal
Imports risersoft.app.dataporter
Imports System.Reflection
Imports GSTN.API.Library.Models.GstNirvana

Public Class Import_PVTaskProvider
    Inherits ImportTaskProviderAdvanceBase

    Public Overrides Property RowsNotMatchingCode As Integer = 948
    Public Overrides Property PreValidationErrorCode As Integer = 951
    Public Overrides Property DocType As String = "PV"
    Public Overrides Property TemplateFunctionName As String = "GstListAdvanceVouchTemplate()"

    Public Overrides Property TemplateName As String = "Advance_Paid_Refund"


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
        Dim oRet = Await Me.ExecuteImport(path, myUtils.cStrTN(rTask("username")), myUtils.cStrTN(rTask("importfileid")))
        Return oRet
    End Function


    Public Overrides Async Function TryImportRowGroup(provider As IAppDataProvider, objGroup As clsRowGroup, objPortion As clsPortionInfo) As Task(Of clsProcOutput)

        Dim oRet = Await Me.HandleGroupData(provider, objGroup, objPortion)
        Dim info As New ImportErrorInfo()
        If oRet.WarningCount > 0 Then info.Voucher.AddWarning(Me.DocumentExistsErrorCode, oRet.WarningMessage)

        'Have a new dataset ready for data to be saved from database and copy level 1 and level 2 rows into it
        Dim dsDB = oRet.Data
        Dim rVouch = dsDB.Tables(0).Select()(0)
        Dim dic = objGroup.dic

        'Do the pre-operations, like getting IDs
        oRet = Me.ExecutePreValidation(provider, rVouch, dsDB.Tables("item"), objGroup.Rows(0), objGroup)
        If oRet.Success Then
            For Each str1 As String In New String() {"campus", "gstreg", "party", "returnperiod", "returnperiodbefore", "refund", "pos", "supplyfrom", "orig", "AmendVouchMy", "AmendVouchOrig", "GstRegPP"}
                If Not dic.Exists(str1) Then dic.Add(str1, Nothing)
            Next

            Me.RunValidator(info, objGroup.Rows, rVouch, dsDB, "item", Sub(obj, rItem)
                                                                           If rItem Is Nothing Then
                                                                               obj.SetValue("GSTUtils", New clsJINTFuncsGSTN(myContext))
                                                                               For Each kvp In dic
                                                                                   obj.AddOrUpdateRow(kvp.Value, kvp.Key)
                                                                               Next
                                                                           End If
                                                                       End Sub)
            If info.Errorcount = 0 Then
                'If all OK, go ahead and save. If not OK, add validation errors obtained to a new error datatable with same schema as ds, but with Validation and Warning columns.
                Dim VouchDescrip = "No. " & rVouch("vouchnum") & " Dt. " & Format(rVouch("dated"), "dd-MMM-yyyy")
                Try
                    Dim oProc As New clsGSTAdvanceBase(provider, "PV", "GSTR2")
                    oProc.UpdateInvoiceNumberDynamicPart(dic("gstreg")("gstregid"), rVouch)

                    provider.dbConn.BeginTransaction(provider, Me.GetType.Name, EnumfrmMode.acAddM, "GstAdvanceVouchID")

                    oProc.PopulateCalc(myUtils.cValTN(rVouch("GstAdvanceVouchID")), rVouch, dic("gstreg"), dsDB.Tables("item"), Nothing, dic("refund"), dic("orig"), Me.dsMaster)

                    For Each str1 As String In New String() {"item", "vouch"}
                        provider.objSQLHelper.SaveResults(dsDB.Tables(str1), objGroup.dicSQL(str1), dicOpInfo(str1), False, "0=1")
                    Next

                    provider.objSQLHelper.SaveResults(dsDB.Tables("vouch"), objGroup.dicSQL("vouch"), dicOpInfo("vouch"))

                    oProc.PopulateATA(rVouch, dsDB.Tables("item"))
                    myUtils.ChangeAll(dsDB.Tables("item").Select, "GstAdvanceVouchID=" & rVouch("GstAdvanceVouchID"))

                    provider.objSQLHelper.SaveResults(dsDB.Tables("item"), objGroup.dicSQL("item"), dicOpInfo("item"))


                    provider.dbConn.CommitTransaction(VouchDescrip, rVouch("GstAdvanceVouchID"))
                Catch ex As Exception
                    oRet.AddException(ex)
                    provider.dbConn.RollBackTransaction(VouchDescrip, ex.Message, False)
                    Dim obj = info.Voucher.AddException(Me.DatabaseTransactionErrorCode, ex)
                    Me.UpdateError(objGroup.Rows, info.Voucher)
                End Try
            Else
                If Not Me.ImportFileID = Guid.Empty Then
                    Dim nr = Me.CreateFileVouchRow(objPortion, rVouch, dsDB, objGroup, info, Sub(r1)
                                                                                                 r1("ctin") = objGroup.Rows(0)("ctin")
                                                                                                 r1("totalamount") = rVouch("amountTot")
                                                                                             End Sub)
                End If
                oRet.AddError(info.ErrorDescripTot)

            End If

        Else
            oRet.AddError("Unforeseen error in pre validation")
            myContext.Logger.logInformation(oRet.Message & oRet.StackTrace)
            info.Voucher.AddError(Me.PreValidationErrorCode, "Unforeseen error in pre validation")
            Me.UpdateError(objGroup.Rows, info.Voucher)
        End If
        Return oRet

    End Function


    Protected Overrides Sub UpdateItem(rSource As DataRow, rDest As DataRow)
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
