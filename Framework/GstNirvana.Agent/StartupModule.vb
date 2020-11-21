Imports Microsoft.Azure.WebJobs
Imports Microsoft.Extensions.Hosting
Imports Microsoft.Extensions.DependencyInjection
Imports Microsoft.Extensions.Logging
Imports Microsoft.Azure.WebJobs.Host
Imports Microsoft.Extensions.Configuration
Imports Microsoft.Azure.WebJobs.Extensions.Timers
Imports risersoft.shared.dotnetfx
Imports risersoft.shared.agent
Imports risersoft.shared
' To learn more about Microsoft Azure WebJobs SDK, please see https://go.microsoft.com/fwlink/?LinkID=320976
Module StartupModule

    ' Please set the following connection strings in app.config for this WebJob to run:
    ' AzureWebJobsDashboard and AzureWebJobsStorage
    Sub Main(args As String())
        Dim builder = New HostBuilder().UseEnvironment("Development").
            ConfigureWebJobs(Sub(b As IWebJobsBuilder)
                                 b.AddAzureStorageCoreServices.
                                 AddAzureStorage.
                                 AddServiceBus.
                                 AddTimers
                             End Sub).
            ConfigureAppConfiguration(Sub(b As IConfigurationBuilder)

                                          b.AddCommandLine(args)
                                      End Sub).
            ConfigureLogging(Sub(context As HostBuilderContext, b As ILoggingBuilder)
                                 b.SetMinimumLevel(LogLevel.Debug)
                                 b.AddConsole()
                                 b.AddDebug()
                                 Dim appInsightsKey As String = context.Configuration("APPINSIGHTS_INSTRUMENTATIONKEY")
                                 If Not String.IsNullOrEmpty(appInsightsKey) Then
                                     'b.AddApplicationInsights(Function(o) o.InstrumentationKey = appInsightsKey)
                                 End If
                             End Sub).
            ConfigureServices(Sub(services As IServiceCollection)
                                  services.AddSingleton(Of ISampleServiceA, SampleServiceA)()
                                  services.AddSingleton(Of ISampleServiceB, SampleServiceB)()
                                  services.AddSingleton(Of INameResolver, QueueNameResolver)
                              End Sub).UseConsoleLifetime()

        Dim host = builder.Build()

#If DEBUG Then
        risersoft.API.GSTN.GSTNConstants.base_path = ".\bin\Debug"
#End If

        Try
            risersoft.API.GSTN.GSTNConstants.publicip = GlobalCore.GetConfigSetting("publicip") 'New System.Net.WebClient().DownloadString("http://ipinfo.io/ip").Trim
        Catch ex As Exception
            risersoft.API.GSTN.GSTNConstants.publicip = "11.10.1.1"
        End Try

        System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Ssl3 Or System.Net.SecurityProtocolType.Tls Or System.Net.SecurityProtocolType.Tls11 Or System.Net.SecurityProtocolType.Tls12

        Globals.myApp = New clsConsoleApp(New clsExtendAgentAppGst)

        Using host
            host.Run
        End Using
    End Sub
    'https://github.com/Azure/azure-webjobs-sdk/issues/1871
    'https://nguyentoanuit.wordpress.com/2018/06/18/create-azure-webjob-in-net-core/
End Module
