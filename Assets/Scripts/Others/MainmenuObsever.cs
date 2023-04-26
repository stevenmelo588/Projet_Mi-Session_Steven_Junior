using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MyObserver : IMenuObserver
{
    public void OnMenuAction(string action)
    {
        Debug.Log(action);
    }
}

public class MainmenuObserver : MonoBehaviour
{
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameManager.RegisterMenuObserver(new MyObserver());
    }

    private void OnDestroy()
    {
        gameManager.RemoveObserver(new MyObserver());
    }
}
