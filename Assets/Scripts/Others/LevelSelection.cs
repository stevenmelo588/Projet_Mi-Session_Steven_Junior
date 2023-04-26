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

        // Vérifie si le tableau lvlButton est initialisé avant de l'utiliser
        if (lvlButton != null)
        {
            for (int i = 0; i < lvlButton.Length; i++)
            {
                // Vérifie si le bouton de niveau existe avant de l'utiliser
                if (lvlButton[i] != null)
                {
                    if (i + 2 > levelAt)
                    {
                        lvlButton[i].interactable = false;
                    }
                }
                else
                {
                    Debug.LogWarning("Le bouton de niveau à l'index " + i + " n'est pas assigné.");
                }
            }
        }
        else
        {
            Debug.LogWarning("Le tableau lvlButton n'est pas initialisé.");
        }
    }
}

