using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuManager : MonoBehaviour
{
    [SerializeField] Level test;

    [Header("--- UI/Player/Options Menu ---")]
    [SerializeField] private GameObject[] UICanvas;
    [SerializeField] private Selectable[] SelectedOnStart;

    //// Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        SelectedPanel(0);
    }

    public void SelectedPanel(int panelIndex)
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

    public void BtnExit()
    {
        Application.Quit();
    }

    IEnumerator StartGameCoroutine()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(test.sceneName);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }
    public void BtnStart()
    {
        //SceneManager.LoadSceneAsync(2);
        StartCoroutine(StartGameCoroutine());
    }
}