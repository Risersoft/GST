Imports System.Configuration
Imports Microsoft.Azure.WebJobs
Imports Microsoft.Extensions.Logging
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports risersoft.shared
Imports risersoft.shared.agent
Imports risersoft.shared.cloud
Imports risersoft.shared.cloud.common
Imports risersoft.shared.DotnetFx
Public Class TriggerFunctions
    Public Async Function ProcessWorkItem_ServiceBus(
<ServiceBusTrigger("%gstqueue%", Connection:="ServiceBus")> ByVal msg As String, ByVal messageId As String, ByVal deliveryCount As Integer, ByVal log As ILogger) As Task
        log.LogInformation($"Processing ServiceBus message (Id={messageId}, DeliveryCount={deliveryCount})")


        Dim dic = JsonConvert.DeserializeObject(Of Dictionary(Of String, String))(msg)

        Dim TenantId As String = dic("tenantid")
        Dim ApiTaskId As String = dic("apitaskid")
        Dim BaseHost = dic("basehost")

        Dim oRet = AgentAuthProvider.GenerateAccountInfo(myApp, BaseHost, TenantId)
        If Not oRet.Success Then
            log.LogInformation($"Message could not be processed (Id={messageId}), (Message={oRet.Message})")
        Else
            Dim scheduler = New clsTaskScheduler(myApp, False)
            Dim oRet2 = Await scheduler.ExecuteServerAccApiTask(oRet.Result.Account, oRet.Result.Env, BaseHost, ApiTaskId)

            log.LogInformation($"Message complete (Id={messageId})")
        End If
    End Function

    Public Async Sub CronJob(<TimerTrigger("* */15 * * * *", RunOnStartup:=True)> timerInfo As TimerInfo, log As ILogger)
        log.LogInformation("Timer job fired!")
        Dim scheduler = New clsTaskScheduler(myApp, False) With {.ExecuteByAgent = True}

        Await scheduler.RunServerScheduler()
        log.LogInformation("Timer job finished")
    End Sub
End Class
Public Class QueueNameResolver
    Implements INameResolver

    Public Function Resolve(ByVal name As String) As String Implements INameResolver.Resolve
        Dim authority As String = GlobalCore.GetConfigSetting("authority")
        Dim queueName As String = TaskProviderFactory.GetQueueName(authority, "")
        Return queueName
    End Function
End Class

'https://github.com/Azure/azure-functions-host/issues/3408
'https://github.com/Azure/azure-webjobs-sdk/issues/1924

'TimerTrigger
'https://docs.microsoft.com/en-us/azure/azure-functions/functions-bindings-timer#cron-expressions
'https://docs.microsoft.com/en-us/azure/app-service/web-sites-create-web-jobs#CreateScheduledCRON
'https://stackoverflow.com/questions/35003122/azure-webjob-that-runs-every-couple-of-minutes
'https://blogs.msdn.microsoft.com/amitagarwal/2018/01/01/custom-schedule-for-azure-web-job-timer-triggers/
'https://github.com/Azure/azure-webjobs-sdk-extensions/blob/dev/src/ExtensionsSample/Samples/TimerSamples.cs