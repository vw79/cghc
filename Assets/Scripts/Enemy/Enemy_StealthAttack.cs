using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_StealthAttack : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealthSystem player = collision.GetComponent<PlayerHealthSystem>();
            player.TakeDamage();
            player.TakeDamage();
            player.TakeDamage();
        }
    }
}
