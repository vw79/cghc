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
    private float horizontalMoveSpeed = 5f;
    private float acceleration = 0;

    public bool onGround = false;

    private int jumpCount = 0;
    private int jumpLimit = 2;

    private Vector2 _boundsTopLeft;
    private Vector2 _boundsTopRight;
    private Vector2 _boundsBottomLeft;
    private Vector2 _boundsBottomRight;

    private float _boundsWidth;
    private float _boundsHeight;



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


        if (moveVectorX > 0)
            transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z);
        else if (moveVectorX < 0)
            transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);



        animator.SetFloat("Speed", Mathf.Abs(moveVectorX));


        // Gravity detection
        SetRayOrigins();
        DetectGravity();


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
                moveVectorY = 30f;  //original 14f
                jumpCount++;

                animator.Play("jump");
            }
        }
    }

    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            onGround = true;
            jumpCount = 0;
        }
    }
    */

    private void SetRayOrigins()
    {
        Bounds playerBounds = this.GetComponent<BoxCollider2D>().bounds;

        _boundsBottomLeft = new Vector2(playerBounds.min.x, playerBounds.min.y);
        _boundsBottomRight = new Vector2(playerBounds.max.x, playerBounds.min.y);
        _boundsTopLeft = new Vector2(playerBounds.min.x, playerBounds.max.y);
        _boundsTopRight = new Vector2(playerBounds.max.x, playerBounds.max.y);

        _boundsHeight = Vector2.Distance(_boundsBottomLeft, _boundsTopLeft);
        _boundsWidth = Vector2.Distance(_boundsBottomLeft, _boundsBottomRight);
    }

    private void DetectGravity()
    {
        // Calculate ray origin
        Vector2 leftOrigin = ((_boundsBottomLeft + _boundsTopLeft) / 2f) + new Vector2(0.02f,0);
        Vector2 rightOrigin = ((_boundsBottomRight + _boundsTopRight) / 2f) - new Vector2(0.02f, 0);
        Vector2 downOrigin = (_boundsBottomLeft + _boundsBottomRight) / 2;

        float rayLength = _boundsHeight / 2f;
        if(moveVectorY < 0)
        {
            rayLength = _boundsHeight / 2f - moveVectorY / 100f;
        }

        RaycastHit2D[] hits = new RaycastHit2D[4];
        for (int i = 0; i < 4; i++)
        {
            Vector2 rayOrigin = Vector2.Lerp(leftOrigin, rightOrigin, (float)i / (float)(4 - 1));
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, -transform.up, rayLength, LayerMask.GetMask("Floor"));
            hits[i] = hit;
            Debug.DrawRay(rayOrigin, -transform.up * rayLength, Color.green);
        }
        RaycastHit2D hit2 = Physics2D.BoxCast(downOrigin, new Vector2(_boundsWidth, 0.1f), 0, Vector2.down, rayLength, LayerMask.GetMask("Floor"));
        Debug.Log(hit2.collider);

        // Check if any of the rays hit the ground
        bool rayHit = false;
        foreach (RaycastHit2D hit in hits)
        {
            if (hit)
            {
                rayHit = true;
            }
        }

        if(rayHit)
        {
            onGround = true;
            jumpCount = 0;
        }
        else
        {
            onGround = false;
        }
    }
}
