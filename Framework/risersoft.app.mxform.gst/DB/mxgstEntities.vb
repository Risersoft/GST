Imports risersoft.shared.dal
Imports risersoft.shared.portable.Model

Partial Public Class mxgstEntities
    Public ReadOnly TenantId As Guid
    Public ReadOnly UserId, UserName, AppName As String
    Public Sub New(strConn As String, info As RSCallerInfo)
        MyBase.New(strConn)
        Me.TenantId = info.UserAccount.AccountId
        Me.UserId = info.identity.Id.ToString
        Me.UserName = info.identity.UniqueName
        Me.AppName = info.AppHost.AppName
        AddHandler Me.Database.Connection.StateChange, AddressOf Connection_StateChange

        Me.Database.Log = Sub(log) System.Diagnostics.Debug.WriteLine(log)
    End Sub
    Sub Connection_StateChange(sender As Object, e As System.Data.StateChangeEventArgs)
        If e.CurrentState = System.Data.ConnectionState.Open Then
            TryCast(sender, System.Data.SqlClient.SqlConnection).SetSessionContext("TenantId", TenantId)
        End If
    End Sub

    Public Overrides Function SaveChanges() As Integer
        ChangeTracker.UpdateRLSID("TenantID", TenantId)
        ChangeTracker.UpdateUsers(UserId, UserName, AppName)
        Return MyBase.SaveChanges()
    End Function
End Class
