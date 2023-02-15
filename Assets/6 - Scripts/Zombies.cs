using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Zombies : MonoBehaviour
{
    private GameManager manager;
    [SerializeField] private Image zombieHealth;   

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<GameManager>();
        zombieHealth.fillAmount = 1.0f;
        manager.RefreshZombiesKilled();
    }

    public void Hit() 
    {
        if (zombieHealth.fillAmount <= 0.0f)
        {
            manager.zombiesKilled++;
            gameObject.SetActive(false);
        }
        else
        {
            zombieHealth.fillAmount -= 0.1f;
        }
        manager.RefreshZombiesKilled();
    }

    
}