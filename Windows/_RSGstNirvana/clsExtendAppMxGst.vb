Imports risersoft.shared
Imports risersoft.shared.dal
Imports risersoft.app.mxform.gst
Imports risersoft.shared.win
Imports risersoft.shared.cloud
Imports System.Threading.Tasks
Imports risersoft.app.mxent
Imports System.ServiceModel
Imports risersoft.app2.shared
Imports risersoft.app.reports.erp
Imports risersoft.chrome.winforms
Imports risersoft.app.mxform
Imports risersoft.app.reports.gst
Imports risersoft.app.config
Imports System.Configuration
Imports System.IO
Imports System.Threading
Imports risersoft.shared.agent
Imports Microsoft.Extensions.Logging

Public Class clsExtendAppMxGst
    Inherits clsAppExtendRsBase

    Protected Friend strApp As String = "", mFileProvider As ICloudFileProvider, mQueueProvider As IQueueProvider
    Dim dic As clsCollecString(Of Boolean), cts As CancellationTokenSource
    Public Overrides Function GetLicProductInfo() As LicProductInfo
        Return New LicProductInfo("gst", 1.0, "GstNirvana.Pro")
    End Function
    Public Overrides Function AboutBox() As risersoft.shared.IForm
        Return New frmAboutBox
    End Function
    Public Overrides Function fBrowser() As IMxBrowser
        Dim f As New BrowserForm
        f.MakeDownLevel()
        Return f
    End Function
    Public Overrides Function StartupAppCode() As String
        Return strApp
    End Function

    Public Overrides Function ProgramName() As String
        Return "GstNirvana"
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
        If dic Is Nothing Then dic = myFuncsBase.dicMat(False, True, True, True, True, True, True, False)
        Return dic
    End Function

    Public Overrides Function FileServerRequired() As Boolean
        Return False
    End Function
    Public Overrides Sub UpdateSettings(s As risersoft.shared.myAppSettings)
        s.RelateLoginPerson = True
        's.AppLoadBehaviour = EnumLoadBehaviour.acForceXML
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
        Dim Provider As IAppDataProvider = ProviderFactory.CreateDataProvider(context, cb)
        Return Provider

    End Function

    Public Overrides Function dicFormModelTypes() As clsCollecString(Of Type)
        If dicFormModel Is Nothing Then
            dicFormModel = New clsCollecString(Of Type)
            Me.AddTypeAssembly(dicFormModel, GetType(frmDBSecurityModel).Assembly, GetType(clsFormDataModel))
            Me.AddTypeAssembly(dicFormModel, GetType(InvoiceRepo).Assembly, GetType(clsFormDataModel))
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
        End If
        Return dicTaskProvider
    End Function
    Public Overrides Function OnAppInit(oApp As clsCoreApp) As Boolean
        risersoft.app.gst.myFuncs.GetFormFunc = AddressOf risersoft.app.gst.myFuncs.GetForm
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
        ChromeHandler.Shutdown()
        Return MyBase.OnAppClose(oApp)
    End Function
    Public Overrides Function frmDel(pView As clsView, IDX As String, sysentXML As String) As IForm
        Dim f As frmDel = New frmDel
        f.PrepForm(pView, EnumfrmMode.acEditM, IDX, sysentXML)
        Return f

    End Function
    Public Overrides Function CreateLogger(context As IProviderContext) As ILogger
        Return myWinUtils2.CreateLogger(context)
    End Function
    Public Overrides Function fncHoliday(context As IProviderContext) As Func(Of DateTime, Integer, Boolean)
        Return Function(x, y)
                   Return False
               End Function
    End Function
End Class
