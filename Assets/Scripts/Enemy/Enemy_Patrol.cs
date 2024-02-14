using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Patrol : MonoBehaviour
{
    public float speed;
    public Transform[] waypoints;
    private Transform currentTarget;
    private Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentTarget = waypoints[0];
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, currentTarget.position) < 0.2f)
        {
            if (currentTarget == waypoints[0])
            {
                currentTarget = waypoints[1];
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z) ;
            }
            else
            {
                currentTarget = waypoints[0];
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
        }

        // Move towards the current target using Rigidbody2D
        Vector2 direction = currentTarget.position - transform.position;
        rb.velocity = direction.normalized * speed;
    }
}
