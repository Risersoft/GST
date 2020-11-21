Imports Newtonsoft.Json
Imports risersoft.shared
Imports GSTN.API.Library.Models.GstNirvana
Imports risersoft.app.mxent
Imports risersoft.shared.dal

Public Class Import_RoleTaskProvider
    Inherits ImportTaskProviderGstBase
    Public Overrides Property DocName As String = "Role"

    Public Sub New(controller As clsAppController)
        MyBase.New(controller)
    End Sub

    Public Overrides Property DocType As String = "ROLE"


    Public Overrides Property TemplateName As String = "RoleManagement"
    Public Overrides Property TemplateFunctionName As String = "mmListCampus()"

    Public Overrides Async Function TryImportRowGroup(provider As IAppDataProvider, objGroup As clsRowGroup, objPortion As clsPortionInfo) As Task(Of clsProcOutput)

        Dim oRet = Await Me.HandleGroupData(provider, objGroup, objPortion)
        Dim info As New ImportErrorInfo()
        If oRet.WarningCount > 0 Then info.Voucher.AddWarning(Me.DocumentExistsErrorCode, oRet.WarningMessage)
        'Have a new dataset ready for data to be saved from database and copy level 1 and level 2 rows into it
        Dim dsDB = oRet.Data
        Dim dic = objGroup.dic
        If objGroup.Rows.Count > 1 Then
            oRet.AddError("Multiple Records found from Same RoleName")
            info.Voucher.AddError(Me.PreValidationErrorCode, "Multiple Records found from Same RoleName")
            Me.UpdateError(objGroup.Rows, info.Voucher)
        ElseIf dsDB.Tables(0).Select.Length = 0 Then
            oRet.AddError("Data not specified correctly")
        Else
            Dim rVouch = dsDB.Tables(0).Select()(0)
            'Do the pre-operations, like getting IDs
            oRet = Me.ExecutePreValidation(provider, rVouch, dsDB.Tables("dbroleuserfield"), objGroup.Rows(0), objGroup)
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
                    Dim VouchDescrip = "Name. " & rVouch("RoleName") & ""
                    Try
                        provider.dbConn.BeginTransaction(provider, Me.GetType.Name, EnumfrmMode.acAddM, "DBRoleID")

                        For Each str1 As String In New String() {"dbrole", "dbroleuserfield"}
                            provider.objSQLHelper.SaveResults(dsDB.Tables(str1), objGroup.dicSQL(str1), dicOpInfo(str1), False)
                        Next

                        provider.dbConn.CommitTransaction(VouchDescrip, rVouch("DBRoleID"))
                    Catch ex As Exception
                        oRet.AddException(ex)
                        provider.dbConn.RollBackTransaction(VouchDescrip, ex.Message, False)
                        Dim obj = info.Voucher.AddException(Me.DatabaseTransactionErrorCode, ex)
                        Me.UpdateError(objGroup.Rows, info.Voucher)
                    End Try


                Else
                    If Not Me.ImportFileID = Guid.Empty Then
                        Dim nr = Me.CreateFileVouchRow(objPortion, rVouch, dsDB, objGroup, info, Sub(r1)
                                                                                                     r1("vouchnum") = objGroup.Rows(0)("RoleName")
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
        'Conversion of lookup values
        Try

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

        Dim RoleNameFilter = String.Format("rolename='{0}'", Me.CleanFilterString(myUtils.cStrTN(rXL("rolename"))))
        Dim strf1 As String = myUtils.CombineWhere(False, RoleNameFilter)
        dicSQL.Add("dbrole", "select * from dbrole where " & strf1 & "")
        dicSQL.Add("dbroleuserfield", "select * from dbroleuserfield where DBRoleID in (select DBRoleID from dbrole where " & strf1 & ")")

        Return dicSQL

    End Function

    Protected Overrides Function HandleGroupData(provider As IAppDataProvider, objGroup As clsRowGroup, objPortion As clsPortionInfo) As Task(Of clsProcOutput)
        Dim oRet As New clsProcOutput
        Dim rXL = objGroup.Rows(0)
        objGroup.dicSQL.Clear()
        Dim dic = objGroup.dic
        Dim dic2 = Me.GenerateSQL(provider, objGroup, objPortion)
        dic2.CopyTo(objGroup.dicSQL)

        Dim RoleName As String = myUtils.cStrTN(rXL("RoleName")).Replace("'", "")
        Dim dsDB = provider.objSQLHelper.ExecuteDataset(CommandType.Text, objGroup.dicSQL)
        Me.CheckAddOpInfo(provider, objGroup.dicSQL)

        'Role Import
        myContext.Tables.SetAuto(dsDB.Tables("dbrole"), dsDB.Tables("dbroleuserfield"), "dbroleid")

        Dim rr1() As DataRow = dsDB.Tables("dbrole").Select("RoleName='" & RoleName & "'")

        Dim rDBRole, rDBRoleUser As DataRow
        'Get UserID from master
        'If found then search in dsDB, otherwise updateerror
        Dim oRet3 As New clsProcOutput
        Dim arr() As String = Split(myUtils.cStrTN(rXL("allowusers")), ",")
        For Each str2 In arr
            Dim rrUser() As DataRow = dsMaster.Tables("users").Select("UserName='" & str2 & "'")
            If rrUser.Length = 0 Then
                oRet3.AddError($"User not found: {str2}")
            End If
        Next
        If oRet3.Success Then
            If rr1.Length > 0 Then
                rDBRole = rr1(0)
            Else
                rDBRole = myContext.Tables.AddNewRow(dsDB.Tables("dbrole"))
                rDBRole("RoleName") = rXL("RoleName")
                rDBRole("PermissionList") = rXL("PermissionList")
                rDBRole("AllowUsers") = rXL("AllowUsers")
                rDBRole("allowusersfinal") = myUtils.MakeCSV(",", myUtils.cStrTN(rXL("allowusers")))
                rDBRole("importfileid") = Me.ImportFileID
            End If

            'RoleUser
            For Each r1 As DataRow In rXL.Table.DataSet.Tables("UserAssignment").Select("rolename='" & rXL("rolename") & "'")
                'Get UserID from master
                'If found then search in dsDB, otherwise updateerror
                Dim oRet2 As New clsProcOutput
                Dim arr2() As String = Split(myUtils.cStrTN(r1("allowusers")), ",")
                For Each str2 In arr2
                    Dim rrUser() As DataRow = dsMaster.Tables("users").Select("UserName='" & str2 & "'")
                    If rrUser.Length = 0 Then
                        oRet2.AddError($"User not found: {str2}")
                    End If
                Next

                If oRet2.Success Then
                    Dim rrGstReg() As DataRow = dsMaster.Tables("gstreg").Select("GSTIN='" & r1("GSTIN") & "'")
                    Dim FieldName As String, FieldValue As Integer
                    FieldName = "GstRegID"
                    If (Not rrGstReg Is Nothing) AndAlso rrGstReg.Length > 0 Then
                        FieldValue = rrGstReg(0)("GSTRegID")
                        Dim rr2() As DataRow = dsDB.Tables("dbroleuserfield").Select("dbroleid = " & myUtils.cValTN(rDBRole("dbroleid")) & " and FieldName= " & FieldName & " and FieldValue = " & FieldValue & "")
                        If rr2.Length > 0 Then
                            rDBRoleUser = rr2(0)
                        Else
                            rDBRoleUser = myContext.Tables.AddNewRow(dsDB.Tables("dbroleuserfield"))
                            rDBRoleUser("dbroleid") = rDBRole("dbroleid")
                            rDBRoleUser("FieldName") = "GSTRegID"
                            rDBRoleUser("FieldValue") = rrGstReg(0)("GSTRegID")
                            rDBRoleUser("AllowUsers") = r1("AllowUsers")
                            rDBRoleUser("GSTRegID") = rrGstReg(0)("GSTRegID")
                            rDBRoleUser("CompanyID") = rrGstReg(0)("CompanyID")
                        End If
                    Else
                        Me.UpdateErrorMsg(r1, Me.RowsNotMatchingCode, "GSTIN Not Found")
                    End If

                Else
                    Me.UpdateErrorMsg(r1, Me.RowsNotMatchingCode, oRet2.Message)
                End If

            Next
        Else
            Me.UpdateErrorMsg(rXL, Me.RowsNotMatchingCode, oRet3.Message)
        End If



        oRet.Data = dsDB
        Return Task.FromResult(oRet)
    End Function

    Protected Overrides Function GenerateFilter() As String
        Return "isnull(rolename,'')<>''"
    End Function

    Public Overrides Sub AddRecord(info As GstImportInfo, objGroup As clsRowGroup)
        info.AddRecord(myUtils.cStrTN(objGroup.Rows(0)("rolename")), "", objGroup.Rows.Count, objGroup.Output.Success)
    End Sub

    Protected Overrides Sub PopulateErrorFile(importer As ISSGImport, ds As DataSet, dtErrorFinal As DataTable)
        MyBase.PopulateErrorFile(importer, ds, dtErrorFinal)
        Dim dt2 As DataTable = ds.Tables("UserAssignment").Clone
        myUtils.CopyRows(ds.Tables("UserAssignment").Select("isnull(errorcode,'')<>''"), dt2)
        importer.CopyData("UserAssignment", dt2, 1, "dd/MM/yyyy", AddressOf DateFromString)

    End Sub
End Class
