using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour
{
    private bool isFacingRight = true;
    [SerializeField] private float speed = 10f;

    public void Initialise(PlayerShoot player)
    {
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
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(-Vector2.up * speed * Time.deltaTime);
        }
    }
}
