using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour
{
    private bool isFacingRight = true;
    private PlayerShoot playerRef;
    private Rigidbody2D rb;
    [SerializeField] private float speed = 10f;

    public void Initialise(PlayerShoot player)
    {
        rb = GetComponent<Rigidbody2D>();
        playerRef = player;
        // Adjust spear's direction
        if (player.transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(transform.localScale.x, -transform.localScale.y, transform.localScale.z);
            isFacingRight = false;
        }
        else
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
            isFacingRight = true;
        }
    }

    private void Update()
    {
        if (isFacingRight)
        {
            rb.MovePosition(rb.position + Vector2.right * speed * Time.deltaTime);
        }
        else
        {
            rb.MovePosition(rb.position + Vector2.left * speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        playerRef.ResetStatus();
        Destroy(gameObject);
    }
}
