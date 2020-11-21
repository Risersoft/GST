Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web.Http
Imports System.Web.Http.Dispatcher
Imports risersoft.shared.bot
Imports risersoft.shared.sync.Controllers
Imports risersoft.shared.web

Public Module WebApiConfig
    Public Sub Register(ByVal config As HttpConfiguration)

        ' Web API configuration and services
        config.ParameterBindingRules.Insert(0, AddressOf MultiPostParameterBinding.CreateBindingForMarkedParameters)

        config.Services.Replace(GetType(IHttpControllerSelector), New MyHttpControllerSelector(config))

        ' Web API routes
        config.MapHttpAttributeRoutes()

        config.Routes.MapHttpRoute(
         name:="ValuesApi",
    routeTemplate:="api/Values/{id}",
    defaults:=New With {.controller = "Values", .id = RouteParameter.Optional}
     )

        config.Routes.MapHttpRoute(
         name:="ActionApi",
    routeTemplate:="api/{controller}/{action}/{id}",
    defaults:=New With {.id = RouteParameter.Optional, .action = RouteParameter.Optional}
     )


        UnityConfig.RegisterComponents(config)
    End Sub

End Module
