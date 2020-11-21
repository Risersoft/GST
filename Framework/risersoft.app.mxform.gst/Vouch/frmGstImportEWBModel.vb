Imports risersoft.shared
Imports risersoft.app.mxent
Imports System.Runtime.Serialization
<DataContract>
Public Class frmGstImportEWBModel
    Inherits clsFormDataModel

    Protected Overrides Sub InitViews()
    End Sub

    Public Sub New(context As IProviderContext)
        MyBase.New(context)
        Me.InitViews()
        Me.InitForm()
    End Sub

    Private Sub InitForm()


        Dim Sql As String = "Select GstRegID, GSTIN from GstReg  Order by GSTIN"
        Me.AddLookupField("GstRegID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql), "GstReg").TableName)

        Sql = "Select PostPeriodID, ret_pd from PostPeriod  Order by ret_pd"
        Me.AddLookupField("PostPeriodID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql), "PostPeriod").TableName)


    End Sub

    Public Overrides Function PrepForm(oView As clsViewModel, ByVal prepMode As EnumfrmMode, ByVal prepIDX As String, Optional ByVal strXML As String = "") As Boolean
        Dim Sql As String

        Me.FormPrepared = False
        If prepMode = EnumfrmMode.acAddM Then prepIDX = 0
        Sql = "Select * from GstRegPP Where GstRegPPID = " & prepIDX
        Me.InitData(Sql, "", oView, prepMode, prepIDX, strXML)


        Dim dic As New clsCollecString(Of String)
        dic.Add("total", String.Format("select sec_nm, ret_pd, count(Invoiceid) As InvoiceCount, sum(Val) As val, sum(iamt) As iamt, sum(camt) As camt, sum(samt) As samt,
 sum(csamt) As csamt,sum(txval) As txval from gstlistinvoice() where doctype='IP' and gstregid={0} and postperiodid={1} group by sec_nm, ret_pd", myRow("gstregid"), myRow("postperiodid")))
        Me.AddDataSet("summ", dic)

        Me.FormPrepared = True

        Return Me.FormPrepared
    End Function

    Public Overrides Function Validate() As Boolean
        Me.InitError()

        Return Me.CanSave
    End Function

    Public Overrides Function GenerateDataParamsOutput(dataKey As String, ds As DataSet, Params As List(Of clsSQLParam)) As clsProcOutput
        Dim GstRegID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@gstregid", Params))
        Dim PostPeriodID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@postperiodid", Params))
        Dim oRet As New clsProcOutput
        Select Case dataKey
            Case "import"


        End Select
        Return oRet
    End Function
    Public Overrides Function GenerateParamsOutput(dataKey As String, Params As List(Of clsSQLParam)) As clsProcOutput
        Dim GstRegID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@gstregid", Params))
        Dim PostPeriodID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@postperiodid", Params))
        Dim oRet As New clsProcOutput
        Select Case dataKey.Trim.ToLower

            Case "payload"

        End Select
        Return oRet

    End Function
    Public Overrides Function VSave() As Boolean

        If Me.Validate Then

            VSave = True

        End If
    End Function
End Class
