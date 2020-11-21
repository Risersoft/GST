Imports risersoft.shared

Public MustInherit Class ImportTaskProviderAdvanceBase
    Inherits ImportTaskProviderGstBase
    Public Overrides Property DocName As String = "Advance"

    Public Sub New(controller As clsAppController)
        MyBase.New(controller)
    End Sub
    Public Overrides Function ExecutePreValidation(provider As IAppDataProvider, rVouch As DataRow, dtItems As DataTable, rXL As DataRow, objGroup As clsRowGroup) As clsProcOutput
        Dim oRet As New clsProcOutput, LineSNum As Integer = 0
        Dim PartyType As String = Me.fncPartyType(rXL)
        Dim PartySubTypeTable As String = Me.fncPartySubTypeTable(rXL)
        Dim dic = objGroup.dic
        Try
            oRet = Me.ExecutePreValidationMaster(rVouch, dtItems, rXL, dic)
            Dim oProc As New clsGSTInvoiceSale(provider)
            oProc.oMaster = Me.oMaster
            rVouch("uniquekey") = oProc.CalcUniqueKey(rXL("gstin"), myUtils.cValTN(rVouch("returnperiodid")), rVouch("vouchnum"), myUtils.cValTN(rVouch("isamendment")))
            Me.CalculateDate(rXL, rVouch, "Dated")
            For Each r1 As DataRow In dtItems.Select("", "linesnum")
                LineSNum = LineSNum + 1
                r1("linesnum") = LineSNum
            Next

            'isamendment
            If myUtils.IsInList(DocType, "PV") Then
                rVouch("rchrg") = If(myUtils.IsInList(myUtils.cStrTN(rXL("rchrg")), "yes", "y"), "Y", "N")
            End If

            rVouch("isamendment") = False
            Select Case myUtils.cStrTN(rXL("DocumentType")).Trim.ToLower
                Case "advance payment", "advance receipt"
                    rVouch("AdvanceVouchType") = "A"
                Case "payment cancellation", "advance receipt cancellation"
                    rVouch("AdvanceVouchType") = "C"
                Case "advance adjustment"
                    rVouch("AdvanceVouchType") = "T"
                Case "advance adjustment cancellation"
                    rVouch("AdvanceVouchType") = "J"
                Case "advance payment amendment", "advance amendment"
                    rVouch("AdvanceVouchType") = "A"
                    rVouch("isamendment") = True
                Case "refund voucher"
                    rVouch("AdvanceVouchType") = "R"

            End Select
            If myUtils.cValTN(rVouch("AmountTot")) = 0 Then
                For Each r1 As DataRow In dtItems.Select
                    rVouch("amounttot") = myUtils.cValTN(rVouch("amounttot")) + myUtils.cValTN(r1("ad_amt"))
                Next
            End If

            For Each str1 As String In New String() {"partygstregtype"}
                Dim rrPOS() As DataRow = dsMaster.Tables(str1).Select("Descrip='" & myUtils.cStrTN(rXL(str1)) & "'")
                If rrPOS.Length > 0 Then rVouch(str1) = rrPOS(0)("codeword")
            Next

            If dic.ContainsKey("gstreg") Then
                Dim rGstReg = dic("gstreg")

                Dim CampusFilter As String = myContext.SQL.GenerateSQLWhere(rGstReg, "gstregid")
                Dim PartyFilter As String = myContext.SQL.GenerateSQLWhere(rVouch, PartySubTypeTable & "ID")
                'OrigVouchID
                If myUtils.cBoolTN(rVouch("isamendment")) Then
                    Dim rOrig = Me.FindVouch(provider, CampusFilter, myUtils.cStrTN(rXL("origVouchNum")), myUtils.cStrTN(rXL("origDated")), PartyFilter)
                    If Not rOrig Is Nothing Then
                        rVouch("OrigVouchID") = myUtils.cValTN(rOrig("gstAdvanceVouchID"))
                        dic.Add("orig", rOrig)
                        Dim dicSQL As New clsCollecString(Of String), frmIDX As Integer = 0
                        dicSQL.Add("my", "Select GstAdvanceVouchID from GstAdvanceVouch where OrigVouchID = " & frmIDX)
                        dicSQL.Add("orig", "Select GstAdvanceVouchID from GstAdvanceVouch where OrigVouchID = " & myUtils.cValTN(rVouch("OrigVouchID")) & " and GstAdvanceVouchID<>" & frmIDX)
                        Me.AddSQL(provider, dic, dicSQL, "AmendVouch")
                    End If
                End If


                'sply_ty
                If myUtils.IsInList(DocType, "PV") Then
                    ' Supplyfromtaxareaid
                    For Each str1 As String In New String() {"supplyfrom"}
                        Dim rPOS = myFuncs2.FindTaxAreaRow(dsMaster.Tables("taxarea"), myUtils.cStrTN(rXL(str1)))
                        If rPOS IsNot Nothing Then
                            rVouch(str1 & "taxareaid") = rPOS("taxareaid")
                            dic.Add(str1, rPOS)
                        End If
                    Next
                    If Not rGstReg Is Nothing Then rVouch("sply_ty") = myFuncs2.FindSupplyTypePurchase(myUtils.cStrTN(rXL("ctin")), myUtils.cStrTN(rGstReg("GstRegType")), If(dic.ContainsKey("pos"), dic("pos"), Nothing), If(dic.ContainsKey("supplyfrom"), dic("supplyfrom"), Nothing))
                Else
                    rVouch("sply_ty") = myFuncs2.FindSupplyTypeSale(rGstReg, myUtils.cStrTN(rVouch("PartyGstRegType")), If(dic.ContainsKey("pos"), dic("pos"), Nothing))
                End If
            End If


        Catch ex As Exception
            oRet.AddException(ex)

        End Try
        Return oRet
    End Function
    Protected Overrides Function GenerateSQL(provider As IAppDataProvider, objGroup As clsRowGroup, objPortion As clsPortionInfo) As clsCollecString(Of String)
        Dim dicSQL As New clsCollecString(Of String)
        Dim rXL = objGroup.Rows(0), dic = objGroup.dic
        Dim PartyType As String = Me.fncPartyType(rXL)
        Dim PartySubTypeTable As String = Me.fncPartySubTypeTable(rXL)
        Dim CampusFilter As String = If(dic.ContainsKey("campus"), myContext.SQL.GenerateSQLWhere(dic("campus"), "campusid"), "0=1")
        Dim FYFilter As String = "0=1"
        If dic.ContainsKey("returnperiod") Then
            Dim PostPeriodIDCSV = myUtils.MakeCSV(oMaster.PostPeriodTable.Select(myContext.SQL.GenerateSQLWhere(dic("returnperiod"), "finyearid")), "postperiodid")
            FYFilter = "returnperiodid in (" & PostPeriodIDCSV & ")"
        End If

        Dim CounterFilter As String = If(dic.ContainsKey("party"), myContext.SQL.GenerateSQLWhere(dic("party"), PartySubTypeTable & "ID"), PartySubTypeTable & "ID" & "=0")
        'An amendment of a previous vouchnum can be present once per month.
        Dim PPFilter As String = If(myUtils.InStrList(myUtils.cStrTN(rXL("DocumentType")), "amendment", "cancellation") AndAlso dic.ContainsKey("returnperiod"), myContext.SQL.GenerateSQLWhereField(dic("returnperiod"), "postperiodid", "returnperiodid"), "")
        Dim VouchNumFilter = String.Format("VouchNum='{0}'", Me.CleanFilterString(myUtils.cStrTN(rXL("VouchNum"))))
        Dim strf1 As String = myUtils.CombineWhere(False, If(myUtils.IsInList(DocType, "PC"), CampusFilter, CounterFilter), VouchNumFilter, FYFilter, PPFilter)
        dicSQL.Add("vouch", "select * from gstadvancevouch where " & strf1)
        dicSQL.Add("item", "select * from gstAdvanceVouchItem where gstadvancevouchid in (select  gstadvancevouchid from  gstadvancevouch where " & strf1 & ")")

        Return dicSQL
    End Function
    Public Overrides Function FindVouch(provider As IAppDataProvider, CampusFilter As String, VouchNum As String, strDated As String, CounterFilter As String) As DataRow
        If (Not String.IsNullOrEmpty(VouchNum)) AndAlso (Not String.IsNullOrEmpty(strDated)) Then
            Try
                Dim Dated As DateTime
                If IsNumeric(strDated) Then
                    Dated = DateTime.FromOADate(myUtils.cValTN(strDated))
                Else
                    Dated = Me.DateFromString(myUtils.cStrTN(strDated))
                End If
                Dim strf1 As String = myUtils.CombineWhere(False, "campusid in (select campusid from campus where " & CampusFilter & ")",
                                                           "vouchnum='" & VouchNum & "'", "dated='" & Format(Dated, "dd-MMM-yyyy") & "'", CounterFilter)
                Dim oDoc As New clsGSTAdvanceBase(myContext, Me.DocType, "")
                Dim sql As String = oDoc.LoadVouchSQL(strf1)
                Dim dt1 As DataTable = provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql).Tables(0)
                If dt1.Rows.Count > 0 Then Return dt1.Rows(0)
            Catch ex As Exception
                myContext.Logger.logInformation("Cannot find Advance voucher due to exception: " & ex.Message)
            End Try
        End If
    End Function

    Protected Overrides Function GenerateFilter() As String
        Return "isnull(gstin,'')<>''"
    End Function

End Class
