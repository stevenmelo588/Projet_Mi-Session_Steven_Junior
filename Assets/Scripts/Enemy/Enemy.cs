using System;
using System.Collections;
// using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer), typeof(PolygonCollider2D), typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    // private bool isDead;

    // public static Enemy Instance { get; private set; }

    // [SerializeField] 
    public EnemyScriptableObjects enemyScriptableOBJ;
    [SerializeField] private EnemyHealthController enemyHealthController;

    //float SPEED = enemyScriptableOBJ.Speed;
    private const float SPEED = EnemyScriptableObjects.SPEED;
    private const float COLOR_SHIFT_TIME = 0.05f;

    // private float EnemyHealth = 1; //Change to Scriptable Object to be more precise

    // [SerializeField]
    // private float knockBackStrength = 5, resetDelay = 0.01f;

    // private GameObject player => GameObject.FindGameObjectWithTag("Player");
    private GameObject Player => GameManager.Instance.Player;

    [SerializeField] private SpriteRenderer enemySpriteRenderer;
    public Rigidbody2D enemyRigidbody2D;
    public Collider2D enemyCollider;

    // public UnityEvent<GameObject> OnHitEvent, OnDeathEvent;

    // [Header("Animations")]
    // [SerializeField] 
    public Animator EnemyAnimator;
    // [SerializeField] private const string IDLE_ANIMATION    = "IDLE_STATE"; 
    // [SerializeField] private const string HIT_ANIMATION     = "HIT_STATE"; 
    // [SerializeField] private const string DEATH_ANIMATION   = "DEATH_STATE"; 
    // [SerializeField] private const string WALK_ANIMATION    = "WALK_STATE";

    // [SerializeField] private const string WALK_LEFT_ANIMATION   = "WALK_LEFT_STATE"; 

    // private float currentEnemyHealth;
    // private bool hittingPlayer = false;    

    //private void OnEnable()
    //{
    //    StartCoroutine(MoveTowardsPlayerCoroutine());
    //    //EnemyAnimator.Play(enemyScriptableOBJ.IDLE_WALK_STATE);
    //}
    private void Awake()
    {
        // if (Instance != this || Instance == null)
        //     Instance = this;

        // isDead = enemyHealthController.isDead;

        // enemySpriteRenderer.sprite = enemyScriptableOBJ.EnemySprite;
        enemyHealthController.InitEnemyHealth(enemyScriptableOBJ.MaxHealth);
    }
    void Start()
    {
        EnemyAnimator.Play(enemyScriptableOBJ.IDLE_WALK_STATE);
        // sprite.transform.position;

        // enemyCollider.enabled = true;
        // enemyRigidbody2D.WakeUp();
        // Debug.Log(1 * 0.5);

        // enemyCollider.transform.position = enemySpriteRenderer.transform.position;
        // Debug.Log($"{playerLocation} | {enemySpriteRenderer.transform.position} | {enemyCollider.transform.position}");
        // playerLocation = ;
        // MoveTowardsPlayer();
        StartCoroutine(MoveTowardsPlayerCoroutine());
    }
    void Update()
    {
        //if (enemyHealthController.isDead)       
        //    Death();         

        if (transform.position.x > 0)
            enemySpriteRenderer.flipX = true;

        // enemyCollider.transform.position = enemySpriteRenderer.sprite.rect.position;
        // if(isDead){
        //     StopCoroutine(MoveTowardsPlayer());
        //     StartCoroutine(DisableEnemy());
        // }

        // else
        //     enemySpriteRenderer.flipX = false;
        // enemyCollider.transform.position = enemySpriteRenderer.transform.position;
        // Debug.Log($"{playerLocation} | {enemySpriteRenderer.transform.position} | {enemyCollider.transform.position}");
    }

    public void PlayHitAnimation()
    {
        EnemyAnimator.Play(enemyScriptableOBJ.HIT_STATE);
        //enemySpriteRenderer.color = Color.red;
        //enemySpriteRenderer.color = Color.white;
        //HitEffect();
        StartCoroutine(HitEffect());
    }

    //void HitEffect()
    //{
    //    enemySpriteRenderer.color = Color.red;
    //    Invoke(nameof(ResetEnemyColor), COLOR_SHIFT_TIME);
    //}

    // void ResetEnemyColor()
    // {
    //     enemySpriteRenderer.color = Color.white;
    // }

    IEnumerator HitEffect()
    {
        //yield return new WaitForSeconds(COLOR_SHIFT_TIME);
        //enemySpriteRenderer.color = Color.grey; 
        enemySpriteRenderer.color = Color.red;
        yield return new WaitForSeconds(COLOR_SHIFT_TIME);
        enemySpriteRenderer.color = Color.white;
        //enemySpriteRenderer.color = new Color(0.25f, 0, 0, 0.5f);
    }

    public void PlayDeathAnimation()
    {
        //enemySpriteRenderer.color = Color.Lerp(new Color(0, 0, 0, 1f), new Color(0, 0, 0, 0f), Time.deltaTime);
        EnemyAnimator.Play(enemyScriptableOBJ.DEATH_STATE);
    }


    public void Death()
    {
        //PlayDeathAnimation();
        //EnemyAnimator.Play(enemyScriptableOBJ.DEATH_STATE);
        //StopAllCoroutines();
        enemyCollider.enabled = false;
        EnemySpawner.enemyCount--;
        // Debug.Log(EnemySpawner.enemyCount);
        StartCoroutine(DisableEnemy());
    }

    // public void EnemyHit(int damageAmount, GameObject attackSender){
    //     if(isDead || attackSender.layer == gameObject.layer)
    //         return;
    //     // if(attackSender.layer == gameObject.layer)
    //         // return;

    //     currentEnemyHealth -= damageAmount;

    //     if(currentEnemyHealth > 0){
    //         EnemyAnimator.Play(enemyScriptableOBJ.HIT_STATE);
    //         OnHitEvent?.Invoke(attackSender);
    //     }
    //     else{
    //         EnemyAnimator.Play(enemyScriptableOBJ.DEATH_STATE);
    //         OnDeathEvent?.Invoke(attackSender);            
    //         isDead = true;
    //         EnemySpawner.enemyCount--;
    //     }
    // }

    // Start is called before the first frame update
    
    // public void Knockback(GameObject sender){
    //     StopAllCoroutines();
    //     EnemyAnimator.Play(enemyScriptableOBJ.HIT_STATE);
    //     Vector2 knockBackDirection = (transform.position - sender.transform.position).normalized;
    //     enemyRigidbody2D.AddForce(knockBackDirection * enemyScriptableOBJ.KnockBack, ForceMode2D.Impulse);
    //     StartCoroutine(ResetKnockBack());
    //     // enemyRigidbody2D.velocity = Vector2.zero;
    //     // ReceiveDamage(10);
    // }

    // public void ReceiveDamage(float damageAmount, GameObject sender){
    //     // EnemyAnimator.enabled = true;
    //     if(currentEnemyHealth > 0){
    //         // StopCoroutine(MoveTowardsPlayer());
    //         currentEnemyHealth -= damageAmount;
    //         Vector2 knockBackDirection = (transform.position - sender.transform.position).normalized;
    //         // enemyRigidbody2D.AddForce(knockBackDirection * knockBackStrength, ForceMode2D.Impulse);
    //         // enemyRigidbody2D.velocity = Vector2.zero;
    //         StartCoroutine(ResetKnockBack());
    //     }
    //     else{
    //         // StopCoroutine(MoveTowardsPlayer());
    //         EnemyAnimator.Play(enemyScriptableOBJ.DEATH_STATE);
    //         isDead = true;
    //         // enemyRigidbody2D.velocity = Vector2.zero; 
    //         // enemyCollider.enabled = false;
    //         // enemyRigidbody2D.Sleep();
    //         EnemySpawner.enemyCount--;
    //         Debug.Log(EnemySpawner.enemyCount);
    //         // StartCoroutine(DisableEnemy());
    //         // Knockback(player);
    //     }
    //         // enemyRigidbody2D.velocity = Vector2.zero;
    //     // EnemyAnimator.enabled = false;
    //     // EnemyAnimator.enabled = true;
    //         // enemyRigidbody2D.Sleep();
    // }

    // private void OnTriggerEnter2D(Collider2D other) {        
    //     Debug.Log($"{other.tag} was hit");
    //     Debug.Log(other == player.GetComponent<Collider2D>());
    //     Debug.Log($"{other.gameObject.layer} | {player.layer} | {other.gameObject.layer == player.layer} | {other.gameObject.layer != player.layer}");

    //     if(other.CompareTag("Player") && other == player.GetComponent<Collider2D>()){
    //         // hittingPlayer = true;
    //         // ReceiveDamage(10, player);
    //         // Knockback(player);
    //     }
    //     // else
    //     //     hittingPlayer = false;

    //     // if (other.CompareTag("Slime"))
    //     // {
    //     //     enemyRigidbody2D.velocity = Vector2.zero;
    //     //     // Physics2D.IgnoreCollision(enemyCollider, other);
    //     // }
    //     // else
    // }    

    // IEnumerator ResetKnockBack(){
    //     yield return new WaitForSeconds(enemyScriptableOBJ.KnockBackResetDelay);
    //     enemyRigidbody2D.velocity = Vector2.zero;
    //     // yield return new WaitForSeconds(resetDelay);
    //     // StartCoroutine(MoveTowardsPlayer());
    // }
    public void MoveToPlayer()
    {
        EnemyAnimator.Play(enemyScriptableOBJ.IDLE_WALK_STATE);
        //StartCoroutine(MoveTowardsPlayerCoroutine());
    }

    IEnumerator MoveTowardsPlayerCoroutine()
    {
        while (enemyHealthController.IsDead == false)
        {
            if (Vector2.Distance(transform.position, Player.transform.position) > 0.5f)
            {
                Vector2 moveToDirection = (Player.transform.position - transform.position).normalized;

                //EnemyAnimator.Play(enemyScriptableOBJ.IDLE_WALK_STATE);
                //transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, 2f * Time.fixedDeltaTime);
                //enemyRigidbody2D.AddForce(moveToDirection * enemyScriptableOBJ.Speed * Time.fixedDeltaTime);
                enemyRigidbody2D.AddForce(SPEED * Time.fixedDeltaTime * moveToDirection); //Supposedly provides better performance
            }
            // else
            // ReceiveDamage(10);
            yield return new WaitForSeconds(0f); //Default = 1f
        }
    }

    IEnumerator DisableEnemy()
    {
        // StopCoroutine(MoveTowardsPlayer());
        //enemyRigidbody2D.Sleep();
        //enemyCollider.enabled = false;
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
        // GC.Collect();
    }

    // private void FixedUpdate() {
    //     if(hittingPlayer)
    //         ReceiveDamage(10, player);        
    // }

    // Update is called once per frame
    

    // public void MoveTowardsPlayer(){
    //     while (isDead == false && hittingPlayer == false)
    //     {  
    //         if(Vector2.Distance(this.transform.position, player.transform.position) > player.GetComponent<CircleCollider2D>().radius){
    //             EnemyAnimator.Play(WALK_ANIMATION);
    //             transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, enemySO.Speed * Time.deltaTime);
    //         }
    //     }
    // }
}
