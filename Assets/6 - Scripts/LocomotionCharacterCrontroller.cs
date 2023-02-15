using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LocomotionCharacterCrontroller : MonoBehaviour
{
    private GameManager manager;
    private CharacterController controller;
    private Shoot shootController;
    private LoseHealth playerHP;

    //Physics, Ground and Movement Variables 
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpSpeed = 8.0f;
    [SerializeField] private float gravity = 20.0f;
    [SerializeField] private LayerMask allButPlayer;
    [SerializeField] private float distanceFromGround = 0.25f;
    private Vector3 playerVel;
    private bool IsGrounded = false;

    //Player LookAt Temporary
    [SerializeField] private Transform playerWalkLookAt;    

    //Animator Variables
    private Animator anim;
    private float animMagnitude;
    const string IsGROUNDED = "IsGrounded";
    const string MAGNITUDE = "Magnitude";
    const string JUMPING = "Jump";
    const string Y = "Y";
    const string DirectionX = "DirectionX";
    const string DirectionY = "DirectionY";    

    //Camera Variables
    private float angleOffset = 0f;
    private float moveAngle = 0f;
    private float lookAngle = 0f;
    [SerializeField] private Transform cam;
    [Range(0f, 0.3f)] [SerializeField] private float turnSmoothTime = 0.12f;
    private float refRotVelocity; 

    //InputSystem Variables
    private Vector2 move = Vector2.zero;
    private bool sprint = false;
    private bool jump = false;

    [SerializeField] private Transform playerSpineTransform;

    // Start is called before the first frame update
    void Start()
    {        
        playerHP        = GetComponentInChildren<LoseHealth>();
        controller      = GetComponent<CharacterController>();
        anim            = GetComponent<Animator>();
        shootController = GetComponent<Shoot>();
        manager         = GameManager.instance;

        Mathf.Clamp(playerSpineTransform.rotation.eulerAngles.x, -110, -40);
    }


    public void OnMove(InputAction.CallbackContext context)
    {        
        move = context.ReadValue<Vector2>();  
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        sprint = context.performed;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        jump = context.performed;
    }    

    bool CheckIfGrounded()
    {
        if (controller.isGrounded) return true;        

        return Physics.CheckSphere
            (transform.position,
             distanceFromGround,
             allButPlayer,
             QueryTriggerInteraction.Ignore);        
    }

    IEnumerator WaitForJumpAnim() 
    {
        anim.SetTrigger(JUMPING);
        yield return new WaitForSeconds(2);
        playerVel.y = jumpSpeed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        IsGrounded = controller.isGrounded;
        if (IsGrounded && !playerHP.playerIsDead)
        {               
            playerVel = new Vector3(move.x, 0f, move.y).normalized;
            angleOffset = cam.eulerAngles.y;

            Debug.Log(playerVel);

            moveAngle = Mathf.Atan2(playerVel.x, playerVel.z) * Mathf.Rad2Deg + angleOffset;
            lookAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, moveAngle, ref refRotVelocity, turnSmoothTime);
            animMagnitude = playerVel.magnitude;

            if (playerVel.magnitude <= 1.0f && playerVel.magnitude > 0.25f)
            {
                transform.LookAt(playerWalkLookAt);
                Vector3 forward = Vector3.forward * playerVel.magnitude;
                playerVel = Quaternion.Euler(0f, moveAngle, 0f) * forward;
            }
            else if(playerVel.magnitude > 1.0f)
            {
                transform.LookAt(playerWalkLookAt);
                Vector3 forward = Vector3.forward * playerVel.magnitude;
                playerVel = Quaternion.Euler(0f, moveAngle, 0f) * forward;
            }
            else if (playerVel.magnitude == 0.0f)
            {
                Vector3 forward = Vector3.forward * playerVel.magnitude;
                playerVel = Quaternion.Euler(0f, moveAngle, 0f) * forward;
            }
            playerSpineTransform.rotation = Quaternion.Euler(0f, lookAngle, 0f);

            float speedMultiplyer = moveSpeed;
            if (sprint)
            {                    
                speedMultiplyer *= 2f;
                animMagnitude *= 2f;                    
            }
            playerVel *= speedMultiplyer;
            if (jump)
            {
                StartCoroutine(WaitForJumpAnim());
                jump = false;
            }

            if (!manager.pause && shootController.shootTrigger && manager.ammoLeft >= 0)
            {
                anim.SetTrigger("Shooting");                  
            }
            else if (manager.pause && shootController.shootTrigger)
            {
                shootController.shootTrigger = false;
            }

            if (Input.GetButtonDown("Reload"))
            {
                manager.ammoLeft = 100;
                manager.RefreshAmmo();
            }
        }
        else
        {
            playerVel = new Vector3(0f, 0f, 0f).normalized;
        }
        anim.SetFloat(MAGNITUDE, animMagnitude, 0.15f, Time.deltaTime);
        anim.SetBool(IsGROUNDED, CheckIfGrounded());
        anim.SetFloat(Y, playerVel.y);
        anim.SetFloat(DirectionX, move.y);
        anim.SetFloat(DirectionY, move.x);
        playerVel.y -= gravity * Time.deltaTime;
        controller.Move(playerVel * Time.deltaTime);        
    }

    private void OnTriggerEnter(Collider other)
    {        
        if (other.CompareTag("Village1"))
            manager.txtLocation.text = "Village 1";
        if (other.CompareTag("Village2"))
            manager.txtLocation.text = "Village 2";
        if (other.CompareTag("Village3"))
            manager.txtLocation.text = "Village 3";
        if (other.CompareTag("Base"))
            manager.txtLocation.text = "Base";
        if (other.CompareTag("Zombies"))
            playerHP.PlayerHealth();
        if (other.CompareTag("Fox"))
            manager.GameWinner();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Village1"))
            manager.txtLocation.text = "Village 1";
        if (other.CompareTag("Village2"))
            manager.txtLocation.text = "Village 2";
        if (other.CompareTag("Village3"))
            manager.txtLocation.text = "Village 3";
        if (other.CompareTag("Base"))
            manager.txtLocation.text = "Base";
    }

    private void OnTriggerExit(Collider other)
    {        
        manager.txtLocation.text = "Forest";        
    }
}
