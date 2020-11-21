Imports risersoft.app.mxent
Imports risersoft.shared

Public Class clsGSTEWB
    Inherits clsGSTDocBase
    Public Sub New(context As IProviderContext)
        MyBase.New(context, "EWB", "", "EwaybillID")
    End Sub

    Public Overrides Function DeleteVouchFilter(strf1 As String) As clsProcOutput
        Dim dic As New clsCollecString(Of String), oRet As New clsProcOutput

        Dim strf2 As String = "EwaybillID In (select EwaybillID From Ewaybill where " & strf1 & ")"
        dic.Add("item", "delete from Ewaybillitem where " & strf2)
        dic.Add("vehicle", "delete from EwaybillVehicle where " & strf2)
        dic.Add("vouch", "delete from Ewaybill where " & strf1)
        Try
            myContext.Provider.dbConn.BeginTransaction(myContext)
            For Each kvp In dic
                Trace.WriteLine("Deleting " & kvp.Key)
                Dim RowCount As Integer = 1
                While RowCount > 0
                    RowCount = myContext.Provider.objSQLHelper.ExecuteNonQuery(CommandType.Text, kvp.Value & "; select @@rowcount")
                    Trace.WriteLine($"RowCount={RowCount}")
                End While
            Next
            myContext.Provider.dbConn.CommitTransaction()
            oRet.AddMessage("Your Ewaybill data has been deleted.")
        Catch ex As Exception
            myContext.Provider.dbConn.RollBackTransaction()
            oRet.AddException(ex)
        End Try
        Return oRet
    End Function

    Public Overrides Function DeleteVouchImport(ImportFileID As String) As clsProcOutput
        Dim CampusFilter = "gstregid in (select gstregid from campus where campusid in (select campusid from Ewaybill where importfileid='" & ImportFileID & "'))"
        Dim PeriodFilter = "postperiodid in (select returnperiodid from Ewaybill where importfileid='" & ImportFileID & "')"
        Dim oRet = Me.CheckFinalFilter(CampusFilter, PeriodFilter)
        If oRet.Success Then
            Dim strf1 As String = String.Format("importfileid='{0}'", ImportFileID)
            oRet = Me.DeleteVouchFilter(strf1)
        End If
        Return oRet
    End Function

    Public Overrides Function DeleteVouch(ID As Integer, AcceptWarning As Boolean) As clsProcOutput
        Dim oRet = Me.DeleteVouch(ID, AcceptWarning, "GstListEWB()", "EwaybillID")
        Return oRet
    End Function
    Public Overrides Sub PopulateCalc(IDValue As Integer, rVouch As DataRow, rGstReg As DataRow, dtVouchItem As DataTable, dtCalc As DataTable, rCDN As DataRow, rOrig As DataRow, dsMaster As DataSet)
        Dim rCalc = Me.GetCalcRow(IDValue, rVouch, dtCalc, dtVouchItem, rOrig)

    End Sub

    Public Overrides Function GetCalcRow(IDValue As Integer, rVouch As DataRow, dtCalc As DataTable, dtItem As DataTable, rOrig As DataRow) As DataRow
        Dim rCalc = Me.GetCalcRow(IDValue, rVouch, dtCalc)

        'Initialize

        For Each str1 As String In New String() {"txval", "iamt", "camt", "samt", "csamt"}
            If dtItem.Columns.Contains(str1) Then
                rCalc(str1 & "summ") = If(dtItem Is Nothing, 0,
                   myUtils.cValTN(dtItem.Compute($"sum({str1})", $"isnull({IDField},0)={IDValue}")))
            End If
        Next


        Return rCalc
    End Function

    Public Overrides Function LoadVouchSQL(strFilter As String) As String
        'Dim sql = "Select EwaybillID, VendorID, CustomerID,TransactionType, Campusid, InvoiceNum, InvoiceDate, VendorName, STIN, POSTaxAreaID, ReturnPeriodID, txval, iamt, camt, samt, csamt from gstlistTCS() " &
        'myUtils.CombineWhere(True, strFilter)

        'Return sql
    End Function

    Public Overrides Function DeletePeriod(CampusFilterCheck As String, CampusFilterDoc As String, PeriodFilter As String, strFilter As String, DeleteSummary As Boolean) As clsProcOutput
        Dim oRet = Me.CheckFinalFilter(CampusFilterCheck, PeriodFilter)
        If oRet.Success Then
            Dim strf1 As String = myUtils.CombineWhere(False, CampusFilterDoc, PeriodFilter)
            oRet = Me.DeleteVouchFilter(strf1)
        End If
        Return oRet
    End Function

    Public Overrides Function CheckFinalFilter(GstRegFilter As String, PeriodFilter As String) As clsProcOutput
        'Ewaybill is not filed as return
        Dim oRet As New clsProcOutput
        Return oRet
    End Function

End Class
