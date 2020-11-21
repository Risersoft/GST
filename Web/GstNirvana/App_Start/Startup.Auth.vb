Imports System.Security.Claims
Imports System.Threading.Tasks
Imports Microsoft.Owin.Security
Imports Microsoft.Owin.Security.Cookies
Imports Microsoft.Owin.Security.OAuth
Imports Owin
Imports risersoft.shared
Imports risersoft.shared.web

Partial Public Class Startup

    Public Sub ConfigureAuth(app As IAppBuilder)
        ' Enable Application Sign In Cookie
        app.UseRSAuthClient()
        app.UseRSAuthBearerToken()

        'app.UseC1Reports()
        Dim options = New BasicAuthenticationOptions("SecureApi", Function(username, password)
                                                                      Return Authenticate(username, password)
                                                                  End Function) With {.AuthenticationMode = AuthenticationMode.Passive}
        app.UseBasicAuthentication(options)


    End Sub
    Private Function Authenticate(username As String, password As String) As Task(Of IEnumerable(Of Claim))
        ' authenticate user
        If myUtils.IsInList(username, "gstn") AndAlso myUtils.IsInList(password, "open") Then
            Return Task.FromResult(Of IEnumerable(Of Claim))(New List(Of Claim)() From {
                New Claim("name", username)
            })
        End If

        Return Nothing
    End Function


End Class
