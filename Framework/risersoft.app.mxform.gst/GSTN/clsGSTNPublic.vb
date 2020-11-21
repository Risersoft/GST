Imports risersoft.shared
Imports risersoft.API.GSTN
Imports risersoft.API.GSTN.Public
Imports risersoft.API.GSTN.Auth
Imports System.Net

Public Class clsGSTNPublic
    Inherits clsGSTNTrackedAPIBase

    Public Sub New(context As IProviderContext)
        MyBase.New(context, "PUBLIC", "")
        Me.DefaultGSP = Function(x)
                            If myUtils.IsInList(x, "test") Then Return "GSTN" Else Return "ASPONE"
                        End Function
    End Sub

    Public Overrides Function GetAuthClientFromToken(r1 As DataRow, rGstReg As DataRow) As IGSTNAuthProvider
        Dim token As New TokenResponseModel
        If Not r1 Is Nothing Then
            token.auth_token = r1("authtoken")
            token.expiry = r1("expiry")
            Dim config = Me.GetGspConfig()
            Dim client As GSTNPublicAuthClient = Me.GetAuthClient(config.Item1, config.Item2)
            CType(client, GSTNPublicAuthClient).token = token
            Return client
        End If

    End Function
    Protected Friend Function GetAuthClient(gsp As String, env As String) As GSTNPublicAuthClient
        Dim client As GSTNPublicAuthClient
        Select Case gsp.Trim().ToLower()
            Case "kpmg"
                client = New KpmgPublicAuthClient(env)
            Case "aspone"
                client = New AspOnePublicAuthClient(env)
            Case Else
                client = New GSTNPublicAuthClient(gsp, env)

        End Select
        client.Init()
        Return client
    End Function
    Protected Friend Function GetClient(gsp As String, provider As IGSTNAuthProvider) As GSTNPublicApiClient
        Dim client As GSTNPublicApiClient
        Select Case gsp.Trim().ToLower()
            Case "aspone"
                client = New AspOnePublicApiClient(provider)
            Case Else
                client = New GSTNPublicApiClient(provider)
        End Select
        Return client
    End Function
    Public Function GetToken(GstRegID As Integer) As GSTNResult(Of TokenResponseModel)
        Dim config = Me.GetGspConfig()
        Dim client As GSTNPublicAuthClient = Me.GetAuthClient(config.Item1, config.Item2)
        Dim model As GSTNResult(Of TokenResponseModel) = client.RequestToken("")
        Me.SaveTransaction(GstRegID, 0, "TOKEN", "", client.dicParams)
        If (model IsNot Nothing) AndAlso (model.Data IsNot Nothing) Then
            If (client.DecryptedKey Is Nothing) Then
                model.Data.decryptSek = ""
            Else
                model.Data.decryptSek = Convert.ToBase64String(client.DecryptedKey)
            End If
            Dim nr = Me.SaveToken(myContext.Police.UniqueUserID, GstRegID, model.Data, "")
        End If
        Return model

    End Function


    Public Function SearchPAN(pan As String) As GSTNResult(Of TaxPayerModel)
        Dim output As New GSTNResult(Of TaxPayerModel)
        Dim GstRegID As Integer = 0
        Dim oRet = Me.CheckToken(GstRegID)
        If oRet.Success Then
            Dim nr = oRet.GetCalcRows("token")(0)
            Dim provider As IGSTNAuthProvider = Me.GetAuthClientFromToken(nr, Nothing)
            Dim config = Me.GetGspConfig()
            Dim client = Me.GetClient(config.Item1, provider)
            output = client.SearchPAN(pan)
            Me.SaveTransaction(GstRegID, 0, "SEARCH", "", client.dicParams)
        End If

        Return output
    End Function
    Public Function SearchGSTIN(gstin As String) As GSTNResult(Of TaxPayerModel)
        Dim output As New GSTNResult(Of TaxPayerModel)
        Dim GstRegID As Integer = 0
        Dim oRet = Me.CheckToken(GstRegID)
        If oRet.Success Then
            Dim nr = oRet.GetCalcRows("token")(0)
            Dim provider As IGSTNAuthProvider = Me.GetAuthClientFromToken(nr, Nothing)
            Dim config = Me.GetGspConfig()
            Dim client = Me.GetClient(config.Item1, provider)
            output = client.SearchGSTIN(gstin)
            Me.SaveTransaction(GstRegID, 0, "SEARCH", "", client.dicParams)
        End If

        Return output
    End Function
    Public Function TrackReturns(gstin As String, fy As String) As GSTNResult(Of TrackReturnModel)
        Dim output As New GSTNResult(Of TrackReturnModel)
        Dim GstRegID As Integer = 0
        Dim oRet = Me.CheckToken(GstRegID)
        If oRet.Success Then
            Dim nr = oRet.GetCalcRows("token")(0)
            Dim provider As IGSTNAuthProvider = Me.GetAuthClientFromToken(nr, Nothing)
            Dim config = Me.GetGspConfig()
            Dim client = Me.GetClient(config.Item1, provider)
            output = client.TrackReturns(gstin, fy)
            Me.SaveTransaction(GstRegID, 0, "TRACK", "", client.dicParams)
        End If

        Return output
    End Function
    Public Overrides Function CheckToken(GstRegID As Integer) As clsProcOutput
        Dim oRet As New clsProcOutput
        Dim nr = Me.GetActiveToken(myContext.Police.UniqueUserID, GstRegID)
        If nr Is Nothing Then
            Dim Model = Me.GetToken(GstRegID)
            If Model Is Nothing Then
                oRet.AddError("Cannot get token")
            Else
                nr = Me.GetActiveToken(myContext.Police.UniqueUserID, GstRegID)
                oRet.AddDataRow("token", nr)
            End If
        Else
            oRet.AddDataRow("token", nr)
        End If
        Return oRet

    End Function

End Class
