using Serilog;
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Risersoft.API.GSTN;
using System.Threading.Tasks;
using System.Net.Security;
using Microsoft.Owin.Infrastructure;
using Newtonsoft.Json;
using System.Linq;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using risersoft.shared;

public abstract class GSTNApiClientBase : IDisposable
{
    public GSPCredentials credential;
   public IGSTNAuthProvider provider;
    protected internal string path, BasePath;
    private string url2;
    protected internal string gstin;
    public Dictionary<string, string> dicParams { get; set; }

    TimeSpan timeout = TimeSpan.FromSeconds(100);
    public GSTNApiClientBase(string gstin, string gsp, string env, string API)
    {
        this.gstin = gstin.Trim();
        this.credential = GSTNConstants.GenerateCredential(gsp, API, env);
        dicParams = new Dictionary<string, string>();
    }
    public void SetPathTemplate(string pathtemplate, string pathsuffix, string apicategory)
    {
        string version = "0.3";
        if (credential != null)
        {
            if (credential.versions.ContainsKey(apicategory)) version = credential.versions[apicategory];
        }

        if (pathsuffix == "/ledgers" && pathtemplate.EndsWith("/returns"))
        {
            pathtemplate = pathtemplate.Replace("/returns", string.Empty);
        }
        this.path = string.Format(pathtemplate + pathsuffix, version + this.credential.versionSuffix);

        this.BasePath = string.Format(pathtemplate, version + this.credential.versionSuffix);
        this.url2 = credential.base_url + this.path;

    }
    public TimeSpan RequestTimeout
    {
        get { return timeout; }
        set { timeout = value; }
    }
    public string Serialize<T>(T input)
    {
        string finalJson = "";
        if (input != null)
        {
            finalJson = JsonConvert.SerializeObject(input, Newtonsoft.Json.Formatting.Indented,
                        new JsonSerializerSettings
                        {
                            NullValueHandling = NullValueHandling.Ignore
                        });
        }
        return finalJson;
    }
    public string PrepareQueryString(string path, IDictionary<string, string> Params)
    {
        url2 = WebUtilities.AddQueryString(path, Params);
        return url2;
    }
    public string PrepareQueryString(IDictionary<string, string> Params)
    {
        return this.PrepareQueryString(credential.base_url + path, Params);
    }


    public async Task<GSTNResult<TOutput>> GetAsync<TOutput>()
    {
        using (var client = GetHttpClient("GET"))
        {
            HttpResponseMessage response = await client.GetAsync(url2);
            return BuildResponse<TOutput>(response);
        }

    }

    public async Task<GSTNResult<TOutput>> PostAsync<TInput, TOutput>(TInput data)
    {
        using (var client = GetHttpClient("POST"))
        {
            HttpResponseMessage response;
            if (typeof(TInput) == typeof(string))
            {
                var content = new StringContent((string)(object)data, System.Text.Encoding.UTF8, "application/json");
                response = await client.PostAsync(url2, content);
            }
            else
            {
                response = await client.PostAsJsonAsync(url2, data);
            }
            return BuildResponse<TOutput>(response);
        }

    }

    public async Task<GSTNResult<TOutput>> PutAsync<TInput, TOutput>(TInput data)
    {
        using (var client = GetHttpClient("PUT"))
        {
            HttpResponseMessage response = await client.PutAsJsonAsync(url2, data);
            return BuildResponse<TOutput>(response);
        }

    }

    public async Task<GSTNResult<TOutput>> PatchAsync<TInput, TOutput>(TInput data)
    {
        using (var client = GetHttpClient("PATCH"))
        {
            HttpRequestMessage request = new HttpRequestMessage(new HttpMethod("PATCH"), url2);
            request.Content = new StringContent(JsonConvert.SerializeObject(data));
            HttpResponseMessage response = await client.SendAsync(request);
            return BuildResponse<TOutput>(response);
        }

    }

    public async Task<GSTNResult<bool>> DeleteAsync()
    {
        using (var client = GetHttpClient("DELETE"))
        {
            HttpResponseMessage response = await client.DeleteAsync(url2);
            return BuildResponse<bool>(response);
        }

    }



    public GSTNResult<TOutput> Get<TOutput>()
    {


        var _task = Task.Run(() => { return GetAsync<TOutput>(); });

        _task.Wait();
        var result = _task.Result;
        return result;
    }

    public GSTNResult<TOutput> Post<TInput, TOutput>(TInput data)
    {


        var _task = Task.Run(() => { return PostAsync<TInput, TOutput>(data); });

        _task.Wait();
        var result = _task.Result;
        return result;
    }

    public GSTNResult<TOutput> Put<TInput, TOutput>(TInput data)
    {

        var _task = Task.Run(() => { return PutAsync<TInput, TOutput>(data); });

        _task.Wait();
        var result = _task.Result;
        return result;
    }

    public GSTNResult<TOutput> Patch<TInput, TOutput>(TInput data)
    {


        var _task = Task.Run(() => { return PatchAsync<TInput, TOutput>(data); });

        _task.Wait();
        var result = _task.Result;
        return result;
    }

    public GSTNResult<bool> Delete()
    {


        var _task = Task.Run(() => { return DeleteAsync(); });

        _task.Wait();
        var result = _task.Result;
        return result;
    }


    protected void LogMessage(string msg)
    {
        Trace.WriteLine(msg);
        Log.Information(msg);

    }

