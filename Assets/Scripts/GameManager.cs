using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections.Generic;

// If we want to be more specific with what we clone we can this Interface 
// instead of the Generic ICloneable Interface
public interface IPrototype
{
    public GameObject Clone(GameObject objectToClone);
}

public interface IMenuObserver
{ void OnMenuAction(string action); }

public interface IMenuHandler
{ bool CanHandle(string action); void Handle(string action); }

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    private List<IMenuObserver> menuObservers = new List<IMenuObserver>();

    private List<IMenuHandler> menuHandlers = new List<IMenuHandler>();

    public GameObject gameOverMenu;
    public GameObject Player { get => player; set => player = value; }
    public Canvas PlayerPanel { get => playerPanel; set => playerPanel = value; }

    private VolumeSet volSet;

    
    [Header("--- UI/Player/Options Menu ---")]
    [SerializeField] private Canvas playerPanel;
    [SerializeField] private GameObject[] UICanvas;
    [SerializeField] private GameObject[] GameOverPanel;
    [SerializeField] private Selectable[] SelectedOnStart;

    [HideInInspector] public bool pause = false;
    public int totalEnemyCount = 100;

    [SerializeField] private Level gameLevel;
    [SerializeField] private Level mainMenuLevel;
    [SerializeField] private Level mainMenuLoadingLevel;
    [SerializeField] private Level restartGameLoadingLevel;
    // [SerializeField] private Level startMenuLoadingLevel;
    [SerializeField] private GameObject player;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
            instance = this;

        Player = GameObject.FindGameObjectWithTag("Player");
       
        menuHandlers.Add(new MainMenuHandler(this));

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    IEnumerator Start()
    {
        yield return new WaitForFixedUpdate();
        PlayerCanvasActive();
        //RefreshZombiesKilled();
    }

    IEnumerator RestartGameCoroutine()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadSceneAsync(gameLevel.sceneName);
    }

    IEnumerator MainMenuCoroutine()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadSceneAsync(mainMenuLevel.sceneName);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void RegisterMenuObserver(IMenuObserver observer)
    {
        menuObservers.Add(observer);
    }

    public void RemoveObserver(IMenuObserver observer)
    {
        menuObservers.Remove(observer);
    }

    public void SelectedMenuPanel(int panelIndex)
    {
        for (int i = 0; i < UICanvas.Length; i++)
        {
            UICanvas[i].SetActive(i == panelIndex);
            if (i == panelIndex)
            {
                SelectedOnStart[i].Select();
                foreach(IMenuObserver observer in menuObservers)
                {
                    observer.OnMenuAction("SelectedPanel" + panelIndex);
                }
            }
        }

        foreach(IMenuHandler handler in menuHandlers)
        {
            if (handler.CanHandle("SelectedPanel" + panelIndex))
            {
                handler.Handle("SelectedPanel" + panelIndex);
                break;
            }
        }
    }

    public void SelectedGameOverPanel(int panelIndex)
    {
        for (int i = 0; i < UICanvas.Length; i++)
        {
            GameOverPanel[i].SetActive(i == panelIndex);
        }
    }
    
    public void OnPause(InputAction.CallbackContext context)
    {
        if (pause = context.performed)
        {
            MenuCanvasActive();
        }
        else if (pause && pause == context.performed)
        {
            Resume();
        }
    }

    private void PlayerCanvasActive()
    {
        pause = false;
        PlayerPanel.gameObject.SetActive(true);
        for (int i = 0; i < UICanvas.Length; i++)
        {
            UICanvas[i].SetActive(false);
        }
        for (int i = 0; i < GameOverPanel.Length; i++)
        {
            GameOverPanel[i].SetActive(false);
        }
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void MenuCanvasActive()
    {
        pause = true;
        PlayerPanel.gameObject.SetActive(false);
        for (int i = 0; i < GameOverPanel.Length; i++)
        {
            GameOverPanel[i].SetActive(false);
        }
        SelectedMenuPanel(0);
        Time.timeScale = 0.0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void Resume()
    {
        PlayerCanvasActive();
        Time.timeScale = 1.0f;
    }

    public void BtnRetry()
    {
        SceneManager.LoadSceneAsync(restartGameLoadingLevel.sceneName);
        StartCoroutine(RestartGameCoroutine());
        Time.timeScale = 1.0f;
        PlayerCanvasActive();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        foreach (IMenuObserver observer in menuObservers)
        {
            observer.OnMenuAction("Retry");
        }

    }

    public void MainMenu()
    {
        SceneManager.LoadSceneAsync(mainMenuLoadingLevel.sceneName);
        StartCoroutine(MainMenuCoroutine());
        foreach (IMenuObserver observer in menuObservers)
        {
            observer.OnMenuAction("MainMenu");
        }
    }

    //public void GameWinner()
    //{
    //    if (!isGameWinner && !playerHealth.playerIsDead)
    //    {
    //        isGameWinner = true;
    //        PlayerPanel.SetActive(false);
    //        SelectedGameOverPanel(1);
    //        Invoke("MenuCanvasActive", 5f);
    //    }        
    //}
     
    public void EnableGameoverMenu()
    {
        gameOverMenu.SetActive(true);
    }

    private void OnEnable()
    {
        CharacterHealthMananger.onPlayerDeaf += EnableGameoverMenu;
    }

    private void OnDisable()
    {
        CharacterHealthMananger.onPlayerDeaf -= EnableGameoverMenu;
    }


}