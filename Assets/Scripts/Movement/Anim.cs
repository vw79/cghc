using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public Animator animator;

    void Start()
    {
        
    }

    void Update()
    {
        if (playerMovement.RB.velocity.x > 0.1 || playerMovement.RB.velocity.x < -0.1)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

        if (playerMovement.RB.velocity.y > 0.1)
        {
            animator.SetBool("isJumping", true);
        }
        else
        {
            animator.SetBool("isJumping", false);
        }
    }
}
