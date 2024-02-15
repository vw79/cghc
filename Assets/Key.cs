using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Key : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private Image visualCue;
    public bool isKey;

    private bool playerInRange;

    private void Awake()
    {
        playerInRange = false;
        visualCue = GetComponentInChildren<Image>();
    }

    private void Start()
    {
        visualCue.enabled = false;
    }

    private void Update()
    {
        if (playerInRange)
        {
            visualCue.enabled = true;
            if (Input.GetKeyDown(KeyCode.E))
            {
                isKey = true;
                visualCue.enabled = false;
                Destroy(this.gameObject);
            }
        }
        else
        {
            visualCue.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            playerInRange = false;
        }
    }
}

