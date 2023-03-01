using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealthController : MonoBehaviour
{
    // public static EnemyHealthController Instance { get; private set; } 

    public bool IsDead { get; private set; }
    private float currentEnemyHealth;

    public UnityEvent<GameObject> OnHitEvent;
    public UnityEvent OnDeathEvent;

    // private void Awake() {
    //     // if (Instance != this)
    //         Instance = this;
    //         // Destroy(gameObject);
    //     // else
    // }

    public void InitEnemyHealth(float enemyHealth)
    {
        currentEnemyHealth = enemyHealth;
        IsDead = false;
    }

    public void EnemyHit(float damageAmount, GameObject attackSender)
    {
        if (IsDead)
            return;
        //if (attackSender.layer == gameObject.layer)
        //    return;

        currentEnemyHealth -= damageAmount;
        OnHitEvent?.Invoke(attackSender);

        if (currentEnemyHealth == 0)
        {
            OnDeathEvent?.Invoke();
            IsDead = true;
            //currentEnemyHealth -= damageAmount;
        }
        //else
        //{
        //    // EnemyAnimator.Play(enemyScriptableOBJ.DEATH_STATE);
        //    // EnemySpawner.enemyCount--;
        //}

        //Debug.Log(currentEnemyHealth);
    }

    //// Start is called before the first frame update
    //void Start()
    //{

    //}

    // Update is called once per frame
    void Update()
    {
        if (currentEnemyHealth <= 0)
        {
            OnDeathEvent?.Invoke();
            IsDead = true;
        }
    }
}
