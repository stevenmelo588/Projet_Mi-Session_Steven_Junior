using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    Vector3 destination;
    NavMeshAgent agent;

    private Animator anim;   

    [SerializeField] private AudioSource _ZombieAudioSource;
    [SerializeField] private AudioClip  biteClip = null;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        destination = agent.destination;
        anim = GetComponent<Animator>();        
        _ZombieAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Vector3.Distance(destination, player.position) < 60f)
        {
            destination = player.position;
            agent.destination = destination;
            
            anim.SetFloat("Magnitude", agent.velocity.magnitude);
            anim.SetBool("PlayerFound", true);

            _ZombieAudioSource.PlayOneShot(_ZombieAudioSource.clip);            
        }
        else if (Vector3.Distance(destination, player.position) < 2.5f) 
        {
            destination = transform.position;
            agent.destination = destination;            

            anim.SetFloat("Magnitude", agent.velocity.magnitude);
            anim.SetBool("PlayerFound", true);
            anim.SetBool("BitePlayer", true);

            _ZombieAudioSource.clip = biteClip;
            _ZombieAudioSource.Play();           
        }
        else
        {
            anim.SetBool("PlayerFound", false);
            anim.SetBool("BitePlayer", false);

            _ZombieAudioSource.PlayOneShot(_ZombieAudioSource.clip);
        }
    }    
}