using System.Collections;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
// using SurviveTheRust.Assets;
// using SurviveTheRust.Assets.API;
// using SurviveTheRust.Assets.Scripts.Builder;
//using Unity.VisualScripting;
//using SurviveTheRust.Assets.Scripts.Managers;
// using SurviveTheRust.Assets.Scripts.PromoCodeLogic;
// using SurviveTheRust.Assets.Scripts.ScriptableObjects.AlertMessages;
using TMPro;
using UnityEditor.Build.Pipeline.Tasks;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

/// <summary>
/// Manages the API requests between the client and the server
/// </summary>
public class APIRequestManager : MonoBehaviour
{
    private const string XParseApplicationID = "X-Parse-Application-Id";
    private const string XParseRestAPIKey = "X-Parse-REST-API-Key";
    private const string ContentType = "Content-Type";
    private const string JsonApplication = "application/json";

    private const string DeathTrackerURL = "https://parseapi.back4app.com/classes/DeathTracker";
    private const string PromoCodesURL = "https://parseapi.back4app.com/classes/PromoCodes";



    // public static AlertMessagesSO[] alertMessages;

    // public static void AssignAlertMessages(AlertMessagesSO[] alertMessagesToAssign)
    // {
    //     for (int i = 0; i < alertMessages.Length; i++)
    //     {
    //         alertMessages[i] = alertMessagesToAssign[i];
    //     }
    // }

