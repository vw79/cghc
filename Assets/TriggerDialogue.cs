using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerDialogue : MonoBehaviour
{
    private Dialogue dialogue;
    [SerializeField] private string[] lines;

    private void Awake()
    {
        dialogue = GameObject.FindGameObjectWithTag("Dialogue").GetComponent<Dialogue>();
    }

    private void Start()
    {
        dialogue.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            dialogue.gameObject.SetActive(true);
            dialogue.SetLines(lines);
            Destroy(this.gameObject);
        }
    }
}
