using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{
    private GameManager manager;
    private LoseHealth playerHealth;
    private Muzzle muzzleFX;

    [SerializeField] private float MaxRange = 200f;
    [SerializeField] private AudioSource[] gunSFX;
    [SerializeField] private GameObject bloodFX;
    
    public bool shootTrigger;


    // Start is called before the first frame update
    void Start()
    {
        manager = GameManager.instance;
        muzzleFX = FindObjectOfType<Muzzle>();
        playerHealth = FindObjectOfType<LoseHealth>();
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        shootTrigger = context.performed;
    }

    // Update is called once per frame
    void Update()
    {
        if (!manager.pause || !playerHealth.playerIsDead)
        {
            if (shootTrigger && manager.ammoLeft >= 0)
            {
                manager.ammoLeft--;
                manager.RefreshAmmo();
                shootTrigger = false;
                if (gunSFX[0]) gunSFX[0].PlayOneShot(gunSFX[0].clip);
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit, MaxRange))
                {
                    muzzleFX.StartEmit(hit.point);
                    if (hit.collider.CompareTag("Zombies"))
                    {
                        hit.collider.gameObject.GetComponent<Zombies>().Hit();
                        Quaternion rotFX = Quaternion.LookRotation(hit.normal);
                        GameObject blood = Instantiate(bloodFX, hit.point, rotFX);
                        blood.transform.parent = hit.transform;
                    }
                }
                else
                {
                    muzzleFX.StartEmit(MaxRange);
                }
            }
            else if (shootTrigger && manager.ammoLeft <= 0)
            {
                if (gunSFX[1]) gunSFX[1].PlayOneShot(gunSFX[1].clip);
                shootTrigger = false;
            }
        }
        else
        {
            shootTrigger = false;
        }
    }
}
