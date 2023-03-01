using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SwordAttack : MonoBehaviour
{
    public static SwordAttack Instance { get { return instance; } }
    private static SwordAttack instance;

    public float swordDamage = 3f;

    public Collider2D swordColl;

    Vector2 attackOffest;

    [System.Serializable]
    public class SwordHitEnemyEvent : UnityEvent<Enemies> { }
    public SwordHitEnemyEvent OnSwordHitEnemy;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {
        attackOffest = transform.position;
    }

    public void AttackRight()
    {
        swordColl.enabled = true;
        transform.localPosition = attackOffest;
    }

    public void AttackLeft()
    {
        swordColl.enabled = true;
        transform.localPosition = new Vector2(-attackOffest.x, attackOffest.y);
    }

    public void StopAttack()
    { swordColl.enabled = false; }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            //Deal damage to the enemy
            Enemies enemies = other.GetComponent<Enemies>();

            if (enemies != null)
            {
                enemies.Health -= swordDamage;
                OnSwordHitEnemy.Invoke(enemies);
            }
        }
    }
}


