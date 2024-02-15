using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableBlue: MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.isBlue = true;
            GameManager.Instance.blueImage.enabled = true;
            Destroy(gameObject);
        }
    }
}
