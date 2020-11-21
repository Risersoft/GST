Imports risersoft.shared
Imports risersoft.app.mxent
Imports System.Runtime.Serialization
Imports System.Data.SqlClient
Imports Newtonsoft.Json

<DataContract>
Public Class frmSystemOptionsModel
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
        Dim vlistc As New clsValueList
        vlistc.Add(True, "Prod")
        vlistc.Add(False, "Test")
        Me.ValueLists.Add("Envc", vlistc)
        Me.AddLookupField("GSTNProduction", "Envc")



    End Sub

    Public Overrides Function PrepForm(oView As clsViewModel, ByVal prepMode As EnumfrmMode, ByVal prepIDX As String, Optional ByVal strXML As String = "") As Boolean
        Dim sql As String

        Me.FormPrepared = False
        prepMode = EnumfrmMode.acEditM
        sql = "Select * from systemoptions"
        Me.InitData(sql, "", oView, prepMode, prepIDX, strXML,,, True)


        Dim dic As New clsCollecString(Of String)
        dic.Add("permission", "select * from applicationsettings where settingtype='permission'")
        dic.Add("property", "select * from applicationsettings where settingtype='property'")   'can be used for properties instead of fields in system options
        Me.AddDataSet("setting", dic)

        Dim nr1 = Me.CheckAddUpdSetting(Me.DatasetCollection("setting").Tables("permission"), "kasp.menu", "permissionlist", "Permissions", "permission")
        nr1("settingoptions") = JsonConvert.SerializeObject(BarManagerUtils.GetToolBarNodes(myContext, "kasp"))

        Dim nr2 = Me.CheckAddUpdSetting(Me.DatasetCollection("setting").Tables("permission"), "kasp-mob.menu", "permissionlist", "Permissions", "permission")
        nr2("settingoptions") = JsonConvert.SerializeObject(BarManagerUtils.GetToolBarNodes(myContext, "kasp-mob"))

        For Each dt1 As DataTable In Me.DatasetCollection("setting").Tables
            myContext.Provider.objSQLHelper.SaveResults(dt1, "select * from applicationsettings where 0=1")
        Next

        Me.FormPrepared = True
        Return Me.FormPrepared
    End Function
    Public Overrides Function Validate() As Boolean
        Me.InitError()
        Return Me.CanSave
    End Function

    Public Overrides Function VSave() As Boolean
        VSave = False
        Dim SummaryUpload As Boolean = False
        If Me.Validate Then
            If Me.CanSave Then
                Dim InvoiceSaleDescrip As String = "System Options"
                Try
                    myRow("DisableAutoCalcVal") = myUtils.cBoolTN(myRow("DisableAutoCalcVal"))
                    myRow("DisableAutoCalcTaxVal") = myUtils.cBoolTN(myRow("DisableAutoCalcTaxVal"))
                    myRow("DisableAutoCalcTaxAmt") = myUtils.cBoolTN(myRow("DisableAutoCalcTaxAmt"))

                    myContext.Provider.dbConn.BeginTransaction(myContext, Me.Name, Me.frmMode.ToString, "systemoptionID", frmIDX)
                    myContext.Provider.objSQLHelper.SaveResults(myRow.Row.Table, "Select * from systemoptions")

                    'For Each dt1 As DataTable In Me.DatasetCollection("setting").Tables
                    '    myContext.Provider.objSQLHelper.SaveResults(dt1, "select * from applicationsettings where 0=1")
                    'Next

                    For Each dt1 As DataTable In Me.DatasetCollection("setting").Tables
                        dt1.AcceptChanges()
                        For Each r1 As DataRow In dt1.Select
                            If myUtils.cValTN(r1("applicationsettingid")) > 0 Then r1.SetModified() Else r1.SetAdded()
                        Next
                        myContext.Provider.objSQLHelper.SaveResults(dt1, "select * from applicationsettings where 0=1")
                    Next

                    frmIDX = myRow("systemoptionID")
                    frmMode = EnumfrmMode.acEditM
                    myContext.Provider.dbConn.CommitTransaction(InvoiceSaleDescrip, frmIDX)
                    VSave = True

                Catch ex As Exception
                    myContext.Provider.dbConn.RollBackTransaction(InvoiceSaleDescrip, ex.Message)
                    Me.AddError("", ex.Message)
                End Try
            End If
        End If
    End Function
    Public Function CheckAddUpdSetting(dt1 As DataTable, Key As String, Category As String, Description As String, Type As String) As DataRow
        Dim rr() As DataRow = dt1.Select("SettingKey='" & Key & "'"), nr As DataRow
        If rr.Length > 0 Then
            nr = rr(0)
        Else
            nr = myContext.Tables.AddNewRow(dt1)
            nr("settingkey") = Key
            nr("createdonutc") = DateTime.UtcNow
        End If
        nr("SettingCategory") = Category
        nr("SettingDesc") = Description
        nr("settingtype") = Type
        Return nr
    End Function

End Class
