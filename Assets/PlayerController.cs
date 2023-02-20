using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    
    public float moveSpeed = 1.0f;
    public float collisionOffset = 0.5f;
    public ContactFilter2D movementFilter;
    List<RaycastHit2D> castCollision = new List<RaycastHit2D>();
    Vector2 movementInput;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // if movement input is not 0 try to move
    void FixedUpdate()
    {
        if(movementInput != Vector2.zero)
        {
            bool sucess = TryMove(movementInput);

            if(!sucess)
            {
                sucess = TryMove(new Vector2(movementInput.x, 0));
            }
        }
    }

    private bool TryMove(Vector2 direction)
    {
        //Check the potential collision 
        int count = rb.Cast(
        movementInput, //X and Y  values between -1 and 1 that represent the direction from the body to look for collision 
        movementFilter, //The setting that determine where a collision can occur on such as layers to collise with
        castCollision, // List of collision to store the found collision into after the cast is finished
        moveSpeed * Time.fixedDeltaTime + collisionOffset);// The amount to cast equal to the movement plus an offset

        if (count == 0)
        { rb.MovePosition(rb.position + movementInput * moveSpeed * Time.fixedDeltaTime);
            return  true; }
        else
        { return false; }
    }

    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }
}
