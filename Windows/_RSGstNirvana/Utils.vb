Imports System.IO
Imports System.Net
Imports GSTN.API.Library.Models.GstNirvana
Imports Newtonsoft.Json
Imports risersoft.API.GSTN
Imports risersoft.app.config
Imports risersoft.app.dataporter
Imports risersoft.app.mxent
Imports risersoft.app.mxform
Imports risersoft.app.mxform.gst
Imports risersoft.app2.shared
Imports risersoft.shared
Imports risersoft.shared.cloud
Imports risersoft.shared.DotnetFx
Imports risersoft.shared.win

Public Class Utils
    Public Shared Sub Main(ByVal args() As String)
        myApp = New clsRSWinCloudApp(New clsExtendAppMxGst)
        myWinApp.CheckInitConsole(args)
        Dim fMain As frmMax = AppStarter.StartWinFormApp(args)

        'Dim Sql As String = "Select * from APITask where ApiTaskID = '00E6B13F-8163-41C5-81FF-2798E8C8C00D'"
        'Dim r1 As DataRow = SQLHelper.ExecuteDataset(CommandType.Text, Sql).Tables(0).Rows(0)
        'Dim Provider As New GSTR3BTaskProvider(myWinApp.Controller)
        'Dim oRet = Provider.ExecuteServer(r1, Nothing)
        'Dim str As String = oRet.Result.Message

        If Not fMain Is Nothing Then
            GSTNConstants.base_path = myWinApp.AppPath
            Try
                GSTNConstants.publicip = New WebClient().DownloadString("http://ipinfo.io/ip").Trim
            Catch ex As Exception
                GSTNConstants.publicip = "11.10.1.1"

            End Try
            'TestPayload()
            'TestExcel()
            'TestImport1()
            'TestMatch()
            'TestMatch2()
            'CreateJson()
            'TestTemplate()
            'TestImport1Map()
            'TestImportGSTR1()
            'TestImportCHL()
            'TestImport2A()
            'TestImportRole()
            'TestConvertJson()
            'TestPublicAPITaskProvider()
            'TestDeleteVouchImport()

            'Dim cont As DialogResult
            'Dim f3 As New frmHsnSac
            'If f3.PrepForm(fMain.myView, EnumfrmMode.acEditM, 6358) Then
            '    cont = f3.ShowDialog()
            'End If

            Application.Run(fMain)
            Trace.WriteLine("Exiting.")
            Application.Exit()
        End If
    End Sub
    Private Shared Sub TestExcel()
        Dim oProc As New clsGSTNReturnGSTR1(myApp.Controller)
        oProc.GenerateExcel(2, 1255, System.Guid.NewGuid.ToString & "-GSTR1.zip", "")
    End Sub

    Private Shared Sub Testfile(file As String)
        Dim cGuid As Guid, ParentFileID, _newFileName As String
        Dim ErrorSuffix As String = "_Error"
        Dim newGuid = Guid.NewGuid
        Dim OrigFileName = Path.GetFileNameWithoutExtension(file)
        If myUtils.EndsWith(OrigFileName, ErrorSuffix) Then
            Dim flName As String() = Left(OrigFileName, OrigFileName.Length - ErrorSuffix.Length).Split(New String() {"--"}, StringSplitOptions.RemoveEmptyEntries)
            If Guid.TryParse(flName.Last, cGuid) Then
                ParentFileID = cGuid.ToString
                _newFileName = Path.GetFileNameWithoutExtension(flName.First) + "--" + newGuid.ToString + ErrorSuffix + Path.GetExtension(file)
            End If
        End If

    End Sub
    Private Shared Sub TestStatus()
        Dim sql As String = "select * from gstntransaction where gstntransactionid='2AD1AC52-B30A-4E61-9B58-0C9F3E5FBB46'"
        Dim dt1 As DataTable = myApp.Controller.DataProvider.objSQLHelper.ExecuteDataset(CommandType.Text, sql).Tables(0)
        Dim str1 As String = dt1.Rows(0)("statusinfo")
        Dim str2 As String = dt1.Rows(0)("requestpayload")
    End Sub
    Private Shared Sub CreateJson()
        Dim importer As New Import_PVTaskProvider(myApp.Controller)
        Dim oSchema = importer.GenerateSchema
        Dim conn As New clsSCMSExcel(myApp.Controller)
        'conn.FileNameRelative = "E:\Onedrive\Documents\Rsoft\Projects\KPMG\GSTR-1 Sample data.xlsx"
        conn.FileNameRelative = "E:\GST\Advance Paid_Refund Template V1.0 (2).xlsx"
        Dim ds = conn.ReadDataSet(oSchema, False)
        Dim converter As New clsPOCOConverter(myApp.Controller)
        converter.DateValue = AddressOf importer.CalculateDate
        Dim lst = converter.GenerateObjectList(Of GstPaymentVendorInfo)(ds.Tables(0), "", "", "isnull(gstin,'')<>''")
        Dim str1 As String = JsonConvert.SerializeObject(lst, New JsonSerializerSettings With {.NullValueHandling = NullValueHandling.Ignore, .DefaultValueHandling = DefaultValueHandling.Ignore})

    End Sub
    Private Shared Sub TestImport1()
        Dim importer As New Import_ISTaskProvider(myApp.Controller)
        importer.ExecuteImport("E:\Upload-10LAC Invoice.xlsx", "info@risersoft.com", Guid.Empty.ToString)
    End Sub
    Private Shared Sub TestImport1Map()
        Dim importer As New Import_IPTaskProvider(myApp.Controller)
        importer.ExecuteImport("E:\GST\Data mapping file Inward_Map_ 07042019.xlsx", "info@risersoft.com", Guid.Empty.ToString)
        'importer.ExecuteImport("E:\GST\Data mapping file 07022019_MAPA_(1).xlsx", "info@risersoft.com", Guid.Empty.ToString)
    End Sub

    Private Shared Sub TestImport2A()
        Dim importer As New Import_GSTR2ATaskProvider(myApp.Controller)
        importer.ExecuteImport("E:\GST\json1.json", "info@risersoft.com", Guid.Empty.ToString)
    End Sub

    Private Shared Sub TestImportGSTR1()
        Dim importer As New Import_GSTR1TaskProvider(myApp.Controller)
        importer.ExecuteImport("E:\GST\json1.json", "info@risersoft.com", Guid.Empty.ToString)
    End Sub

    Private Shared Sub TestImportRole()
        Dim importer As New Import_RoleTaskProvider(myApp.Controller)
        importer.ExecuteImport("E:\GST\RoleManagement (2) - Copy.xlsx", "info@risersoft.com", Guid.Empty.ToString)
    End Sub



    Private Shared Sub TestConvertJson()
        Dim importer As New ConvertJsonTaskProvider(myApp.Controller)
        importer.ExecuteConvert("E:\GST\b2b.json", "b2b.xlsx", "gstr2", Guid.Empty.ToString)
    End Sub

    Private Shared Sub TestPublicAPITaskProvider()
        Dim importer As New PublicAPITaskProvider(myApp.Controller)
        importer.ExecuteSearch()
    End Sub

    Private Shared Sub TestMatch()
        Dim oProc As New clsInvoicePurchRecon(myApp.Controller)
        oProc.ReconcileCP(77, 1257, 1268, False, False)
    End Sub

    Private Shared Async Sub TestMatch2()
        Dim GstRegID = 52, ReturnPeriodID = 1257
        Dim oProc As New clsInvoicePurchRecon(myApp.Controller)
        Await oProc.ReconcileCP(GstRegID, ReturnPeriodID, ReturnPeriodID, True, True)

        Dim oProc2 As New clsGSTNReturnGSTR2(myApp.Controller)
        Dim vw = Await oProc2.PrepareViewPAN("listpurchinvoicematch", GstRegID, ReturnPeriodID, ReturnPeriodID)
        Dim provider As New XLTaskProvider(myApp.Controller)
        Dim filename As String = myApp.Controller.CalcRLSId.ToString & "-GSTR2-Reconcile-" & Replace(Now.ToLongTimeString, ":", "").Replace(" ", "") & ".xlsx"
        Await provider.ExecuteExportAndMail(vw, filename, "info@risersoft.com",
                               "Reconciliation", "Your invoice reconciliation task has finished and report is available at {0}")

    End Sub
    Private Shared Sub TestPayload()
        Dim oProc As New clsGSTNReturnGSTR1(myApp.Controller)
        oProc.GeneratePayload(2, 1255, "UM", "E:\test.zip")
    End Sub

    Private Shared Sub UpdateTransactions()
        Dim sql As String = "select * from gstntransaction where returnkey is null"
        Dim dt1 As DataTable = SQLHelper.ExecuteDataset(CommandType.Text, sql).Tables(0)
        For Each r1 As DataRow In dt1.Select
            If myUtils.EndsWith(r1("transtype"), "GSTR1", "GSTR2") Then
                Dim arr() As String = Split(r1("transtype"), "-")
                r1("returnkey") = arr(1)
                r1("transtype") = "RETSAVE"
                r1("transsubtype") = arr(0)
            End If
        Next
        SQLHelper.SaveResults(dt1, sql)
    End Sub
    Private Shared Sub SetFiledStatus()

        Dim oMaster As New clsMasterDataFICO(myWinApp.Controller)

        Dim GstRegIDCSV = myUtils.MakeCSV(oMaster.GstRegTable.Select, "Gstregid")

        For Each rGstReg As DataRow In oMaster.GstRegTable.Select
            Dim IsPending As Boolean = False
            Dim dtGSTPP As DataTable = Nothing
            For Each rPP As DataRow In oMaster.PostPeriodTable.Select("", "sdate")

                Dim nr As DataRow = oMaster.GstRegPPRow(rGstReg("GstRegID"), rPP("PostPeriodID"))
                dtGSTPP = nr.Table
                If rPP("sdate") < myUtils.cDateTN(rGstReg("regdate"), myFuncsBase.GstSTartDate) Then
                    nr.Delete()
                Else
                    If IsPending Then
                        nr("ispending") = False
                    Else
                        IsPending = (Not myUtils.IsInList(myUtils.cStrTN(nr("GSTR3")), "F")) AndAlso (rPP("sdate") >= myFuncsBase.GstSTartDate)
                        nr("ispending") = IsPending
                    End If
                    Debug.WriteLine(myUtils.RowValuesText(nr))
                End If
            Next
            myUtils.DeleteRows(dtGSTPP.Select("gstregid not in (" & GstRegIDCSV & ")"))
            myWinApp.Controller.DataProvider.objSQLHelper.SaveResults(dtGSTPP, "select * from gstregpp where 0=1")

        Next
    End Sub
End Class
