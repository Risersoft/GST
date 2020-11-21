Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.ServiceModel
Imports System.ServiceModel.Activation
Imports System.Web
Imports System.Web.Mvc
Imports System.Web.Routing
Imports risersoft.shared
Imports risersoft.shared.sync
Imports risersoft.shared.web

Public Module RouteConfig
    Public Sub RegisterRoutes(ByVal routes As RouteCollection)
        routes.IgnoreRoute("{resource}.axd/{*pathInfo}")
        routes.IgnoreRoute("{*staticfile}", New With {.staticfile = ".*\.(css|js|gif|jpg)(/.*)?"})

        routes.MapRoute(
            name:="Default",
            url:="{controller}/{action}/{id}/{fileId}",
            defaults:=New With {.controller = "Home", .action = "Index", .id = UrlParameter.Optional, .fileId = UrlParameter.Optional},
         constraints:=New With {.isMVC = New NonSvcConstraint()})

        routes.Add(New ServiceRoute("DataService.svc", New ServiceHostFactory(), GetType(clsAppDataProviderDuplexSvc)))
        routes.Add(New ServiceRoute("DataServiceSimplex.svc", New ServiceHostFactory(), GetType(clsAppDataProviderSimplexSvc)))
        routes.Add(New ServiceRoute("BlobService.svc", New ServiceHostFactory(), GetType(clsCloudFileProviderSvc)))
        'routes.Add(New ServiceRoute("DBSyncService.svc", New ServiceHostFactory(), GetType(DatabaseSyncServiceSvc)))

    End Sub
End Module
'http://www.paraesthesia.com/archive/2011/07/21/running-static-files-through-virtualpathprovider-in-iis7.aspx/
Public Class MyServiceHostFactory
    Inherits ServiceHostFactory
    Protected Overrides Function CreateServiceHost(serviceType As Type, baseAddresses() As Uri) As ServiceHost
        Dim host As ServiceHost = New ServiceHost(serviceType, baseAddresses)
        'Dim bnd As NetTcpBinding = ChannelProxyFactory.CreateTCPBinding
        'host.AddServiceEndpoint(GetType(IAppDataProviderBase), bnd, "")
        host.AddDefaultEndpoints()
        Return host
    End Function
End Class
