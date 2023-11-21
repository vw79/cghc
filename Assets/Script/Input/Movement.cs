using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    private Input input = null;
    private Vector2 moveVector = Vector2.zero;
    private Rigidbody2D rb = null;
    public float moveSpeed = 5f;
    private float acceleration = 0;

    private bool isPlayerMoving = false;

    private void Awake()
    {
        input = new Input();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        input.Enable();
        input.Player.Movement.performed += OnMovementPerformed;
        input.Player.Movement.canceled += OnMovementCancelled;
    }

    private void OnDisable()
    {
        input.Disable();
        input.Player.Movement.performed -= OnMovementPerformed;
        input.Player.Movement.canceled -= OnMovementCancelled;
    }

    private void OnMovementPerformed(InputAction.CallbackContext value)
    {
        isPlayerMoving = true;
        moveVector = value.ReadValue<Vector2>();
        acceleration = 0.15f;
    }

    private void OnMovementCancelled(InputAction.CallbackContext value)
    {
        isPlayerMoving = false;
        // moveVector = Vector2.zero;
        acceleration = -0.15f;
    }

    private void Update()
    {
        moveSpeed += acceleration;
        moveSpeed = Mathf.Clamp(moveSpeed, 0, 7);
        rb.velocity = moveVector * moveSpeed;
    }
}
