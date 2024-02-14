using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Health : MonoBehaviour
{
    public void Die()
    {
        if (transform.parent != null) 
            Destroy(transform.parent.gameObject);
        Destroy(this.gameObject);
    }
}
