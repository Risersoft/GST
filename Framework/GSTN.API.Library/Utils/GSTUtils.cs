using QRCoder;
using risersoft.shared.portable.Contracts;
using Risersoft.API.GSTN.Auth;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Risersoft.API.GSTN
{
    public class GSTUtils
    {

        public static bool ValidateGSTIN(string gstin)
        {
            Regex rPan = new Regex("^[0-9]{2}[A-Z]{5}[0-9]{4}[A-Z]{1}[1-9A-Z]{1}[A-Z][0-9A-Z]{1}$");
            return rPan.IsMatch(gstin);
        }
        public static bool ValidateUIN(string gstin)
        {
            Regex rPan = new Regex("^[0-9]{4}[A-Z]{3}[0-9]{5}[A-Z]{2}[0-9A-Z]{1}$");
            return rPan.IsMatch(gstin);
        }
        public static Image GenerateQR(string str1)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(str1, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);
            return qrCodeImage;
        }



    }
    public class GSPCredentials
    {

        public string code;
        public string API;
        public string Env;
        public string base_url;
        public string base_url_file;
        public string cert_file_http;
        public string cert_file_rsa;
        public Dictionary<string, string> versions;
        public Dictionary<string, string> headers = new Dictionary<string, string>();
        public string versionSuffix = "";
        public string client_id = "";
        public string client_secret = "";
        public string username = "";
        public string password = "";
    }

    public interface IGSTNAuthProvider:IServiceAuthorizer
    {
        TokenResponseModel token { get; set; }

        string AuthToken();
        string PathTemplate();
        GSTNResult<TokenResponseModel> RefreshToken();
        GSTNResult<OTPResponseModel> Logout();
        GSTNResult<TokenResponseModel> RequestToken(string otp);
        GSTNResult<OTPResponseModel> RequestOTP();
        Dictionary<string, string> dicParams { get; set; }
        void PopulateHeaders(HttpClient client);
        bool Init();
        byte[] DecryptedKey { get; set; }
        GSPCredentials credential();
        string IPAddr { get; set; }

    }
}
