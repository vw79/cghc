using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class FalloutWorld : MonoBehaviour
{
    public GameObject gameovermenu;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Time.timeScale = 0f;
            gameovermenu.SetActive(true);
        }
    }
}
