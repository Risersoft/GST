Imports risersoft.shared
Imports risersoft.app.mxent
Imports System.Runtime.Serialization

<DataContract>
Public Class frmGstValidationRuleModel
    Inherits clsFormDataModel
    Dim PPFinal As Boolean = False
    Dim ObjVouch As New clsVoucherNum(myContext)

    Protected Overrides Sub InitViews()
    End Sub

    Public Sub New(context As IProviderContext)
        MyBase.New(context)
        Me.InitViews()
        Me.InitForm()
    End Sub

    Private Sub InitForm()
        Dim sql As String
        sql = myFuncsBase.CodeWordSQL("Validation", "DocType", "")
        Me.AddLookupField("DocType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "DocType").TableName)

        sql = myFuncsBase.CodeWordSQL("Validation", "RuleType", "")
        Me.AddLookupField("RuleType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "RuleType").TableName)

        sql = myFuncsBase.CodeWordSQL("Validation", "RuleLevel", "")
        Me.AddLookupField("RuleLevel", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "RuleLevel").TableName)

        sql = myFuncsBase.CodeWordSQL("Validation", "ResultType", "")
        Me.AddLookupField("ResultType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "ResultType").TableName)

    End Sub

    Public Overrides Function PrepForm(oView As clsViewModel, ByVal prepMode As EnumfrmMode, ByVal prepIDX As String, Optional ByVal strXML As String = "") As Boolean
        Dim sql As String
        Me.FormPrepared = False
        If prepMode = EnumfrmMode.acAddM Then prepIDX = 0
        sql = "Select * from ValidationRule Where ValidationRuleID = " & prepIDX
        Me.InitData(sql, "", oView, prepMode, prepIDX, strXML)

        Me.FormPrepared = True
        Return Me.FormPrepared
    End Function

    Public Overrides Function Validate() As Boolean
        Me.InitError()

        Return Me.CanSave
    End Function

    Public Overrides Function VSave() As Boolean
        VSave = False
        If Me.Validate Then
            If Me.CanSave Then
                Dim PaymentDescrip As String = " Code: " & myRow("Code").ToString & ", DocType: " & myRow("DocType")
                Try
                    myContext.Provider.dbConn.BeginTransaction(myContext, Me.Name, Me.frmMode.ToString, "ValidationRuleID", frmIDX)
                    myContext.Provider.objSQLHelper.SaveResults(myRow.Row.Table, "Select * from ValidationRule Where ValidationRuleID = " & frmIDX)
                    frmIDX = myRow("ValidationRuleID")

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
            If AcceptWarning Then
                Select Case EntityKey.Trim.ToLower
                    Case "rule"
                        Dim sql As String = "Select * from ValidationRule where ValidationRuleID =" & ID
                        Dim dt As DataTable = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql).Tables(0)
                        If dt.Rows.Count > 0 Then
                            Dim sql1 As String = "delete from ValidationRule where ValidationRuleID =" & ID
                            myContext.Provider.objSQLHelper.ExecuteNonQuery(CommandType.Text, sql1)
                        End If

                End Select
            ElseIf oRet.WarningCount = 0 Then
                oRet.AddWarning("Are you sure ?")
            End If


        Catch ex As Exception
            oRet.AddException(ex)
        End Try


        Return oRet
    End Function

End Class
