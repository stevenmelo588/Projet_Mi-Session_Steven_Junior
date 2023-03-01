using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyKnockBack : MonoBehaviour
{
    Enemy currentEnemy => GetComponent<Enemy>();

    private Rigidbody2D EnemyRigidbody2D => currentEnemy.enemyRigidbody2D;

    public UnityEvent OnStart, OnDone;
    //private bool hit = false;

    public void Knockback(GameObject attackSender)
    {
        StopAllCoroutines();
        OnStart?.Invoke();
        //hit = true;
        Vector2 knockBackDirection = (transform.position - attackSender.transform.position).normalized;
        //Vector2 knockBackDirection = (attackSender.transform.position - transform.position).normalized;

        Vector2 knockBack = knockBackDirection * currentEnemy.enemyScriptableOBJ.KnockBack;

        EnemyRigidbody2D.AddForce(knockBack, ForceMode2D.Impulse);

        Debug.Log(EnemyRigidbody2D.velocity);

        // Enemy.Instance.EnemyAnimator.Play(Enemy.Instance.enemyScriptableOBJ.HIT_STATE);
        //Vector2 knockBackDirection = (transform.position - attackSender.transform.position).normalized;
        // Vector2 knockBackDirection = new Vector2(this.transform.position.x, this.transform.position.y) - (Vector2)attackSender.transform.position;
        // print(((enemyRigidbody2D.position + knockBackDirection) * currentEnemy.enemyScriptableOBJ.KnockBack * 1000) * Time.fixedDeltaTime);
        // enemyRigidbody2D.MovePosition(((enemyRigidbody2D.position + knockBackDirection) * currentEnemy.enemyScriptableOBJ.KnockBack * 1000) * Time.fixedDeltaTime);
        // enemyRigidbody2D.MovePosition((knockBackDirection * currentEnemy.enemyScriptableOBJ.KnockBack) * Time.fixedDeltaTime);
        //enemyRigidbody2D.AddForce(knockBackDirection, ForceMode2D.Impulse);
        //currentEnemy.enemyCollider.enabled = false;
        StartCoroutine(ResetKnockBack());
        // enemyRigidbody2D.velocity = Vector2.zero;
        // ReceiveDamage(10);
    }

    private IEnumerator ResetKnockBack()
    {
        yield return new WaitForSeconds(currentEnemy.enemyScriptableOBJ.KnockBackResetDelay);
        //hit = false;
        //currentEnemy.enemyCollider.enabled = true;
        EnemyRigidbody2D.velocity = Vector3.zero;
        OnDone?.Invoke();
        // yield return new WaitForSeconds(resetDelay);
        // StartCoroutine(MoveTowardsPlayer());
    }

    //private void AddForce()
    //{
    //    Vector2 knockBackDirection = (transform.position - attackSender.transform.position).normalized;
    //    enemyRigidbody2D.AddForce(knockBackDirection, ForceMode2D.Impulse);

    //    //Vector2 knockBackDirection = (this.enemyRigidbody2D.position * currentEnemy.enemyScriptableOBJ.KnockBack).normalized * 100;
    //    //print(enemyRigidbody2D.position + knockBackDirection * Time.fixedDeltaTime);
    //    //enemyRigidbody2D.MovePosition(enemyRigidbody2D.position + knockBackDirection * Time.fixedDeltaTime);
    //}

    //private void FixedUpdate()
    //{
    //    // Debug.Log(this.enemyRigidbody2D.position);

    //    if (hit)
    //        AddForce();
    //}

    //// Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}
}
