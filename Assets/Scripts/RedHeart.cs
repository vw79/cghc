using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedHeart : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerHealthSystem>().Heal(1);
            Destroy(gameObject);
        }
    }
}
