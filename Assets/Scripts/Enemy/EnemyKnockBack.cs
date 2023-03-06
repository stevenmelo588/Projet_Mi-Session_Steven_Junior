using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyKnockBack : MonoBehaviour
{
    Enemy CurrentEnemy => GetComponent<Enemy>();

    private Rigidbody2D EnemyRigidbody2D => CurrentEnemy.enemyRigidbody2D;

    public UnityEvent OnStart, OnDone;

    public void Knockback(GameObject attackSender)
    {
        StopAllCoroutines();
        OnStart?.Invoke();
        Vector2 knockBackDirection = (transform.position - attackSender.transform.position).normalized;
        //Vector2 knockBackDirection = (attackSender.transform.position - transform.position).normalized;

        Vector2 knockBack = knockBackDirection * CurrentEnemy.enemyScriptableOBJ.KnockBack;

        EnemyRigidbody2D.AddForce(knockBack, ForceMode2D.Impulse);

        StartCoroutine(ResetKnockBack());
    }

    private IEnumerator ResetKnockBack()
    {
        yield return new WaitForSeconds(CurrentEnemy.enemyScriptableOBJ.KnockBackResetDelay);
        EnemyRigidbody2D.velocity = Vector3.zero;
        OnDone?.Invoke();
    }
}
