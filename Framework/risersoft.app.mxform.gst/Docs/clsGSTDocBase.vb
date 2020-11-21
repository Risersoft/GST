Imports risersoft.app.mxent
Imports risersoft.shared
Imports System.Collections.Concurrent
Public Class clsGSTDocBase
    Public DocType, ReturnField, IDField As String
    Protected Friend myContext As IProviderContext
    Public oMaster As clsMasterDataFICO
    Protected Friend objLock As Object
    Protected Friend dicOpInfo As ConcurrentDictionary(Of String, clsSaveOpInfo)
    Protected Friend UpdateBatchSize As Integer = 1000
    Public Sub New(context As IProviderContext, DocType As String, ReturnField As String, IDField As String)
        myContext = context
        oMaster = New clsMasterDataFICO(myContext)
        Me.DocType = DocType
        Me.ReturnField = ReturnField
        Me.IDField = IDField
        objLock = New Object
        dicOpInfo = New ConcurrentDictionary(Of String, clsSaveOpInfo)
    End Sub
    Public Function CalcUniqueKey(SellerCode As String, ReturnPeriodID As Integer, InvoiceNum As String, AmendNum As Integer) As String
        Dim rPP As DataRow = oMaster.rPostPeriod(ReturnPeriodID)
        Dim rFY As DataRow = If(rPP Is Nothing, Nothing, myContext.CommonData.rFinYear(rPP("finyearid")))
        Return myFuncs.CalcUniqueKey(Me.DocType, SellerCode, rFY, InvoiceNum, AmendNum)
    End Function
    Protected Friend Overridable Function GetFactor(rVouch As DataRow) As Decimal
        Return 1
    End Function
    Protected Friend Overridable Function GetFactor2(rVouch As DataRow) As Decimal
        Return 1
    End Function
    Public Overridable Function GetCalcRow(IDValue As Integer, rVouch As DataRow, dtCalc As DataTable) As DataRow
        Dim rCalc As DataRow

        If dtCalc Is Nothing Then
            'mxgstdb
            rCalc = rVouch
        Else
            Dim rrCalc() As DataRow = dtCalc.Select(IDField & "=" & IDValue)
            If rrCalc.Length > 0 Then
                rCalc = rrCalc(0)
            Else
                rCalc = myContext.Tables.AddNewRow(dtCalc)
                rCalc(IDField) = IDValue
            End If
        End If
        Return rCalc
    End Function

    Public Overridable Function GetCalcRow(IDValue As Integer, rVouch As DataRow, dtCalc As DataTable, dtItem As DataTable, rOrig As DataRow) As DataRow
        Dim rCalc = Me.GetCalcRow(IDValue, rVouch, dtCalc)
        Dim factor = Me.GetFactor(rVouch)
        rCalc("factor") = factor
        If rCalc.Table.Columns.Contains("factor2") Then
            Dim factor2 = Me.GetFactor2(rVouch)
            rCalc("factor2") = factor2
        End If
        'Initialize
        For Each str1 As String In New String() {"txval", "iamt", "camt", "samt", "csamt", "iamti", "iamtc", "iamts", "samti", "samts", "camti", "camtc"}
            Dim str2 = If(myUtils.IsInList(Me.DocType, "PC", "PV"), If(myUtils.IsInList(str1, "txval"), "ad_amt", str1), str1)
            If dtItem.Columns.Contains(str2) Then
                rCalc(str1 & "summ") = If(dtItem Is Nothing, 0,
                   myUtils.cValTN(dtItem.Compute($"sum({str2})", $"isnull({IDField},0)={IDValue}")))
                rCalc("diff" & str1) = If(rOrig Is Nothing, 0,
                    myUtils.cValTN(rCalc(str1 & "summ")) - myUtils.cValTN(rOrig(str1)))
            End If
        Next
        If rCalc.Table.Columns.Contains("returnperiodid") AndAlso rCalc.Table.Columns.Contains("finyearid") Then
            Dim rPP = oMaster.rPostPeriod(myUtils.cValTN(rCalc("returnperiodid")))
            If rPP IsNot Nothing Then rCalc("finyearid") = rPP("finyearid")
        End If

        Return rCalc
    End Function
    Public Overridable Function GetValidator(IDValue As Integer, rVouch As DataRow, rGstReg As DataRow, rCDN As DataRow, rOrig As DataRow, dsMaster As DataSet) As clsJintValidator
        Dim oProc As New clsJintValidator(myContext)
        If dsMaster.Tables("options").Select.Length > 0 Then
            oProc.AddOrUpdateRow(dsMaster.Tables("options").Rows(0), "")
        End If
        oProc.AddOrUpdateRow(rVouch, "")
        oProc.AddOrUpdateRow(rCDN, "cdn")
        oProc.AddOrUpdateRow(rOrig, "orig")
        oProc.AddOrUpdateRow(rGstReg, "gstreg")
        Return oProc
    End Function
    Public Overridable Sub PopulateCalc(IDValue As Integer, rGstReg As DataRow, rVouch As DataRow, dtVouchItem As DataTable, dtCalc As DataTable, rCDN As DataRow, rOrig As DataRow, dsMaster As DataSet)
    End Sub

    Protected Friend Function CalculateSection(oProc As clsJintValidator, ReturnType As String, dsMaster As DataSet) As String
        Dim GSTRSection As String = ""

        For Each rSec As DataRow In dsMaster.Tables("section").Select("returntype='" & ReturnType & "'")
            If myUtils.IsInList(Me.DocType, myUtils.cStrTN(rSec("doctype")), myUtils.cStrTN(rSec("doctype2"))) Then
                Try
                    Dim cont1 As Boolean = If(myUtils.cStrTN(rSec("cdnorigfilter")).Trim.Length > 0, oProc.Validate(rSec("cdnorigfilter").ToString), True)
                    Dim cont2 As Boolean = If(myUtils.cStrTN(rSec("typefilter")).Trim.Length > 0, oProc.Validate(rSec("typefilter").ToString), True)
                    Dim cont3 As Boolean = If(myUtils.cStrTN(rSec("outerfilter")).Trim.Length > 0, oProc.Validate(rSec("outerfilter").ToString), True)
                    If cont1 AndAlso cont2 AndAlso cont3 Then
                        GSTRSection = rSec("section")
                        Exit For
                    End If
                Catch ex As Exception
                    Trace.WriteLine("CalculateSection:" & ex.Message)
                End Try

            End If
        Next
        Return GSTRSection

    End Function

    Public Overridable Function CheckFinal(GstRegID As Integer, ReturnPeriodID As Integer) As clsProcOutput
        Dim oRet = Me.CheckFinalFilter("gstregid=" & GstRegID, "postperiodid=" & ReturnPeriodID)
        Return oRet
    End Function

    Public Overridable Function CheckFinalFilter(GstRegFilter As String, PeriodFilter As String) As clsProcOutput
        Dim dic As New clsCollecString(Of String), oRet As New clsProcOutput
        dic.Add("gstreg", "select * from gstreg where " & GstRegFilter)
        dic.Add("pp", "select * from (select *,postperiodid as returnperiodid from postperiod) as t1 where " & PeriodFilter)
        Dim ds = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, dic)
        For Each rGst As DataRow In ds.Tables("gstreg").Select
            For Each rPP As DataRow In ds.Tables("pp").Select
                Dim result As Boolean = myFuncs2.IsFinalPP(myContext, rGst("GstRegID"), rPP, ReturnField)
                If result Then
                    oRet.AddError("Posting Period Finailzed")
                    Exit For
                End If

            Next
            If Not oRet.Success Then Exit For
        Next
        Return oRet
    End Function
    Public Overridable Function DeleteVouch(ID As Integer, AcceptWarning As Boolean) As clsProcOutput

    End Function
    Public Overridable Function DeletePeriod(CampusFilterCheck As String, CampusFilterDoc As String, PeriodFilter As String, strFilter As String, DeleteSummary As Boolean) As clsProcOutput
        Return New clsProcOutput
    End Function
    Public Overridable Function DeleteVouchFilter(strf1 As String) As clsProcOutput

    End Function
    Public Overridable Function DeleteCPVouchFilter(strf1 As String) As clsProcOutput

    End Function
    Public Overridable Function DeletePeriodCP(CampusFilterCheck As String, CampusFilterDoc As String, PeriodFilter As String) As clsProcOutput
        Return New clsProcOutput
    End Function
    Public Overridable Function DeleteVouchImport(ImportFileID As String) As clsProcOutput
        Return New clsProcOutput
    End Function
    Public Overridable Function DeleteCPVouchImport(ImportFileID As String) As clsProcOutput
        Return New clsProcOutput
    End Function
    Public Overridable Function DeleteVouch(ID As Integer, AcceptWarning As Boolean, FunctionName As String, IDField As String) As clsProcOutput
        Dim oRet As New clsProcOutput
        Dim GstRegID As Integer = 0, dic As New clsCollecString(Of String)
        dic.Add(Me.DocType, String.Format("Select * from {0} Where {1} = {2}", FunctionName, IDField, ID))
        Dim dt1 As DataTable = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, dic).Tables(0)
        If dt1.Rows.Count > 0 Then
            GstRegID = dt1.Rows(0)("gstregid")
            oRet = Me.CheckFinal(GstRegID, dt1.Rows(0)("returnperiodid"))
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
    Public Overridable Function AddDeleteAction(r1 As DataRow, GstRegID As Integer, CTIN As String) As DataRow

    End Function
    Public Overridable Sub PopulateGstType(r1 As DataRow, dtItems As DataTable, TaxAreaCode As String)

    End Sub
    Public Overridable Function MatchUniqueFields(r1 As DataRow) As Boolean

    End Function
    Public Overridable Sub CalcVouchActionRP(GstRegID As Integer, ReturnPeriodID As Integer, rVouch As DataRow)
        rVouch("ActionFlag") = "U"      'upload
        If myUtils.IsInList(myUtils.cStrTN(rVouch("gstinvoicetype")), "b2b", "cdn", "b2ba", "cdna") Then
            rVouch("matchcode") = "RP"        'Reconciliation Pending
        Else
            rVouch("matchcode") = "NR"
        End If
    End Sub
    Public Overridable Sub UpdateInvoiceNumberDynamicPart(GstRegID As Integer, rInv As DataRow)

    End Sub
    Public Overridable Sub UpdateInvoiceNumberDynamicPart(GstRegID As Integer, rInv As DataRow, dtSettings As DataTable)

    End Sub

    Public Overridable Function LoadVouchSQL(strFilter As String) As String

    End Function
    Public Overridable Function LoadVouchCPSQL(strFilter As String) As String
    End Function

    Public Overridable Function GetFirstRow(dic As clsCollecString(Of DataSet), Key As String) As DataRow
        If dic.Exists(Key) AndAlso dic(Key).Tables.Count > 0 AndAlso dic(Key).Tables(0).Rows.Count > 0 Then
            Return dic(Key).Tables(0).Rows(0)
        End If

    End Function
    Protected Friend Sub CheckAddOpInfo(provider As IAppDataProvider, dic As clsCollecString(Of String))
        For Each kvp In dic
            If (Not dicOpInfo.ContainsKey(kvp.Key)) AndAlso (Not myUtils.EndsWith(kvp.Key, "_0")) Then
                SyncLock (objLock)
                    Dim info = provider.objSQLHelper.GenerateSaveInfo(kvp.Value, UpdateBatchSize)
                    dicOpInfo.AddOrUpdate(kvp.Key, Function(key) info, Function(key, obj) info)
                End SyncLock
            End If
        Next
    End Sub
End Class
