using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stealth : MonoBehaviour
{
    public GameObject stealthFilter; // Reference to the stealth filter object
    public AudioSource hide;

    private string defaultTag;
    private bool isPlayerInHidingZone = false; // Track if the player is in a hiding zone
    private bool isStealthModeActive = false; // Track the state of stealth mode
    private bool isCooldownActive = false; // Track if the cooldown is active
    private SpriteRenderer playerSpriteRenderer;

    // Cooldown and hiding duration
    private float cooldownTime = 3f;
    private float maxHidingTime = 5f;

    private void Start()
    {
        // Store the player's default tag
        defaultTag = gameObject.tag;

        // Get the SpriteRenderer from the child GameObject
        playerSpriteRenderer = GetComponentInChildren<SpriteRenderer>();

        // Ensure the stealth filter is initially deactivated
        stealthFilter.SetActive(false);
    }

    private void Update()
    {
        // Check for player input to toggle stealth mode
        if (Input.GetKeyDown(KeyCode.E) && isPlayerInHidingZone && !isCooldownActive)
        {
            ToggleStealthMode();
        }
        if (Input.GetMouseButtonDown(0) && isStealthModeActive)
        {
            ToggleStealthMode();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("HidingSpot")) // Ensure your hiding spots are tagged "HidingSpot"
        {
            isPlayerInHidingZone = true;
            Debug.Log("Player entered hiding zone");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("HidingSpot"))
        {
            isPlayerInHidingZone = false;
            Debug.Log("Player exited hiding zone");
            // If player exits the hiding spot while in stealth mode, force exit stealth mode
            if (isStealthModeActive)
            {
                ToggleStealthMode();
            }
        }
    }

    private void ToggleStealthMode()
    {
        isStealthModeActive = !isStealthModeActive; // Toggle the state of stealth mode

        if (isStealthModeActive)
        {
            // Enter stealth mode
            gameObject.tag = "Stealth";
            stealthFilter.SetActive(true);
            Debug.Log("Entering Stealth Mode");
            playerSpriteRenderer.sortingLayerName = "Hide";
            playerSpriteRenderer.sortingOrder = 1;
            hide.Play();

            // Start the coroutine to automatically exit stealth mode after max hiding time
            StartCoroutine(ExitStealthModeAfterDelay(maxHidingTime));
        }
        else
        {
            // Exit stealth mode and start the cooldown if not already started by the coroutine
            if (!isCooldownActive)
            {
                StartCooldown();
            }
        }
    }

    private IEnumerator ExitStealthModeAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (isStealthModeActive)
        {
            StartCooldown();
        }
    }

    private void StartCooldown()
    {
        ExitStealthMode();
        isCooldownActive = true;
        StartCoroutine(CooldownTimer());
        Debug.Log("Stealth mode cooldown started.");
    }

    private IEnumerator CooldownTimer()
    {
        yield return new WaitForSeconds(cooldownTime);
        isCooldownActive = false;
        Debug.Log("Stealth cooldown finished. You can hide again.");
    }

    private void ExitStealthMode()
    {
        // Set stealth mode to inactive
        isStealthModeActive = false;
        gameObject.tag = defaultTag;
        stealthFilter.SetActive(false);
        playerSpriteRenderer.sortingLayerName = "Default";
        playerSpriteRenderer.sortingOrder = 0;
        hide.Stop();
        Debug.Log("Exiting Stealth Mode");
    }
}
