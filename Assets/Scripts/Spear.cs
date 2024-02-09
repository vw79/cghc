using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour
{
    private bool isFacingRight = true;
    private Vector2 shootDirection;
    private PlayerShoot playerRef;
    private Rigidbody2D rb;
    [SerializeField] private float speed = 10f;

    public void Initialise(PlayerShoot player, Vector2 direction)
    {
        rb = GetComponent<Rigidbody2D>();
        playerRef = player;
        shootDirection = direction;

        // Adjust spear's direction
        float angle;
        if(direction.x > 0)
        {
            angle = 360 - Vector2.Angle(Vector2.up, direction);
        }
        else
        {
            angle = Vector2.Angle(Vector2.up, direction);
        }
        transform.rotation = Quaternion.Euler(0, 0, angle);
        Debug.Log(angle);
    }

    private void Update()
    {
        rb.MovePosition(rb.position + shootDirection * speed * Time.fixedDeltaTime);
    }

    private void TeleportPlayer()
    {
        playerRef.transform.position = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        TeleportPlayer();
        playerRef.ResetStatus();
        Destroy(gameObject);
    }
}
