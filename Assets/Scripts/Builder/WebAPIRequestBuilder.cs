using System.Collections;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
// using SurviveTheRust.Assets.API;
using UnityEngine;
using UnityEngine.Networking;

// namespace SurviveTheRust.Assets.Scripts.Builder
// {
public class WebAPIRequestBuilder : WebAPI
{
    public const string POST = kHttpVerbPOST;
    public const string GET = kHttpVerbGET;
    public const string DELETE = kHttpVerbDELETE;
    // public const string POST = kHttpVerbGET;  

    private const string XParseApplicationID = "X-Parse-Application-Id";
    private const string XParseRestAPIKey = "X-Parse-REST-API-Key";
    private const string XParseRevocable = "X-Parse-Revocable-Session";

    private const string ContentType = "Content-Type";
    private const string JsonApplication = "application/json";
    private const string OctetStreamApplication = "application/octet-stream";

    private static string contentType;

    private WebAPIRequestBuilder ParseApplicationID()
    {
        this.SetRequestHeader(XParseApplicationID, Secrets.APPLICATION_ID);
        return this;
    }

    private WebAPIRequestBuilder ParseRestAPIKey()
    {
        this.SetRequestHeader(XParseRestAPIKey, Secrets.REST_API_KEY);
        return this;
    }

    //public void CheckAccountDetails()
    //{

    //}

    public WebAPIRequestBuilder URIGet(string uri)
    {
        Get(uri);
        this.SetRequestHeader(XParseApplicationID, Secrets.APPLICATION_ID);
        this.SetRequestHeader(XParseRestAPIKey, Secrets.REST_API_KEY);
        return this;
    }

    public WebAPIRequestBuilder SetURI(string uri)
    {
        this.uri = new System.Uri("https://parseapi.back4app.com/" + uri);
        return this;
    }

    public WebAPIRequestBuilder SetURL(string url)
    {
        this.url = "https://parseapi.back4app.com/" + url;
        return this;
    }

    public WebAPIRequestBuilder Revocable()
    {
        this.SetRequestHeader(XParseRevocable, "1");
        return this;
    }

    public WebAPIRequestBuilder SetJSON(object json)
    {
        this.json = JsonConvert.SerializeObject(json);
        contentType = JsonApplication;
        this.SetRequestHeader(ContentType, contentType);
        this.uploadHandler = new UnityEngine.Networking.UploadHandlerRaw(Encoding.UTF8.GetBytes(this.json));
        this.downloadHandler = new UnityEngine.Networking.DownloadHandlerBuffer();
        return this;
    }

    //public WebAPIRequestBuilder SetBinaryFile(object binaryFile) 
    //{
    //    this.json = JObject.
    //    return this;
    //}

    public WebAPIRequestBuilder SetMethod(string method)
    {
        this.method = method;
        return this;
    }

    public WebAPIRequestBuilder Build()
    {
        this.SetRequestHeader(XParseApplicationID, Secrets.APPLICATION_ID);
        this.SetRequestHeader(XParseRestAPIKey, Secrets.REST_API_KEY);
        //this.SetRequestHeader(ContentType, contentType);
        return this;
    }
}
// }