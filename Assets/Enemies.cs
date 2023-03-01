using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    Animator anim;

    public float Health
    {
        set
        {
            health = value;
            
            if(health <= 0)
            {
                Defeated();
            }         
        }

        get
        {
            return health;
        }
    }
    
    public  float health = 1;
    public float damage = 10;
    public float knockBackForce = 500f;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void  Defeated()
    {
        anim.SetTrigger("dead");
    }

    public void Removed()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D collider = collision.collider;
        IDamageble damageble = collider.GetComponent<IDamageble>();

        if(damageble != null)
        {
            //Offset for collision detection changes the direction
            Vector2 direction = (collider.transform.position - transform.position).normalized;

            //Knockback is direction of the enemies
            Vector2 knockback = direction * knockBackForce;

            //After making sure the collider has a script we can run the Onhit implementation and pass our vector 2 force
            damageble.OnHit(damage, knockback);

        }
    }
}
