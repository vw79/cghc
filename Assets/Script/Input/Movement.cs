using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rb = null;
    private float moveVectorX = 0;
    public float moveSpeed = 5f;
    private float acceleration = 0;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        moveSpeed += acceleration;
        moveSpeed = Mathf.Clamp(moveSpeed, 0, 7);
        rb.velocity = new Vector2(moveVectorX, 0) * moveSpeed;
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
            acceleration = -0.25f;
        }
    }
}
