using eSignASPLib;
using eSignASPLib.DTO;
using Risersoft.API.GSTN;
using Newtonsoft.Json;
using Org.BouncyCastle.Bcpg.OpenPgp.Examples;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using GSTN.API.Library.Models.EWB;
using GSTN.API.Library.Clients;
using risersoft.shared;
using Serilog;
using risersoft.shared.agent;
using risersoft.shared.dotnetfx;
using risersoft.shared.cloud;

namespace Risersoft.API.GSTN.Console
{
    class Program
    {
        static eSign eSignObj;
        static void Main(string[] args)
        {

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File("logs\\myapp.txt",rollingInterval: RollingInterval.Day,rollOnFileSizeLimit:true)
                .CreateLogger();

            Log.Information("Hello, world!");
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)48 | (SecurityProtocolType)192 | (SecurityProtocolType)768 | (SecurityProtocolType)3072;

            try
            {
                GSTNConstants.publicip = new WebClient().DownloadString("http://ipinfo.io/ip").Trim();
            }
            catch
            {
                GSTNConstants.publicip = "11.10.1.1";
            }
            //RunConsole(args);
            RunKPMGEWBTest(args);
            ////TestGSTR1Error();
            //RunSave(args);
            //SearchGSTIN("09AAACK7126N1Z6","KPMG","PROD");
            //TrackReturns("09AAACK7126N1Z6", "2018-19","KPMG", "PROD");
            //SearchPAN("GSPTN0191G", "GSTN", "TEST");
            System.Console.Write("Press any key to end this program ...");
            System.Console.ReadLine();

        }

        private static void RunKPMGTest(string[] args)
        {
            var gstin = "33TBBTN5892A1Z5";
            var userid = "TN_NT_TP46";
            var fp = "102018";
            var auth = GetKpmgAuthTP(gstin, userid, "TEST");
            TestGSTR1Get(auth, gstin, userid, fp);
            TestGSTR1Save(auth, gstin, userid, fp, "33GSPTN0191G1ZB", "27GSPUP0191G1CO");
            var result = auth.Logout();
        }
        private static void RunKPMGEWBProd(string[] args)
        {
            TestEWBKPMG("09AAFCG9185Q1ZK", "09AAFCG918_API_123", "09AAFCG9185Q1ZK", "PROD");


        }

        private static void RunKPMGEWBTest(string[] args)
        {
            TestEWBKPMG("05AAACD5191L1ZK", "05AAACD5191L1ZK", "abc123@@", "TEST");

            

        }
        private static void RunKPMGProd(string[] args)
        {
            var gstin = "27AAGCD1268C1ZX";
            var userid = "dysonindia_mh";
            var fp = "022019";
            var auth = GetKpmgAuthTP(gstin, userid, "PROD");
            TestGSTR1Get(auth, gstin, userid, fp);
            //TestGSTR1Save(auth, gstin, userid, fp, "33GSPTN0191G1ZB", "27GSPUP0191G1CO");
            var result = auth.Logout();
        }

        private static void RunSave(string[] args)
        {
            var gstin = "33GSPTN0192G1ZA";
            var userid = "risersoft.tn.2";
            var fp = "122018";
            var gsp = "GSTN";
            IGSTNAuthProvider auth9 = GetAuth(gstin, gsp, userid);
            TestGSTR1Save2(auth9, gstin, userid, fp, "", "");
            auth9.Logout();
        }

