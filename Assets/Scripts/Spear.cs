using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour
{
    private bool isFacingRight = true;
    private Vector2 shootDirection;
    private Vector2 startPosition;
    private PlayerShoot playerRef;
    private Rigidbody2D rb;
    [SerializeField] private float speed = 10f;
    [SerializeField] private Vector2 maxDistance;
    [SerializeField] private Transform teleportPoint;

    public void Initialise(PlayerShoot player, Vector2 direction)
    {
        rb = GetComponent<Rigidbody2D>();
        playerRef = player;
        startPosition = transform.position;


        if(player.transform.localScale.x > 0)
        {
            direction.x = Mathf.Max(0,direction.x);
        }
        else
        {
            direction.x = Mathf.Min(0,direction.x);
        }
        shootDirection = direction.normalized;

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
    }

    private void Update()
    {
        rb.MovePosition(rb.position + shootDirection * speed * Time.fixedDeltaTime);

        if((Mathf.Abs(startPosition.x - transform.position.x) > maxDistance.x) || (Mathf.Abs(startPosition.y - transform.position.y) > maxDistance.y))
        {
            playerRef.ResetStatus();
            Destroy(gameObject);
        }
    }

    private void TeleportPlayer()
    {
        playerRef.transform.position = teleportPoint.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        TeleportPlayer();
        playerRef.ResetStatus();
        Destroy(gameObject);
    }
}
