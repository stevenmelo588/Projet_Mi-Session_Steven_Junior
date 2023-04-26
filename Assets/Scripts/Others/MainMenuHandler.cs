using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MainMenuHandler: IMenuHandler
{
    private GameManager gameManager;

    public MainMenuHandler(GameManager gameManager)
    { this.gameManager = gameManager; }

    public bool CanHandle (string action)
    {
        return action == "MainMenu";
    }

    public void Handle (string action)
    { gameManager.MainMenu();}
}
