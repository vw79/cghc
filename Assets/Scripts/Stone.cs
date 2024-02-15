using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stone : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private Image visualCue;
    private CanvasGroup explosionCanvasGroup;

    private bool playerInRange;

    private void Awake()
    {
        playerInRange = false;
        visualCue = GetComponentInChildren<Image>();
        explosionCanvasGroup = GameObject.FindGameObjectWithTag("ExplosionFilter").GetComponent<CanvasGroup>();
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
                StartCoroutine(GameManager.Instance.FadeIn(explosionCanvasGroup.GetComponent<CanvasGroup>(), 1f, 3));
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
