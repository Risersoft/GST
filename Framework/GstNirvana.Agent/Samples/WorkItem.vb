Imports System.ComponentModel.DataAnnotations
Imports System.Linq
Imports System.Threading
Imports System.Threading.Tasks
Imports Microsoft.Azure.WebJobs.Host
Imports Microsoft.Extensions.Logging
Imports SampleHost.Models
Imports System.Runtime.InteropServices
Public Class WorkItem
    Public Property ID As String
    Public Property Priority As Integer
    Public Property Region As String
    Public Property Category As Integer
    Public Property Description As String
End Class
Public Class WorkItemValidatorAttribute
    Inherits FunctionInvocationFilterAttribute

    Public Overrides Function OnExecutingAsync(ByVal executingContext As FunctionExecutingContext, ByVal cancellationToken As CancellationToken) As Task
        executingContext.Logger.LogInformation("WorkItemValidator executing...")
        Dim workItem = TryCast(executingContext.Arguments.First().Value, WorkItem)
        Dim errorMessage As String = Nothing

        If Not TryValidateWorkItem(workItem, errorMessage) Then
            executingContext.Logger.LogError(errorMessage)
            Throw New ValidationException(errorMessage)
        End If

        Return MyBase.OnExecutingAsync(executingContext, cancellationToken)
    End Function

    Private Shared Function TryValidateWorkItem(ByVal workItem As WorkItem, <Out> ByRef errorMessage As String) As Boolean
        errorMessage = Nothing

        If String.IsNullOrEmpty(workItem.ID) Then
            errorMessage = "ID cannot be null or empty."
            Return False
        End If

        If workItem.Priority > 100 OrElse workItem.Priority < 0 Then
            errorMessage = "Priority must be between 0 and 100"
            Return False
        End If

        Return True
    End Function
End Class
