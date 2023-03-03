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

    [SerializeField] bool isInvicibleEnable = false;
    [SerializeField] float invicibleTime = 0.25f;
    float invicibleTimelaps = 0f;

    //Enemy enemy;
    //Heatlhtext heatlhtext;

    public float Health
    {
        set
        {
            if (value < _maxHealth)
            {
                //for(int i = 0; i < 1; i++)
                //{
                    //GameObject obj = ((GameObject)Clone());
                    RectTransform textTransform = ((GameObject)Clone()).GetComponent<RectTransform>(); //Default -> Instanciate(life)
                    textTransform.transform.position = Camera.main.WorldToScreenPoint(gameObject.transform.position);

                    Canvas canvas = FindObjectOfType<Canvas>();
                    //Canvas canvas = obj.GetComponentInParent<Canvas>();
                    textTransform.SetParent(canvas.transform);
                    //obj.GetComponent<Heatlhtext>().SpawnText(value);
                //}
                //anim.Play(PlayerController.)
                //anim.SetTrigger("Hit");

                //(Canvas) playerCanvas

            }

            _maxHealth = value; //_maxHealth -> default

            if (Health <= 0)
            {
                //anim.Play(PlayerController.PLAYER_DEATH);
                anim.SetBool("isAlive", false);
                Target = false;
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

            //enemy.enemyScriptableOBJ.
            //heatlhtext.TextMesh.text = damage.ToString();
            Heatlhtext.dmgText = damage.ToString();

            //Heatlhtext.setDamage?.Invoke(damage);

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
