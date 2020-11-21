Imports Newtonsoft.Json
Imports risersoft.shared
Imports System.Runtime.Serialization

<DataContract>
Public Class frmDashboardSettingModel
    Inherits clsFormDataModel

    Protected Overrides Sub InitViews()
    End Sub

    Public Sub New(context As IProviderContext)
        MyBase.New(context)
        Me.InitViews()
        Me.InitForm()
    End Sub

    Private Sub InitForm()

        Dim vlist1 As New clsValueList
        vlist1.Add("TenantID", "Tenant")
        vlist1.Add("CompanyID", "Company")
        vlist1.Add("GSTRegID", "GSTReg")
        Me.ValueLists.Add("FieldName", vlist1)
        Me.AddLookupField("FieldName", "FieldName")

    End Sub

    Public Overrides Function PrepForm(oView As clsViewModel, ByVal prepMode As EnumfrmMode, ByVal prepIDX As String, Optional ByVal strXML As String = "") As Boolean
        Me.FormPrepared = False
        If prepMode = EnumfrmMode.acAddM Then prepIDX = 0
        Dim Sql As String = "select * from DashboardSetting where DashboardSettingID =" & prepIDX
        Me.InitData(Sql, "", oView, prepMode, prepIDX, strXML)

        'Get Company List from Company Table
        Dim str2 As String = myUtils.CombineWhere(True, myContext.AppModel.strFilterDBAppUser(myContext, fRow, "CompanyID", "CompanyID"))
        Dim sqlCompany = String.Format("Select CompanyID, CompName from Company " & str2 & " order By compname")
        Me.AddLookupField("CompanyID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sqlCompany), "Company").TableName)

        'Get GSTReg List from Company Table
        Dim str3 As String = myUtils.CombineWhere(True, myContext.AppModel.strFilterDBAppUser(myContext, fRow, "GstRegID", "CompanyID"))
        Dim sqlGstReg = String.Format("Select GSTRegId, GSTIN from GstReg " & str3 & " order by gstin")
        Me.AddLookupField("GSTRegId", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sqlGstReg), "GstReg").TableName)


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
            Dim DashDescrip As String = myUtils.cStrTN(myRow("FieldName"))
            Try
                myContext.Provider.dbConn.BeginTransaction(myContext, Me.Name, Me.frmMode.ToString, "DashboardSettingID", frmIDX)
                myContext.Provider.objSQLHelper.SaveResults(myRow.Row.Table, Me.sqlForm)
                frmIDX = myRow("DashboardSettingID")

                frmMode = EnumfrmMode.acEditM
                myContext.Provider.dbConn.CommitTransaction(DashDescrip, frmIDX)
                VSave = True
            Catch e As Exception
                myContext.Provider.dbConn.RollBackTransaction(DashDescrip, e.Message)
                Me.AddException("", e)
            End Try
        End If
    End Function


End Class

