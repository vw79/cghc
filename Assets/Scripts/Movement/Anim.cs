using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public Animator animator;
    public CapsuleCollider2D capCollider;
    public GameObject playerGameObjet;

    private Vector2 defaultColliderOffset = new Vector2(0.02005888f, -0.6382086f);
    private Vector2 wallClingColliderOffset = new Vector2(-0.1546608f, -0.6382086f);

    void Start()
    {
        capCollider.offset = defaultColliderOffset;
    }

    void Update()
    {
        if (Mathf.Abs(playerMovement.RB.velocity.x) > 0.1f)
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

        // Handle wall cling animation
        if (playerMovement.IsSliding)
        {
            capCollider.offset = wallClingColliderOffset;
            animator.SetBool("isWallClinging", true);
        }
        else
        {
            capCollider.offset = defaultColliderOffset;
            animator.SetBool("isWallClinging", false);
        }

        // Handle falling animation
        if (playerMovement.RB.velocity.y < -0.1 && !playerMovement.IsSliding)
        {
            animator.SetBool("isFalling", true);
        }
        else
        {
            animator.SetBool("isFalling", false);
        }

        // Handle dash animation
        animator.SetBool("isDashing", playerMovement.IsDashing);
      

        //test for now
        // Handle attack animation
        if (Input.GetMouseButtonDown(0))
        {
            playerMovement.RB.velocity = new Vector2(0, playerMovement.RB.velocity.y);
            animator.Play("Attack");
            playerMovement.enabled = false;
            StartCoroutine(AttackWait());
        }

        IEnumerator AttackWait()
        {
            yield return new WaitForSeconds(0.8f);
            playerMovement.enabled = true;  
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            animator.Play("Death");
            StartCoroutine(DeathWait());       
        }

        IEnumerator DeathWait()
        {
            yield return new WaitForSeconds(0.97f);
            Destroy(playerGameObjet);
        }
    }
}