        private static void RunConsole(string[] args)
        {
            string gstin = "", gsp = "", userid = "", fp = "", filename = "", ctin = "", etin = "";

            if ((args != null) && (args.Count() > 0) && (System.IO.File.Exists(args[0])))
            {
                filename = args[0];
            }
            else
            {
                System.Console.Write("Enter input filename [Press Enter for None]:");
                filename = System.Console.ReadLine();
            }


            if (!String.IsNullOrEmpty(filename) && (System.IO.File.Exists(filename)))
            {
                var fileContents = File.ReadAllText(filename);
                string[] arr = Microsoft.VisualBasic.Strings.Split(fileContents, Microsoft.VisualBasic.Constants.vbCrLf);
                gsp = arr[0];
                userid = arr[1];
                gstin = arr[2];
                fp = arr[3];
                ctin = arr[4];
                etin = arr[5];
            }
            else
            {
                System.Console.Write("Enter GSP:");
                gsp = System.Console.ReadLine();

                System.Console.Write("Enter UserID:");
                userid = System.Console.ReadLine();

                System.Console.Write("Enter GSTIN:");
                gstin = System.Console.ReadLine();

                System.Console.Write("Enter FP:");
                fp = System.Console.ReadLine();

                System.Console.Write("Enter CTIN:");
                ctin = System.Console.ReadLine();

                System.Console.Write("Enter ETIN:");
                etin = System.Console.ReadLine();

            }



            System.Console.WriteLine("1=GSTR1 Get, 2=GSTR2 Get, 3=GSTR3 Get, 4=Ledger, " +
                "5=File with eSign, 6=CSV conversion, 7=PGP, 8=File With DSC, " +
                "9=GSTR1 Save, 10=GSTR2 Save, 11=GSTR3 Save, 12=Refresh Token, 13=Register DSC, " +
                "14=Search,  15= EWB, 16=TOKEN");
            string selection = System.Console.ReadLine();

            switch (selection)
            {
                case "1":
                    IGSTNAuthProvider auth1 = GetAuth(gstin, gsp, userid);
                    TestGSTR1Get(auth1, gstin, userid, fp);
                    auth1.Logout();
                    break;
                case "2":
                    IGSTNAuthProvider auth2 = GetAuth(gstin, gsp, userid);
                    TestGSTR2Get(auth2, gstin, userid, fp);
                    auth2.Logout();
                    break;
                case "3":
                    TestGSTR3(gstin, gsp, userid, fp);
                    break;
                case "4":
                    TestLedger(gstin, gsp, userid,fp, "19-08-2016", "20-09-2016");
                    break;
                case "5":
                    System.Console.Write("Enter path to license file:");
                    string path = System.Console.ReadLine();
                    System.Console.Write("Enter your Aadhar Num:");
                    string aadhaarnum = System.Console.ReadLine();
                    string transactionid = GetUIDAIOtp(path, aadhaarnum);
                    System.Console.Write("Enter OTP:");
                    string otp = System.Console.ReadLine();
                    FileGSTR1WithESign(gstin, gsp, userid, fp, aadhaarnum, transactionid, otp);
                    break;
                case "6":
                    TestCSV(gstin, gsp, userid, fp);
                    break;
                case "7":
                    TestPGP("the quick brown fox jumped over the lazy dog");
                    break;
                case "8":
                    System.Console.Write("Enter your PAN:");
                    string pan = System.Console.ReadLine();
                    FileGSTR1WithDSC(gstin, gsp, userid, fp, pan);
                    break;
                case "9":
                    IGSTNAuthProvider auth9 = GetAuth(gstin, gsp, userid);
                    TestGSTR1Save(auth9, gstin, userid, fp, ctin, etin);
                    auth9.Logout();
                    break;
                case "10":
                    TestGSTR2Save(gstin, gsp, userid, fp, ctin);
                    break;
                case "12":
                    IGSTNAuthProvider auth12 = GetAuth(gstin, gsp, userid);
                    auth12.RefreshToken();
                    break;
                case "13":
                    System.Console.Write("Enter your PAN:");
                    string pan2 = System.Console.ReadLine();
                    RegisterDSC(gstin, gsp, userid, pan2);
                    break;
                case "14":

                    SearchGSTIN(gstin, gsp,"PROD");
                    break;
                case "15":
                    TestEWBAspOne(gstin, userid);
                    break;
                case "16":
                    TestEWBTokenKpmg(gstin, userid);
                    break;
            }

            System.Console.WriteLine("Press any key to end this program");
            System.Console.ReadKey(false);

        }
        private static void TestPGP(string message)
        {
            string pwd = "mypassword";
            System.IO.Directory.CreateDirectory(@"D:\Keys");
            PGPSnippet.KeyGeneration.PGPKeyGenerator.GenerateKey("GSTNUser", pwd, @"D:\Keys\");
            System.Console.WriteLine("Keys Generated Successfully");

            String encoded = DetachedSignatureProcessor.CreateSignature(message, @"D:\Keys\PGPPrivateKey.asc", "signature.asc", pwd.ToCharArray(), true);
            System.Console.WriteLine("Obtained Signature = " + encoded);
            bool verified = DetachedSignatureProcessor.VerifySignature(message, encoded, @"D:\Keys\PGPPublicKey.asc");
            if (verified)
            {
                System.Console.WriteLine("signature verified.");
            }
            else
            {
                System.Console.WriteLine("signature verification failed.");
            }


        }
        private static string GetUIDAIOtp(string path, string aadhaarnum)
        {
            eSignObj = new eSign(path);    //Get your own license file from e-Mudhra
            Settings.PfxPath = "resources\\Docsigntest.pfx";
            Settings.PfxPassword = "emudhra";
            Settings.UIDAICertificatePath = "resources\\uidai_auth_prod.cer";
            Settings.AuthMode = AuthMode.OTP;
            string guid = System.Guid.NewGuid().ToString();
            Response OTPResponse = eSignObj.GetOTP(aadhaarnum, guid);
            return guid;
        }
        private static void FileGSTR1WithESign(string gstin, string gsp, string userid, string fp, string aadhaarnum, string transactionId, string Otp)
        {
            IGSTNAuthProvider client = GetAuth(gstin, gsp, userid);
            GSTR1ApiClient client2 = new GSTR1ApiClient(client, gstin, userid, fp);
            var model2 = client2.GetSummary().Data;

            //https://groups.google.com/forum/#!searchin/gst-suvidha-provider-gsp-discussion-group/authorized|sort:relevance/gst-suvidha-provider-gsp-discussion-group/9-_Mk7LatDs/eQ6_1kHTBAAJ
            //https://groups.google.com/forum/#!searchin/gst-suvidha-provider-gsp-discussion-group/authorized|sort:relevance/gst-suvidha-provider-gsp-discussion-group/acd-F7XPYz4/7z83KM4IBgAJ

            var Base64Payload = Convert.ToBase64String(Encoding.UTF8.GetBytes(client2.dicParams["ResponsePayload"]));
            var Base64Hash = EncryptionUtils.convertByteArrayToString(EncryptionUtils.sha256_hash(Base64Payload));

            AuthMetaDetails MetaDetails = new AuthMetaDetails();
            MetaDetails.fdc = "NA";
            MetaDetails.udc = "NA";//Unique device code. 
            MetaDetails.pip = "NA";
            MetaDetails.lot = "P";
            MetaDetails.lov = "560103";
            MetaDetails.idc = "NA";

            var json4 = eSignObj.SignText(aadhaarnum, Otp, transactionId, Base64Hash, MetaDetails);
            var result4 = client2.File(model2, json4.SignedText, "Esign", aadhaarnum);

        }
        private static void FileGSTR1WithDSC(string gstin, string gsp, string userid, string fp, string pan)
        {
            IGSTNAuthProvider client = GetAuth(gstin, gsp, userid);
            GSTR1ApiClient client2 = new GSTR1ApiClient(client, gstin, userid, fp);
            var model2 = client2.GetSummary().Data;

            var base64PayLoad = Convert.ToBase64String(Encoding.UTF8.GetBytes(client2.dicParams["ResponsePayload"]));
            var PayLoadHash = Encoding.UTF8.GetBytes(EncryptionUtils.convertByteArrayToString(EncryptionUtils.sha256_hash(base64PayLoad)));

            var cert = DSCUtils.getCertificate();

            var json4 = Convert.ToBase64String(DSCUtils.SignCms(PayLoadHash, cert));
            var result4 = client2.File(model2, json4, "DSC", pan);

        }

        private static GSTNPublicApiClient GetPublicClient(string gsp, string env)
        {
            IGSTNAuthProvider provider;
            switch (gsp.Trim().ToLower())
            {
                case "aspone":
                    provider = new AspOnePublicAuthClient(env);
                    break;
                case "kpmg":
                    provider = new KpmgPublicAuthClient(env);
                    break;
                default:
                    provider = new GSTNPublicAuthClient(gsp, env);
                    break;
            }
            provider.Init();
            var result2 = provider.RequestToken("");
            GSTNPublicApiClient client2;
            switch (gsp.Trim().ToLower())
            {
                case "aspone":
                    client2 = new AspOnePublicApiClient(provider);
                    break;
                default:
                    client2 = new GSTNPublicApiClient(provider);
                    break;
            }
            return client2;
        }
        private static void SearchGSTIN(string gstin, string gsp, string env)
        {
            var client2 = GetPublicClient(gsp, env);
            var output = client2.SearchGSTIN(gstin);

        }

        private static void TrackReturns(string gstin, string fy, string gsp, string env)
        {
            var client2 = GetPublicClient(gsp, env);
            var output = client2.TrackReturns(gstin,fy);

        }

        private static void SearchPAN(string pan, string gsp, string env)
        {
            var client2 = GetPublicClient(gsp, env);
            var output = client2.SearchPAN(pan);

        }

        private static void RegisterDSC(string gstin, string gsp, string userid, string pan)
        {
            IGSTNAuthProvider client = GetAuth(gstin, gsp, userid);
            GSTNDSClient client2 = new GSTNDSClient(client, gstin);

            var cert = DSCUtils.getCertificate();
            byte[] data = Encoding.UTF8.GetBytes(pan);
            var sign = Convert.ToBase64String(DSCUtils.SignCms(data, cert));
            var result = client2.RegisterDSC(pan, sign);
        }
        private static IGSTNAuthProvider GetAuth(string gstin, string gsp, string userid)
        {

            GSTNAuthClient client = new GSTNAuthClient(gstin, userid, gsp, "TEST", GSTNConstants.publicip);
            var result = client.RequestOTP();

            System.Console.Write("Enter OTP:");
            string otp = System.Console.ReadLine();

            var result2 = client.RequestToken(otp);
            return client;
        }

        private static IGSTNAuthProvider GetKpmgAuthTP(string gstin, string userid,string env)
        {

            KpmgGspAuthClient client = new KpmgGspAuthClient(gstin, userid, env, GSTNConstants.publicip);
            client.Init();

            var result = client.RequestOTP();
            System.Console.Write("Enter OTP:");
            string otp = System.Console.ReadLine();

            var result2 = client.RequestToken(otp);
            return client;
        }
        private static IGSTNAuthProvider GetKpmgAuthEWB(string gstin, string userid, string password, string env)
        {

            KpmgEWBAuthClient client = new KpmgEWBAuthClient(gstin, userid, password, env);
            var result2 = client.RequestToken("");
            return client;
        }

        private static void TestGSTR1Get(IGSTNAuthProvider client, string gstin, string userid, string fp)
        {

            GSTR1.GSTR1Total model = new GSTR1.GSTR1Total();
            GSTR1ApiClient client2 = new GSTR1ApiClient(client, gstin, userid, fp);
            model = client2.GetSection("B2B", "", "", "").Data;

        }
        private static void TestGSTR2Get(IGSTNAuthProvider auth, string gstin, string userid, string fp)
        {
            GSTR2.GSTR2Total model = new GSTR2.GSTR2Total();
            GSTR2ApiClient client2 = new GSTR2ApiClient(auth, gstin, userid, fp);
            System.Console.Write("Action Required? Y/N/Enter");
            string action = System.Console.ReadLine();
            model = client2.GetSection("B2B", "", action, "").Data;
            var model2 = client2.GetSummary().Data;
        }
        private static void TestGSTR3(string gstin, string gsp, string userid, string fp)
        {
            IGSTNAuthProvider client = GetAuth(gstin, gsp, userid);
            GSTR3.GSTR3Total model = new GSTR3.GSTR3Total();
            GSTR3ApiClient client2 = new GSTR3ApiClient(client, gstin, userid, fp);
            var info = client2.Generate(fp).Data;
            model = client2.GetDetails(fp).Data;
        }
        private static void TestLedger(string gstin, string gsp, string userid, string fp, string fr_dt, string to_dt)
        {
            IGSTNAuthProvider client = GetAuth(gstin, gsp, userid);
            LedgerApiClient client2 = new LedgerApiClient(client, gstin, userid,fp);
            var info = client2.GetCashDtl(fr_dt, to_dt).Data;
        }
        private static string TestCSV(string gstin, string gsp, string userid, string fp)
        {
            IGSTNAuthProvider client = GetAuth(gstin, gsp, userid);
            GSTR1.GSTR1Total model = new GSTR1.GSTR1Total();
            GSTR1ApiClient client2 = new GSTR1ApiClient(client, gstin, userid, fp);
            model = client2.GetSection("B2B", "", "Y", "").Data;

            var client3 = new MxApiClient("http://www.maximprise.com/api/gst");
            string str1 = client3.Json2CSV(client2.dicParams["ResponsePayload"], "gstr1", "b2b").Data;
            return str1;
        }
        private static void TestGSTR1Error()
        {
            var filename = "sampledatatp\\gstr1-error.json";
           GlobalCore.myCoreApp = new clsConsoleApp(new clsExtendAgentAppGst());
            var str1 = File.ReadAllText(filename);
            var model = JsonConvert.DeserializeObject<StatusInfo<GSTR1.GSTR1Total>>(str1);

            clsPOCOConverter oProc = new clsPOCOConverter(Globals.myApp.Controller);
            var dt1 = oProc.GenerateTable(model.error_report.b2b);
        }
        private static void TestGSTR1Save2(IGSTNAuthProvider auth, string gstin, string userid, string fp, string ctin, string etin)
        {

            GSTR1.GSTR1Total model = new GSTR1.GSTR1Total();
            model.gstin = gstin;
            model.fp = fp;
            model.b2cs = new List<GSTR1.B2csOutward>
            {
                new GSTR1.B2csOutward
                {
                     rt=5,
                      iamt=100
                }
            };
            GSTR1ApiClient client2 = new GSTR1ApiClient(auth, gstin, userid, fp);
            var str2 = client2.Serialize(model);
            var info = client2.Save(model);
            System.Console.WriteLine("Reference_ID: " + info.Data.reference_id);
            var status = client2.GetStatus(info.Data.reference_id);
            System.Console.WriteLine(JsonConvert.SerializeObject(status.Data));



        }
        private static void TestGSTR1Save(IGSTNAuthProvider auth, string gstin, string userid, string fp, string ctin, string etin)
        {
            var filename = "sampledatatp\\gstr1.json";
            if (String.IsNullOrEmpty(ctin))
            {
                System.Console.Write("Enter CTIN:");
                ctin = System.Console.ReadLine();
            }
            if (String.IsNullOrEmpty(etin))
            {
                System.Console.Write("Enter ETIN:");
                etin = System.Console.ReadLine();
            }

            var str1 = File.ReadAllText(filename).Replace("%ctin%", ctin).Replace("%etin%", etin);
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };
            GSTR1.GSTR1Total model = JsonConvert.DeserializeObject<GSTR1.GSTR1Total>(str1, settings);
            model.gstin = gstin;
            model.fp = fp;
            GSTR1ApiClient client2 = new GSTR1ApiClient(auth, gstin, userid, fp);
            var str2 = client2.Serialize(model);
            var info = client2.Save(model);
            System.Console.WriteLine("Reference_ID: " + info.Data.reference_id);
            var status = client2.GetStatus(info.Data.reference_id);
            System.Console.WriteLine(JsonConvert.SerializeObject(status.Data));



        }
        private static void TestGSTR2Save(string gstin, string gsp, string userid, string fp, string ctin)
        {
            IGSTNAuthProvider auth = GetAuth(gstin, gsp, userid);
            var filename = "sampledatatp\\b2bin.json";
            if (String.IsNullOrEmpty(ctin))
            {
                System.Console.Write("Enter CTIN:");
                ctin = System.Console.ReadLine();
            }

            var str1 = File.ReadAllText(filename).Replace("%ctin%", ctin);
            GSTR2.GSTR2Total model = JsonConvert.DeserializeObject<GSTR2.GSTR2Total>(str1);
            model.gstin = gstin;
            model.fp = fp;
            GSTR2ApiClient client2 = new GSTR2ApiClient(auth, gstin, userid, fp);
            var info = client2.Save(model);
            System.Console.WriteLine("Reference_ID: " + info.Data.reference_id);
            var status = client2.GetStatus(info.Data.reference_id);
            System.Console.WriteLine(JsonConvert.SerializeObject(status.Data));


        }

