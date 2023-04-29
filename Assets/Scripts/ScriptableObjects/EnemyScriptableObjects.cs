using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyScriptableObject", menuName = "ScriptableObject/Enemies")]
public class EnemyScriptableObjects : ScriptableObject
{    
    public const float SPEED = 500f;

    [SerializeField] private float speed;

    [Header("Enemy Stats")]
    [SerializeField] private float shieldHealth;
    [SerializeField] private float maxHealth;
    [SerializeField] private float damage;
    [SerializeField] private float knockBack;
    [SerializeField] private float knockBackResetDelay;
    [SerializeField] private Sprite enemySprite;

    // [SerializeField] private Animator enemyAnimator;
    [Header("Animations")]
    [SerializeField] private string idleWalkState  = "IDLE_WALK_STATE";
    //[SerializeField] private string walkState  = "WALK_STATE";
    [SerializeField] private string hitState   = "HIT_STATE";
    [SerializeField] private string deathState = "DEATH_STATE";
    [SerializeField] private string attackStateRight = "ATTACK_STATE_RIGHT";
    [SerializeField] private string attackStateLeft = "ATTACK_STATE_LEFT";

    [Header("Enenmy loopDrop")]
    [SerializeField] private GameObject coinDrop;
    //private const float EnemySpeed = speed;

    public float ShieldHealth { get => shieldHealth; set => shieldHealth = value; }
    public float MaxHealth { get => maxHealth; set => maxHealth = value; }
    public float Damage { get => damage; set => damage = value; }
    public float Speed {
        get => speed;
        set => speed = (speed != 0) ? value : SPEED;
        //{
        //    if (speed != 0)
        //        speed = value;
        //    else
        //        speed = SPEED;
        //}
    }
    public float KnockBack { get => knockBack; set => knockBack = value; }
    public Sprite EnemySprite { get => enemySprite; set => enemySprite = value; }
    // public Animator EnemyAnimator { get => enemyAnimator; set => enemyAnimator = value; }

    // public string IDLE_ANIMATION => IDLE_STATE;

    // public string HIT_ANIMATION => HIT_STATE;

    // public string DEATH_ANIMATION => DEATH_STATE;

    // public string WALK_ANIMATION => WALK_STATE;

    public string IDLE_WALK_STATE { get => idleWalkState; set => idleWalkState = value; }
    public string HIT_STATE { get => hitState; set => hitState = value; }
    public string DEATH_STATE { get => deathState; set => deathState = value; }
    public string ATTACK_STATE_RIGHT { get => attackStateRight; set => attackStateRight = value; }
    public string ATTACK_STATE_LEFT { get => attackStateLeft; set => attackStateLeft = value; }
    //public string WALK_STATE { get => walkState; set => walkState = value; }
    public float KnockBackResetDelay { get => knockBackResetDelay; set => knockBackResetDelay = value; }
    public GameObject CoinDrop { get => coinDrop; set => coinDrop = value; }

    public EnemyScriptableObjects()
    {
        this.ShieldHealth = shieldHealth;
        this.MaxHealth = maxHealth;
        this.Damage = damage;
        this.Speed = speed;
        this.KnockBack = knockBack;
        this.KnockBackResetDelay = knockBackResetDelay;
        this.EnemySprite = enemySprite;
        this.CoinDrop = coinDrop;
    }
}
