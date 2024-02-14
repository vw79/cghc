using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Fireball : MonoBehaviour
{
    public float lifetime = 5f;
    public float speed = 5f;
    private Rigidbody2D rb;
    private Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, lifetime);
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = direction * speed;
    }

    public void Initialise(Transform shooter)
    {
        if (shooter.localScale.x <= 0)
        {
            direction = Vector2.right;
        }
        else
        {
            direction = Vector2.left;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerHealthSystem>().TakeDamage();
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Destroy(this.gameObject);
        }
    }
}
