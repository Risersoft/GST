Imports System.Configuration
Imports System.IO
Imports kpmg.app.mxform.asp
Imports Microsoft.Extensions.Logging
Imports risersoft.app.mxent
Imports risersoft.app.mxform
Imports risersoft.app.mxform.gst
Imports risersoft.app.reports.erp
Imports risersoft.app.reports.gst
Imports risersoft.shared
Imports risersoft.shared.cloud
Imports risersoft.shared.dal
Imports risersoft.shared.dotnetfx
Imports risersoft.shared.portable.Models.Nav
Imports Serilog

Public Class clsExtendAgentAppGst
    Inherits clsAppExtendRsBase

    Protected Friend mFileProvider As ICloudFileProvider, mQueueProvider As IQueueProvider
    Dim dic As clsCollecString(Of Boolean)


    Public Overrides Function StartupAppCode() As String
        Return ""       'will be taken as task by the Task Scheduler.
    End Function

    Public Overrides Function AppCodesHandled() As List(Of String)
        Return New List(Of String)(New String() {"gst"})
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
        If dic Is Nothing Then dic = myFuncs.dicMat(False, True, True, True, True, True, True, False)

        Return dic
    End Function

    Public Overrides Function FileServerRequired() As Boolean
        Return False
    End Function



    Public Overrides Function GenerateDataTable(pView As clsViewModel, conf As clsBandConf, pdclientview As String, strFilter As String) As DataTable
        Dim oGen As New clsPDCViewGenerator(pView.myContext)
        Dim dt1 As DataTable = oGen.GenerateDataTable(pView, conf, pdclientview, strFilter)

        Return dt1
    End Function
    Public Overrides Function FileProviderCode(context As IProviderContext) As String
        Return "blob"
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
        If mQueueProvider Is Nothing Then
            Dim setting1 As ConnectionStringSettings = ConfigurationManager.ConnectionStrings("ServiceBus")

            If setting1 IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(setting1.ConnectionString) Then
                mQueueProvider = New clsAzureSBQProvider(context, setting1.ConnectionString)
            End If
        End If

        Return mQueueProvider
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
            Me.AddTypeAssembly(dicTaskProvider, GetType(GSTR1TaskProvider).Assembly, GetType(clsTaskProviderBase))
        End If

        Return dicTaskProvider
    End Function

    Public Overrides Function AboutBox() As IForm
        Throw New NotImplementedException()
    End Function

    Public Overrides Function CreateDataProvider(context As clsAppController, cb As IAsyncWCFCallBack) As IAppDataProvider
        Dim Provider = New clsAppDataProvider(context.Controller, cb)

        Return Provider
    End Function
    Public Overrides Sub UpdateSettings(s As myAppSettings)
        MyBase.UpdateSettings(s)
        s.AppLoadBehaviour = EnumLoadBehaviour.acForceXML
    End Sub
    Public Overrides Function CreateLogger(ByVal context As IProviderContext) As Microsoft.Extensions.Logging.ILogger
        Dim log As Microsoft.Extensions.Logging.ILogger
        'WebJobs console output is routed to logs
        Dim log2 = New LoggerConfiguration().WriteTo.Console().CreateLogger()
        Using Factory = LoggerFactory.Create(Function(builder) builder.AddSerilog(log2))
            log = Factory.CreateLogger(Of IProviderContext)()
        End Using
        Return log
    End Function
End Class
