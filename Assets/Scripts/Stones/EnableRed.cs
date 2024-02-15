using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableRed : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.isRed = true;
            GameManager.Instance.redImage.enabled = true;
            Destroy(gameObject);
        }
    }
}
