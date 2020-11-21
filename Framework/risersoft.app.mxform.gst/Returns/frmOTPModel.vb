Imports risersoft.shared
Imports risersoft.shared.cloud
Imports risersoft.shared.cloud.common
Imports System.Runtime.Serialization
Imports System.Configuration
Imports Newtonsoft.Json

<DataContract>
Public Class frmOTPModel
    Inherits clsFormDataModel

    Protected Overrides Sub InitViews()
        myView = Me.GetViewModel("GSTReg")
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
        Me.AddLookupField("ReturnPeriodID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql), "PostPeriod").TableName)
    End Sub

    Public Overrides Function PrepForm(oView As clsViewModel, ByVal prepMode As EnumfrmMode, ByVal prepIDX As String, Optional ByVal strXML As String = "") As Boolean
        Dim Sql As String
        Me.FormPrepared = False
        If Not prepIDX Is Nothing Then
            prepMode = EnumfrmMode.acEditM
            Sql = "Select CompanyID,CompName,PANNum from Company Where CompanyID = " & prepIDX
            Me.InitData(Sql, "returnperiodid", oView, prepMode, prepIDX, strXML)

            myView.MainGrid.MainConf("formatxml") = "<COL KEY=""Descrip"" CAPTION=""State""/>"
            Sql = "Select GSTRegID, GSTIN, Descrip, 0 as ExpiryMins, '' as OTP from GstListGSTReg() where  CompanyID = " & prepIDX
            myView.MainGrid.QuickConf(Sql, True, "1-1-1-1")
            Dim oProc As New clsGSTNReturnGSTR1(myContext)
            For Each r1 As DataRow In myView.MainGrid.myDS.Tables(0).Select
                Dim rToken = oProc.GetActiveToken(myContext.Police.UniqueUserID, myUtils.cValTN(r1("GSTRegID")))
                If rToken Is Nothing Then
                    r1("ExpiryMins") = 0
                Else
                    r1("ExpiryMins") = DateDiff(DateInterval.Minute, TaskProviderFactory.FindLocalTime, rToken("ExpiryOn"))
                    r1("OTP") = myUtils.cStrTN(rToken("OTP"))
                End If
            Next
            myView.MainGrid.PrepEdit("<BAND INDEX=""0""><COL KEY=""OTP""/></BAND>", EnumEditType.acEditOnly)


            Me.FormPrepared = True
        End If
        Return Me.FormPrepared
    End Function
    Public Overrides Function Validate() As Boolean
        Me.InitError()
        Return Me.CanSave
    End Function

    Public Overrides Function VSave() As Boolean
        VSave = False
        If Me.Validate Then
            VSave = True
        End If
    End Function

    Public Overrides Function GenerateParamsOutput(dataKey As String, Params As List(Of clsSQLParam)) As clsProcOutput
        Dim oRet As New clsProcOutput
        Dim GstRegID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@gstregid", Params))

        If GstRegID = 0 Then
            oRet.AddError("Please select GSTIN")
        Else
            Select Case dataKey.Trim.ToLower
                Case "otp"
                    Dim oProc As New clsGSTNReturnGSTR1(myContext)
                    oRet = oProc.CheckToken(GstRegID)
                Case "token"
                    Dim oProc As New clsGSTNReturnGSTR1(myContext)
                    Dim OTP As String = myUtils.cStrTN(myContext.SQL.ParamValue("@otp", Params))
                    oRet = oProc.GetTokenOutput(oProc.GetToken(GstRegID, OTP))
                Case "refreshtoken"
                    Dim oProc As New clsGSTNReturnGSTR1(myContext)
                    oProc.RefreshToken(GstRegID)
                    oRet.JsonData = New With {.status = 200}
            End Select
        End If

        Return oRet
    End Function

End Class
