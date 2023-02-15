using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Loading : MonoBehaviour
{
    private AsyncOperation async;
    [SerializeField] private Text txtPercent;
    [Header("Use waitForUserInput for Cutscene or Title Screen")]
    [SerializeField] private bool waitForUserInput = false;
    [SerializeField] private GameObject txtWaitForUserInput;
    [SerializeField] private GameObject loading;
    private bool ready = false;
    [Header("Use only if not using WaitForUserInput")]
    [SerializeField] private float delay = 0f;
    [Header("LoadSceneByName override Build Index")]
    [Tooltip("Leave this field empty if you want to use the Build Index")]
    [SerializeField] private string loadSceneByName = "";
    [Header("Only in use if loadSceneByName is empty")]
    [Tooltip("-1 = Load next scene, any positive number = build index")]
    [SerializeField] private int loadSceneByIndex = -1;

    private bool anyKey = false;

    // Start is called before the first frame update
    void Start()
    {
        InitSettings();
        LoadScene();        
    }

    public void OnAnyKey(InputAction.CallbackContext context) 
    {
        anyKey = context.performed;
    }
    
    void InitSettings()
    {
        Time.timeScale = 1.0f;
        Input.ResetInputAxes();
        System.GC.Collect();
    }
    
    void LoadScene()
    {
        if (loadSceneByName != "") //If I have a name
        {
            async = SceneManager.LoadSceneAsync(loadSceneByName);
        }
        else if (loadSceneByIndex < 0 || loadSceneByIndex > SceneManager.sceneCountInBuildSettings - 1) //If I don't have a valid build index
        {
            Scene currentScene = SceneManager.GetActiveScene();
            async = SceneManager.LoadSceneAsync(currentScene.buildIndex + 1);
        }        
        else //Use build index
        {            
            async = SceneManager.LoadSceneAsync(loadSceneByIndex);
        }

        async.allowSceneActivation = false;
        if (!waitForUserInput)
        {
            Invoke("Activate", delay);
        }
    }

    public void Activate() 
    {
        ready = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (txtPercent)
            txtPercent.text = ((async.progress + 0.1f) * 100).ToString("f2") + "%";
        
        if (SplashScreen.isFinished && waitForUserInput)
        {
            if (txtWaitForUserInput) 
            {
                //ready = true;
                txtWaitForUserInput.SetActive(true);
                loading.SetActive(false);
            }           

            if (anyKey) 
            {                
                ready = true;
                anyKey = false;
            }
        }   

        if (SplashScreen.isFinished && ready)
        {
            async.allowSceneActivation = true;
        }
    }
}