        private static void TestEWBAspOne(string gstin,  string userid)
        {
            //abc123@@
            System.Console.Write("Enter Password:");
            string password = System.Console.ReadLine();

            AspOneEWBAuthClient auth = new AspOneEWBAuthClient(gstin, userid, password, "TEST");
            AspOneEWBApiClient client2 = new AspOneEWBApiClient(auth, gstin);
            var authoutput = auth.RequestToken("");
            TestEWB(gstin, userid, client2);
        }

        private static void TestEWBKPMG(string gstin, string userid, string password, string env)
        {

            KpmgEWBAuthClient auth = new KpmgEWBAuthClient(gstin, userid, password, env);
            if (auth.Init())
            {
                KpmgEWBApiClient client2 = new KpmgEWBApiClient(auth, gstin);
                var authoutput = auth.RequestToken("");
                TestEWB(gstin, userid, client2);
                var result = auth.Logout();
            }
        }

        private static void TestEWB(string gstin, string userid, EWBApiClientBase client2)
        {

            System.Console.WriteLine("1=Generate, 2=Get, 3=Update, 4=Cancel");
            string selection = System.Console.ReadLine();

            switch (selection)
            {
                case "1":

                    var filename2 = "sampledataewb\\ewb.json";
                    var str1 = File.ReadAllText(filename2);
                    GenerateEWBInfo model = JsonConvert.DeserializeObject<GenerateEWBInfo>(str1);
                    var str2 = JsonConvert.SerializeObject(model);
                    var info = client2.Generate(model);

                    System.Console.WriteLine(JsonConvert.SerializeObject(info));
                    break;
                case "2":
                    System.Console.WriteLine("Enter EWB No.");
                    string ewbnum = System.Console.ReadLine();
                    var info2 = client2.GetDetails((long)myUtils.cValTN(ewbnum));
                    System.Console.WriteLine(JsonConvert.SerializeObject(info2));
                    break;
                case "3":

                    var filename3 = "sampledataewb\\UpdVeh.json";
                    var str31 = File.ReadAllText(filename3);
                    EWBUpdVehRequestInfo model3 = JsonConvert.DeserializeObject<EWBUpdVehRequestInfo>(str31);
                    var str32 = JsonConvert.SerializeObject(model3);
                    var info3 = client2.UpdateVehicle(model3);

                    System.Console.WriteLine(JsonConvert.SerializeObject(info3));
                    break;
                case "4":

                    var filename4 = "sampledataewb\\CancelEWB.json";
                    var str41 = File.ReadAllText(filename4);
                    EWBCancelRequestInfo model4 = JsonConvert.DeserializeObject<EWBCancelRequestInfo>(str41);
                    var str42 = JsonConvert.SerializeObject(model4);
                    var info4 = client2.Cancel(model4);

                    System.Console.WriteLine(JsonConvert.SerializeObject(info4));
                    break;

            }


        }
        private static void TestEWBTokenKpmg(string gstin, string userid)
        {

            System.Console.Write("Enter Password:");
            string password = System.Console.ReadLine();

            KpmgEWBAuthClient auth = new KpmgEWBAuthClient(gstin, userid, password, "PROD");
            var authoutput = auth.RequestToken("");


        }


    }
}
