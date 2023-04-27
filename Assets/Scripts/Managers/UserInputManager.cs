using System;
using System.Collections.Generic;
using System.Linq;
// using SurviveTheRust.Assets.API;
// using SurviveTheRust.Assets.Localization;
// using SurviveTheRust.Assets.Scripts.InputActions;
// using SurviveTheRust.Assets.Scripts.Localization;
// using SurviveTheRust.Assets.Scripts.Managers;
// using SurviveTheRust.Assets.Scripts.PlayerScripts.Camera;
// using SurviveTheRust.Assets.Scripts.PlayerScripts.Locomotion;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class UserInputManager : MonoBehaviour, PlayerInputActions.IPlayerActionMapActions,
    PlayerInputActions.IUIActionMapActions
{
    private static readonly UserInputManager _instance = null;

    public static UserInputManager Instance
    {
        get => (_instance == null) ? FindObjectOfType<UserInputManager>() : _instance;
    }

    private static Camera MainCamera => Camera.main;

    // public CameraLook camLook = new CameraLook();
    // public FPSPlayerController playerController => FindObjectOfType<FPSPlayerController>();

    [System.Serializable]
    public struct PlayerInputs
    {
        public static PlayerInputActions PlayerActions => new();

        [SerializeField]
        public static InputActionMap PlayerActionMap =>
            PlayerActions.PlayerActionMap; //=> new PlayerInputActions.PlayerActionMapActions().Get();

        // public InputActionMap playerLocomotionActionMap => new PlayerInputActions.PlayerLocomotionActionMapActions().Get();
        [SerializeField] public static InputActionMap UIActionMap => new PlayerInputActions.UIActionMapActions().Get();

        [SerializeField] public List<InputActionMap> PlayerInputActionsMaps;

        // public PlayerInputs(InputActionMap PlayerActionMap, InputActionMap UIActionMap){
        //     // for (int i = 0; i < 2; i++)
        //     // {
        //         PlayerInputActionsMaps.Add(PlayerActionMap);
        //         PlayerInputActionsMaps.Add(UIActionMap);
        //     // }
        // }
    }

    // private delegate void DeviceChangedEventHandler();
    // private static event DeviceChangedEventHandler onDeviceChangeEvent;

    public UnityEvent onDeviceChangeUnityEvent;

    [SerializeField] public PlayerInputs playerInputs;

    [SerializeField] PlayerInput playerInput;

    public List<InputActionMap> PlayerInputActionMaps;
    // public List<InputActionMap> PlayerInputActionMaps
    // {
    //     get
    //     {
    //         if (playerInputActionMaps == null)
    //         {
    //             playerInputActionMaps = new List<InputActionMap>();

    //             foreach (InputActionMap map in PlayerInputActionsAsset.actionMaps)
    //                 playerInputActionMaps.Add(map);
    //         }
    //         return playerInputActionMaps;
    //     }
    // }

    public List<InputAction> PlayerInputActions;
    // public List<InputAction> PlayerInputActions
    // {
    //     get
    //     {
    //         if (playerInputActions == null)
    //         {
    //             playerInputActions = new List<InputAction>();

    //             foreach (InputActionMap actionMaps in PlayerInputActionMaps)
    //                 foreach (InputAction action in actionMaps.actions)
    //                     playerInputActions.Add(action);
    //         }
    //         return playerInputActions;
    //     }
    // }

    //public InputActionAsset playerInputActionsAsset;
    //public InputActionAsset PlayerInputActionsAsset
    //{
    //    get
    //    {
    //        if (playerInputActionMaps == null)
    //        {
    //            playerInputActionMaps = new List<InputActionMap>();
    //            foreach (InputActionMap actionsMap in PlayerInputActionsAsset.actionMaps)
    //            {
    //                playerInputActionMaps.Add(actionsMap);
    //                if (playerInputActions == null)
    //                {
    //                    playerInputActions = new List<InputAction>();
    //                    foreach (InputAction action in actionsMap.actions)
    //                        playerInputActions.Add(action);
    //                }
    //            }
    //        }

    //        return playerInputActionsAsset; //?? (playerInputActionsAsset = new PlayerInputActions().asset)
    //    }
    //}

    // private SerializedObject serializedObject;

    // private void OnValidate() {
    //     serializedObject = new SerializedObject(this);
    // }    

    // public override void OnInspectorGUI(){

    // }

    [SerializeField] private InputActionAsset PlayerInputActionsAsset;

    // [SerializeField] InputActionAsset PlayerInputActionsAsset;
    // [SerializeField] 

    // public PlayerInput test;

    private bool _isAiming;
    private bool _isCrouched;
    private bool _isSprinting;

    public bool IsAiming
    {
        get => _isAiming;
        set => _isAiming = value;
    }

    //private InputActionMap PlayerActionMap => new PlayerInputActions.PlayerActionMapActions().Get();
    private InputActionMap PlayerActionMap;

    //private InputActionMap UIActionMap => new PlayerInputActions.UIActionMapActions().Get();
    private InputActionMap UIActionMap;

    private void Awake()
    {
        PlayerActionMap = PlayerInputActionsAsset.FindActionMap("PlayerActionMap");
        UIActionMap = PlayerInputActionsAsset.FindActionMap("UIActionMap");

        enemyDeaths = Convert.ToInt32(UIManager.EnemyDeathText?.text);

        //enemyDeaths = int.Parse(UIManager.EnemyDeathText.text);

        // onDeviceChangeEvent += CheckDeviceChange;
    }

    //private void Awake()
    //{
    //    //Debug.Log($"{PlayerActionMap.name} | {UIActionMap.name}");

    //    //List<string> ActionMapNames = new List<string>();
    //    //List<InputActionMap> actionMaps = new List<InputActionMap>();

    //    //foreach (InputActionMap actionMaps in PlayerInputActionsAsset.actionMaps)
    //    //{
    //    //    Debug.Log(actionMaps.name);
    //    //    PlayerInputActionMaps.Add(actionMaps);
    //    //}


    //    //PlayerInputActionMaps.Where(x => actionMaps.name = (x.name == actionMaps.name) ? actionMaps.name.Replace("", "") : "");
    //    //PlayerInputActionMaps[0].name = ;

    //    //foreach (InputAction action in actionsMap.actions)
    //    //{
    //    //    PlayerInputActions.Add(action);
    //    //}

    //    //foreach(InputActionMap actionMaps in PlayerInputActionMaps)
    //    //{
    //    //    PlayerInputActionMaps.Where(x => x.name.Replace("", PlayerInputActionsAsset.actionMaps.ToList().);
    //    //}

    //    //playerInputActionsAsset = new PlayerInputActions().asset;
    //    //playerInputActionsMaps.Add(PlayerInputs.PlayerActionMap);
    //    //playerInputs.PlayerActionMap;

    //    //PlayerInputActionsAsset = test.asset;

    //    //for (int i = 0; i < PlayerInputActionsMaps.Count(); i++)
    //    //{
    //    //    // PlayerInputActionsMaps[i].asset.name = PlayerInputActionsAsset.actionMaps[i].name;
    //    //    // Debug.Log(PlayerInputActionsAsset.actionMaps[i].name);
    //    //}
    //    //foreach (InputActionMap actionMap in PlayerInputActionsAsset.actionMaps)
    //    //{
    //    //    // PlayerInputActionsMaps[test.asset.actionMaps.IndexOfReference(actionMap)].name;
    //    //    // test.asset.actionMaps.IndexOfReference(actionMap);
    //    //    // Debug.Log(actionMap.name);
    //    //    // Debug.Log(PlayerInputActionsMaps[test.asset.actionMaps.IndexOfReference(actionMap)].name);
    //    //}
    //    //testMap = playerLocomotion.PlayerLocomotionActionMap.Get();
    //    //PlayerInputActionsMaps = PlayerInputActionsAsset.actionMaps.ToList(); //.ToList()

    //}

    // Start is called before the first frame update

    //private void Awake()
    //{
    //    Debug.Log(InputSystem.devices);

    //    //playerInput.SwitchCurrentControlScheme();
    //}

    private int enemyDeaths = 0;

    void Start()
    {
        Debug.Log($"{PlayerActionMap.name} | {UIActionMap.name}");

        // camLook.InitSettings(playerController.transform, MainCamera.transform); //Camera.main.transform

        //enemyDeaths = int.Parse(UIManager.EnemyDeathText.text);
        UIManager.LocalEnemyDeathText.text = enemyDeaths.ToString();
        Debug.Log("UserInputManager Started");
    }

    // public void OnDeviceLost(PlayerInput userInput)
    // {
    //     var test = userInput.user.controlScheme;
    //     Debug.Log(test);
    //     //+= PlayerInput.DeviceLostMessage;
    //
    //     //Debug.Log("Test");
    //
    //     //switch (userInput)
    //     //{
    //     //    case PlayerInput.:
    //     //    default:
    //     //        break;
    //     //}
    // }

    //InputDeviceChange deviceChange

    //Could Improve performance since the list is cached
    //private static List<JsonTextLocalizer> JsonTextLocalizers => FindObjectsOfType<JsonTextLocalizer>().ToList();

    //Could Improve performance since the list is cached
    List<SpreadSheetTextLocalizer> SpreadSheetTextLocalizers => FindObjectsOfType<SpreadSheetTextLocalizer>().ToList();

    //readonly KeyCode pressedKey = new KeyCode();

    void IncrementDeathCounter()
    {
        //enemyDeaths += 1;
        UIManager.LocalEnemyDeathText.text = (enemyDeaths += 1).ToString();
    }

    Vector2 mousePosition;

    private void Update()
    {
        //Debug.Log(mousePosition);

        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    StartCoroutine(APIRequestManager.CreateDeathTracker(UIManager.Steven));
        //    StartCoroutine(APIRequestManager.CreateDeathTracker(new EntityJsonFormater { Name = "David", Count = 5 }));
        //}

        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    StartCoroutine(APIRequestManager.GetDeathTrackerRequest(GameManager.Enemy, UIManager.EnemyDeathText));
        //}

        //if (Input.GetKeyDown(KeyCode.U))
        //    IncrementDeathCounter();

        //if (Input.GetKeyDown(KeyCode.I))
        //{
        //    StartCoroutine(APIRequestManager.IncrementDeathTracker(enemyDeaths));
        //    StartCoroutine(APIRequestManager.GetDeathTrackerRequest(GameManager.Enemy, UIManager.EnemyDeathText));
        //}
    }

    // Update is called once per frame
    // void Update()
    // {
    //     // CheckDeviceChange();
    //
    //     //string language = "";
    //
    //     //if (Input.GetKeyDown(KeyCode.Alpha1))
    //     //    ReadJsonLocalizationData.CurrentLanguage = "en";
    //     //else if (Input.GetKeyDown(KeyCode.Alpha2))
    //     //    ReadJsonLocalizationData.CurrentLanguage = "fr";
    //     //else if (Input.GetKeyDown(KeyCode.Alpha3))
    //     //    ReadJsonLocalizationData.CurrentLanguage = "es";
    //     //else if (Input.GetKeyDown(KeyCode.Alpha4))
    //     //    ReadJsonLocalizationData.CurrentLanguage = "de";
    //     //else if (Input.GetKeyDown(KeyCode.Alpha5))
    //     //    ReadJsonLocalizationData.CurrentLanguage = "ru";
    //
    //     //if (!string.IsNullOrWhiteSpace(ReadJsonLocalizationData.CurrentLanguage))
    //     //{
    //     //    //ReadJsonLocalizationData.CurrentLanguage = language;
    //
    //     //    //SpreadSheetTextLocalizers.ForEach(spreadSheetTextLocalizer => spreadSheetTextLocalizer.LocateKey());
    //     //    //FindObjectsOfType<TextLocalizer>().ToList().ForEach(textLocalizer => textLocalizer.LocateKey());
    //
    //     //    //foreach (JsonTextLocalizer textLocalizer in JsonTextLocalizers)
    //     //    //    textLocalizer.LocateKey();
    //     //    //              ==
    //     //    JsonTextLocalizers.ForEach(jsonTextLocalizer => jsonTextLocalizer.LocateKey());
    //     //}
    //
    //
    //     //switch (pressedKey)
    //     //{
    //     //    case KeyCode.Alpha1:
    //     //        language = "en";
    //     //        break;
    //     //    case KeyCode.Alpha2:
    //     //        language = "fr";
    //     //        break;
    //     //    case KeyCode.Alpha3:
    //     //        language = "es";
    //     //        break;
    //     //    case KeyCode.Alpha4:
    //     //        language = "de";
    //     //        break;
    //     //    case KeyCode.Alpha5:
    //     //        language = "ru";
    //     //        break;
    //     //    default:
    //     //        language = "en";
    //     //        break;
    //     //        //KeyCode.Alpha1 => language = "en",
    //     //        //    KeyCode.Alpha2 => language = "fr",
    //     //        //    KeyCode.Alpha3 => language = "es",
    //     //        //    KeyCode.Alpha4 => language = "de",
    //     //        //    KeyCode.Alpha5 => language = "ru",
    //     //        //    _ => language = "en",
    //     //};
    //
    //     //if (Input.anyKeyDown)
    //     //    language = pressedKey switch
    //     //    {
    //     //        KeyCode.Alpha1 => "en",
    //     //        KeyCode.Alpha2 => "fr",
    //     //        KeyCode.Alpha3 => "es",
    //     //        KeyCode.Alpha4 => "de",
    //     //        KeyCode.Alpha5 => "ru",
    //     //        _ => "",
    //     //    };
    //
    //     //Debug.Log(language);
    // }

    public void CheckDeviceChange()
    {
        InputSystem.onDeviceChange += (device, change) =>
        {
            switch (change)
            {
                // case InputDeviceChange.Added:
                //     // InputSystem.devices
                //     Debug.Log($"{device.displayName} was added");
                //     break;
                // case InputDeviceChange.Removed:
                //     Debug.Log($"{device.displayName} was removed");
                //     //Debug.Log(InputSystem.devices);
                //     break;
                case InputDeviceChange.Disconnected:
                    onDeviceChangeUnityEvent?.Invoke();
                    Debug.Log($"{device.displayName} was disconnected");
                    // InputSystem.devices.ToList().ForEach(x => Debug.Log(x.device.displayName));
                    //Debug.Log(InputSystem.devices);
                    break;
                case InputDeviceChange.Reconnected:
                    onDeviceChangeUnityEvent?.Invoke();
                    Debug.Log($"{device.displayName} was reconnected");
                    // InputSystem.devices.ToList().ForEach(x => Debug.Log(x.device.displayName));
                    //Debug.Log(InputSystem.devices);
                    break;
                    // case InputDeviceChange.Enabled:
                    //     Debug.Log($"{device.displayName} was Enabled");
                    //     break;
                    // case InputDeviceChange.Disabled:
                    //     Debug.Log($"{device.displayName} was Disabled");
                    //     InputSystem.devices.ToList().ForEach(x => Debug.Log(x.device.displayName));
                    //     break;
            }

            //InputSystem.devices.ToList().ForEach(x => Debug.Log((x.enabled) ? x.displayName + " is active " : x.device + " is inactive "));
            // Debug.Log();
        };
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        Debug.Log(playerInput.currentActionMap.name);

        switch (context.phase)
        {
            case InputActionPhase.Disabled:
                break;
            case InputActionPhase.Waiting:
                break;
            case InputActionPhase.Started:
                break;
            case InputActionPhase.Performed:
                playerInput.SwitchCurrentActionMap(playerInput.currentActionMap.name == PlayerActionMap.name ? UIActionMap.name : PlayerActionMap.name);
                Debug.Log("performed");
                break;
            case InputActionPhase.Canceled:
                Debug.Log("Canceled");
                break;
                // default:
                //     break;
        }

        //InputActionMap actionMap = playerInputActionMaps.;
        //playerInput.SwitchCurrentControlScheme();
        //playerInput.SwitchCurrentActionMap(playerInput.currentActionMap.name == PlayerActionMap.name
        //    ? UIActionMap.name
        //    : PlayerActionMap.name);

        Debug.Log(playerInput.currentActionMap.name);

        // Debug.Log(playerInput.currentActionMap.name);
        //
        // switch (context.phase)
        // {
        //     case InputActionPhase.Disabled:
        //         break;
        //     case InputActionPhase.Waiting:
        //         break;
        //     case InputActionPhase.Started:
        //         break;
        //     case InputActionPhase.Performed:
        //         Debug.Log("performed");
        //         break;
        //     case InputActionPhase.Canceled:
        //         Debug.Log("Canceled");
        //         break;
        //     // default:
        //     //     break;
        // }
        //
        // //InputActionMap actionMap = playerInputActionMaps.;
        // //playerInput.SwitchCurrentControlScheme();
        // if (playerInput.currentActionMap.name == PlayerActionMap.name)
        //     playerInput.SwitchCurrentActionMap(UIActionMap.name);
        // else
        //     playerInput.SwitchCurrentActionMap(PlayerActionMap.name);
        //
        // Debug.Log(playerInput.currentActionMap.name);
    }

    #region - Player UI Settings -

    public void OnClick(PlayerInputActions.UIActionMapActions context)
    {
        //InputAction.CallbackContext context
    }

    public void OnNavigate(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }

    public void OnSubmit(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }

    public void OnCancel(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }

    public void OnPoint(InputAction.CallbackContext context)
    {
        Mouse.current.WarpCursorPosition(new Vector2(0, 0));

        float mousePosition = context.ReadValue<Vector2>().x;

        Debug.Log(mousePosition);

        //context.ReadValue<Vector2>();

        //throw new NotImplementedException();
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        context.ReadValueAsButton();
    }

    public void OnScrollWheel(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }

    public void OnMiddleClick(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }

    public void OnRightClick(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }

    public void OnTrackedDevicePosition(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }

    public void OnTrackedDeviceOrientation(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }

    #endregion

    #region - Player Setting -

    public void OnMove(InputAction.CallbackContext context)
    {
        //playerController
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
    }

    public void OnJump(InputAction.CallbackContext context)
    {
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
    }

    public void OnChangeLanguage(InputAction.CallbackContext context)
    {
        //if (!context.performed) return;
        //var language = ""; //String

        //language = context.control.path switch
        //{
        //    "/Keyboard/1" => "en",
        //    "/Keyboard/2" => "fr",
        //    "/Keyboard/3" => "es",
        //    "/Keyboard/4" => "de",
        //    "/Keyboard/5" => "ru",
        //    _ => "en",
        //};
        //if (string.IsNullOrWhiteSpace(language)) return;
        //ReadJsonLocalizationData.CurrentLanguage = language;
        //JsonTextLocalizers.ForEach(jsonTextLocalizer => jsonTextLocalizer.LocateKey());

        // if (context.performed)
        // {
        //     string language = "";
        //
        //     language = context.control.path switch
        //     {
        //         "/Keyboard/1" => "en",
        //         "/Keyboard/2" => "fr",
        //         "/Keyboard/3" => "es",
        //         "/Keyboard/4" => "de",
        //         "/Keyboard/5" => "ru",
        //         _ => "en",
        //     };
        //     if (!string.IsNullOrWhiteSpace(language))
        //     {
        //         ReadJsonLocalizationData.CurrentLanguage = language;
        //         JsonTextLocalizers.ForEach(jsonTextLocalizer => jsonTextLocalizer.LocateKey());
        //     }
        // }
    }

    #endregion

    #region - Player Look Settings -

    public void OnLook(InputAction.CallbackContext context)
    {
        // camLook.CameraLookRotation(context.ReadValue<Vector2>(), playerController.transform, MainCamera.transform,
        //     this); //Camera.main.transform
    }

    public void OnAim(InputAction.CallbackContext context)
    {
    }

    #endregion

    #region - Player Weapon Settings -

    public void OnShoot(InputAction.CallbackContext context)
    {
    }

    public void OnReload(InputAction.CallbackContext context)
    {
    }

    public void OnSwitchWeapon(InputAction.CallbackContext context)
    {
    }

    #endregion
}
