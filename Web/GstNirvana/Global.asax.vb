Imports System.Net
Imports System.Net.Security
Imports System.Security.Claims
Imports System.Security.Cryptography.X509Certificates
Imports System.Web.Helpers
Imports System.Web.Hosting
Imports System.Web.Http
Imports System.Web.Optimization
Imports AutoMapper
Imports risersoft.API.GSTN
Imports risersoft.app.mxform.gst
Imports risersoft.shared
Imports risersoft.shared.web
Imports risersoft.shared.web.mvc
Imports risersoft.web.mvc.gst
Imports Serilog
Imports Serilog.Events

Public Class MvcApplication
    Inherits System.Web.HttpApplication

    Protected Sub Application_Start()
        Dim str1 = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs\log-.txt")
        Log.Logger = New LoggerConfiguration().WriteTo.File(str1, rollingInterval:=RollingInterval.Day, rollOnFileSizeLimit:=True).CreateLogger()
        System.Diagnostics.Trace.Listeners.Add(New TraceListener2())
        Dim oApp = New clsMvcWebApp(New clsExtendWebAppGst)
        myWebApp = oApp
        oApp.fncVirutalPathProvider = Function()
                                          Dim provider = New EmbeddedVirtualPathProvider()
                                          provider.AddTypeAssembly(GetType(EWBController).Assembly)
                                          Return provider
                                      End Function
        HostingEnvironment.RegisterVirtualPathProvider(oApp.fncVirutalPathProvider())
        myFuncs2.InitializeMapper()
        ControllerBuilder.Current.SetControllerFactory(GetType(MyControllerFactory))
        AreaRegistration.RegisterAllAreas()
        GlobalConfiguration.Configure(AddressOf WebApiConfig.Register)
        FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters)
        RouteConfig.RegisterRoutes(RouteTable.Routes)
        BundleConfig.RegisterBundles(BundleTable.Bundles)
        AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier
        GSTNConstants.base_path = Server.MapPath("/bin/")

        Try
            GSTNConstants.publicip = GlobalCore.GetConfigSetting("publicip") 'New WebClient().DownloadString("http://ipinfo.io/ip").Trim
        Catch ex As Exception
            GSTNConstants.publicip = "11.10.1.1"
        End Try
        ServicePointManager.ServerCertificateValidationCallback = New RemoteCertificateValidationCallback(Function(sender As Object, certificate As X509Certificate, chain As X509Chain, sslPolicyErrors As SslPolicyErrors)
                                                                                                              Return True
                                                                                                          End Function)

        Dim razorEngine = ViewEngines.Engines.OfType(Of RazorViewEngine)().FirstOrDefault()
        razorEngine.ViewLocationFormats = razorEngine.ViewLocationFormats.Concat(New String() {"~/Views/Forms/{0}.cshtml", "~/Views/Forms/{0}.vbhtml"}).ToArray()
        razorEngine.PartialViewLocationFormats = razorEngine.PartialViewLocationFormats.Concat(New String() {"~/Views/Forms/{0}.cshtml", "~/Views/Forms/{0}.vbhtml"}).ToArray()
        'https://www.ryadel.com/en/asp-net-mvc-add-custom-locations-to-the-view-engine-default-search-patterns/
    End Sub
End Class
