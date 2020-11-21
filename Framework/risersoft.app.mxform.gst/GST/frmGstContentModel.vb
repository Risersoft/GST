Imports risersoft.shared
Imports risersoft.app.mxent
Imports System.Runtime.Serialization
Imports risersoft.shared.cloud
Imports System.Configuration
Imports System.IO

<DataContract>
Public Class frmGstContentModel
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

        Dim vlistcategory As New clsValueList
        vlistcategory.Add("Acts", "Acts")
        vlistcategory.Add("Notifications", "Notifications")
        vlistcategory.Add("Circulars", "Circulars")
        vlistcategory.Add("Others", "Others")

        Me.ValueLists.Add("Category", vlistcategory)
        Me.AddLookupField("Category", "Category")
    End Sub

    Public Overrides Function PrepForm(oView As clsViewModel, ByVal prepMode As EnumfrmMode, ByVal prepIDX As String, Optional ByVal strXML As String = "") As Boolean
        Dim sql As String
        Me.FormPrepared = False
        If prepMode = EnumfrmMode.acAddM Then prepIDX = 0
        sql = "Select * from gstContent Where gstContentID = " & prepIDX
        Me.InitData(sql, "", oView, prepMode, prepIDX, strXML)

        Me.FormPrepared = True
        Return Me.FormPrepared
    End Function

    Public Overrides Function Validate() As Boolean
        Me.InitError()

        'If myUtils.cStrTN(myRow("RoleName")).Trim.Length = 0 Then Me.AddError("RoleName", "Enter Role Name")

        Return Me.CanSave
    End Function

    Public Overrides Function VSave() As Boolean
        VSave = False
        If Me.Validate Then
            If Me.CanSave Then
                Dim PaymentDescrip As String = "Title: " & myRow("Title").ToString
                Try
                    myContext.Provider.dbConn.BeginTransaction(myContext, Me.Name, Me.frmMode.ToString, "gstContentID", frmIDX)
                    myContext.Provider.objSQLHelper.SaveResults(myRow.Row.Table, "Select * from gstContent Where gstContentID = " & frmIDX)
                    frmIDX = myRow("gstContentID")

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
    Public Overrides Function GenerateParamsOutput(dataKey As String, Params As List(Of clsSQLParam)) As clsProcOutput
        Dim FileName As String = myUtils.cStrTN(myContext.SQL.ParamValue("@filename", Params))
        Dim oRet As New clsProcOutput
        Select Case dataKey.Trim.ToLower
            Case "sas"
                Dim OrigFileName = Path.GetFileNameWithoutExtension(FileName)
                ' Generate File Name with new guid
                Dim _NewFileName = OrigFileName + "--" + System.Guid.NewGuid.ToString + Path.GetExtension(FileName)

                Dim provider As New clsBlobFileProvider(myContext)
                Dim oRet2 = provider.CreateUploadUri(ConfigurationManager.AppSettings("StorageContainerName"), _NewFileName, "")
                If oRet2.Success Then
                    oRet.JsonData = New With {.status = 200, .success = oRet.Success, .message = oRet.Message, .Data = oRet2.Result.Uri.ToString, .flName = _NewFileName}
                Else
                    'oRet.AddError("Cannot Create")
                    oRet.JsonData = New With {.status = 200, .success = oRet.Success, .message = oRet.Message} '"Unable to upload file."
                End If

        End Select

        Return oRet
    End Function

End Class
