using System;
using System.Collections;
// using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer), typeof(CircleCollider2D), typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    public EnemyScriptableObjects enemyScriptableOBJ;
    [SerializeField] private EnemyHealthController enemyHealthController;

    private const float SPEED = EnemyScriptableObjects.SPEED;
    private const float COLOR_SHIFT_TIME = 0.05f;

    private GameObject Player => GameManager.Instance.Player;

    [SerializeField] private SpriteRenderer enemySpriteRenderer;
    public Rigidbody2D enemyRigidbody2D;
    public Collider2D enemyCollider;

    public Animator EnemyAnimator;

    private void Awake()
    {
        enemyHealthController.InitEnemyHealth(enemyScriptableOBJ.MaxHealth);
    }
    void Start()
    {
        EnemyAnimator.Play(enemyScriptableOBJ.IDLE_WALK_STATE);
        StartCoroutine(MoveTowardsPlayerCoroutine());
    }

    public void PlayHitAnimation()
    {
        EnemyAnimator.Play(enemyScriptableOBJ.HIT_STATE);
        StartCoroutine(HitEffect());
    }

    IEnumerator HitEffect()
    {
        enemySpriteRenderer.color = Color.red;
        yield return new WaitForSeconds(COLOR_SHIFT_TIME);
        enemySpriteRenderer.color = Color.white;
    }

    public void PlayDeathAnimation()
    {
        EnemyAnimator.Play(enemyScriptableOBJ.DEATH_STATE);
    }

    public void Death()
    {
        enemyCollider.enabled = false;
        StartCoroutine(DisableEnemy());
    }

    public void MoveToPlayer()
    {
        if(!enemyHealthController.IsDead)
            EnemyAnimator.Play(enemyScriptableOBJ.IDLE_WALK_STATE);
        //StartCoroutine(MoveTowardsPlayerCoroutine());
    }

    IEnumerator MoveTowardsPlayerCoroutine()
    {
        while (enemyHealthController.IsDead == false)
        {
            if (Vector2.Distance(transform.position, Player.transform.position) > 0.5f)
            {
                Vector2 moveToDirection = Vector2.ClampMagnitude((Player.transform.position - transform.position).normalized, SPEED);

                if (moveToDirection.x < 0)
                    enemySpriteRenderer.flipX = true;
                else
                    enemySpriteRenderer.flipX = false;

                enemyRigidbody2D.AddForce(SPEED * Time.deltaTime * moveToDirection); //Supposedly provides better performance
            }
            yield return new WaitForSeconds(0f); //Default = 1f
        }
    }

    IEnumerator DisableEnemy()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
        // GC.Collect();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Collider2D collider = collision.collider;
        IDamageble damageble = collider.GetComponent<IDamageble>();

        if (damageble != null)
        {
            damageble.Onhit(enemyScriptableOBJ.Damage);
        }
    }
}
