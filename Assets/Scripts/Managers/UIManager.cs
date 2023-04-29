using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
// using SurviveTheRust.Assets.API;
// using SurviveTheRust.Assets.Localization;
// using SurviveTheRust.Assets.Scripts.AddressablesScripts;
// using SurviveTheRust.Assets.Scripts.Localization;
// using SurviveTheRust.Assets.Scripts.Managers;
// using SurviveTheRust.Assets.Scripts.SaveSettings;
// using SurviveTheRust.Assets.Scripts.ScriptableObjects.AlertMessages;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class EntityJsonFormater
{
    //public string ObjectID { get; set; }
    public string Name { get; set; }
    public int Count { get; set; }
}

/// <summary>
/// Get the data from a Json Formated Entity present inside of the DataBase
/// </summary>
/// <para name="EntityObjectID">The Entity's Object ID (used to retrieve the information of an Entity)</para>
/// <para name="EntityName">The Entity's Name (used to retrieve the information of an Entity)</para>
/// <para name="EntityDeathCount">The Entity's Death Counter/Tracker (used to retrieve the information of an Entity)</para>
public class EntityDataFromJsonFormater
{
    public string EntityObjectID { get; set; }
    public string EntityName { get; set; }
    public int EntityDeathCount { get; set; }
}

[DisallowMultipleComponent]
public class UIManager : MonoBehaviour
{
    public delegate void SendAlertMessagePopUp();
    public SendAlertMessagePopUp sendAlertMessage;

    [SerializeField] private SceneLoader sceneLoader;

    [System.Serializable]
    private struct UserAccountData
    {
        [Space(-10)]
        [Header("--- User Account Creation ---")]
        [SerializeField] public InputField[] NewAccountCredentials;


        //[SerializeField] public InputField NewAccountUsername;
        //[SerializeField] public InputField NewAccountEmail;
        //[SerializeField] public InputField NewAccountPassword;

        //[Space(10)]
        [Header("--- User Account Login ---")]
        [SerializeField] public InputField[] AccountLoginCredentials;

        //[SerializeField] public InputField AccountLoginUsername;
        //[SerializeField] public InputField AccountLoginPassword;
    }

    //[System.Serializable]
    //private struct LoginAccountData
    //{
    //    [SerializeField] public InputField NewAccountUsername;
    //    [SerializeField] public InputField NewAccountEmail;
    //    [SerializeField] public InputField NewAccountPassword;
    //}

    //[Header("--- User Account Input Fields ---")]
    [Header("--- Create/Login Menu ---")]
    [Space(5)]
    [SerializeField] private UserAccountData UserAccountInputFieldData;
    [SerializeField] private Button CreateAccountBTN;
    [SerializeField] private Button AccountLoginBTN;

    private string[] validEmails =
    {
        "gmail.com",
        "yahoo.com",
        "outlook.com",
        "hotmail.com",
        "aol.com"
    };

    //[SerializeField] private LoginAccountData loginAccountData; 

    //[Space(10)]
    [Header("--- UI/Player/Options Menu ---")]
    //[SerializeField] private Base[] UIPanels;
    [SerializeField] private GameObject[] UICanvas;
    [SerializeField] private Selectable[] SelectedOnStart;

    [Header("--- Save File/UI  ---")]
    [SerializeField] private GameObject[] SaveUICanvas;
    [SerializeField] private Selectable[] SaveSelectedOnStart;
    [SerializeField] private Button[] SaveFileConfirmButtons;
    [SerializeField] private Button SaveFileConfirmButton;
    [SerializeField] private InputField[] SaveFileNames;

    [Header("--- PopUp Alert UI  ---")]
    [SerializeField] private GameObject AlertMessagePanel;
    [SerializeField] private AlertMessagesSO[] alertMessages;
    public UnityEvent<AlertMessagesSO> activeAlertMessagePopup;
    [SerializeField] private TMP_Text AlertTypeText;
    [SerializeField] private TMP_Text MessageNameText;
    [SerializeField] private Text MessageText;

    [Space(10)]

    [Header("-[ PromoCode UI Objects ]-")]
    [SerializeField] Button PromoCodeRedeemBTN;
    [SerializeField] InputField PromoCodeInputField;

    //[SerializeField] private InputField NewAccountUsername;
    //[SerializeField] private InputField NewAccountEmail;
    //[SerializeField] private InputField NewAccountPassword;

    //[SerializeField] private InputField[] CreateAccountInputFields;

    //[SerializeField] private InputField[] UserAccountLoginInputFields;

    [SerializeField] private Text weatherText;
    [SerializeField] private TMP_Text weatherTMPText;

    [SerializeField] private Text enemyDeathText;
    [SerializeField] private Text localEnemyDeathText;

