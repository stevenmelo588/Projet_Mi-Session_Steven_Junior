using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsLogical : MonoBehaviour
{
    Animator coinAnimator => GetComponent<Animator>();
    // Start is called before the first frame update
    void OnEnable()
    { 
        coinAnimator.Play("Spining");
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}
}
