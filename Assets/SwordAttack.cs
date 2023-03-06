using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SwordAttack : MonoBehaviour
{
    public static SwordAttack Instance { get { return instance; } }
    private static SwordAttack instance;

    public float swordDamage = 3f;

    public BoxCollider2D swordColl;

    Vector2 attackOffest;

    [System.Serializable]
    public class SwordHitEnemyEvent : UnityEvent<Enemy> { }
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

    //public void Attack(float attackOffsetVector)
    //{
    //    swordColl.enabled = true;

    //    transform.localPosition.Set(attackOffsetVector, 0, 0);

    //    foreach (Collider2D collider in Physics2D.OverlapBoxAll(transform.position, swordColl.size, 0))
    //    {
    //        EnemyHealthController enemyHealth;
    //        if (enemyHealth = collider.GetComponent<EnemyHealthController>())
    //        {
    //            enemyHealth.EnemyHit(swordDamage, transform.gameObject);
    //        }
    //    }
    //}

    public void AttackRight()
    {
        swordColl.enabled = true;

        transform.localPosition = attackOffest;
        foreach (Collider2D collider in Physics2D.OverlapBoxAll(transform.position, swordColl.size, 0))
        {
            EnemyHealthController enemyHealth;
            if (enemyHealth = collider.GetComponent<EnemyHealthController>())
            {
                enemyHealth.EnemyHit(swordDamage, transform.gameObject);
            }
        }
    }

    public void AttackLeft()
    {
        swordColl.enabled = true;
        transform.localPosition = new Vector2(-attackOffest.x, attackOffest.y);
        foreach (Collider2D collider in Physics2D.OverlapBoxAll(transform.position, swordColl.size, 0))
        {
            EnemyHealthController enemyHealth;
            if (enemyHealth = collider.GetComponent<EnemyHealthController>())
            {
                enemyHealth.EnemyHit(swordDamage, transform.gameObject);
            }
        }
    }

    public void StopAttack() { swordColl.enabled = false; }

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    //foreach (Collider2D collider in Physics2D.OverlapCircleAll(transform.position, swordColl.radius))
    //    //{
    //        EnemyHealthController enemyHealth;
    //        if (enemyHealth = other.GetComponent<EnemyHealthController>())
    //        {
    //            enemyHealth.EnemyHit(1, transform.gameObject);
    //        }
    //    //}

    //    //if (other.tag == "Enemy")
    //    //{
    //    //    //Deal damage to the enemy
    //    //    Enemies enemies = other.GetComponent<Enemies>();

    //    //    if (enemies != null)
    //    //    {
    //    //        enemies.Health -= swordDamage;
    //    //        OnSwordHitEnemy.Invoke(enemies);
    //    //    }
    //    //}
    //}
}