    public static IEnumerator GetWeatherCoroutine(TMP_Text weatherTMPText, Text weatherText)
    {
        //The request must be of type IDisposable (in this case UnityWebRequest) since it disposes it self automatically
        using (var request = UnityWebRequest.Get("https://wttr.in/Lisbon?format=3"))
        {
            yield return request.SendWebRequest();
            Debug.Log(request.result);

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(request.error);
                yield break;
            }

            weatherTMPText.text = request.downloadHandler.text;
            weatherText.text = request.downloadHandler.text;
        }
    }

    public static IEnumerator CreateDeathTracker(EntityJsonFormater entity)
    {
        // using (var request = UnityWebRequest.Post("https://parseapi.back4app.com/classes/DeathTracker", UnityWebRequest.kHttpVerbPOST))
        // using (var request = new UnityWebRequest("https://parseapi.back4app.com/classes/DeathTracker", UnityWebRequest.kHttpVerbPOST))
        using (var request = new UnityWebRequest(DeathTrackerURL, UnityWebRequest.kHttpVerbPOST))
        {
            request.SetRequestHeader(XParseApplicationID, Secrets.APPLICATION_ID);
            request.SetRequestHeader(XParseRestAPIKey, Secrets.REST_API_KEY);
            request.SetRequestHeader(ContentType, JsonApplication);

            // var json = "{\"Name\": \"Enemy\", \"Count\": 0}";

            var jsonTest = JsonConvert.SerializeObject(entity, Formatting.Indented);
            Debug.Log(jsonTest);

            // request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json));
            request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(jsonTest));
            request.downloadHandler = new DownloadHandlerBuffer();

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(request.error);
                yield break;
            }

            Debug.Log(request.downloadHandler.text);
        }
    }

    /// <summary>
    /// Get the Death Tracker Count and store it inside of a Legacy Text 
    /// </summary>
    /// <param name="enemyDeathText">Legacy Text</param>
    /// <returns></returns>
    public static IEnumerator GetDeathTrackerRequest(EntityJsonFormater entity, Text enemyDeathText)
    {
        const string uri = DeathTrackerURL + "/?where={\"Name\":\"Enemy\"}";

        // , UnityWebRequest.kHttpVerbGET
        using (var request = UnityWebRequest.Get(uri))
        {
            request.SetRequestHeader(XParseApplicationID, Secrets.APPLICATION_ID);
            request.SetRequestHeader(XParseRestAPIKey, Secrets.REST_API_KEY);

            yield return request.SendWebRequest();
            Debug.Log(request.result);

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(request.error);
                yield break;
            }

            Debug.Log(request.downloadHandler.text);

            var matchesObjectID = Regex.Matches(request.downloadHandler.text, "\"objectId\":(\"(\\w+)\")", RegexOptions.Multiline);
            var matchesObjectName = Regex.Matches(request.downloadHandler.text, "\"Name\":(\"(\\w+)\")", RegexOptions.Multiline);
            var matchesDeathCount = Regex.Matches(request.downloadHandler.text, "\"Count\":(\\d+)", RegexOptions.Multiline);

            //JsonConvert.DeserializeObject()

            Debug.Log(matchesObjectID.First().Groups[2].Value);
            Debug.Log(matchesDeathCount.Count);

            GameManager.Instance.playerObjectID = matchesObjectID.First().Groups[2].Value;

            //entity.ObjectID = matchesObjectID.First().Groups[2].Value;
            entity.Name = matchesObjectName.First().Groups[2].Value;
            entity.Count = int.Parse(matchesDeathCount.First().Groups[1].Value);

            yield return new WaitForSeconds(1f);
            //enemyDeathText.text = matchesDeathCount.First().Groups[1].Value; 
            enemyDeathText.text = entity.Count.ToString();
        }
    }

    #region TMP_Text GetDeathTrackerRequest
    /// <summary>
    /// Get the Death Tracker Count and store it inside of a Text Mesh Pro Text
    /// </summary>
    /// <param name="enemyDeathText">Text Mesh Pro Text</param>
    /// <returns></returns>
    //public static IEnumerator GetDeathTrackerRequest(TMP_Text enemyDeathText)
    //{
    //    const string uri = DeathTrackerURL + "/?where={\"Name\":\"Enemy\"}";

    //    // , UnityWebRequest.kHttpVerbGET
    //    using (var request = UnityWebRequest.Get(uri))
    //    {
    //        request.SetRequestHeader(XParseApplicationID, Secrets.APPLICATION_ID);
    //        request.SetRequestHeader(XParseRestAPIKey, Secrets.REST_API_KEY);

    //        yield return request.SendWebRequest();
    //        Debug.Log(request.result);

    //        if (request.result != UnityWebRequest.Result.Success)
    //        {
    //            Debug.LogError(request.error);
    //            yield break;
    //        }

    //        Debug.Log(request.downloadHandler.text);
    //        var matchesDeathCount = Regex.Matches(request.downloadHandler.text, "\"Count\":(\\d+)", RegexOptions.Multiline);
    //        var matchesObjectID = Regex.Matches(request.downloadHandler.text, "\"objectId\":(\"(\\w+)\")", RegexOptions.Multiline);
    //        Debug.Log(matchesObjectID.First().Groups[2].Value);
    //        Debug.Log(matchesDeathCount.Count);
    //        GameManager.Instance.playerObjectID = matchesObjectID.First().Groups[2].Value;
    //        enemyDeathText.text = matchesDeathCount.First().Groups[1].Value;
    //    }
    //}
    #endregion

    //Could be a good Idea to pass the entities ObjectID as a parameter to this method instead of having it hard coded inside the GameManager
    //(Variables will be strored inside the GameManager Script as a Global Reference) 
    public static IEnumerator IncrementDeathTracker(int value)
    {
        var url = DeathTrackerURL + "/" + GameManager.Instance.playerObjectID;

        Debug.Log(value);

        using (var request = new UnityWebRequest(url, UnityWebRequest.kHttpVerbPUT))
        {
            request.SetRequestHeader(XParseApplicationID, Secrets.APPLICATION_ID);
            request.SetRequestHeader(XParseRestAPIKey, Secrets.REST_API_KEY);
            request.SetRequestHeader(ContentType, JsonApplication);

            //JsonConvert.

            //UIManager.Enemy.Name = request.downloadHandler.text;
            GameManager.Enemy.Count += value;

            var updatedJsonValues = JsonConvert.SerializeObject(GameManager.Enemy, Formatting.Indented);

            request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(updatedJsonValues));
            request.downloadHandler = new DownloadHandlerBuffer();
            //var matches = Regex.Matches(request.downloadHandler.text, "\"Count\":(\\d+)", RegexOptions.Multiline);

            //Debug.Log(matches.Count);

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(request.error);
                yield break;
            }

            Debug.Log(request.downloadHandler.text);
            // var matches = Regex.Matches(request.downloadHandler.text, "\"objectId\":(\\w+)", RegexOptions.Multiline);
            //var matches = Regex.Matches(request.downloadHandler.text, "\"Count\":(\\d+)", RegexOptions.Multiline);
            //request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(value.ToString())); 
            //Debug.Log(matches.Count);
        }
    }

    public static IEnumerator CreatePromoCodes(EntityPromoCodes entityPromoCodes)
    {
        using (var request = new UnityWebRequest(PromoCodesURL, UnityWebRequest.kHttpVerbPOST))
        {
            request.SetRequestHeader(XParseApplicationID, Secrets.APPLICATION_ID);
            request.SetRequestHeader(XParseRestAPIKey, Secrets.REST_API_KEY);
            request.SetRequestHeader(ContentType, JsonApplication);

            // var json = "{\"Name\": \"Enemy\", \"Count\": 0}";

            var jsonTest = JsonConvert.SerializeObject(entityPromoCodes, Formatting.Indented);
            Debug.Log(jsonTest);

            // request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json));
            request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(jsonTest));
            request.downloadHandler = new DownloadHandlerBuffer();

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(request.error);
                yield break;
            }

            Debug.Log(request.downloadHandler.text);
        }

        //yield return null;
    }

    public static IEnumerator CheckIfPromoCodeIsValid(string promoCode/* , AlertMessagesSO[] alertMessages */)
    {
        //var url = PromoCodesURL + $"/?where={PromoCode:\"{promoCode.text}\"}";

        //string url = PromoCodesURL + "/?where={\"PromoCode\":\"" + promoCode.text + "\"}";
        //string url = "classes/PromoCodes" + "/?where={\"PromoCode\":" + $"\"{promoCode}\"" + ",\"IsUsed\":false"  + "}";
        //string url = "classes/PromoCodes" + "/?where={\"PromoCode\":" + $"\"{promoCode}\"" + "}";
        string url = PromoCodesURL + "/?where={\"PromoCode\":" + $"\"{promoCode}\"" + "}";

        //using (var request = new UnityWebRequest(PromoCodesURL, UnityWebRequest.kHttpVerbGET))
        using (var request = WebAPIRequestBuilder.Get(url)
            //.SetURI(url)
            //.SetMethod("GET")
            //.build())
            )
        {
            //var matchesPromoCode = Regex.Matches(request.downloadHandler.text, "\"PromoCode\":(\"w+)", RegexOptions.Multiline);
            request.SetRequestHeader(XParseApplicationID, Secrets.APPLICATION_ID);
            request.SetRequestHeader(XParseRestAPIKey, Secrets.REST_API_KEY);

            yield return request.SendWebRequest();

            // Debug.Log(request.result);

            if (request.result != UnityWebRequest.Result.Success)
            {
                // Debug.LogError(request.error);
                yield break;
            }

            var requestResultToken = JObject.Parse(request.downloadHandler.text);

            if (!requestResultToken["results"].HasValues)
            {
                GameManager.Instance.UiManager.SetAlertMessageText(1);
                GameManager.Instance.UiManager.ActivateAlertMessagesPanel();
            }

            // GameManager.Instance.UiManager.activeAlertMessagePopup?.Invoke(alertMessages[1]);
            // GameManager.Instance.UiManager.SendAlert(alertMessages[1]);

            // if (requestResultToken.Value<string>("PromoCode").ToString() == promoCode)
            //     Debug.Log("promocode found"); //Replace with alert message

            if (JObject.Parse(request.downloadHandler.text).Value<bool>("IsUsed") == true)
            {
                GameManager.Instance.UiManager.SetAlertMessageText(3);
                GameManager.Instance.UiManager.ActivateAlertMessagesPanel();
            }
            // GameManager.Instance.UiManager.activeAlertMessagePopup?.Invoke(alertMessages[3]);
            // GameManager.Instance.UiManager.SendAlert(alertMessages[3]);

            GameManager.Instance.UiManager.SetAlertMessageText(8);
            GameManager.Instance.UiManager.ActivateAlertMessagesPanel();

            // Debug.Log(JObject.Parse(request.downloadHandler.text)["results"] != null);
            Debug.Log(request.downloadHandler.text);
            // Debug.Log(JObject.Parse(request.downloadHandler.text)["results"].HasValues);

            //var obj = JObject.Parse(request.downloadHandler.text);
            //obj.GetValue("PromoCode");
        }
    }

    public static IEnumerator VerifyExistingUserAccounts(string username, string email, string password)
    {
        using (var request = WebAPIRequestBuilder.Get(""))
        {
            request.SetRequestHeader(XParseApplicationID, Secrets.APPLICATION_ID);
            request.SetRequestHeader(XParseRestAPIKey, Secrets.REST_API_KEY);

            yield return null;
        }
    }

    // , AlertMessagesSO[] alertMessages
    public static IEnumerator CreateUserAccount(string username, string email, string password/* , AlertMessagesSO[] alertMessages */)
    {
        //var t = new { username, password, email };
        using (var request = new WebAPIRequestBuilder()
            .SetURL("users")
            .SetMethod("POST")
            .Revocable()
            .SetJSON(new { username, password, email })
            //.SetJSON(new { username = "stevenmelo588", password = "1234", email = "stevenmelo588@gmail.com" })
            .Build())
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                // Debug.LogError(request.downloadHandler.text);

                if (JObject.Parse(request.downloadHandler.text).Value<string>("code").ToString() == "202")
                    GameManager.Instance.UiManager.SetAlertMessageText(4);
                // {
                // GameManager.Instance.UiManager.ActivateAlertMessagesPanel();
                // }
                // GameManager.Instance.UiManager.activeAlertMessagePopup?.Invoke(alertMessages[4]);
                // GameManager.Instance.UiManager.SendAlert(alertMessages[4]);
                if (JObject.Parse(request.downloadHandler.text).Value<string>("code").ToString() == "203")
                    GameManager.Instance.UiManager.SetAlertMessageText(5);

                GameManager.Instance.UiManager.ActivateAlertMessagesPanel();

                // GameManager.Instance.UiManager.activeAlertMessagePopup?.Invoke(alertMessages[5]);
                // GameManager.Instance.UiManager.SendAlert(alertMessages[5]);

                // Debug.LogError(request.error + " | " + request.GetRequestHeader(ContentType));
                yield break;
            }

            var loginAccountData = JObject.Parse(request.downloadHandler.text);

            GameManager.Instance.UiManager.SetAlertMessageText(0);
            GameManager.Instance.UiManager.ActivateAlertMessagesPanel();
            // GameManager.Instance.UiManager.activeAlertMessagePopup?.Invoke(alertMessages[0]);
            // GameManager.Instance.UiManager.SendAlert(alertMessages[0]);

            //using (var emailVerifRequest = new WebAPIRequestBuilder()
            //    .SetURL("/verificationEmailRequest")
            //    .SetJSON(t)
            //    .Build())
            //{
            //yield return emailVerifRequest.SendWebRequest();
            //yield break;
            //}

            // yield return request.SetURL("/verificationEmailRequest").SendWebRequest();

            // Debug.Log(request.downloadHandler.text);
        }

        //yield return null;
    }

    public static IEnumerator UserAccountLogin(string usernameData, string passwordData/* , AlertMessagesSO[] alertMessages */)
    {
        // /username=<" + $"{username}" + ">"
        using (var request = new WebAPIRequestBuilder()
            .SetURL("login")
            .SetMethod(WebAPIRequestBuilder.POST)
            .Revocable()
            .SetJSON(new { username = usernameData, password = passwordData })
            .Build())
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                // UIManager.
                // Debug.LogError(request.downloadHandler.text);

                if (JObject.Parse(request.downloadHandler.text).Value<string>("code").ToString() == "101")
                    GameManager.Instance.UiManager.SetAlertMessageText(2);
                // GameManager.Instance.UiManager.activeAlertMessagePopup?.Invoke(alertMessages[2]);
                // GameManager.Instance.UiManager.SendAlert(alertMessages[2]);

                GameManager.Instance.UiManager.ActivateAlertMessagesPanel();

                yield break;
            }

            // Debug.Log(request.downloadHandler.text);

            // using(var loginRequest = new WebAPIRequestBuilder().SetMethod(WebAPIRequestBuilder.GET).Revocable().Build())
            // {
            //     yield return loginRequest.SendWebRequest();

            //     if (request.result != UnityWebRequest.Result.Success)
            //     {
            //         // UIManager.
            //         Debug.Log(request.downloadHandler.text);
            //         yield break;
            //     }

            // }

            var loginAccountData = JObject.Parse(request.downloadHandler.text);

            if (loginAccountData.Value<bool>("emailVerified") == false)
            {
                GameManager.Instance.UiManager.SetAlertMessageText(6);
                GameManager.Instance.UiManager.ActivateAlertMessagesPanel();
            }
            // GameManager.Instance.UiManager.activeAlertMessagePopup?.Invoke(alertMessages[6]);
            // GameManager.Instance.UiManager.SendAlert(alertMessages[6]);

            // Debug.Log(loginAccountData.Value<string>("username").ToString() + " | " + loginAccountData.Value<string>("sessionToken").ToString());
            // Debug.Log(loginAccountData["sessionToken"][0].ToString());

            // yield return request.SetMethod(WebAPIRequestBuilder.GET).SendWebRequest();

            GameManager.playerUserName = loginAccountData.Value<string>("username").ToString();
            GameManager.playerSessionToken = loginAccountData.Value<string>("sessionToken").ToString();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static IEnumerator UploadSaveData(string saveFile)
    {
        using (var request = new WebAPIRequestBuilder()
        .SetURL("classes/PlayerSaveData")
        .SetMethod(WebAPIRequestBuilder.POST)
        .SetJSON(new { __type = "File", name = saveFile })
        .Build())
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(request.downloadHandler.text);
                yield break;
            }

            var saveFileData = JObject.Parse(request.downloadHandler.text);

            Debug.Log(saveFileData.GetValue("SaveFile").ToString());
        }

        // Media Type for the Binary Save File:
        // application/octet-stream

        // yield return null;
    }

    public static IEnumerator DeleteCloudSaveData(string saveFile)
    {
        using (var request = new WebAPIRequestBuilder()
        .SetURL("classes/PlayerSaveData/?where=\"SaveFile\":" + $"\"{saveFile}\"" + "}")
        .SetMethod(WebAPIRequestBuilder.GET)
        .Build())
        {
            yield return request.SendWebRequest();

            var saveFileParseObject = JObject.Parse(request.downloadHandler.text);
            
            Debug.Log(saveFileParseObject.GetValue("objectId").ToString());

            // using (var saveDeletionRequest = new WebAPIRequestBuilder()
            // .SetURL($"classes/PlayerSaveData/{}")
            // .SetMethod(WebAPIRequestBuilder.DELETE)
            // .Build())
            // {
            //     yield return saveDeletionRequest.SendWebRequest();
            // }
        }
    }
}