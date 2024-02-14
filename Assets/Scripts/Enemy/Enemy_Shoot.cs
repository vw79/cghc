using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Shoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    

    private void OnEnable()
    {
         Enemy_Fireball fireball = Instantiate(bulletPrefab, transform.position, Quaternion.identity).GetComponent<Enemy_Fireball>();
        fireball.Initialise(this.transform);
    }
}
