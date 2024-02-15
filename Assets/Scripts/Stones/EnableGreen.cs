using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableGreen : MonoBehaviour
{
    void Start()
    {
        if (GameManager.Instance.isLava) 
        { 
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.isGreen = true;
            GameManager.Instance.greenImage.enabled = true;
            Destroy(gameObject);
        }
    }
}
