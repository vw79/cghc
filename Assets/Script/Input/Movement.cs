using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public Animator animator;
    private Rigidbody2D rb = null;
    private Vector3 originalScale;
    private float moveVectorX = 0;
    private float moveVectorY = 0;
    public float horizontalMoveSpeed = 5f;
    private float acceleration = 0;

    private bool onGround = false;

    private int jumpCount = 0;
    private int jumpLimit = 2;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        originalScale = transform.localScale;
    }

    private void Update()
    {
        HorizontalMovement();
        VerticalMovement();

        //Apply movement
        rb.velocity = new Vector2(moveVectorX * horizontalMoveSpeed, moveVectorY);

        animator.SetFloat("Speed", Mathf.Abs(moveVectorX));

        if (moveVectorX > 0)
            transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z);
        else if (moveVectorX < 0)
            transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);
    }

    private void HorizontalMovement()
    {
        horizontalMoveSpeed += acceleration;
        horizontalMoveSpeed = Mathf.Clamp(horizontalMoveSpeed, 0, 7);
    }

    private void VerticalMovement()
    {
        //Apply gravity
        if(!onGround)
        {
            moveVectorY -= 0.5f;
        }
        else
        {
            moveVectorY = 0;
        }
    }

    public void Move(InputAction.CallbackContext value)
    {
        if(value.performed)
        {
            moveVectorX = value.ReadValue<float>();
            acceleration = 0.25f;
        }
        
        if(value.canceled)
        {
			moveVectorX = 0;
            acceleration = -0.25f;
        }
    }

    public void Jump(InputAction.CallbackContext value)
    {
        if(value.performed)
        {
            if(jumpCount < jumpLimit)
            {
                onGround = false;
                moveVectorY = 20f;  //original 14f
                jumpCount++;

                animator.Play("jump");
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            onGround = true;
            jumpCount = 0;
        }
    }
}
