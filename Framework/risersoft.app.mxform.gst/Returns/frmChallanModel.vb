Imports risersoft.shared
Imports risersoft.app.mxent
Imports System.Runtime.Serialization
<DataContract>
Public Class frmChallanModel
    Inherits clsFormDataModel
    Dim ObjVouch As New clsVoucherNum(myContext)

    Protected Overrides Sub InitViews()
    End Sub

    Public Sub New(context As IProviderContext)
        MyBase.New(context)
        Me.InitViews()
        Me.InitForm()
    End Sub

    Private Sub InitForm()
        Dim sql As String = myFuncsBase.CodeWordSQL("Invoice", "TaxType", "")
        Me.AddLookupField("TaxInvoiceType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "TaxInvoiceType").TableName)
    End Sub

    Public Overrides Function PrepForm(oView As clsViewModel, ByVal prepMode As EnumfrmMode, ByVal prepIDX As String, Optional ByVal strXML As String = "") As Boolean
        Dim dic As New clsCollecString(Of String)

        Me.FormPrepared = False
        If prepMode = EnumfrmMode.acEditM Then
            Dim oRet As clsProcOutput = Me.GetRowLock(prepMode, "ODNoteID", prepIDX)
            If oRet.Success Then
                Dim Sql As String = "Select * from OdNote where ODNoteID = " & myUtils.cValTN(prepIDX)
                Me.InitData(Sql, "", oView, prepMode, prepIDX, strXML)
                Dim dt As DataTable = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, "Select InsureTransit from Dispatch Where DispatchID=" & myUtils.cValTN(myRow("DispatchID"))).Tables(0)
                If dt.Rows.Count > 0 AndAlso myUtils.cBoolTN(dt.Rows(0)(0)) Then
                    Me.ModelParams.Add(New clsSQLParam("@InsReq", "'Required'", GetType(String), False))
                Else
                    Me.ModelParams.Add(New clsSQLParam("@InsReq", "'Not Required'", GetType(String), False))
                End If

                If myUtils.cValTN(myRow("DispatchID")) > 0 Then
                    Sql = "Select Sum(AmountTot) as AmountTot from ODNoteSpare where ItemType = 'AC' and ODNoteID = " & frmIDX
                    dt = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql).Tables(0)
                    If dt.Rows.Count > 0 Then
                        Me.ModelParams.Add(New clsSQLParam("@TakenAmtCurr", myUtils.cValTN(dt.Rows(0)("AmountTot")), GetType(Decimal), False))
                    End If

                    Sql = "Select Sum(AccessAmount) as AccessAmount from ODNote where DispatchID = " & myUtils.cValTN(myRow("DispatchID")) & " and ODNoteID <> " & frmIDX
                    dt = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql).Tables(0)
                    If dt.Rows.Count > 0 Then
                        Me.ModelParams.Add(New clsSQLParam("@DeducAmtOther", myUtils.cValTN(dt.Rows(0)("AccessAmount")), GetType(Decimal), False))
                    End If

                    Sql = "Select Sum(AmountTot) as AmountTot from ODNoteSpare where ItemType = 'AC' and ODnoteID in (Select ODnoteID from ODNote where DispatchID = " & myUtils.cValTN(myRow("DispatchID")) & ") and ODNoteID <> " & frmIDX
                    dt = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql).Tables(0)
                    If dt.Rows.Count > 0 Then
                        Me.ModelParams.Add(New clsSQLParam("@TakenAmtOther", myUtils.cValTN(dt.Rows(0)("AmountTot")), GetType(Decimal), False))
                    End If
                End If

                If myUtils.cStrTN(myRow("ChallanNum")).Trim.Length = 0 AndAlso myUtils.cValTN(myRow("DocSysNumCh")) = 0 Then
                    myRow("ChallanDate") = Now.Date
                    myRow("RemovalDate") = Now.Date
                    myRow("RG23ADate") = Now.Date
                End If

                If myUtils.cStrTN(myRow("VehicleNum")).Trim.Length = 0 Then
                    Me.AddError("", Nothing, 0, "", "", "First enter Vehicle No. Then Proceed.")
                Else
                    Me.FormPrepared = True
                End If
            Else
                Me.AddError("", Nothing, 0, "", "", oRet.Message)
            End If
        End If
        Return Me.FormPrepared
    End Function

    Public Overrides Function Validate() As Boolean
        Me.InitError()
        If myRow("ChallanDate") Is Nothing Then Me.AddError("ChallanDate", "Select Challan Date")
        If (Not myUtils.NullNot(Me.myRow("ChallanDate"))) AndAlso myContext.CommonData.FinRow(myRow("ChallanDate")) Is Nothing Then Me.AddError("ChallanDate", "Enter Correct Date")
        Return Me.CanSave
    End Function

    Public Overrides Function VSave() As Boolean
        VSave = False
        If Me.Validate Then
            Dim ChallanDescrip As String = " Challan No: " & myRow("ChallanNum").ToString & ", Dt. " & Format(myRow("ChallanDate"), "dd-MMM-yyyy")
            If myUtils.IsInList(myUtils.cStrTN(myRow("ChallanType")), "TRJW", "TRWS", "TRSO") Then
                myRow("Returnable") = 1
            End If

            Try
                myContext.Provider.dbConn.BeginTransaction(myContext, Me.Name, Me.frmMode.ToString, "OdNoteID", frmIDX)
                myContext.Provider.objSQLHelper.SaveResults(myRow.Row.Table, Me.sqlForm)
                frmMode = EnumfrmMode.acEditM
                frmIDX = myRow("OdNoteID")
                myContext.Provider.dbConn.CommitTransaction(ChallanDescrip, frmIDX)
                VSave = True
            Catch e As Exception
                myContext.Provider.dbConn.RollBackTransaction(ChallanDescrip, e.Message)
                Me.AddException("", e)
            End Try
        End If
    End Function

    Public Overrides Function GenerateDataOutput(dataKey As String, ds As DataSet, frmIDX As Integer) As clsProcOutput
        Dim oRet As New clsProcOutput
        Select Case dataKey
            Case "genaratechallanno"
                oRet.Description = ObjVouch.GetNewSerialNo("OdNoteID", myUtils.cStrTN(ds.Tables(0).Rows(0)("TaxType")), ds.Tables(0).Rows(0), "ChallanNum")
        End Select
        Return oRet
    End Function
End Class