    protected HttpClient GetHttpClient(string RequestType)
    {
        var client = new HttpClient();
        if ((credential != null) && (!string.IsNullOrEmpty(credential.cert_file_http)))
        {
            //https
            X509Certificate cert = EncryptionUtils.getPublicKey(credential.cert_file_http);
            WebRequestHandler handler = new WebRequestHandler();
            handler.ClientCertificates.Add(cert);
            client = new HttpClient(handler);
            //https://blogs.msdn.microsoft.com/henrikn/2012/08/07/httpclient-httpclienthandler-and-webrequesthandler-explained/
        }
        client.Timeout = timeout;
        BuildHeaders(client);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        dicParams.Add("RequestType", RequestType);
        dicParams.Add("RequestURL", url2);
        if (client.DefaultRequestHeaders.Contains("txn")) dicParams.Add("txn", client.DefaultRequestHeaders.GetValues("txn").FirstOrDefault());



        this.LogMessage(RequestType + ":" + url2);
        var str1 = client.DefaultRequestHeaders.ToString();
        this.LogMessage(str1);

        return client;
    }

    protected internal abstract void BuildHeaders(HttpClient client);

    protected virtual GSTNResult<TOutput> BuildResponse<TOutput>(HttpResponseMessage response)
    {
        //This function can be used to convert simple API result to ResultInfo based API result
        GSTNResult<TOutput> resultInfo = new GSTNResult<TOutput>();

        resultInfo.HttpStatusCode = Convert.ToInt32(response.StatusCode.ToString("D"));
        this.dicParams.Add("StatusCode", response.StatusCode.ToString("D"));

        if (typeof(TOutput) == typeof(System.IO.Stream))
        {
            this.LogMessage("Response is a file stream");

            try
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    resultInfo.Data = (TOutput)(Object)(response.Content.ReadAsStreamAsync().Result);
                }
                else
                {
                    this.dicParams.Add("ResponseError", response.ReasonPhrase);
                    resultInfo.Message = response.ReasonPhrase;
                }
            }
            catch (Exception exception)
            {
                this.LogMessage("Obtained Exception for file stream:" + exception.Message + System.Environment.NewLine);
                this.LogMessage("Stack trace of exception for file stream:" + exception.StackTrace + System.Environment.NewLine);
                this.dicParams.Add("ResponseError", response.ReasonPhrase);
            }
        }
        else
        {
            var str1 = response.Content.ReadAsStringAsync().Result;

            this.LogMessage("Obtained Response:" + str1 + System.Environment.NewLine);
            this.dicParams.Add("Response", str1);
            this.dicParams.Add("ResponsePayload", str1);

            try
            {
                if ((resultInfo.HttpStatusCode == (int)HttpStatusCode.OK) || (resultInfo.HttpStatusCode == (int)HttpStatusCode.Accepted))
                {
                    if (typeof(TOutput) == typeof(String))
                    {
                        var result = (TOutput)(Object)str1;
                        resultInfo.Data = result;
                    }
                    else
                    {
                        var result = JsonConvert.DeserializeObject<TOutput>(str1);
                        resultInfo.Data = result;
                    }
                }
                else
                {
                    this.dicParams.Add("ResponseError", str1);
                    resultInfo.Message = str1;
                }
            }
            catch (Exception ex)
            {
                this.LogMessage("Obtained Exception:" + ex.Message + System.Environment.NewLine);
                this.dicParams.Add("ResponseError", ex.Message);
            }
        }

        return resultInfo;
    }


    protected virtual GSTNResult<TOutput> BuildResult<TOutput>(HttpStatusCode status, TOutput data)
    {
        //This function can be used to convert simple API result to ResultInfo based API result
        GSTNResult<TOutput> resultInfo = new GSTNResult<TOutput>();
        resultInfo.HttpStatusCode = (int)status;
        if (resultInfo.HttpStatusCode == (int)HttpStatusCode.OK)
        {
            resultInfo.Data = data;
        }
        return resultInfo;
    }

    protected virtual GSTNResult<TOutput> BuildResult<TOutput>(GSTNResult<ResponseDataInfo> response, TOutput data)
    {
        //This function can be used to convert simple API result to ResultInfo based API result
        GSTNResult<TOutput> resultInfo = new GSTNResult<TOutput>();
        resultInfo.HttpStatusCode = response.HttpStatusCode;
        if (resultInfo.HttpStatusCode == (int)HttpStatusCode.OK)
        {
            resultInfo.Data = data;
        }
        return resultInfo;
    }
    protected virtual GSTNResult<TOutput> BuildExceptionResult<TOutput>(Exception ex)
    {
        //This function can be used to convert simple API result to ResultInfo based API result
        GSTNResult<TOutput> resultInfo = new GSTNResult<TOutput>();
        resultInfo.HttpStatusCode = (int)HttpStatusCode.InternalServerError;
        resultInfo.Message = ex.Message;
        return resultInfo;
    }
    public void Dispose()
    {
    }
    public byte[] bytAppKey = null;

    public byte[] GetAppKeyBytesNew()
    {
        if (bytAppKey == null)
        {
            bytAppKey = EncryptionUtils.CreateAesKeyBC();
        }
        return bytAppKey;

    }
    public string GetAppKeyEncodedNew()
    {
        if (bytAppKey == null)
        {
            bytAppKey = EncryptionUtils.CreateAesKeyBC();
        }
        return Convert.ToBase64String(bytAppKey);

    }
    public byte[] GetAppKeyBytes()
    {

        if (bytAppKey == null)
        {
            string str1 = "";
            if (myUtils.IsInList(this.credential.Env, "test"))
            {
                bytAppKey = this.GetAppKeyBytesNew();
            }
            else
            {
                str1 = this.GetFixedAppKeyEncoded();
                bytAppKey = Convert.FromBase64String(str1);
            }
        }
        return bytAppKey;

    }
    public string GetFixedAppKeyEncoded()
    {
        string str1 = "WLwOB0dEpqcxLQMGDFNh/e7mncgLaV9TGM5iXaWmj4g=";
        return str1;

    }




}

