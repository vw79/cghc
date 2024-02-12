using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim : MonoBehaviour
{
    private GameObject player;
    private PlayerMovement playerMovement;
    private Animator animator;
    private CapsuleCollider2D capCollider;
    private GameObject playerGameObjet;

    private Vector2 defaultColliderOffset = new Vector2(0.02005888f, -0.6382086f);
    private Vector2 wallClingColliderOffset = new Vector2(-0.1546608f, -0.6382086f);

    private PlayerAttack playerAttack;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
        capCollider = player.GetComponent<CapsuleCollider2D>();
        playerAttack = player.GetComponent<PlayerAttack>();
    }

    void Start()
    {
        capCollider.offset = defaultColliderOffset;
    }

    void FixedUpdate()
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
    }
}
