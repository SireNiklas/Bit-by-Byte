using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{

    //[SerializeField] private float velocity = 0.0f;

    [SerializeField] private Animator animator;

    private void Awake()
    {
        
        animator = GetComponent<Animator>();
        
    }

    private void PlayerAnimation()
    {
        
    }
    
}
