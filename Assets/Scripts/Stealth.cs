using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stealth : MonoBehaviour
{
    public GameObject stealthFilter; // Reference to the stealth filter object
    public PlayerMovement playerMovement; // Reference to the player's movement script

    private string defaultTag;
    private bool isPlayerInHidingZone = false; // Track if the player is in a hiding zone
    private bool isStealthModeActive = false; // Track the state of stealth mode

    private void Start()
    {
        // Store the player's default tag
        defaultTag = gameObject.tag;
        // Ensure the stealth filter is initially deactivated
        stealthFilter.SetActive(false);
    }

    private void Update()
    {
        // Check for player input to toggle stealth mode
        if (Input.GetKeyDown(KeyCode.E) && isPlayerInHidingZone)
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
        }
        else
        {
            // Exit stealth mode
            gameObject.tag = defaultTag;
            stealthFilter.SetActive(false);
            Debug.Log("Exiting Stealth Mode");
        }
    }
}
