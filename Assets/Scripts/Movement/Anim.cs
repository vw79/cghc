using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim : MonoBehaviour
{
    private GameObject player;
    private PlayerMovement playerMovement;
    private Animator animator;
    private PlayerHealthSystem playerHealth;

    private PlayerAttack playerAttack;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
        playerAttack = player.GetComponent<PlayerAttack>();
        playerHealth = player.GetComponent<PlayerHealthSystem>();
    }

    void Update()
    {
        if (!playerHealth.isDead)
        {
            // Run      
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                animator.SetBool("isRunning", true);
            }
            else
            {
                animator.SetBool("isRunning", false);
            }

            // Jump
            if (playerMovement.IsJumping)
            {
                animator.Play("Jump");
            }

            // WallCling
            if (playerMovement.IsSliding)
            {
                animator.Play("WallSlide");
            }

            // Fall
            if (playerMovement.RB.velocity.y < -0.1 && !playerMovement.IsSliding)
            {
                animator.Play("Fall");
            }

            // Dash
            if (playerMovement.IsDashing)
            {
                animator.Play("Dash");
            }

            // WallJump
            if (playerMovement.IsWallJumping)
            {
                animator.Play("Dash");
            }
        }
        
    }
}
