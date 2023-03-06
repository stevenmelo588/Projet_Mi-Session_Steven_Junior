using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;

// If we want to be more specific with what we clone we can this Interface 
// instead of the Generic ICloneable Interface
public interface IPrototype
{
    public GameObject Clone(GameObject objectToClone);
}

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    public GameObject Player { get => player; set => player = value; }
    public Canvas PlayerPanel { get => playerPanel; set => playerPanel = value; }

    //private LocomotionCharacterCrontroller playerInput;
    //private LoseHealth playerHealth;
    private VolumeSet volSet;
    //private Shoot shootManager;

    //[Header("--- Ammo TextMeshPro ---")]
    //[SerializeField] private TMP_Text ammoLeftTxt;
    //[Header("--- Zombies TextMeshPro ---")]
    //[SerializeField] private TMP_Text txtZombiesKilled;
    //[Header("--- Location TextMeshPro ---")]
    //[SerializeField] public TMP_Text txtLocation;
    [Header("--- UI/Player/Options Menu ---")]
    [SerializeField] private Canvas playerPanel;
    [SerializeField] private GameObject[] UICanvas;
    [SerializeField] private GameObject[] GameOverPanel;
    [SerializeField] private Selectable[] SelectedOnStart;

    //private const string txt = "0";
    //private bool isGameWinner = false;

    [HideInInspector] public bool pause = false;
    //[HideInInspector] public int zombiesKilled = 0;
    //public int totalEnemyCount = 100;

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

        //if (instance == null)
        //{
        //    instance = this;
        //}
        //else if (instance != this)
        //{
        //    Destroy(gameObject);
        //}
        //RefreshAmmo();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    IEnumerator Start()
    {
        //playerHealth = FindObjectOfType<LoseHealth>();
        //shootManager = GetComponent<Shoot>();
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

    //public void RefreshZombiesKilled()
    //{
    //    txtZombiesKilled.text = txt + zombiesKilled.ToString();
    //}

    public void SelectedMenuPanel(int panelIndex)
    {
        for (int i = 0; i < UICanvas.Length; i++)
        {
            UICanvas[i].SetActive(i == panelIndex);
            if (i == panelIndex)
            {
                SelectedOnStart[i].Select();
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
    }

    public void MainMenu()
    {
        // Cursor.visible = false;
        // Cursor.lockState = CursorLockMode.Confined;
        SceneManager.LoadSceneAsync(mainMenuLoadingLevel.sceneName);
        StartCoroutine(MainMenuCoroutine());
    }

    //public void RefreshAmmo() 
    //{
    //    //ammoLeftTxt.text = ammoLeft.ToString("D3");

    //    if (ammoLeft <= 0)
    //    {
    //        ammoLeftTxt.text = "Out of Ammo";            
    //    }
    //}



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

    //public void GameOver() 
    //{
    //    if (!isGameWinner && playerHealth.playerIsDead)
    //    {
    //        isGameWinner = false;
    //        PlayerPanel.SetActive(false);
    //        SelectedGameOverPanel(0);
    //        Invoke("MenuCanvasActive", 5f);
    //    }
    //}
}