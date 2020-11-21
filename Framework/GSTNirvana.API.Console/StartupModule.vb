Imports System.IO
Imports System.Net
Imports GSTN.API.Library.Models.EWB
Imports GSTN.API.Library.Models.GstNirvana
Imports Newtonsoft.Json
Imports risersoft.API.GSTN
Imports risersoft.API.GSTN.Public
Imports risersoft.app.mxform.gst
Imports risersoft.shared
Imports risersoft.shared.DotnetFx
Imports risersoft.shared.agent

Module StartupModule

    Sub Main(ByVal args() As String)
        Try
            risersoft.API.GSTN.GSTNConstants.publicip = GlobalCore.GetConfigSetting("publicip") 'New System.Net.WebClient().DownloadString("http://ipinfo.io/ip").Trim
        Catch ex As Exception
            risersoft.API.GSTN.GSTNConstants.publicip = "11.10.1.1"
        End Try

        System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Ssl3 Or System.Net.SecurityProtocolType.Tls Or System.Net.SecurityProtocolType.Tls11 Or System.Net.SecurityProtocolType.Tls12

        GlobalCore.myCoreApp = New clsConsoleApp(New clsExtendAppMxGst)
        Dim oRet = GlobalCore.myCoreApp.Init(args).Result

        'TestImportIS()

        System.Console.WriteLine("Press any key to start ...")
        Dim startkey As [String] = System.Console.ReadLine()

        Dim server As String = "http://dev.mylocalhost.in:56492"
        'Dim server As String = "http://www.gstnirvana.com"
        'Dim server As String = "http://gst.kgcpplustest.in.kpmg.com"


        Dim provider As New clsClientCredTokenProvider("3U#R62K9J7SR", "KXAFHS", "http://dev.mylocalhost.in:11626", True)
        'Dim provider As New clsClientCredTokenProvider("CDFF6XCDFF6X", "CDFF6X", "http://login.risersoft.com", True)
        'Dim provider As New clsClientCredTokenProvider("25DNU#V3PY56", "XCUXDH", "http://login.risersoft.com", True)
        'Dim provider As New clsClientCredTokenProvider("3U#R62K9J7SR", "KXAFHS", "http://login.kgcpplustest.in.kpmg.com", True)

        Dim token = provider.AuthenticateAsync().Result
        Dim client As New WebApiClientToken(provider.ClientId, provider.TokenResponse.AccessToken)

        System.Console.WriteLine("1=Post Invoice, 2=Search GSTIN, 3=Convert, 4=EWB, 6=Save Invoice")
        Dim selection As [String] = System.Console.ReadLine()

        Select Case selection
            Case "1"
                Dim invoice As New GstInvoiceInfo()
                invoice.INUM = "Test1"
                invoice.IDT = DateTime.Now.[Date]
                invoice.CTIN = "33GSPXXXXXX1ZA"
                invoice.GSTInvoiceType = "b2b"
                invoice.Sply_Ty = "inter"
                invoice.CampusID = 1
                invoice.inv_typ = "R"
                invoice.DocType = "IS"
                invoice.InvoiceItems = New List(Of GstInvoiceItemInfo)()
                invoice.InvoiceItems.Add(New GstInvoiceItemInfo() With {
                           .Description = "Cotton",
                           .BasicRate = 1000,
                           .Hsn_Sc = "1000982",
                           .Qty = 10,
                           .Uqc = "Kg",
                           .TXVAL = 10000,
                           .RT = 5,
                           .IAMT = 500,
                           .TY = "G"
                        })

                client.PrepareQueryString(server & "/api/data/invoice", New Dictionary(Of String, String)())
                Dim _info = client.Post(Of GstInvoiceInfo, ResultInfo(Of Integer, HttpStatusCode))(invoice)
            Case "2"
                Dim params2 As New Dictionary(Of String, String)()
                params2.Add("GSTIN", "09AAACI1195H1ZK")
                client.PrepareQueryString(server & "/api/data/gstreginfo", params2)
                Dim _info2 = client.[Get](Of ResultInfo(Of TaxPayerModel, HttpStatusCode))()
                Dim str2 As String = JsonConvert.SerializeObject(_info2)
                System.Console.WriteLine(str2)
            Case "3"
                Dim json = System.IO.File.ReadAllText("payload/b2bin.json")
                Dim client3 = New MxApiClient(server & "/api/convert")
                Dim str3 As String = client3.Json2CSV(json, "gstr1", "b2b").Data
            Case "4"
                Dim json = System.IO.File.ReadAllText("payload/ewb.json")
                Dim info = JsonConvert.DeserializeObject(Of GenerateEWBInfo)(json)
                client.PrepareQueryString(server & "/api/ewb/post", New Dictionary(Of String, String)())
                Dim _info = client.Post(Of GenerateEWBInfo, ResultInfo(Of Integer, HttpStatusCode))(info)
                System.Console.WriteLine("Done")
            Case "5"
                Dim params2 As New Dictionary(Of String, String)()
                params2.Add("ewbnum", "671074313370")
                client.PrepareQueryString(server & "/api/ewb/pdf", params2)
                Dim _info2 = client.[Get](Of Stream)()
                Dim str2 As String = JsonConvert.SerializeObject(_info2)
                System.Console.WriteLine(str2)
            Case "6"
                Dim json = System.IO.File.ReadAllText("payload/payload1.json")
                Dim invoice = JsonConvert.DeserializeObject(Of GstSaleInvoiceInfo)(json)
                client.PrepareQueryString(server & "/api/Data/GSTR1/Save", New Dictionary(Of String, String)())
                Dim _info = client.Post(Of GstSaleInvoiceInfo, ResultInfo(Of clsProcOutput, HttpStatusCode))(invoice)
                Trace.WriteLine(_info.Data.Message)
            Case "7"
                client.PrepareQueryString(server & "/api/ewb/clear", New Dictionary(Of String, String)() From {{"GSTIN", "xxxx"}})
                Dim _info = client.Get(Of ResultInfo(Of Boolean, HttpStatusCode))
                System.Console.WriteLine("Done")

        End Select

        System.Console.WriteLine("Press any key to end this program")
        System.Console.ReadKey(False)
    End Sub
    Private Sub TestImportIS()
        Dim importer As New Import_ISTaskProvider(myApp.Controller)
        importer.ExecuteImport("E:\GSTR-1 Sample data 100 rows new.xls", "info@risersoft.com", Guid.NewGuid.ToString)
    End Sub
End Module
