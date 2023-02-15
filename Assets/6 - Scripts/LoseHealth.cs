using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoseHealth : MonoBehaviour
{
    private GameManager manager;
    [SerializeField] private Image playerHealth;
    private Collider[] colliders;
    private Collider rootCollider;

    public bool playerIsDead = false;

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<GameManager>();
        playerHealth.fillAmount = 1.0f;
        playerIsDead = false;
        rootCollider = GetComponent<Collider>();
        colliders = GetComponentsInChildren<Collider>();
        ActivateRagdoll(false);
    }

    void ActivateRagdoll(bool isActive)
    {
        foreach (Collider col in colliders)
        {
            if (col != rootCollider)
            {
                col.isTrigger = !isActive;
                col.attachedRigidbody.isKinematic = !isActive;
            }
        }
        rootCollider.attachedRigidbody.isKinematic = isActive;
        rootCollider.enabled = !isActive;
    }

    public void PlayerHealth()
    {
        if (playerHealth.fillAmount <= 0.0f)
        {
            Die();            
        }
        else
        {
            playerHealth.fillAmount -= 0.1f;
            playerIsDead = false;
        }        
    }

    public void Die() 
    {
        GetComponentInParent<Animator>().enabled = false;
        GetComponentInParent<LocomotionCharacterCrontroller>().enabled = false;
        GetComponentInParent<Shoot>().enabled = false;
        GetComponentInParent<CinemachineInputSystem>().enabled = false;
        ActivateRagdoll(true);
        playerIsDead = true;
        //manager.GameOver();
    }
}