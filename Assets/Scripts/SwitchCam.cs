using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCam : MonoBehaviour
{
    public GameObject cam2;
    public float delay = 3f; // Duration before cam2 is deactivated

    private void OnTriggerEnter2D(Collider2D other)
    {
        cam2.SetActive(true);
        StartCoroutine(DeactivateCam2AfterDelay());
    }

    private IEnumerator DeactivateCam2AfterDelay()
    {
        yield return new WaitForSeconds(delay);
        cam2.SetActive(false);
    }
}