    [SerializeField] private TMP_Dropdown languageSelectionDropdown;

    // private List<InputField> CreateAccountInputFields;
    // private List<InputField> AccountLoginInputFields;

    public static Text EnemyDeathText { get; set; }
    public static Text LocalEnemyDeathText { get; set; }

    public static EntityJsonFormater Steven = new()
    {
        Name = "Steven",
        Count = 0,
    };

    //public static EntityJsonFormater Enemy = new();

    private void Awake()
    {
        EnemyDeathText = enemyDeathText;
        LocalEnemyDeathText = localEnemyDeathText;

        // for (int i = 0; i < APIRequestManager.alertMessages.Length; i++)
        // {
        //     APIRequestManager.alertMessages[i] = alertMessages[i];
        // }
    }

    // Start is called before the first frame update
    private void Start()
    {
        if (weatherText && weatherTMPText)
            StartCoroutine(APIRequestManager.GetWeatherCoroutine(weatherTMPText, weatherText));
        if (enemyDeathText && localEnemyDeathText)
            StartCoroutine(APIRequestManager.GetDeathTrackerRequest(GameManager.Enemy, UIManager.EnemyDeathText));

        // for (int i = 0; i < this.alertMessages.Length; i++)
        // {
        //     APIRequestManager.alertMessages[i] = alertMessages[i];
        // }

        // if (alertMessages.Length > 0)
        //     APIRequestManager.AssignAlertMessages(alertMessages);

        //Might Move it to the Awake Function since we want the dropdown
        //to be populated as fast as possible
        if (languageSelectionDropdown)
        {
            PopulateLanguageDropdown();
            ChangeLanguage();
        }

        //Cursor.visible = true;
        //Cursor.lockState = CursorLockMode.Confined;

        //if (UICanvas[1].activeInHierarchy)
        //{
        //CreateAccountInputFields.Add(UserAccountInputFieldData.NewAccountUsername);
        //CreateAccountInputFields.Add(UserAccountInputFieldData.NewAccountEmail);
        //CreateAccountInputFields.Add(UserAccountInputFieldData.NewAccountPassword);

        //AccountLoginInputFields.Add(UserAccountInputFieldData.AccountLoginUsername);
        //AccountLoginInputFields.Add(UserAccountInputFieldData.AccountLoginPassword);
        //}

        //if (UICanvas != null)
        SelectedPanel(0);

        Debug.Log("UIManager Started");
    }

    private void Update()
    {
        // for(int i = 0; i < SaveFileNames.Length; i++)
        SaveFileConfirmButton.interactable = (SaveFileNames[0].text.Length > 0) ? true: false; 

        // for (int i = 0; i < SaveFileNames.Length; i++)
        // {
        //     if (SaveFileNames[i].text.Length > 0)
        //     {
        //         SaveFileConfirmButtons[i].interactable = true;
        //         //SaveFileConfirmButtons[i].enabled = true;
        //     }
        //     else
        //     {
        //         SaveFileConfirmButtons[i].interactable = false;
        //         //SaveFileConfirmButtons[i].enabled = false;
        //     }
        // }

        // for (int i = 0; i < UserAccountInputFieldData.NewAccountCredentials.Length; i++)
        // {
        //     if (UserAccountInputFieldData.NewAccountCredentials[i].text.Length > 0)
        //     {
        //         CreateAccountBTN.interactable = true;
        //         //SaveFileConfirmButtons[i].enabled = true;
        //     }
        //     else
        //     {
        //         CreateAccountBTN.interactable = false;
        //         //SaveFileConfirmButtons[i].enabled = false;
        //     }
        // }

        for (int i = 0; i < UserAccountInputFieldData.NewAccountCredentials.Length; i++)
            CreateAccountBTN.interactable = (UserAccountInputFieldData.NewAccountCredentials[i].text.Length > 0) ? true : false;

        for (int i = 0; i < UserAccountInputFieldData.AccountLoginCredentials.Length; i++)
            AccountLoginBTN.interactable = (UserAccountInputFieldData.AccountLoginCredentials[i].text.Length > 0) ? true : false;

        // {

        // if (UserAccountInputFieldData.AccountLoginCredentials[i].text.Length > 0)
        // {
        //     AccountLoginBTN.interactable = true;
        //     //SaveFileConfirmButtons[i].enabled = true;
        // }
        // else
        // {
        //     AccountLoginBTN.interactable = false;
        //     //SaveFileConfirmButtons[i].enabled = false;
        // }
        // }

        // for (int i = 0; i < PromoCodeInputField.Length; i++)
        // {
        PromoCodeRedeemBTN.interactable = (PromoCodeInputField.text.Length > 0) ? true : false;
        // }
    }

