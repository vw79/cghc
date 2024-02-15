using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaDamage : MonoBehaviour
{
    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(DamageOverTime());
        }
    }

    IEnumerator DamageOverTime()
    {
        int timesToDamage = 3;
        while (timesToDamage > 0)
        {
            timesToDamage--;
            player.GetComponent<PlayerHealthSystem>().TakeDamage(); 
            yield return new WaitForSeconds(1);
        }
    }
}
