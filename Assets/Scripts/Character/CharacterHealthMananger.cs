using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealthMananger : MonoBehaviour, IDamageble, ICloneable
{
    static private Canvas playerCanvas => GameManager.Instance.PlayerPanel;

    public GameObject life;
    Animator anim;
    Rigidbody2D rb;
    Collider2D col;
    bool isAlive = true;
    public bool disableSilumation = false;

    public static event Action onPlayerDeaf;

    [SerializeField] bool isInvicibleEnable = false;
    [SerializeField] float invicibleTime = 0.25f;
    float invicibleTimelaps = 0f;

    public float Health
    {
        set
        {
            if (value < _maxHealth)
            {  
                    RectTransform textTransform = ((GameObject)Clone()).GetComponent<RectTransform>();
                    textTransform.transform.position = Camera.main.WorldToScreenPoint(gameObject.transform.position);

                    Canvas canvas = FindObjectOfType<Canvas>();
                    
                    textTransform.SetParent(canvas.transform);
            }

            _maxHealth = value; 

            if (Health <= 0)
            {
                anim.SetBool("isAlive", false);
                Target = false;
                onPlayerDeaf?.Invoke();

            }
        }

        get
        {
            return _maxHealth;
        }
    }

    public bool Target
    {
        get { return _tragetable; }

        set { _tragetable = value;
            
           if(disableSilumation)
            { rb.simulated = false; }

            col.enabled = value;
        }
    
    }

    public bool Invicible { 
        get{ return _invicible; 
        }
        set
        {
            _invicible = value;
            if(_invicible == true)
            {
                invicibleTimelaps = 0f;
            }

        }
         
    }

    float _maxHealth = 100;

    bool _tragetable = true;

    bool _invicible = false;
   
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
        //make sur the player is alive
        anim.SetBool("isAlive", isAlive);
        //ani
        col = GetComponent<Collider2D>();
    }

    public void Onhit(float damage)
    {
        if (!Invicible)
        {
            Health -= damage;

            Heatlhtext.dmgText = damage.ToString();

            if (isInvicibleEnable)
            {
                //Active inviciblity and timer 
                Invicible = true;
            }
        }
       
    }

    public void OnObjectDestroy()
    {
        Destroy(gameObject);
    }

    void IDamageble.OnHit(float damage, Vector2 knockBack)
    {
        if (!Invicible)
        {
            Health -= damage;
            //apply force to move the player  when the enemies hit him
            rb.AddForce(knockBack, ForceMode2D.Impulse);

            if(isInvicibleEnable)
            {
                //Active inviciblity and timer 
                Invicible = true;
            }
        }
    }

    public void FixedUpdate()
    {
        if(Invicible)
        {
            invicibleTimelaps += Time.deltaTime;

            if(invicibleTimelaps > invicibleTime)
            {
                Invicible = false;
            }
        }

    }

    public object Clone()
    {
        return Instantiate(life);
    }

}
