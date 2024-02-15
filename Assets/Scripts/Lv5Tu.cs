using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lv5Tu : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private Image visualCue;
    public GreenStone greenStone;

    private bool playerInRange;

    private void Awake()
    {
        playerInRange = false;
        visualCue = GetComponentInChildren<Image>();
        greenStone = FindObjectOfType<GreenStone>();
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
            greenStone.canUse = true;
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
