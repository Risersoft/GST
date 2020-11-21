Imports Microsoft.Azure.WebJobs
Imports Microsoft.Extensions.Logging
Imports Newtonsoft.Json.Linq

Public Class SampleFunctions
    Private ReadOnly _sampleServiceA As ISampleServiceA
    Private ReadOnly _sampleServiceB As ISampleServiceB

    Public Sub New(ByVal sampleServiceA As ISampleServiceA, ByVal sampleServiceB As ISampleServiceB)
        _sampleServiceA = sampleServiceA
        _sampleServiceB = sampleServiceB
    End Sub

    <Singleton>
    Public Sub BlobTrigger(
    <BlobTrigger("test")> ByVal blob As String, ByVal logger As ILogger)
        _sampleServiceB.DoIt()
        logger.LogInformation("Processed blob: " & blob)
    End Sub

    Public Sub BlobPoisonBlobHandler(
<QueueTrigger("webjobs-blobtrigger-poison")> ByVal blobInfo As JObject, ByVal logger As ILogger)
        Dim container As String = CStr(blobInfo("ContainerName"))
        Dim blobName As String = CStr(blobInfo("BlobName"))
        logger.LogInformation($"Poison blob: {container}/{blobName}")
    End Sub
    Public Sub ProcessWorkItem(
    <QueueTrigger("test2")> ByVal workItem As String, ByVal logger As ILogger)
        _sampleServiceA.DoIt()
        logger.LogInformation($"Processed work item {workItem}")
    End Sub

    <WorkItemValidator>
    Public Sub ProcessWorkItem_Json(
    <QueueTrigger("test")> ByVal workItem As WorkItem, ByVal logger As ILogger)
        _sampleServiceA.DoIt()
        logger.LogInformation($"Processed work item {workItem.ID}")
    End Sub

    Public Async Function ProcessWorkItem_ServiceBus(
<ServiceBusTrigger("payload", Connection:="ServiceBus")> ByVal item As String, ByVal messageId As String, ByVal deliveryCount As Integer, ByVal log As ILogger) As Task
        log.LogInformation($"Processing ServiceBus message (Id={messageId}, DeliveryCount={deliveryCount})")
        Await Task.Delay(1000)
        log.LogInformation($"Message complete (Id={messageId})")
    End Function


End Class
'https://github.com/Azure/azure-functions-host/issues/3408
'https://github.com/Azure/azure-webjobs-sdk/issues/1924