Imports risersoft.app.mxent
Imports risersoft.shared

Public MustInherit Class clsGSTPartyBase
    Public DocType, ReturnField, IDField, FunctionName As String
    Protected Friend myContext As IProviderContext
    Public Sub New(context As IProviderContext)
        myContext = context
    End Sub
    Public Function DeleteVouchFilter(strf1 As String) As clsProcOutput
        Dim dic As New clsCollecString(Of String), oRet As New clsProcOutput
        dic.Add(DocType, $"delete from {DocType} where " & strf1)
        dic.Add("party", $"delete from party where partyid in (select partyid from {DocType} where " & strf1 & ")")
        dic.Add("partymain", $"delete from partymain where mainpartyid in (select mainpartyid from party where partyid in (select partyid from {DocType} where " & strf1 & "))")
        Dim sql As String = myUtils.MakeCSV(" ; " & vbCrLf, dic.Values.ToArray)
        Try
            myContext.Provider.dbConn.BeginTransaction(myContext)
            myContext.Provider.objSQLHelper.ExecuteNonQuery(CommandType.Text, sql)
            myContext.Provider.dbConn.CommitTransaction()
            oRet.AddMessage($"Your {DocType} has been deleted.")
        Catch ex As Exception
            myContext.Provider.dbConn.RollBackTransaction()
            oRet.AddException(ex)
        End Try
        Return oRet
    End Function

    Public Function DeletePeriod(PartyFilter As String) As clsProcOutput
        Dim oRet = Me.CheckFinalFilter(PartyFilter)
        If oRet.Success Then
            Dim strf1 As String = $"{IDField} In (Select {IDField} from {FunctionName} where " & myUtils.CombineWhere(False, PartyFilter, $"doctype='{DocType}'") & ")"
            oRet = Me.DeleteVouchFilter(strf1)
        End If
        Return oRet
    End Function
    Public Function DeleteVouchImport(ImportFileID As String) As clsProcOutput
        Dim PartyFilter = $"ImportFileid='{ImportFileID}'"
        Dim oRet = Me.CheckFinalFilter(PartyFilter)
        If oRet.Success Then
            Dim strf1 As String = $"{IDField} in (select {IDField} from {DocType} where importfileid='{ImportFileID}')"
            oRet = Me.DeleteVouchFilter(strf1)
        End If
        Return oRet
    End Function
    Public Function DeleteVouch(ID As Integer, AcceptWarning As Boolean) As clsProcOutput
        Dim oRet = Me.DeleteVouch(ID, AcceptWarning, $"{FunctionName}", $"{IDField}")
        Return oRet
    End Function
    Public Overridable Function DeleteVouch(ID As Integer, AcceptWarning As Boolean, FunctionName As String, IDField As String) As clsProcOutput
        Dim oRet As New clsProcOutput
        Dim dic As New clsCollecString(Of String)
        dic.Add(Me.DocType, String.Format("Select * from {0} Where {1} = {2}", FunctionName, IDField, ID))
        Dim dt1 As DataTable = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, dic).Tables(0)
        If dt1.Rows.Count > 0 Then
            Dim IDValue As Integer = dt1.Rows(0)($"{IDField}")
            oRet = Me.CheckUsed(IDValue)
        Else
            oRet.AddError("Cannot find Record")
        End If
        If oRet.ErrorCount = 0 Then
            If AcceptWarning Then
                oRet = Me.DeleteVouchFilter(IDField & "=" & ID)
            ElseIf oRet.WarningCount = 0 Then
                oRet.AddWarning("Are you sure ?")
            End If
        End If
        Return oRet
    End Function
    Public Overridable Function CheckUsed(IDValue As Integer) As clsProcOutput
        Dim oRet = Me.CheckFinalFilter($"{IDField}=" & IDValue)
        Return oRet
    End Function
    Public Overridable Function CheckFinalFilter(PartyFilter As String) As clsProcOutput
        Dim dic As New clsCollecString(Of String), oRet As New clsProcOutput
        dic.Add("invoice", $"select count(*) as cnt from Invoice where {IDField} in (select {IDField} from {DocType} where " & PartyFilter & ")")
        dic.Add("advance", $"Select count(*) As cnt from GstAdvanceVouch where {IDField} in (select {IDField} from {DocType} where " & PartyFilter & ")")
        dic.Add("cpinvoice", $"select count(*) as cnt from CPInvoice where {IDField} in (select {IDField} from {DocType} where " & PartyFilter & ")")
        dic.Add("ewaybill", $"select count(*) as cnt from Ewaybill where {IDField} in (select {IDField} from {DocType} where " & PartyFilter & ")")
        dic.Add("tcs", $"select count(*) as cnt from TCS where {IDField} in (select {IDField} from {DocType} where " & PartyFilter & ")")
        If $"{IDField}" = "CustomerID" Then
            dic.Add("isdinvoice", $"select count(*) as cnt from ISDInvoice where {IDField} in (select {IDField} from {DocType} where " & PartyFilter & ")")
        Else
            dic.Add("tds", $"select count(*) as cnt from TDS where {IDField} in (select {IDField} from {DocType} where " & PartyFilter & ")")
            dic.Add("challan", $"select count(*) as cnt from Challan where {IDField} in (select {IDField} from {DocType} where " & PartyFilter & ")")
        End If
        Dim ds = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, dic)

        For Each dt1 As DataTable In ds.Tables
            For Each rVouch As DataRow In dt1.Select
                If myUtils.cValTN(rVouch("cnt")) > 0 Then
                    oRet.AddError($"Cannot delete {DocType} as it is in Use")
                End If
            Next
        Next

        Return oRet
    End Function
End Class

Public Class clsGSTCustomerParty
    Inherits clsGSTPartyBase
    Public Sub New(context As IProviderContext)
        MyBase.New(context)
        Me.DocType = "Customer"
        IDField = "CustomerID"
        FunctionName = "slslistcustomer()"
    End Sub

End Class
Public Class clsGSTVendorParty
    Inherits clsGSTPartyBase
    Public Sub New(context As IProviderContext)
        MyBase.New(context)
        Me.DocType = "Vendor"
        IDField = "VendorID"
        FunctionName = "purlistvendor()"
    End Sub

End Class