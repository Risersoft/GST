Imports Newtonsoft.Json
Imports risersoft.shared
Imports GSTN.API.Library.Models.GstNirvana
Imports risersoft.app.mxent

Public Class Import_HSNTaskProvider
    Inherits ImportTaskProviderGstBase
    Public Overrides Property DocName As String = "HSN"

    Public Sub New(controller As clsAppController)
        MyBase.New(controller)
    End Sub

    Public Overrides Property DocType As String = "HSN"

    Public Overrides Property TemplateName As String = "HSNSAC"
    Public Overrides Property TemplateFunctionName As String = "GstListHsnSac()"

    Public Overrides Async Function TryImportRowGroup(provider As IAppDataProvider, objGroup As clsRowGroup, objPortion As clsPortionInfo) As Task(Of clsProcOutput)

        Dim oRet = Await Me.HandleGroupData(provider, objGroup, objPortion)
        Dim info As New ImportErrorInfo()
        If oRet.WarningCount > 0 Then info.Voucher.AddWarning(Me.DocumentExistsErrorCode, oRet.WarningMessage)
        'Have a new dataset ready for data to be saved from database and copy level 1 and level 2 rows into it
        Dim dsDB = oRet.Data
        Dim rVouch = dsDB.Tables(0).Select()(0)
        Dim dic = objGroup.dic
        If objGroup.Rows.Count > 1 Then
            oRet.AddError("Multiple Records found from Same HSNCode")
            info.Voucher.AddError(Me.PreValidationErrorCode, "Multiple Records found from Same HSNCode")
            Me.UpdateError(objGroup.Rows, info.Voucher)
        Else
            'Do the pre-operations, like getting IDs
            oRet = Me.ExecutePreValidation(provider, rVouch, Nothing, objGroup.Rows(0), objGroup)
            If oRet.Success Then

                Me.RunValidator(info, objGroup.Rows, rVouch, dsDB, "", Sub(obj, rItem)
                                                                           If rItem Is Nothing Then
                                                                               obj.SetValue("GSTUtils", New clsJINTFuncsGSTN(myContext))
                                                                               For Each kvp In dic
                                                                                   obj.AddOrUpdateRow(kvp.Value, kvp.Key)
                                                                               Next
                                                                           End If
                                                                       End Sub)
                If info.Errorcount = 0 Then
                    'If all OK, go ahead and save. If not OK, add validation errors obtained to a new error datatable with same schema as ds, but with Validation and Warning columns.
                    Dim VouchDescrip = "Code. " & rVouch("Code") & ""
                    Try
                        provider.dbConn.BeginTransaction(provider, Me.GetType.Name, EnumfrmMode.acAddM, "HsnSacID")

                        For Each str1 As String In New String() {"HsnSac"}
                            provider.objSQLHelper.SaveResults(dsDB.Tables(str1), objGroup.dicSQL(str1), dicOpInfo(str1), False)
                        Next

                        provider.dbConn.CommitTransaction(VouchDescrip, rVouch("HsnSacID"))
                    Catch ex As Exception
                        oRet.AddException(ex)
                        provider.dbConn.RollBackTransaction(VouchDescrip, ex.Message, False)
                        Dim obj = info.Voucher.AddException(Me.DatabaseTransactionErrorCode, ex)
                        Me.UpdateError(objGroup.Rows, info.Voucher)
                    End Try


                Else
                    If Not Me.ImportFileID = Guid.Empty Then
                        Dim nr = Me.CreateFileVouchRow(objPortion, rVouch, dsDB, objGroup, info, Sub(r1)
                                                                                                     r1("vouchnum") = objGroup.Rows(0)("Code")
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

        End If

        Return oRet

    End Function

    Public Overrides Function ExecutePreValidation(provider As IAppDataProvider, rInv As DataRow, dtItems As DataTable, rXL As DataRow, objGroup As clsRowGroup) As clsProcOutput
        Dim oRet As New clsProcOutput
        Dim dic = objGroup.dic
        'Conversion of lookup values
        Try
            Dim rrPOS() As DataRow = dsMaster.Tables("ty").Select("Descrip='" & myUtils.cStrTN(rXL("ServiceType")) & "'")
            If rrPOS.Length > 0 Then rInv("ty") = rrPOS(0)("codeword")
            rInv("IncTxRt") = If(myUtils.IsInList(myUtils.cStrTN(rXL("IncTxRt")), "yes", "y"), 1, 0)
            rInv("EwayBillReqd") = If(myUtils.IsInList(myUtils.cStrTN(rXL("EwayBillReqd")), "yes", "y"), 1, 0)
            rInv("ITCInElg") = If(myUtils.IsInList(myUtils.cStrTN(rXL("ITCInElg")), "yes", "y"), 1, 0)

        Catch ex As Exception
            oRet.AddException(ex)

        End Try
        Return oRet
    End Function

    Public Overrides Function FindVouch(provider As IAppDataProvider, CampusFilter As String, InvoiceNum As String, strInvoiceDate As String, CounterFilter As String) As DataRow

    End Function

    Public Overrides Async Function ExecuteServer(rTask As DataRow, input As clsProcOutput) As Task(Of clsProcOutput)
        Dim Params = JsonConvert.DeserializeObject(Of Dictionary(Of String, String))(myUtils.cStrTN(rTask("infojson")))
        Dim path = Await myFuncs.DownloadIfReqd(myContext, rTask, Params("path"))
        Dim oRet = Await Me.ExecuteImport(path, myUtils.cStrTN(rTask("username")), myUtils.cStrTN(rTask("importfileid")))
        Return oRet
    End Function

    Protected Overrides Function GenerateSQL(provider As IAppDataProvider, objGroup As clsRowGroup, objPortion As clsPortionInfo) As clsCollecString(Of String)
        Dim dicSQL As New clsCollecString(Of String)
        Dim rXL = objGroup.Rows(0), dic = objGroup.dic
        Dim CodeFilter = String.Format("Code='{0}'", Me.CleanFilterString(myUtils.cStrTN(rXL("Code"))))
        Dim TyFilter As String
        Dim rrPOS() As DataRow = dsMaster.Tables("ty").Select("Descrip='" & myUtils.cStrTN(rXL("ServiceType")) & "'")
        If rrPOS.Length > 0 Then
            TyFilter = String.Format("Ty='{0}'", Me.CleanFilterString(myUtils.cStrTN(rrPOS(0)("codeword"))))
        Else
            TyFilter = "0=1"
        End If

        Dim strf1 As String = myUtils.CombineWhere(False, CodeFilter, TyFilter)
        dicSQL.Add("hsnsac", "Select * from HSNSAC where " & strf1 & "")
        Return dicSQL

    End Function

    Protected Overrides Function HandleGroupData(provider As IAppDataProvider, objGroup As clsRowGroup, objPortion As clsPortionInfo) As Task(Of clsProcOutput)
        Dim oRet As New clsProcOutput
        Dim rXL = objGroup.Rows(0)

        objGroup.dicSQL.Clear()
        Dim dic2 = Me.GenerateSQL(provider, objGroup, objPortion)
        dic2.CopyTo(objGroup.dicSQL)

        Dim Code As String = myUtils.cStrTN(rXL("Code")).Replace("'", "")
        Dim ty As String = myUtils.cStrTN(rXL("ServiceType")).Replace("'", "")
        Dim dsDB = provider.objSQLHelper.ExecuteDataset(CommandType.Text, objGroup.dicSQL)
        Me.CheckAddOpInfo(provider, objGroup.dicSQL)

        'HSN Import
        myContext.Tables.SetAuto(dsDB.Tables("hsnsac"), Nothing, "hsnsacid")

        Dim rr1() As DataRow = dsDB.Tables("hsnsac").Select()

        Dim rHsnSac As DataRow
        If rr1.Length > 0 Then
            rHsnSac = rr1(0)
        Else
            rHsnSac = myContext.Tables.AddNewRow(dsDB.Tables("hsnsac"))
            rHsnSac("code") = rXL("code")
        End If
        rHsnSac("description") = rXL("description")
        rHsnSac("txrt") = rXL("txrt")
        rHsnSac("Cess_Rt") = rXL("Cess_Rt")
        rHsnSac("Remark") = rXL("Remark")
        rHsnSac("CSAMT") = rXL("CSAMT")
        rHsnSac("Uqc") = rXL("Uqc")
        rHsnSac("Discount") = rXL("Discount")
        rHsnSac("importfileid") = Me.ImportFileID
        rHsnSac("ITCInElgKeyword") = rXL("ITCInElgKeyword")

        oRet.Data = dsDB
        Return Task.FromResult(oRet)
    End Function

    Protected Overrides Function GenerateFilter() As String
        Return "isnull(code,'')<>''"
    End Function

    Public Overrides Sub AddRecord(info As GstImportInfo, objGroup As clsRowGroup)
        info.AddRecord(myUtils.cStrTN(objGroup.Rows(0)("code")), "", objGroup.Rows.Count, objGroup.Output.Success)
    End Sub

End Class
