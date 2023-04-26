using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    public Button[] lvlButton;

    // Start is called before the first frame update
    void Start()
    {
        int levelAt = PlayerPrefs.GetInt("levelAt", 2);

        // V�rifie si le tableau lvlButton est initialis� avant de l'utiliser
        if (lvlButton != null)
        {
            for (int i = 0; i < lvlButton.Length; i++)
            {
                // V�rifie si le bouton de niveau existe avant de l'utiliser
                if (lvlButton[i] != null)
                {
                    if (i + 2 > levelAt)
                    {
                        lvlButton[i].interactable = false;
                    }
                }
                else
                {
                    Debug.LogWarning("Le bouton de niveau � l'index " + i + " n'est pas assign�.");
                }
            }
        }
        else
        {
            Debug.LogWarning("Le tableau lvlButton n'est pas initialis�.");
        }
    }
}

