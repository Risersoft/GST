Imports risersoft.shared
Imports risersoft.app.mxent
Imports System.Runtime.Serialization

<DataContract>
Public Class frmGstPaymentCustomerModel
    Inherits clsFormDataModel
    Dim PPFinal As Boolean = False
    Dim ObjVouch As New clsVoucherNum(myContext)
    Dim dicObjects As New clsCollecString(Of DataRow)

    Protected Overrides Sub InitViews()
        myView = Me.GetViewModel("ItemList")
    End Sub

    Public Sub New(context As IProviderContext)
        MyBase.New(context)
        Me.InitViews()
        Me.InitForm()
    End Sub

    Private Sub InitForm()
        Dim sql As String

        sql = "select TaxAreaID, Descrip,TaxAreaCode from TaxArea Order by Descrip"
        Me.AddLookupField("POSTaxAreaID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "POS").TableName)

        sql = "SELECT  PostPeriodID, convert(varchar,Month) + ' / ' + convert(varchar,Year) as Month , SDate, EDate, ret_pd, IsFinal FROM postperiod Order By SDate desc"
        Me.AddLookupField("ReturnPeriodID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "ReturnPeriod").TableName)

        sql = "Select  Divisionid, (DivisionCode + '-' + DivisionName) as division,DivisionCode,CompanyID from Division order by DivisionCode"
        Me.AddLookupField("DivisionID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Division").TableName)

        sql = "SELECT  CustomerID, PartyName, TaxAreaCode, GSTIN, TaxAreaID, GSTRegType, RegDate FROM  slsListCustomer() Order By PartyName"
        Me.AddLookupField("CustomerID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Customer").TableName)

        sql = myFuncsBase.CodeWordSQL("Party", "GSTRegType", "Tag2 = 'P'")
        Me.AddLookupField("PartyGSTRegType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "PartyGSTReg").TableName)

        Dim vlist2 As New clsValueList
        vlist2.Add(0, "0")
        vlist2.Add(5, "5")
        vlist2.Add(12, "12")
        vlist2.Add(18, "18")
        vlist2.Add(28, "28")
        Me.ValueLists.Add("gstrate1", vlist2)
        Me.AddLookupField("I_RT", "gstrate1")

        Dim vlist21 As New clsValueList
        vlist21.Add(0, "0")
        vlist21.Add(2.5, "2.5")
        vlist21.Add(6, "6")
        vlist21.Add(9, "9")
        vlist21.Add(14, "14")
        Me.ValueLists.Add("gstrate2", vlist21)
        Me.AddLookupField("C_RT", "gstrate2")
        Me.AddLookupField("S_RT", "gstrate2")

        Dim vlistapplicable As New clsValueList
        vlistapplicable.Add(65, "65")
        vlistapplicable.Add(100, "100")
        Me.ValueLists.Add("Diff_Percent", vlistapplicable)
        Me.AddLookupField("Diff_Percent", "Diff_Percent")

        Dim vlistvoucher As New clsValueList
        vlistvoucher.Add("EXPWP", "EXPWP")
        vlistvoucher.Add("EXPWOP", "EXPWOP")
        vlistvoucher.Add("B2CL", "B2CL")
        Me.ValueLists.Add("CDNUR_Typ", vlistvoucher)
        Me.AddLookupField("CDNUR_Typ", "CDNUR_Typ")

        sql = "Select * from validationrule where doctype='PC' and isnull(isdisabled,0)=0"
        myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Rules")

        sql = "Select * from SystemOptions"
        myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "options")

        sql = "Select * from gstrsection"
        myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "section")

    End Sub

    Public Overrides Function PrepForm(oView As clsViewModel, ByVal prepMode As EnumfrmMode, ByVal prepIDX As String, Optional ByVal strXML As String = "") As Boolean
        Dim sql, strf1 As String, dic As New clsCollecString(Of String), oDoc As New clsGSTAdvanceBase(myContext, "PC", "GSTR1")

        Me.FormPrepared = False
        If prepMode = EnumfrmMode.acAddM Then prepIDX = 0
        sql = "Select * from GstAdvanceVouch Where GstAdvanceVouchID = " & prepIDX
        Me.InitData(sql, "OrigVouchID", oView, prepMode, prepIDX, strXML)

        Dim str1 As String = myUtils.CombineWhere(True, myContext.AppModel.strFilterDBAppUser(myContext, Me.fRow, "CampusID"))
        sql = "Select CampusID, DispName, Taxareaid, CompanyID, CampusType,TaxAreaCode, DivisionCodeList, GstRegID, CampusCode, GSTIN, RegDate from mmListCampus() " & str1 & " Order by DispName"
        Me.AddLookupField("CampusID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Campus").TableName)

        If prepMode = EnumfrmMode.acAddM Then
            myRow("diff_percent") = 100
            myRow("isamendment") = False
        End If

        dic.Add("my", "Select GstAdvanceVouchID from GstAdvanceVouch where OrigVouchID = " & frmIDX)
        dic.Add("orig", "Select GstAdvanceVouchID from GstAdvanceVouch where OrigVouchID = " & myUtils.cValTN(myRow("OrigVouchID")) & " and GstAdvanceVouchID<>" & frmIDX)
        Me.AddDataSet("AmendVouch", dic)


        If myUtils.cValTN(myRow("OrigVouchID")) > 0 Then
            myRow("isamendment") = True
            sql = oDoc.LoadVouchSQL("GstAdvanceVouchID = " & myUtils.cValTN(myRow("OrigVouchID")))
            Me.AddDataSet("OrigVouch", sql)
            Dim dt As DataTable = Me.DatasetCollection("OrigVouch").Tables(0)
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                Dim OrgPaymentTypeCode As String = myUtils.cStrTN(dt.Rows(0)("AdvanceVouchType"))
                strf1 = "Codeword in ('" & OrgPaymentTypeCode & "')"
                If frmMode = EnumfrmMode.acAddM Then
                    myRow("advancevouchtype") = OrgPaymentTypeCode
                    'myRow("RefundVouchID") = dt.Rows(0)("RefundVouchID")
                    myRow("customerid") = dt.Rows(0)("customerid")
                    myRow("sply_ty") = dt.Rows(0)("sply_ty")
                    myRow("POSTaxAreaID") = dt.Rows(0)("POSTaxAreaID")
                    myRow("DivisionID") = dt.Rows(0)("DivisionID")
                    myRow("CampusID") = dt.Rows(0)("CampusID")
                    'myRow("VouchNum") = dt.Rows(0)("vouchnum")
                    'myRow("Dated") = dt.Rows(0)("Dated")
                End If
            End If
        End If

        sql = myFuncsBase.CodeWordSQL("AdvanceVouch", "VouchTypecode", strf1)
        Me.AddLookupField("AdvanceVouchType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "AdvanceVouchType").TableName)

        Me.BindDataTable(myUtils.cValTN(prepIDX))

        If frmMode = EnumfrmMode.acAddM Then
            myRow("dated") = Now.Date
            myRow("doctype") = "PC"
            myFuncs2.CheckGetJsonData(myContext, Me)
            If Me.dsCombo.Tables("campus").Rows.Count = 1 Then myRow("campusid") = Me.dsCombo.Tables("campus").Rows(0)("campusid")
        Else
            Dim oMaster As New clsMasterDataFICO(myContext)
            Dim rPostPeriod As DataRow = oMaster.rPostPeriod(myUtils.cValTN(myRow("ReturnPeriodID")))
            If Not IsNothing(rPostPeriod) Then
                PPFinal = myUtils.cBoolTN(rPostPeriod("IsFinal"))
            End If
        End If

        myView.MainGrid.BindGridData(Me.dsForm, 1)
        myView.MainGrid.QuickConf("", True, "0.5-1-0.5-0.5-0.5-0.5-1-1-1-1-2.5", True)
        Dim str2 As String = "<BAND INDEX = ""0"" TABLE = ""GstAdvanceVouchItem"" IDFIELD=""GstAdvanceVouchItemID""><COL KEY =""PaymentItemID, GstAdvanceVouchID, GSTR3B31Section, GSTR3B32Section, GSTR3B5Section, LineSNum, RT, RemarkItem""/><COL KEY=""I_RT"" FORMAT=""00.00"" CAPTION=""IGST Rate"" VLIST=""0|5|12|18|28""/><COL KEY=""C_RT"" FORMAT=""00.00"" CAPTION=""CGST Rate"" VLIST=""0|2.5|6|9|14""/><COL KEY=""S_RT"" FORMAT=""00.00"" CAPTION=""SGST Rate"" VLIST=""0|2.5|6|9|14""/><COL KEY=""Cess_Rt"" FORMAT=""00.00"" CAPTION=""CESS Rate""/><COL KEY=""AD_AMT"" CAPTION=""Amount""/><COL NOEDIT=""TRUE"" KEY=""CSAMT"" CAPTION=""CESS""/><COL NOEDIT=""TRUE"" KEY=""IAMT"" CAPTION=""Integrated Tax""/><COL NOEDIT=""TRUE"" KEY=""CAMT"" CAPTION=""Central Tax""/><COL NOEDIT=""TRUE"" KEY=""SAMT"" CAPTION=""State Tax""/><COL NOEDIT=""TRUE"" KEY=""LineSNum"" CAPTION=""LineItemID""/></BAND>"
        myView.MainGrid.PrepEdit(str2, EnumEditType.acCommandAndEdit)

        Me.FormPrepared = True
        Return Me.FormPrepared
    End Function

    Private Sub BindDataTable(ByVal GstAdvanceVouchID As Integer)
        Dim ds As DataSet, Sql As String

        Sql = "Select GstAdvanceVouchItemID, GstAdvanceVouchID, GSTR3B31Section, GSTR3B32Section, GSTR3B5Section, DiffAD_AMT,DiffIAMT,DiffCAMT,DiffSAMT,DiffCSAMT,RT,LineSNum,AD_AMT,I_RT,C_RT,S_RT,Cess_Rt,IAMT,CAMT,SAMT,CSAMT,RemarkItem from GstAdvanceVouchItem Where GstAdvanceVouchID = " & GstAdvanceVouchID & " "
        ds = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql)

        myUtils.AddTable(Me.dsForm, ds, "GstAdvanceVouchItem", 0)

    End Sub

    Public Function FindCampusID(CampusID As Integer) As DataRow
        Dim rrCampus() As DataRow = Me.dsCombo.Tables("Campus").Select("CampusID=" & CampusID)
        If rrCampus.Length > 0 Then Return rrCampus(0)
    End Function

    Public Overrides Function Validate() As Boolean
        Dim oMaster As New clsMasterDataFICO(myContext), LineSNum As Integer = 0
        Me.InitError()

        myFuncs2.SetDivision(myContext, Me.fRow, myRow.Row, myUtils.cValTN(myRow("campusid")))
        Dim oProc As New clsGSTNReturnGSTR1(myContext)

        myFuncs2.CheckPopulateTaxAreaID(myRow.Row, Me.SelectedRow("CustomerID"), Nothing)

        myRow("sply_ty") = myFuncs2.FindSupplyTypeSale(Me.SelectedRow("CampusID"), myUtils.cStrTN(myRow("PartyGstRegType")), Me.SelectedRow("POSTaxAreaID"))


        For Each r As DataRow In myView.MainGrid.myDV.Table.Select("", "linesnum")
            LineSNum = LineSNum + 1
            If myUtils.cValTN(r("I_RT")) > 0 Then
                r("RT") = myUtils.cValTN(r("I_RT"))
            Else
                r("RT") = myUtils.cValTN(r("C_RT")) + myUtils.cValTN(r("S_RT"))
            End If
            r("linesnum") = LineSNum
        Next

        'Validations

        If dicObjects Is Nothing Then dicObjects = New clsCollecString(Of DataRow) Else dicObjects.Clear()
        dicObjects.Add("gstreg", Me.SelectedRow("campusid"))
        dicObjects.Add("division", Me.SelectedRow("divisionid"))
        dicObjects.Add("party", Me.SelectedRow("customerid"))
        dicObjects.Add("returnperiod", Me.SelectedRow("returnperiodid"))
        If myRow.Row.RowState = DataRowState.Modified Then
            Dim rPP = oMaster.rPostPeriod(myUtils.cValTN(myRow.Row("returnperiodid", DataRowVersion.Original)))
            dicObjects.Add("returnperiodbefore", rPP)
        End If

        If Me.DatasetCollection.ContainsKey("AmendVouch") Then
            If Me.DatasetCollection("AmendVouch").Tables("my").Rows.Count > 0 Then dicObjects.Add("AmendVouchMy", Me.DatasetCollection("AmendVouch").Tables("my").Rows(0))
            If Me.DatasetCollection("AmendVouch").Tables("orig").Rows.Count > 0 Then dicObjects.Add("AmendVouchOrig", Me.DatasetCollection("AmendVouch").Tables("orig").Rows(0))
        End If
        If Me.DatasetCollection.ContainsKey("OrigVouch") Then
            Dim dt As DataTable = Me.DatasetCollection("OrigVouch").Tables(0)
            dicObjects.Add("orig", dt.Rows(0))
            Dim r1 As DataRow = Me.FindCampusID(myUtils.cValTN(dt.Rows(0)("campusid")))
            dicObjects.Add("orig.GstReg", r1)
        End If
        If Me.DatasetCollection.ContainsKey("RefundVouch") Then
            Dim dt As DataTable = Me.DatasetCollection("RefundVouch").Tables(0)
            dicObjects.Add("refund", dt.Rows(0))
            Dim r1 As DataRow = Me.FindCampusID(myUtils.cValTN(dt.Rows(0)("campusid")))
            dicObjects.Add("refund.GstReg", r1)
        End If
        If (Not Me.SelectedRow("campusid") Is Nothing) Then
            Dim rGstPP = oMaster.GstRegPPRow(Me.SelectedRow("campusid")("gstregid"), myUtils.cValTN(myRow("returnperiodid")))
            dicObjects.Add("GstRegPP", rGstPP)
        End If

        For Each str1 As String In New String() {"campus", "gstreg", "party", "returnperiod", "returnperiodbefore", "refund", "orig", "AmendVouchMy", "AmendVouchOrig", "GstRegPP"}
            If Not dicObjects.Exists(str1) Then dicObjects.Add(str1, Nothing)
        Next

        Dim oProc2 As New clsJintValidator(myContext)
        Dim dt1 = Me.dsCombo.Tables("rules")
        If dsCombo.Tables("options").Select.Length > 0 Then
            oProc2.AddOrUpdateRow(dsCombo.Tables("options").Rows(0), "")
        End If
        Dim lst = oProc2.RunValidator(myRow.Row, Me.dsForm, "GstAdvanceVouchItem", "item", dt1, Sub(obj, rItem)
                                                                                                    If rItem Is Nothing Then
                                                                                                        obj.SetValue("GSTUtils", New clsJINTFuncsGSTN(myContext))
                                                                                                        For Each kvp In dicObjects
                                                                                                            obj.AddOrUpdateRow(kvp.Value, kvp.Key)
                                                                                                        Next
                                                                                                    End If
                                                                                                End Sub)

        Me.AddError(lst)

        Return Me.CanSave
    End Function

    Public Overrides Function VSave() As Boolean
        VSave = False

        If Me.Validate Then
            If frmMode = EnumfrmMode.acAddM Then myRow("StatusNum") = 2

            If Me.CanSave Then
                Dim PaymentDescrip As String = " Payment Customer No: " & myRow("VouchNum").ToString & ", Dt. " & Format(myRow("Dated"), "dd-MMM-yyyy")
                Try
                    Dim oProc As New clsGSTAdvanceBase(myContext, "PC", "GSTR1")
                    myRow("uniquekey") = oProc.CalcUniqueKey(Me.SelectedRow("campusid")("campuscode"), myRow("ReturnPeriodID"), myRow("VouchNum"), myUtils.cValTN(myRow("isamendment")))
                    oProc.UpdateInvoiceNumberDynamicPart(Me.SelectedRow("CampusID")("gstregid"), myRow.Row)
                    myContext.Provider.dbConn.BeginTransaction(myContext, Me.Name, Me.frmMode.ToString, "GstAdvanceVouchID", frmIDX)

                    oProc.PopulateCalc(myUtils.cValTN(myRow("GstAdvanceVouchID")), myRow.Row, dicObjects("gstreg"), dsForm.Tables("GstAdvanceVouchItem"), Nothing, dicObjects("refund"), dicObjects("orig"), dsCombo)

                    myContext.Provider.objSQLHelper.SaveResults(myRow.Row.Table, "Select * from GstAdvanceVouch Where GstAdvanceVouchID = " & frmIDX)
                    frmIDX = myRow("GstAdvanceVouchID")


                    oProc.PopulateATA(myRow.Row, dsForm.Tables("GstAdvanceVouchItem"))
                    myUtils.ChangeAll(dsForm.Tables("GstAdvanceVouchItem").Select, "GstAdvanceVouchID=" & frmIDX)

                    myContext.Provider.objSQLHelper.SaveResults(dsForm.Tables("GstAdvanceVouchItem"), "Select GstAdvanceVouchItemID,GstAdvanceVouchID,GSTR3B31Section,GSTR3B32Section,GSTR3B5Section,DiffAD_AMT,DiffIAMT,DiffCAMT,DiffSAMT,DiffCSAMT,LineSNum,AD_AMT,RT,I_RT,C_RT,S_RT,Cess_Rt,IAMT,CAMT,SAMT,CSAMT,RemarkItem from GstAdvanceVouchItem")


                    If Me.vBag.ContainsKey("importfilevouchid") Then
                        Dim sql2 As String = "delete from importfilevouch where importfilevouchid=" & myUtils.cValTN(Me.vBag("importfilevouchid"))
                        myContext.Provider.objSQLHelper.ExecuteNonQuery(CommandType.Text, sql2)
                    End If

                    frmMode = EnumfrmMode.acEditM
                    myContext.Provider.dbConn.CommitTransaction(PaymentDescrip, frmIDX)
                    VSave = True

                Catch e As Exception
                    myContext.Provider.dbConn.RollBackTransaction(PaymentDescrip, e.Message)
                    Me.AddException("", e)
                End Try
            End If
        End If
    End Function

    Public Overrides Function DeleteEntity(EntityKey As String, ID As Integer, AcceptWarning As Boolean) As clsProcOutput
        Dim oRet As New clsProcOutput
        Try
            Select Case EntityKey.Trim.ToLower
                Case "payment"
                    Dim oProc As New clsGSTAdvanceBase(myContext, "PC", "GSTR1")
                    oRet = oProc.DeleteVouch(ID, AcceptWarning)

            End Select

        Catch ex As Exception
            oRet.AddException(ex)
        End Try

        Return oRet
    End Function
    Public Overrides Function GenerateIDOutput(dataKey As String, ID As Integer) As clsProcOutput
        Dim oRet As New clsProcOutput
        Select Case dataKey.Trim.ToLower
            Case "division"
                oRet = myFuncs2.FindDivisionList(myContext, Me.fRow, ID)
        End Select
        Return oRet
    End Function
End Class