    private static List<JsonTextLocalizer> JsonTextLocalizers => FindObjectsOfType<JsonTextLocalizer>().ToList();

    private void PopulateLanguageDropdown()
    {
        List<string> languagesOptions = new() { Languages.EN, Languages.FR, Languages.ES, Languages.DE, Languages.RU };
        List<string> languages = new();
        int currentLanguagePosition = (int)Language.En;

        //Clear the Dropdown of the previous Options
        languageSelectionDropdown.ClearOptions();

        //foreach (var language in languagesOptions)
        //{
        //    var lang = language switch
        //    {
        //        Languages.EN => "English",
        //        Languages.FR => "Français",
        //        Languages.ES => "Espaniol",
        //        Languages.DE => "Deutsch",
        //        Languages.RU => "Russian",
        //        _ => "English",
        //    };

        //    languages.Add(lang);
        //}

        //languagesOptions.ForEach(x => Debug.Log(x = (x == Languages.EN) ? "English" : (x == Languages.FR) ? "French" : (x == Languages.ES) ? "Spanish" : (x == Languages.DE) ? "Deutsch" : (x == Languages.RU) ? "Russian" : null));
        //languagesOptions.ForEach(x => languages.Add(x = (x == Languages.EN) ? "English" : (x == Languages.FR) ? "French" : (x == Languages.ES) ? "Spanish" : (x == Languages.DE) ? "Deutsch" : (x == Languages.RU) ? "Russian" : null));
        languagesOptions.ForEach(x => languages.Add(SelectLanguage(x)));

        //languages.Add( = languages.ForEach(x => x = (x == Languages.EN) ? "" : (x == Languages.FR) ? "" : (x == Languages.ES) ? "" : (x == Languages.FR) ? "" : (x == Languages.FR) ? "" : null));

        //languages.ForEach(x => language switch
        //{

        //});

        languageSelectionDropdown.AddOptions(languages);
        languageSelectionDropdown.value = currentLanguagePosition;
    }

    string SelectLanguage(string language)
    {
        return _ = language switch
        {
            Languages.EN => "English",
            Languages.FR => "Français",
            Languages.ES => "Espaniol",
            Languages.DE => "Deutsch",
            Languages.RU => "Russian",
            _ => "English",
        };
    }

    public void ChangeLanguage()
    {
        //var language = ""; //String

        var language = languageSelectionDropdown.value switch
        {
            0 => "en",
            1 => "fr",
            2 => "es",
            3 => "de",
            4 => "ru",
            _ => "en",
        };

        //if (string.IsNullOrWhiteSpace(language)) return;
        ReadJsonLocalizationData.CurrentLanguage = language;

        JsonTextLocalizers.ForEach(jsonTextLocalizer => jsonTextLocalizer.LocateKey());
    }

    public void SelectedPanel(int panelIndex)
    {
        if (SelectedOnStart.Length! > 0 || UICanvas.Length! > 0)
            return;
        for (int i = 0; i < UICanvas.Length; i++)
        {
            UICanvas[i].SetActive(i == panelIndex);

            //UIPanels[i].targetDisplay;

            if (i == panelIndex)
            {
                SelectedOnStart[i].Select();
            }
        }
    }

    public void SelectedSavePanel(int panelIndex)
    {
        if (SelectedOnStart.Length! > 0 || UICanvas.Length! > 0)
            return;
        for (int i = 0; i < UICanvas.Length; i++)
        {
            SaveUICanvas[i].SetActive(i == panelIndex);

            //UIPanels[i].targetDisplay;

            if (i == panelIndex)
            {
                SaveSelectedOnStart[i].Select();
            }
        }
    }


    public void BtnStart()
    {
        //SceneManager.LoadSceneAsync(2);
        //StartCoroutine(StartGameCoroutine());
    }

    public void BtnExit()
    {
        Application.Quit();
    }

    public void LoadScene1()
    {
        UICanvas[0].SetActive(false);
        sceneLoader.Load("Scene1");
    }

    public void Back()
    {
        sceneLoader.Back();
    }

    public void CreateNewSave(int saveFileIndex)
    {
        Debug.Log(saveFileIndex);
        SaveManager.CreateSaveFileAndDirectory(SaveFileNames[saveFileIndex].text, saveFileIndex);
    }

    public void OnInputFieldStopEditing(string inputFieldText)
    {
        //if (string.IsNullOrEmpty(inputFieldText))
        //{
        //    this.GetComponent<InputField>().
        //}
    }

    // public bool CheckIfAccountDataIsValid()
    // {
    //     foreach (var inputField in CreateAccountInputFields)
    //     {
    //         if (string.IsNullOrWhiteSpace(inputField.text) || string.IsNullOrEmpty(inputField.text))
    //         {
    //             inputField.textComponent.color = Color.red;

