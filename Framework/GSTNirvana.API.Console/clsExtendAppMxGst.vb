Imports System.IO
Imports System.Threading
Imports Microsoft.Extensions.Logging
Imports risersoft.app.mxent
Imports risersoft.app.mxform
Imports risersoft.app.mxform.gst
Imports risersoft.app.reports.gst
Imports risersoft.shared
Imports risersoft.shared.agent
Imports risersoft.shared.cloud
Imports risersoft.shared.dal
Imports risersoft.shared.dotnetfx
Imports risersoft.shared.portable.Models.Nav
Imports risersoft.shared.portable.Models.Publisher
Imports Serilog

Public Class clsExtendAppMxGst
    Inherits clsAppExtendRsBase

    Protected Friend strApp As String = "", mFileProvider As ICloudFileProvider, mQueueProvider As IQueueProvider
    Dim dic As clsCollecString(Of Boolean), cts As CancellationTokenSource

    Public Overrides Function GetLicProductInfo() As LicProductInfo
        '   Return New LicProductInfo("kasp", 1.0, "KGCPPlus.Pro")
        Return New LicProductInfo("gst", 1.0, "GSTNirvana.Std")
    End Function
    Public Overrides Function StartupAppCode() As String
        Return strApp
    End Function
    Public Overrides Function Publisher() As PublisherInfo
        ' Dim info =  New PublisherInfo With {.PublisherId = Guid.Parse("5f59c3cf-8237-4d2a-ac4f-e0fd68fde900"), .PublisherName = "KPMG"}
        Dim info = New PublisherInfo With {.PublisherId = Guid.Parse("ff35cdd7-33af-414f-a8a0-901e97d7f7b9"),
           .PublisherName = "Risersoft"}
        Return info
    End Function
    Public Overrides Function ProgramName() As String
        Return "GSTNirvana"
        'Return "ASP Solution"
    End Function

    Public Overrides Function NewDBName() As String
        Return "mxgstdb"
    End Function


    Public Overrides Function MinDBVersion() As Decimal
        Return My.Settings.MinDBVersion
    End Function

    Public Overrides Function ProgramCode() As String
        Return "mxgst"
    End Function

    Public Overrides Function LinkLabel() As String
        Return "http://www.gstnirvana.com"
    End Function
    Public Overrides Function dicMat() As clsCollecString(Of Boolean)
        'TODO: ETO=Inv, once this is implemented for Max @Kanohar
        If dic Is Nothing Then dic = myFuncs.dicMat(False, True, True, True, True, True, True, False)
        Return dic
    End Function

    Public Overrides Function FileServerRequired() As Boolean
        Return True
    End Function
    Public Overrides Sub UpdateSettings(s As risersoft.shared.myAppSettings)
        s.AppDataDBPublisher = "Risersoft"
        s.RelateLoginPerson = True
        s.EnterpriseMenu = True
    End Sub


    Public Overrides Function GenerateDataTable(pView As clsViewModel, conf As clsBandConf, pdclientview As String, strFilter As String) As DataTable
        Dim oGen As New clsPDCViewGenerator(pView.myContext)
        Dim dt1 As DataTable = oGen.GenerateDataTable(pView, conf, pdclientview, strFilter)
        Return dt1
    End Function

    Public Overrides Function FileProviderClient(context As IProviderContext, ContainerName As String, ProviderCode As String) As clsFileProviderClientBase
        Dim provider As clsFileProviderClientBase
        Select Case ProviderCode.Trim.ToLower
            Case "blob"
                If mFileProvider Is Nothing Then mFileProvider = New clsBlobFileProvider(context)
                provider = New clsBlobFileClient(context, ContainerName, mFileProvider)
            Case Else
                provider = MyBase.FileProviderClient(context, ContainerName, ProviderCode)
        End Select
        Return provider
    End Function
    Public Overrides Function QueueProvider(context As IProviderContext) As IQueueProvider
        If (mQueueProvider Is Nothing) Then mQueueProvider = New clsLocalQueueProvider(context)
        Return mQueueProvider
    End Function
    Public Overrides Function CreateDataProvider(context As clsAppController, cb As IAsyncWCFCallBack) As IAppDataProvider
        Dim Provider = New clsAppDataProvider(context.Controller, cb)
        Return Provider

    End Function

    Public Overrides Function dicFormModelTypes() As clsCollecString(Of Type)
        If dicFormModel Is Nothing Then
            dicFormModel = New clsCollecString(Of Type)
            Me.AddTypeAssembly(dicFormModel, GetType(frmDBSecurityModel).Assembly, GetType(clsFormDataModel))
            Me.AddTypeAssembly(dicFormModel, GetType(InvoiceRepo).Assembly, GetType(clsFormDataModel))
            dicFormModel.AddUpd("frmGstCampusModel", GetType(frmCampusModel))
        End If
        Return dicFormModel
    End Function
    Public Overrides Function dicReportProviderTypes(myContext As clsAppController) As clsCollecString(Of Type)
        If dicReportModelProvider Is Nothing Then
            dicReportModelProvider = New clsCollecString(Of Type)
            Me.AddTypeAssembly(dicReportModelProvider, GetType(exvReportDataProvider).Assembly, GetType(clsReportDataProviderBase))
            Me.AddTypeAssembly(dicReportModelProvider, GetType(gstReportDataProvider).Assembly, GetType(clsReportDataProviderBase))
        End If
        Return dicReportModelProvider

    End Function


    Public Overrides Function dicTaskProviderTypes() As clsCollecString(Of Type)
        If dicTaskProvider Is Nothing Then
            dicTaskProvider = New clsCollecString(Of Type)
            Me.AddTypeAssembly(dicTaskProvider, GetType(EVTaskProvider).Assembly, GetType(clsTaskProviderBase))
            Me.AddTypeAssembly(dicTaskProvider, GetType(GSTR1TaskProvider).Assembly, GetType(clsTaskProviderBase))
        End If
        Return dicTaskProvider
    End Function
    Public Overrides Function OnAppInit(oApp As clsCoreApp) As Boolean
        Dim q = New clsLocalQueueProvider(oApp.Controller)
        mQueueProvider = q
        cts = New CancellationTokenSource
        Dim ct = cts.Token
        q.ConfigureListener(ct, Async Function(dic As Dictionary(Of String, String)) As Task(Of clsProcOutput)
                                    Return Await Task.Run(Async Function()
                                                              Dim scheduler = New clsTaskScheduler(oApp, True)
                                                              Dim oRet2 = Await scheduler.ExecuteLocalApiTask(dic("apitaskId"))
                                                              Return oRet2
                                                          End Function)
                                End Function)
        Return MyBase.OnAppInit(oApp)
    End Function
    Public Overrides Function OnAppClose(oApp As clsCoreApp) As Boolean
        cts.Cancel()
        Return MyBase.OnAppClose(oApp)
    End Function

    Public Overrides Function GetDashBoardSettings(context As IProviderContext, vwState As clsViewState, DashboardXML As String) As DashboardSettingModel
        Return myFuncs2.GetDashBoardSettings(context, vwState, DashboardXML)

    End Function
    Public Overrides Function GenerateParamValue(context As IProviderContext, str1 As String) As String
        If myUtils.IsInList(str1, "gstregid") Then
            Return myFuncs2.GetParamGstRegID(context)
        Else
            Return MyBase.GenerateParamValue(context, str1)
        End If
    End Function
    Public Overrides Function CreateLogger(context As IProviderContext) As Microsoft.Extensions.Logging.ILogger
        Dim Log As Microsoft.Extensions.Logging.ILogger
        Dim path2 = Path.Combine("Logs",
                If(context.Controller.CalcRLSId() = Guid.Empty, "", context.Controller.CalcRLSId().ToString()))
        Dim fileSuffix = If(context.Controller.TaskInfo.ApiTaskID = Guid.Empty, "", context.Controller.TaskInfo.ApiTaskID.ToString())

        If (Not Directory.Exists(path2)) Then Directory.CreateDirectory(path2)

        Dim filePathFormat = Path.Combine(path2, $"log{fileSuffix}.txt")

        Dim Log2 = New LoggerConfiguration().WriteTo.File(filePathFormat,
                fileSizeLimitBytes:=10485760, retainedFileCountLimit:=101).
                WriteTo.Console().CreateLogger()
        Using Factory = LoggerFactory.Create(Function(builder) builder.AddSerilog(Log2))
            Log = Factory.CreateLogger(Of IProviderContext)()
        End Using
        Return Log
    End Function
    Public Overrides Function fncHoliday(context As IProviderContext) As Func(Of DateTime, Integer, Boolean)
        Return Function(x, y)
                   Return False
               End Function
    End Function

    Public Overrides Function AboutBox() As IForm
        Throw New NotImplementedException()
    End Function
End Class
