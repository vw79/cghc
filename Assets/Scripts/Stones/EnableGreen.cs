using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableGreen : MonoBehaviour
{
    public GameObject dialogue2;
    public GameObject dialogue3;
    public GameObject camTrigger1;
    public GameObject canTrigger2;

    private void Start()
    {
        if (GameManager.Instance.isGreen)
        {
            Destroy(dialogue2);
            Destroy(dialogue3);
            Destroy(camTrigger1);
            Destroy(canTrigger2);
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
