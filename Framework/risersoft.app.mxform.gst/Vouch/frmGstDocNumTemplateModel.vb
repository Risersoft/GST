Imports risersoft.shared
Imports risersoft.shared.cloud
Imports risersoft.shared.cloud.common
Imports System.Configuration
Imports System.Runtime.Serialization
Imports risersoft.app.mxent

<DataContract>
Public Class frmGstDocNumTemplateModel
    Inherits clsFormDataModel

    Protected Overrides Sub InitViews()
        myView = Me.GetViewModel("Levels")
    End Sub

    Public Sub New(context As IProviderContext)
        MyBase.New(context)
        Me.InitViews()
        Me.InitForm()
    End Sub

    Private Sub InitForm()
        Dim Sql As String

        Sql = myFuncsBase.CodeWordSQL("GSTN", "DocNature", "")
        Me.AddLookupField("DocNature", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql), "DocNature").TableName)

        Dim vlistc As New clsValueList
        vlistc.Add("Tenant", "Tenant")
        vlistc.Add("Company", "Company")
        vlistc.Add("GSTReg", "GSTReg")
        Me.ValueLists.Add("Level", vlistc)
        Me.AddLookupField("Level", "Level")

    End Sub

    Public Overrides Function PrepForm(oView As clsViewModel, ByVal prepMode As EnumfrmMode, ByVal prepIDX As String, Optional ByVal strXML As String = "") As Boolean
        Dim Sql, str1 As String

        Me.FormPrepared = False
        If prepMode = EnumfrmMode.acAddM Then prepIDX = Guid.NewGuid.ToString
        Sql = "Select * from Tenant"
        Me.InitData(Sql, "", oView, prepMode, prepIDX, strXML)


        'Get Company List from Company Table
        Dim str2 As String = myUtils.CombineWhere(True, myContext.AppModel.strFilterDBAppUser(myContext, fRow, "CompanyID", "CompanyID"))
        Dim sqlCompany = String.Format("Select CompanyID, CompName from Company " & str2 & " order By compname")
        Me.AddLookupField("CompanyID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sqlCompany), "Company").TableName)

        'Get GSTReg List from Company Table
        Dim str3 As String = myUtils.CombineWhere(True, myContext.AppModel.strFilterDBAppUser(myContext, fRow, "GstRegID", "CompanyID"))
        Dim sqlGstReg = String.Format("Select GSTRegId, GSTIN from GstReg " & str3 & " order by gstin")
        Me.AddLookupField("GSTRegId", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sqlGstReg), "GstReg").TableName)


        Sql = "Select InvoiceNumberTemplateId, CompanyId, GSTRegId, DocumentNature, Prefix, Suffix from GstDocNumTemplate"
        Me.myView.MainGrid.BindGridData(myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql), 0)
        Me.myView.MainGrid.QuickConf("", True, "2-2-2", True)
        str1 = "<BAND INDEX=""0"" TABLE=""GstDocNumTemplate"" IDFIELD=""InvoiceNumberTemplateId""><COL KEY=""CompanyId, GSTRegId, Prefix, Suffix""/><COL KEY=""DocumentNature"" CAPTION=""Document Type"" LOOKUPSQL=""" & myFuncsBase.CodeWordSQL("GSTN", "DocNature", "") & """/></BAND>"
        myView.MainGrid.PrepEdit(str1, EnumEditType.acCommandAndEdit)
        Me.myView.MainGrid.myDV.RowFilter = "0=1"

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
                Dim DocNumTemplateDescrip As String = ""
                Try
                    myContext.Provider.dbConn.BeginTransaction(myContext, Me.Name, Me.frmMode.ToString, "TenantID", frmIDX)
                    frmMode = EnumfrmMode.acEditM
                    myView.MainGrid.SaveChanges()
                    myContext.Provider.dbConn.CommitTransaction(DocNumTemplateDescrip, frmIDX)
                    VSave = True

                Catch e As Exception
                    myContext.Provider.dbConn.RollBackTransaction(DocNumTemplateDescrip, e.Message)
                    Me.AddException("", e)
                End Try
            End If
        End If
    End Function


End Class
