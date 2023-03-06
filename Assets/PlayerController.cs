using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 1f;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;

    SpriteRenderer spriteRenderer;

    Vector2 movementInput = Vector2.zero;

    Rigidbody2D rb;

    //[SerializeField] 
    Animator anim;

    //bool isMoving = false;

    bool canMove = true;
    public SwordAttack attack;

    Collider2D swordColl;

    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    /*[SerializeField]*/ public const string PLAYER_IDLE = "PLAYER_IDLE";
    /*[SerializeField]*/ public const string PLAYER_WALK = "PLAYER_WALK";
    /*[SerializeField]*/ public const string PLAYER_ATTACK = "PLAYER_ATTACK";
    /*[SerializeField]*/ public const string PLAYER_DEATH = "PLAYER_DEATH";

    //private void Awake()
    //{
        
    //}

    // Start is called before the first frame update
    void Start()
    {
        //anim.Play(PLAYER_IDLE);
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if(canMove) {
            if (movementInput != Vector2.zero)
            {
                // if movement input is not 0 try to move
                bool success = TryMove(movementInput);

                if (!success)
                {
                    success = TryMove(new Vector2(movementInput.x, movementInput.y));
                }
                if (!success)
                {
                    success = TryMove(new Vector2(0, movementInput.y));
                }
                //anim.Play(PLAYER_WALK);
                anim.SetBool("isMoving", success);
            }
            else
                //anim.Play(PLAYER_IDLE);
            anim.SetBool("isMoving", false);

            //Set direction of sprite 
            if (movementInput.x < 0)
            { 
                spriteRenderer.flipX = true; 
            }
            else if (movementInput.x > 0)
            {
                spriteRenderer.flipX = false; 
            }
        }
    }

    bool TryMove(Vector2 direction)
    {
        if(direction != Vector2.zero)
        {
            int count = rb.Cast(direction,//X and Y values between -1 and 1 
                movementFilter,// Determine where a collisoin  
                castCollisions, //List of collision to store the found collision after the cast 
                moveSpeed * Time.fixedDeltaTime + collisionOffset * Time.fixedDeltaTime);// the amount to cast equal the movement and offest

            if (count == 0)
                rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
            return true;
        }
        //can't move 
        else { return false; }
    }
    //
    public void OnMove(InputValue Value)
    {//get input value for player movement
        movementInput = Value.Get<Vector2>();
    }

    public void SwordAttack()
    {
        //Debug.Log(Time.timeScale);
        //swordColl.transform.position.x
        //float attackDirection = (spriteRenderer.flipX) ? -1 : 1;
            //swordColl.transform.localPosition = -swordColl.transform.position 
        //attackDirection = (spriteRenderer.flipX) ? -swordColl.transform.position.x : swordColl.transform.position.x;

        //attack.Attack(attackDirection);

        LockMovement();
        if (spriteRenderer.flipX == true)
        {
            attack.AttackLeft();
        }
        else
        {
            attack.AttackRight();
        }

        //foreach (Collider2D collider in Physics2D.OverlapBoxAll(transform.position, GetComponent<BoxCollider2D>().size, 0))
        //{
        //    EnemyHealthController enemyHealth;
        //    if (enemyHealth = collider.GetComponent<EnemyHealthController>())
        //    {
        //        enemyHealth.EnemyHit(1, transform.gameObject);
        //    }
        //}
    }

    public void EndSwordAttack()
    {
        UnLockMovement();
        attack.StopAttack();
    }

    void OnFire()
    {
        //SwordAttack();
        //anim.Play(PLAYER_ATTACK);
        //EndSwordAttack();
        anim.SetTrigger("SwordAttack");
    }
    
    void LockMovement()
    {
        canMove = false;
    }

    void UnLockMovement()
    {
        canMove = true;
    }
}