    //             return false;
    //         }
    //         else
    //         {
    //             return true;
    //         }
    //     }

    //     return false;
    // }
    // public void SendAlert(AlertMessagesSO alertMessage)
    // {
    //     /* this. */
    //     AlertTypeText.text = alertMessage.AlertType;
    //     /* this. */
    //     MessageNameText.text = alertMessage.AlertMessageName;
    //     /* this. */
    //     MessageText.text = alertMessage.AlertMessageText;
    // }

    public void SetAlertMessageText(int errorCode)
    {
        // switch (errorCode)
        // {
        //     case 0:
        //         SendAlert(alertMessages[]);
        //         break;
        //     case 1:
        //         break;
        //     case 2:
        //         break;
        //     case 3:
        //         break;
        //     case 4:
        //         break;
        //     case 5:
        //         break;
        //     case 6:
        //         break;
        //     // default:
        //     //     break;
        // }
        AlertTypeText.text = alertMessages[errorCode].AlertType;
        MessageNameText.text = alertMessages[errorCode].AlertMessageName;
        MessageText.text = alertMessages[errorCode].AlertMessageText;
    }

    public void ActivateAlertMessagesPanel()
    {
        AlertMessagePanel.SetActive(true);
    }

    public void ResetAlertMessagesSettings()
    {
        AlertTypeText.text = "";
        MessageNameText.text = "";
        MessageText.text = "";
    }

    // public void OnAlertMessageWindowClose()
    // {
    //     sendAlertMessage -= d;
    // }

    void ClearCreateAccountInputFields()
    {
        for (int i = 0; i < UserAccountInputFieldData.NewAccountCredentials.Length; i++)
            UserAccountInputFieldData.NewAccountCredentials[i].text = "";
    }

    public void CreateNewAccount()
    {
        for (int i = 0; i < validEmails.Length; i++)
            if (!UserAccountInputFieldData.NewAccountCredentials[1].text.Contains("@" + validEmails[i]))
            {
                SetAlertMessageText(7);
                ActivateAlertMessagesPanel();
                return;
            }

        //if(CheckIfAccountDataIsValid())
        StartCoroutine(APIRequestManager.CreateUserAccount(
            UserAccountInputFieldData.NewAccountCredentials[0].text,
            UserAccountInputFieldData.NewAccountCredentials[1].text,
            UserAccountInputFieldData.NewAccountCredentials[2].text/* ,
            alertMessages */));
        ClearCreateAccountInputFields();
        // SendAlert();
    }

    // public void SendAlert(AlertMessagesSO[] alertMessage)
    // {
    //     // APIRequestManager.AssignAlertMessages(alertMessage);

    //     // for (int i = 0; i < alertMessage.Length; i++)
    //     // {
    //     //     this.AlertTypeText.text = alertMessage[i].AlertType;
    //     //     this.MessageNameText.text = alertMessage[i].AlertMessageName;
    //     //     this.MessageText.text = alertMessage[i].AlertMessageText;
    //     // }
    // }


    void ClearAccountLoginInputFields()
    {
        for (int i = 0; i < UserAccountInputFieldData.AccountLoginCredentials.Length; i++)
            UserAccountInputFieldData.AccountLoginCredentials[i].text = "";
        //SaveFileConfirmButtons[i].enabled = true;
    }

    public void AccountLogin()
    {
        StartCoroutine(APIRequestManager.UserAccountLogin(
            UserAccountInputFieldData.AccountLoginCredentials[0].text,
            UserAccountInputFieldData.AccountLoginCredentials[1].text/* ,
            alertMessages */));
        ClearAccountLoginInputFields();
        //StartCoroutine(APIRequestManager.UserAccountLogin(UserAccountLoginInputFields[0].text, UserAccountLoginInputFields[1].text));
    }

    void ClearPromoCodeInputField()
    {
        PromoCodeInputField.text = "";
    }

    public void CheckAndActivatePromoCode()
    {
        StartCoroutine(APIRequestManager.CheckIfPromoCodeIsValid(
            PromoCodeInputField.text/* ,
            alertMessages */));
        ClearPromoCodeInputField();
    }

    public void DeletePlayerLocalSave()
    {
        
    }

    public void DeletePlayerCloudSave(int saveFileIndex)
    {
        //Debug.Log(SaveManager.SaveFiles.ElementAt(saveFileIndex));
    }

    //IEnumerator StartGameCoroutine()
    //{
    //    yield return new WaitForSeconds(5);
    //    //SceneManager.LoadScene(4);
    //    Cursor.visible = false;
    //    Cursor.lockState = CursorLockMode.Confined;
    //}



    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.Q))
    //         StartCoroutine(CreateDeathTracker());
    // }




    // public IEnumerator 
}
