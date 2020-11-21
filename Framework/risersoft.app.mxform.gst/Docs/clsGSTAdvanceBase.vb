Imports risersoft.app.mxent
Imports risersoft.shared

Public Class clsGSTAdvanceBase
    Inherits clsGSTDocBase
    Public Sub New(context As IProviderContext, DocType As String, ReturnField As String)
        MyBase.New(context, DocType, ReturnField, "gstAdvanceVouchID")
    End Sub
    Public Sub PopulateATA(rPay As DataRow, dtAdvanceTax As DataTable)
        If myUtils.cValTN(rPay("OrigVouchID")) > 0 Then
            If myUtils.IsInList(rPay("advancevouchtype"), "T") Then
                Dim dic As New clsCollecString(Of String)
                dic.Add("txpd", "select * from GstAdvanceVouchItem where GstAdvanceVouchID=" & rPay("OrigVouchID"))
                Dim ds = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, dic)

                For Each r1 As DataRow In dtAdvanceTax.Select
                    Dim rr() As DataRow = ds.Tables("txpd").Select("rt=" & myUtils.cValTN(r1("rt")))
                    For Each str1 As String In New String() {"ad_amt", "iamt", "camt", "samt", "csamt"}
                        r1("diff" & str1) = myUtils.cValTN(r1(str1)) - If(rr.Length > 0, myUtils.cValTN(rr(0)(str1)), 0)
                    Next
                Next
            Else
                Dim dic As New clsCollecString(Of String)
                dic.Add("at", "select * from GstAdvanceVouchItem where GstAdvanceVouchID=" & rPay("OrigVouchID"))
                Dim ds = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, dic)

                For Each r1 As DataRow In dtAdvanceTax.Select
                    Dim rr() As DataRow = ds.Tables("at").Select("rt=" & myUtils.cValTN(r1("rt")))
                    For Each str1 As String In New String() {"ad_amt", "iamt", "camt", "samt", "csamt"}
                        r1("diff" & str1) = myUtils.cValTN(r1(str1)) - If(rr.Length > 0, myUtils.cValTN(rr(0)(str1)), 0)
                    Next
                Next
            End If
        End If

    End Sub
    Public Overrides Function DeleteVouchFilter(strf1 As String) As clsProcOutput
        Dim dic As New clsCollecString(Of String), oRet As New clsProcOutput
        Dim strf2 As String = "gstadvancevouchid in (select gstadvancevouchid from gstlistadvancevouch() where " & strf1 & ")"
        dic.Add("item", "delete from gstadvancevouchitem where " & strf2)
        dic.Add("vouch", "delete from gstadvancevouch where " & strf1)
        Dim sql As String = myUtils.MakeCSV(" ; " & vbCrLf, dic.Values.ToArray)
        Try
            myContext.Provider.dbConn.BeginTransaction(myContext)
            myContext.Provider.objSQLHelper.ExecuteNonQuery(CommandType.Text, sql)
            myContext.Provider.dbConn.CommitTransaction()
            oRet.AddMessage("Your advance data has been deleted.")
        Catch ex As Exception
            myContext.Provider.dbConn.RollBackTransaction()
            oRet.AddException(ex)
        End Try
        Return oRet
    End Function

    Public Overrides Function DeletePeriod(CampusFilterCheck As String, CampusFilterDoc As String, PeriodFilter As String, strFilter As String, DeleteSummary As Boolean) As clsProcOutput
        Dim oRet = Me.CheckFinalFilter(CampusFilterCheck, PeriodFilter)
        If oRet.Success Then
            Dim strf1 As String = myUtils.CombineWhere(False, CampusFilterDoc, PeriodFilter, $"doctype='{DocType}'")
            oRet = Me.DeleteVouchFilter(strf1)
        End If
        Return oRet
    End Function
    Public Overrides Function DeleteVouchImport(ImportFileID As String) As clsProcOutput
        Dim CampusFilter = "gstregid in (select gstregid from campus where campusid in (select campusid from gstadvancevouch where importfileid='" & ImportFileID & "'))"
        Dim PeriodFilter = "postperiodid in (select returnperiodid from gstadvancevouch where importfileid='" & ImportFileID & "')"
        Dim oRet = Me.CheckFinalFilter(CampusFilter, PeriodFilter)
        If oRet.Success Then
            Dim strf1 As String = String.Format("importfileid='{0}'", ImportFileID)
            oRet = Me.DeleteVouchFilter(strf1)
        End If
        Return oRet
    End Function
    Public Overrides Function DeleteVouch(ID As Integer, AcceptWarning As Boolean) As clsProcOutput
        Dim oRet = Me.DeleteVouch(ID, AcceptWarning, "gstlistadvancevouch()", "GstAdvanceVouchID")
        Return oRet
    End Function
    Public Overrides Sub UpdateInvoiceNumberDynamicPart(GstRegID As Integer, rVouch As DataRow)
        Dim oRet As New clsProcOutput(Of Long)
        If myUtils.cValTN(rVouch("refundvouchid")) > 0 Then
            oRet = myFuncs2.GetInvoiceNumberDynamicPart(myContext.Provider, GstRegID, "ADVRE", myUtils.cStrTN(rVouch("vouchnum")))
        ElseIf myUtils.IsInList(doctype, "PV") Then
            oRet = myFuncs2.GetInvoiceNumberDynamicPart(myContext.Provider, GstRegID, "ADVPV", myUtils.cStrTN(rVouch("vouchnum")))
        Else
            oRet = myFuncs2.GetInvoiceNumberDynamicPart(myContext.Provider, GstRegID, "ADVPC", myUtils.cStrTN(rVouch("vouchnum")))

        End If
        If oRet.Success Then
            rVouch("invoicenumbertemplateid") = oRet.ID
            rVouch("vouchnumdynamicpart") = oRet.Result
        End If
    End Sub
    Protected Friend Overrides Function GetFactor(rVouch As DataRow) As Decimal
        Dim Factor = If(myUtils.IsInList(myUtils.cStrTN(rVouch("AdvanceVouchType")), "", "R", "C", "T"), -1, 1)
        Return Factor
    End Function
    Public Overrides Sub PopulateCalc(IDValue As Integer, rVouch As DataRow, rGstReg As DataRow, dtVouchItem As DataTable, dtCalc As DataTable, rCDN As DataRow, rOrig As DataRow, dsMaster As DataSet)
        Dim rCalc = Me.GetCalcRow(IDValue, rVouch, dtCalc, dtVouchItem, rOrig)
        Dim oProc = Me.GetValidator(IDValue, rVouch, rGstReg, rCDN, rOrig, dsMaster)
        For Each str1 As String In New String() {"gstr1", "gstr2"}
            rCalc(str1 & "section") = Me.CalculateSection(oProc, str1, dsMaster)
        Next

        For Each rVouchItem As DataRow In dtVouchItem.Select($"isnull({IDField},0)={IDValue}")
            For Each str1 As String In New String() {"gstr3b31", "gstr3b32", "gstr3b5"}
                oProc.AddOrUpdateRow(rVouchItem, "item")
                rVouchItem(str1 & "section") = Me.CalculateSection(oProc, str1, dsMaster)
            Next
        Next
    End Sub
    Public Overrides Function LoadVouchSQL(strFilter As String) As String
        Dim sql = "Select GstAdvanceVouchID,VendorID,CustomerID,DivisionID,CampusID,VouchNum,Dated,PartyName,CTIN,AdvanceVouchType,POSTaxAreaID,OrigVouchID, RefundVouchID, RootVouchID, sply_ty,ReturnPeriodID,ad_amt as txval, iamt,samt,camt, csamt from GstListAdvanceVouch() " &
            myUtils.CombineWhere(True, strFilter)

        Return sql
    End Function

End Class
