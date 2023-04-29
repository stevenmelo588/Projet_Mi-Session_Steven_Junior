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


    Animator anim;

    bool canMove = true;
    public SwordAttack attack;

    Collider2D swordColl;

    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    public const string PLAYER_IDLE = "PLAYER_IDLE";
    public const string PLAYER_WALK = "PLAYER_WALK";
    public const string PLAYER_ATTACK = "PLAYER_ATTACK";
    public const string PLAYER_DEATH = "PLAYER_DEATH";

    public int coins;

    public static PlayerController playerStats;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

     void Awake()
    {
        if (playerStats != null)
        {
            Destroy(playerStats);
        }
        else
        {
            playerStats = this;
        }
        DontDestroyOnLoad(this);
    }

    void FixedUpdate()
    {
        if (canMove)
        {
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
        if (direction != Vector2.zero)
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

    public void OnMove(InputValue Value)
    {//get input value for player movement
        movementInput = Value.Get<Vector2>();
    }

    public void SwordAttack()
    {

        LockMovement();
        if (spriteRenderer.flipX == true)
        {
            attack.AttackLeft();
        }
        else
        {
            attack.AttackRight();
        }
    }

    public void EndSwordAttack()
    {
        UnLockMovement();
        attack.StopAttack();
    }

    void OnFire()
    {
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

